using System;
using System.Threading.Tasks;
using Common.Interfaces;
using Models.DbModels;

namespace BusinessLayer.Interfaces
{
    public interface IFilesManager : IDependency
    {
        Task<StoredFile> GetFile(int fileId);
    }
}
