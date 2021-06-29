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
        private LokellaDbContext context;

        public CacheManager(LokellaDbContext context, IMemoryCache memoryCache)
        {
            this.context = context;
            this.memoryCache = memoryCache;
        }

        public async Task<List<BusinessUser>> GetBusinessUsers()
        {
            return await GetInternal("BusinessUsers", () => context.BusinessUsers.ToListAsync());
        }

        public async Task<List<User>> GetUsers()
        {
            return await GetInternal("Users", () => context.Users.ToListAsync());
        }

        public async Task<StoredFile> GetFile(int fileId)
        {
            return await GetInternal("GetFile_" + fileId, () => context.StoredFiles
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
