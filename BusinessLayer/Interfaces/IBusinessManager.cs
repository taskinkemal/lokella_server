using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.Interfaces;
using Models.DbModels;

namespace BusinessLayer.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBusinessManager : IDependency
    {
        Task<List<Business>> GetBusinesses(int userId, AuthenticationLevel authenticationLevel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qrCode"></param>
        /// <returns></returns>
        Task<Business> GetBusinessByQrCode(string qrCode);

        Task<int> InsertBusinessAsync(Models.TransferObjects.Business business);

        Task<List<SpecialOffer>> GetSpecialOffers(int businessId, bool isActive);

        Task<List<BusinessCategory>> GetBusinessCategories();

        Task<List<MembershipLevel>> GetMembershipLevels();

        Task<BusinessInfo> GetBusinessInfo(int businessId);

        Task<int> VisitBusiness(int businessId, int customerId);

        Task<List<Models.TransferObjects.CustomerVisit>> GetCustomerVisits(int businessId);

        Task<List<User>> GetBusinessUsers(int businessId);

        Task<Models.TransferObjects.BusinessUser> GetBusinessUser(int businessId, int userId);

        Task<int> InsertBusinessUser(Models.TransferObjects.BusinessUser businessUser);

        Task<int> UpdateBusinessUser(Models.TransferObjects.BusinessUser businessUser);

        Task<int> UpdateBusiness(Business business);

        Task<int> UpdateBusinessInfo(BusinessInfo businessInfo);
    }
}
