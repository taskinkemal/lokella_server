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
        /// <param name="context"></param>
        /// <param name="logManager"></param>
        public FilesManager(LokellaDbContext context, ILogManager logManager) : base(context, logManager)
        {
        }

        public Task<StoredFile> GetFile(int fileId)
        {
            return Context.StoredFiles
                .Where(q => q.Id == fileId)
                .FirstOrDefaultAsync();
        }
    }
}
