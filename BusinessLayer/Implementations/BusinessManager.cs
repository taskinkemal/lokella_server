using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Context;
using BusinessLayer.Interfaces;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logManager"></param>
        public BusinessManager(LokellaDbContext context, ILogManager logManager) : base(context, logManager)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Business>> GetBusinesses()
        {
            return await Context.Businesses.ToListAsync();
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
