using System;
using System.Threading.Tasks;
using BusinessLayer.Context;
using BusinessLayer.Interfaces;
using Common;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;

namespace BusinessLayer.Implementations
{
    public class AuthManager : ManagerBase, IAuthManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextProvider"></param>
        /// <param name="logManager"></param>
        public AuthManager(IContextProvider contextProvider, ICacheManager cacheManager, ILogManager logManager) : base(contextProvider, cacheManager, logManager)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<(bool IsAuthenticated, int UserId, AuthenticationLevel Level)> VerifyAccessToken(string accessToken, int? businessId)
        {
            var token = await GetAccessToken(accessToken);

            if (token == null)
            {
                return (IsAuthenticated: false, UserId: 0, Level: AuthenticationLevel.NoAuthentication);
            }

            var user = await Context.Users.FindAsync(token.UserId);

            if (user == null)
            {
                return (IsAuthenticated: false, UserId: 0, Level: AuthenticationLevel.NoAuthentication);
            }

            var level = AuthenticationLevel.NoAuthentication;
            if (user.Role == 1)
            {
                level = AuthenticationLevel.SystemAdmin;
            }
            else
            {
                if (businessId != null)
                {
                    var role = (await Context.BusinessUsers.FirstOrDefaultAsync(b => b.BusinessId == businessId && b.UserId == user.Id))?.Role;

                    level = role == null ? AuthenticationLevel.NoAuthentication : role == 0 ? AuthenticationLevel.User : AuthenticationLevel.UserAdmin;
                }
                else
                {
                    level = AuthenticationLevel.User;
                }
            }

            return (IsAuthenticated: true, UserId: user.Id, Level: level);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<UserToken> GetAccessToken(string accessToken)
        {
            var token = await Context.UserTokens.FirstOrDefaultAsync(t =>
                t.Token == accessToken &&
                t.InsertedAt > DateTime.Now.AddYears(-1));

            return token;
        }
    }
}
