using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Context;
using BusinessLayer.Interfaces;
using Common;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;
using QRCoder;

namespace BusinessLayer.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessManager : ManagerBase, IBusinessManager
    {
        private readonly IUserManager userManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextProvider"></param>
        /// <param name="logManager"></param>
        public BusinessManager(IContextProvider contextProvider, IUserManager userManager, ICacheManager cacheManager, ILogManager logManager) : base(contextProvider, cacheManager, logManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Business>> GetBusinesses(int userId, AuthenticationLevel authenticationLevel)
        {
            if (authenticationLevel == AuthenticationLevel.SystemAdmin)
            {
                return await Context.Businesses.ToListAsync();
            }

            var businesses = GetUserBusinesses(userId);

            var result = await (from bsn in Context.Businesses
                                join lnk in businesses on bsn.Id equals lnk
                                select bsn)
                                .ToListAsync();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qrCode"></param>
        /// <returns></returns>
        public Task<Business> GetBusinessByQrCode(string qrCode)
        {
            var businessId = Convert.ToInt32(qrCode.Replace("LokellaBusinessId_", ""));
            return Context.Businesses
                .Where(q => q.Id == businessId)
                .FirstOrDefaultAsync();
        }

        public async Task<int> InsertBusinessAsync(Models.TransferObjects.Business business)
        {
            var logoBytes = Convert.FromBase64String(business.LogoBase64);

            var file = await Context.StoredFiles.AddAsync(new StoredFile
            {
                MimeType = "",
                FileName = "logo",
                FileContent = logoBytes
            });

            await Context.SaveChangesAsync();

            var logoId = file.Entity.Id;

            var businessDb = await Context.Businesses.AddAsync(new Business
            {
                Name = business.Name,
                Level = business.Level,
                Category = business.Category,
                LogoId = logoId,
                QrCodeId = logoId,
                BackgroundColor = business.BackgroundColor,
                FontColor = business.FontColor,
                MenuSectionColor = business.MenuSectionColor
            });

            await Context.SaveChangesAsync();


            var _qrCode = new QRCodeGenerator();
            var _qrCodeData = _qrCode.CreateQrCode("LokellaBusinessId_" + businessDb.Entity.Id, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(_qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);
            var qrCodeBytes = BitmapToBytesCode(qrCodeImage);

            var fileQrCode = await Context.StoredFiles.AddAsync(new StoredFile
            {
                MimeType = "",
                FileName = "qrCode",
                FileContent = qrCodeBytes
            });

            await Context.SaveChangesAsync();

            var qrCodeId = fileQrCode.Entity.Id;

            businessDb.Entity.QrCodeId = qrCodeId;

            await Context.SaveChangesAsync();

            business.BusinessInfo.Id = businessDb.Entity.Id;

            await Context.BusinessInfos.AddAsync(business.BusinessInfo);

            await Context.SaveChangesAsync();

            return businessDb.Entity.Id;

        }

        public async Task<List<SpecialOffer>> GetSpecialOffers(int businessId, bool isActive)
        {
            var now = DateTime.Now;

            var all = Context.SpecialOffers.ToList();

            var list = await Context.SpecialOffers
                .Where(q => q.BusinessId == businessId)
                .ToListAsync();

            if (isActive)
            {
                return list.Where(q => q.Status == 1 && q.DateFrom <= now && q.DateTo >= now && q.ActiveHours(now))
                .ToList();
            }

            return list;
        }

        public async Task<List<BusinessCategory>> GetBusinessCategories()
        {
            return await Context.BusinessCategories.OrderBy(q => q.Name).ToListAsync();
        }

        public async Task<List<MembershipLevel>> GetMembershipLevels()
        {
            return await Context.MembershipLevels.OrderBy(q => q.Level).ToListAsync();
        }

        public async Task<BusinessInfo> GetBusinessInfo(int businessId)
        {
            return await Context.BusinessInfos.FirstOrDefaultAsync(q => q.Id == businessId);
        }

        public async Task<int> VisitBusiness(int businessId, int customerId)
        {
            await Context.CustomerVisits.AddAsync(new CustomerVisit
            {
                BusinessId = businessId,
                CustomerId = customerId,
                VisitDate = DateTime.Now
            });

            await Context.SaveChangesAsync();

            return 1;
        }

        public async Task<List<Models.TransferObjects.CustomerVisit>> GetCustomerVisits(int businessId)
        {
            var visits = await (from visit in Context.CustomerVisits
                         join customer in Context.Customers on visit.CustomerId equals customer.Id
                         where visit.BusinessId == businessId
                         select new Models.TransferObjects.CustomerVisit
                         {
                             Visitor = customer,
                             VisitDate = visit.VisitDate
                         })
                .AsQueryable()
                .OrderByDescending(q => q.VisitDate)
                .ToListAsync();

            return visits;
        }

        public async Task<List<User>> GetBusinessUsers(int businessId)
        {
            var users = await (from busr in Context.BusinessUsers
                                join usr in Context.Users on busr.UserId equals usr.Id
                                where busr.BusinessId == businessId
                                select new User
                                {
                                    Id = usr.Id,
                                    Email = usr.Email,
                                    FirstName = usr.FirstName,
                                    LastName = usr.LastName,
                                    Role = busr.Role
                                })
                .ToListAsync();

            return users;
        }

        public async Task<Models.TransferObjects.BusinessUser> GetBusinessUser(int businessId, int userId)
        {
            var user = await (from busr in Context.BusinessUsers
                               join usr in Context.Users on busr.UserId equals usr.Id
                               where busr.BusinessId == businessId && busr.UserId == userId
                               select new Models.TransferObjects.BusinessUser
                               {
                                   Id = usr.Id,
                                   BusinessId = busr.BusinessId,
                                   Email = usr.Email,
                                   FirstName = usr.FirstName,
                                   LastName = usr.LastName,
                                   Role = busr.Role,
                                   Password = ""
                               })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                var userEntity = await userManager.GetUser(userId);
                if (userEntity != null && userEntity.Role == 1)
                {
                    user = new Models.TransferObjects.BusinessUser
                    {
                        Id = userEntity.Id,
                        BusinessId = businessId,
                        Email = userEntity.Email,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        Role = userEntity.Role,
                        Password = ""
                    };
                }
            }

            return user;
        }

        public async Task<int> InsertBusinessUser(Models.TransferObjects.BusinessUser businessUser)
        {
            var existingUser = await Context.Users.FirstOrDefaultAsync(u => u.Email == businessUser.Email);
            var userId = 0;

            if (existingUser == null)
            {
                var salt = Guid.NewGuid().ToString();

                userId = await userManager.Insert(new User
                {
                    FirstName = businessUser.FirstName,
                    LastName = businessUser.LastName,
                    Email = businessUser.Email,
                    Role = 0,
                    PasswordHash = SHA1HashValue(businessUser.Password + salt),
                    PasswordSalt = salt
                });
            }
            else
            {
                userId = existingUser.Id;
            }

            var existingBusinessUser = await Context.BusinessUsers.FirstOrDefaultAsync(u => u.BusinessId == businessUser.BusinessId && u.UserId == userId);

            if (existingBusinessUser != null)
            {
                existingBusinessUser.Role = businessUser.Role == 1 ? (short)1 : (short)0;

                Context.BusinessUsers.Update(existingBusinessUser);

                await Context.SaveChangesAsync();
            }
            else
            {
                Context.BusinessUsers.Add(new BusinessUser
                {
                    BusinessId = businessUser.BusinessId,
                    UserId = userId,
                    Role = businessUser.Role
                });

                await Context.SaveChangesAsync();
            }

            return 1;
        }

        public async Task<int> UpdateBusinessUser(Models.TransferObjects.BusinessUser businessUser)
        {
            var existingBusinessUser = await Context.BusinessUsers.FirstOrDefaultAsync(u => u.BusinessId == businessUser.BusinessId && u.UserId == businessUser.Id);
            var existingUser = await Context.Users.FirstOrDefaultAsync(u => u.Id == businessUser.Id);

            if (existingBusinessUser != null)
            {
                existingBusinessUser.Role = businessUser.Role == 1 ? (short)1 : (short)0;

                Context.BusinessUsers.Update(existingBusinessUser);

                existingUser.FirstName = businessUser.FirstName;
                existingUser.LastName = businessUser.LastName;

                if (businessUser.Password != null)
                {
                    var salt = Guid.NewGuid().ToString();
                    existingUser.PasswordHash = SHA1HashValue(businessUser.Password + salt);
                    existingUser.PasswordSalt = salt;
                }

                Context.Users.Update(existingUser);

                await Context.SaveChangesAsync();

                return 1;
            }

            return 0;
        }

        public async Task<int> UpdateBusiness(Business business)
        {
            var existingBusiness = await Context.Businesses.FirstOrDefaultAsync(b => b.Id == business.Id);

            if (existingBusiness != null)
            {
                existingBusiness.Category = business.Category;
                existingBusiness.Level = business.Level;
                existingBusiness.Name = business.Name;
                existingBusiness.LogoId = business.LogoId;
                existingBusiness.BackgroundColor = business.BackgroundColor;
                existingBusiness.FontColor = business.FontColor;
                existingBusiness.MenuSectionColor = business.MenuSectionColor;

                Context.Businesses.Update(existingBusiness);

                await Context.SaveChangesAsync();

                return 1;
            }

            return 0;
        }

        public async Task<int> UpdateBusinessInfo(BusinessInfo businessInfo)
        {
            var existingBusinessInfo = await Context.BusinessInfos.FirstOrDefaultAsync(b => b.Id == businessInfo.Id);

            if (existingBusinessInfo != null)
            {
                existingBusinessInfo.Address = businessInfo.Address;
                existingBusinessInfo.City = businessInfo.City;
                existingBusinessInfo.PhoneNumber = businessInfo.PhoneNumber;
                existingBusinessInfo.PostCode = businessInfo.PostCode;
                existingBusinessInfo.FullName = businessInfo.FullName;
                existingBusinessInfo.WebSite = businessInfo.WebSite;
                existingBusinessInfo.Facebook = businessInfo.Facebook;
                existingBusinessInfo.Instagram = businessInfo.Instagram;

                Context.BusinessInfos.Update(existingBusinessInfo);

                await Context.SaveChangesAsync();

                return 1;
            }

            return 0;
        }

        private static readonly Encoding Encoding1252 = Encoding.GetEncoding(1252);

        private static byte[] SHA1HashValue(string s)
        {
            byte[] bytes = Encoding1252.GetBytes(s);
            bytes = Encoding.Unicode.GetBytes(s);

            var sha1 = SHA512.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            return hashBytes;
        }

        private static byte[] BitmapToBytesCode(Bitmap image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
