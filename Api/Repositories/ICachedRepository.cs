using System;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface ICachedRepository
    {
        Task<T> GetFromCacheAsync<T>(string key, Func<Task<T>> query);
        Task CleanKeyAsync(string key);
    }
}
