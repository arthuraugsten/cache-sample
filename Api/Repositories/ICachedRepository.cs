using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface ICachedRepository
    {
        Task CleanKeyAsync(string key);
    }
}
