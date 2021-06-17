using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Interfaces;
using Models.DbModels;
using Models.TransferObjects;

namespace BusinessLayer.Interfaces
{
    public interface IMenuManager : IDependency
    {
        Task<List<MenuCategory>> GetBusinessMenuCategories(int businessId, bool fetchAll, int? parentId);

        Task<List<MenuItemTo>> GetMenuItems(int categoryId);
    }
}
