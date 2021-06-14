using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Interfaces;
using Models.DbModels;

namespace BusinessLayer.Interfaces
{
    public interface IMenuManager : IDependency
    {
        Task<List<MenuCategory>> GetBusinessMenuCategories(int businessId);
    }
}
