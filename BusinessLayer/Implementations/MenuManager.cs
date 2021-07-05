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
        /// <param name="contextProvider"></param>
        /// <param name="logManager"></param>
        public MenuManager(IContextProvider contextProvider, ICacheManager cacheManager, ILogManager logManager) : base(contextProvider, cacheManager, logManager)
        {
        }

        public async Task<List<MenuCategory>> GetBusinessMenuCategories(int businessId, bool fetchAll, int? parentId)
        {
            return await Context.MenuCategories
                .Where(q => q.BusinessId == businessId && (fetchAll || q.ParentId == parentId))
                .OrderBy(q => q.ParentId)
                .ThenBy(q => q.ItemOrder)
                .ToListAsync();
        }

        public async Task<List<MenuItemTo>> GetMenuItems(int categoryId)
        {
            var items = (from item in Context.MenuItems
                               join cat in Context.MenuCategories on item.CategoryId equals cat.Id
                               where cat.Id == categoryId || cat.ParentId == categoryId
                               select new MenuItemTo
                               {
                                   Item = item,
                                   Category = cat
                               })

                .AsQueryable()
                .OrderBy(q => q.Category.Id)
                .ThenBy(q => q.Item.ItemOrder)
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

        public async Task<int> InsertCategory(MenuCategory category)
        {
            if (category.ParentId != null)
            {
                var parentCategory = await Context.MenuCategories.FirstOrDefaultAsync(m => m.Id == category.ParentId);

                if (parentCategory == null || parentCategory.BusinessId != category.BusinessId)
                {
                    return 0;
                }
            }

            var existingList = await Context.MenuCategories.Where(m => m.BusinessId == category.BusinessId && m.ParentId == category.ParentId).ToListAsync();

            var maxOrder = 0;

            if (existingList.Any())
            {
                maxOrder = existingList.Max(m => m.ItemOrder);
            }

            var entity = await Context.AddAsync(new MenuCategory
            {
                Name = category.Name,
                BusinessId = category.BusinessId,
                ParentId = category.ParentId,
                ItemOrder = maxOrder + 1
            });

            await Context.SaveChangesAsync();

            return entity.Entity.Id;
        }

        public async Task<int> UpdateCategory(MenuCategory category)
        {
            var existing = await Context.MenuCategories.FirstOrDefaultAsync(m => m.Id == category.Id);

            if (existing != null)
            {
                existing.Name = category.Name;

                Context.Update(existing);

                await Context.SaveChangesAsync();

                return 1;
            }

            return 0;
        }

        public async Task<int> ReorderCategories(int businessId, Models.TransferObjects.MenuCategoryList list)
        {
            var allCategories = await Context.MenuCategories.Where(c => c.BusinessId == businessId).ToListAsync();

            var existingList = allCategories.Where(c => list.Items.Contains(c.Id)).ToList();

            var parents = existingList.GroupBy(l => l.ParentId).Select(g => g.Key).ToList();

            if (existingList.Count != list.Items.Count
                || existingList.Any(l => l.BusinessId != businessId)
                || parents.Count != 1)
            {
                return 0;
            }

            for (var i = 0; i < list.Items.Count; i++)
            {
                var item = await Context.MenuCategories.FirstOrDefaultAsync(c => c.Id == list.Items[i]);

                if (item != null)
                {
                    item.ItemOrder = i;
                    Context.Update(item);
                }
            }

            await Context.SaveChangesAsync();

            return 1;
        }

        public async Task<List<CatalogAdditive>> GetAdditives()
        {
            return await Context.CatalogAdditives.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<List<CatalogAllergy>> GetAllergies()
        {
            return await Context.CatalogAllergies.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<List<CatalogMenuItemTag>> GetTags()
        {
            return await Context.CatalogMenuItemTags.OrderBy(c => c.Id).ToListAsync();
        }
    }
}
