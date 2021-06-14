using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Context;
using BusinessLayer.Interfaces;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;

namespace BusinessLayer.Implementations
{
    public class MenuManager : ManagerBase, IMenuManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logManager"></param>
        public MenuManager(LokellaDbContext context, ILogManager logManager) : base(context, logManager)
        {
        }

        public async Task<List<MenuCategory>> GetBusinessMenuCategories(int businessId)
        {
            return await Context.MenuCategories
                .Where(q => q.BusinessId == businessId)
                .OrderBy(q => q.ItemOrder)
                .ToListAsync();
        }
    }
}
