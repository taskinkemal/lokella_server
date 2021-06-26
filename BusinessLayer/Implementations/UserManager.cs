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
        /// <param name="context"></param>
        /// <param name="logManager"></param>
        public UserManager(LokellaDbContext context, ILogManager logManager) : base(context, logManager)
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
            //DECLARE @salt UNIQUEIDENTIFIER=NEWID()

            //insert into sa_lokella.Users values(
            //    N'Kemal', N'Taskin', 1, N'kemal.n.taskin@gmail.com', HASHBYTES('SHA2_512', 'kemal123' + CAST(@salt AS NVARCHAR(36))), @salt)

            await Task.Delay(1);

            return 1;

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
