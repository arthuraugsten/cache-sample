using Microsoft.Extensions.Caching.Memory;
using System;

namespace Api.Services
{
    public sealed class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private MemoryCacheEntryOptions _cacheOptions;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;

            // Recomendado guardar estas configurações no appsettings
            _cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };
        }

        public bool TryGet<T>(string cacheKey, out T value)
        {
            _memoryCache.TryGetValue(cacheKey, out value);

            return value is not null;
        }

        public T Set<T>(string cacheKey, T value)
            => _memoryCache.Set(cacheKey, value, _cacheOptions);

        public void Remove(string cacheKey)
            => _memoryCache.Remove(cacheKey);
    }
}
