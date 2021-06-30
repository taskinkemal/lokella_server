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
    public class UserManager : ManagerBase, IUserManager
    {
        private static Random random = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextProvider"></param>
        /// <param name="logManager"></param>
        public UserManager(IContextProvider contextProvider, ICacheManager cacheManager, ILogManager logManager) : base(contextProvider, cacheManager, logManager)
        {
        }

        public async Task<string> Login(Models.TransferObjects.TokenRequest tokenRequest)
        {
            var user = await Context.Users
                .FromSqlRaw("SELECT * FROM sa_lokella.Users where Email=@email AND PasswordHash=HASHBYTES('SHA2_512', @password+CAST(PasswordSalt AS NVARCHAR(64)))",
                new Microsoft.Data.SqlClient.SqlParameter[]
                {
                    new Microsoft.Data.SqlClient.SqlParameter("email", tokenRequest.Email),
                    new Microsoft.Data.SqlClient.SqlParameter("password", tokenRequest.Password) })
                .FirstOrDefaultAsync();

            var token = GenerateToken();

            await Context.UserTokens.AddAsync(new UserToken
            {
                UserId = user.Id,
                Token = token
            });

            await Context.SaveChangesAsync();

            return token;
        }

        public async Task<int> Insert(User user)
        {
            var entity = Context.Users.Add(user);

            await Context.SaveChangesAsync();

            return entity.Entity.Id;
        }

        private static string GenerateToken()
        {
            const int length = 64;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
