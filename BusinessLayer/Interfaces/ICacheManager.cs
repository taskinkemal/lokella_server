using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Interfaces;
using Models.DbModels;

namespace BusinessLayer.Interfaces
{
    public interface ICacheManager : IDependency
    {
        Task<List<BusinessUser>> GetBusinessUsers();

        Task<List<User>> GetUsers();

        Task<StoredFile> GetFile(int fileId);
    }
}
