using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Interfaces;
using Models.DbModels;
using Models.TransferObjects;

namespace BusinessLayer.Interfaces
{
    public interface IUserManager : IDependency
    {
        Task<string> Login(Models.TransferObjects.TokenRequest tokenRequest);

        Task<int> Insert(User user);
    }
}