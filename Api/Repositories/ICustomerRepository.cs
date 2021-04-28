using Api.Entities;
using System;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public interface ICustomerRepository : ICachedRepository
    {
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Customer customer);
        Task<Customer> GetByIdAsync(Guid id);
        Task<Customer> GetByCodeAsync(string code);
        Task SaveAsync();
    }
}
