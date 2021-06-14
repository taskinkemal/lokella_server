using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Interfaces;
using Models.DbModels;

namespace BusinessLayer.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBusinessManager : IDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="qrCode"></param>
        /// <returns></returns>
        Task<Business> GetBusinessByQrCode(string qrCode);

        Task<int> InsertBusinessAsync(Models.TransferObjects.Business business);
    }
}
