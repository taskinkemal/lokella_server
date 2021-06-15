using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Context;
using BusinessLayer.Interfaces;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;
using Models.TransferObjects;

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

        public async Task<List<MenuItemTo>> GetMenuItems(int categoryId)
        {
            var items = Context.MenuItems
                .Where(q => q.CategoryId == categoryId)
                .Select(q => new MenuItemTo
                {
                    Item = q
                })
                .OrderBy(q => q.Item.ItemOrder)
                .AsQueryable();


            var additives = await (from cat in Context.CatalogAdditives
                                join lnk in Context.MenuItemAdditives on cat.Id equals lnk.AdditiveId
                                join item in items on lnk.MenuItemId equals item.Item.Id
                                select new { MenuItemId = item.Item.Id, Catalog = cat })
                                .ToListAsync();

            var allergies = await (from cat in Context.CatalogAllergies
                                   join lnk in Context.MenuItemAllergies on cat.Id equals lnk.AllergyId
                                   join item in items on lnk.MenuItemId equals item.Item.Id
                                   select new { MenuItemId = item.Item.Id, Catalog = cat })
                                .ToListAsync();

            var tags = await (from cat in Context.CatalogMenuItemTags
                                   join lnk in Context.MenuItemTags on cat.Id equals lnk.TagId
                                   join item in items on lnk.MenuItemId equals item.Item.Id
                                   select new { MenuItemId = item.Item.Id, Catalog = cat })
                                .ToListAsync();

            var prices = await (from lnk in Context.MenuItemPrices
                              join item in items on lnk.MenuItemId equals item.Item.Id
                              select new { MenuItemId = item.Item.Id, Price = lnk })
                                .ToListAsync();

            var result = items.ToList();

            foreach (var item in result)
            {
                item.Additives = additives
                    .Where(q => q.MenuItemId == item.Item.Id)
                    .Select(q => q.Catalog)
                    .ToList();

                item.Allergies = allergies
                    .Where(q => q.MenuItemId == item.Item.Id)
                    .Select(q => q.Catalog)
                    .ToList();

                item.Tags = tags
                    .Where(q => q.MenuItemId == item.Item.Id)
                    .Select(q => q.Catalog)
                    .OrderBy(q => q.ItemOrder)
                    .ToList();

                item.Prices = prices
                    .Where(q => q.MenuItemId == item.Item.Id)
                    .Select(q => q.Price)
                    .ToList();
            }

            return result.ToList();
        }
    }
}
