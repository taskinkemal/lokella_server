using System;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Context;
using BusinessLayer.Interfaces;
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;

namespace BusinessLayer.Implementations
{
    public class FilesManager : ManagerBase, IFilesManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextProvider"></param>
        /// <param name="logManager"></param>
        public FilesManager(IContextProvider contextProvider, ICacheManager cacheManager, ILogManager logManager) : base(contextProvider, cacheManager, logManager)
        {
        }

        public Task<StoredFile> GetFile(int fileId)
        {
            return CacheManager.GetFile(fileId);
        }

        public async Task<int> UploadFile(Models.TransferObjects.File file)
        {
            var bytes = Convert.FromBase64String(file.FileBase64);

            var fileEntity = await Context.StoredFiles.AddAsync(new StoredFile
            {
                MimeType = "",
                FileName = "angebot",
                FileContent = bytes
            });

            await Context.SaveChangesAsync();

            return fileEntity.Entity.Id;
        }
    }
}
