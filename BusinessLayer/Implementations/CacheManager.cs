using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Context;
using BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Models.DbModels;

namespace BusinessLayer.Implementations
{
    public class CacheManager : ICacheManager
    {
        private readonly IMemoryCache memoryCache;
        private IContextProvider contextProvider;

        public CacheManager(IContextProvider contextProvider, IMemoryCache memoryCache)
        {
            this.contextProvider = contextProvider;
            this.memoryCache = memoryCache;
        }

        public async Task<List<BusinessUser>> GetBusinessUsers()
        {
            return await GetInternal("BusinessUsers", () => contextProvider.GetContext().BusinessUsers.ToListAsync());
        }

        public async Task<List<User>> GetUsers()
        {
            return await GetInternal("Users", () => contextProvider.GetContext().Users.ToListAsync());
        }

        public async Task<StoredFile> GetFile(int fileId)
        {
            return await GetInternal("GetFile_" + fileId, () => contextProvider.GetContext().StoredFiles
                .Where(q => q.Id == fileId)
                .FirstOrDefaultAsync());
        }

        private async Task<T> GetInternal<T>(string key, Func<Task<T>> func)
        {
            return await memoryCache.GetOrCreateAsync<T>(key,
                async entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromSeconds(20);
                    return await func();
                });
        }
    }
}
