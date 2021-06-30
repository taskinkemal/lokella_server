using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Context;
using BusinessLayer.Interfaces;
using Common.Interfaces;

namespace BusinessLayer.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ManagerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IContextProvider contextProvider;

        /// <summary>
        /// 
        /// </summary>
        protected ILogManager LogManager { get; }

        /// <summary>
        /// 
        /// </summary>
        protected ICacheManager CacheManager { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextProvider"></param>
        /// <param name="cacheManager"></param>
        /// <param name="logManager"></param>
        protected ManagerBase(IContextProvider contextProvider, ICacheManager cacheManager, ILogManager logManager)
        {
            this.contextProvider = contextProvider;
            CacheManager = cacheManager;
            LogManager = logManager;
        }

        protected LokellaDbContext Context => contextProvider.GetContext();

        protected IQueryable<int> GetUserBusinesses(int userId)
        {
            var items = from busr in Context.BusinessUsers
                        where busr.UserId == userId
                        select busr.BusinessId;

            return items;
        }

        protected async Task<bool> HasRight(int businessId, int userId, short role)
        {
            var allUsers = await CacheManager.GetUsers();
            if (allUsers.Any(u => u.Id == userId && u.Role == 1))
            {
                return true;
            }
            var entry = (await CacheManager.GetBusinessUsers())
                .FirstOrDefault(e => e.BusinessId == businessId);

            if (entry == null)
            {
                return false;
            }

            return entry.Role >= role;
        }
    }
}
