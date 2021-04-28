using Api.Entities;
using Api.Infrastructure;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public sealed class CustomerRepository : CachedRepository, ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context, ICacheService cacheService) : base(cacheService)
            => _context = context;

        public async Task AddAsync(Customer customer)
            => await _context.Customer.AddAsync(customer);

        public async Task DeleteAsync(Customer customer)
        {
            _context.Customer.Remove(customer);
            await CleanKeyAsync(CodeKey(customer.Code));
        }

        public async Task<Customer> GetByCodeAsync(string code)
            => await GetFromCacheAsync(CodeKey(code), async () =>
                await _context.Customer.FirstOrDefaultAsync(t => t.Code == code)
            );

        public async Task<Customer> GetByIdAsync(Guid id)
            => await _context.Customer.FindAsync(id);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customer.Update(customer);
            await CleanKeyAsync(CodeKey(customer.Code));
        }

        private static string CodeKey(string code) => $"{nameof(Customer)}_{nameof(Customer.Code)}_{code}";
    }
}
