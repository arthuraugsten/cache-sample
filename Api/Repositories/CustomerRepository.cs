using Api.Entities;
using Api.Infrastructure;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Api.Repositories
{
    public sealed class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;
        private readonly ICacheService _cacheService;

        public CustomerRepository(CustomerContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task AddAsync(Customer customer)
            => await _context.Customer.AddAsync(customer);

        public async Task DeleteAsync(Customer customer)
        {
            _context.Customer.Remove(customer);
            await CleanKeyAsync(CodeKey(customer.Code));
        }

        public async Task<Customer> GetByCodeAsync(string code)
        {
            var key = CodeKey(code);
            if (!_cacheService.TryGet(key, out Customer customer))
            {
                customer = await _context.Customer.FirstOrDefaultAsync(t => t.Code == code);
                _cacheService.Set(key, customer);
            }

            return customer;
        }

        public async Task<Customer> GetByIdAsync(Guid id)
            => await _context.Customer.FindAsync(id);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customer.Update(customer);
            await CleanKeyAsync(CodeKey(customer.Code));
        }

        public Task CleanKeyAsync(string chave)
        {
            _cacheService.Remove(chave);

            return Task.CompletedTask;
        }

        private static string CodeKey(string code) => $"{nameof(Customer)}_{nameof(Customer.Code)}_{code}";
    }
}
