using System.Threading.Tasks;
using Common.Interfaces;
using Models.TransferObjects;

namespace BusinessLayer.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerManager : IDependency
    {
        Task<int> RegisterCustomerAndSendVerificationEmail(CustomerLogin customerLogin);

        Task<int> DeleteCustomer(int customerId);
    }
}
