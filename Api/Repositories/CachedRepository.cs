using Api.Services;
using System;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public abstract class CachedRepository : ICachedRepository
    {
        private readonly ICacheService _cacheService;

        public CachedRepository(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<T> GetFromCacheAsync<T>(string key, Func<Task<T>> query)
        {
            if (!_cacheService.TryGet(key, out T entity))
            {
                entity = await query();
                _cacheService.Set(key, entity);
            }

            return entity;
        }

        public Task CleanKeyAsync(string key)
        {
            _cacheService.Remove(key);

            return Task.CompletedTask;
        }
    }
}
