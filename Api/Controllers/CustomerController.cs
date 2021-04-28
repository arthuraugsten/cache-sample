using Api.Entities;
using Api.Infrastructure;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class CustomerController : ControllerBase
    {
        private readonly CustomerContext _context;
        private readonly ICustomerRepository _repository;

        public CustomerController(CustomerContext context, ICustomerRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<Customer>> GetAsync()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            Customer? instance1 = default, instance2;
            try
            {
                var customers = new Customer[]
                {
                    new("Arthur", "25"),
                    new("Jéf", "26")
                };

                await _context.Customer.AddRangeAsync(customers);
                await _context.SaveChangesAsync();

                instance1 = await _repository.GetByCodeAsync("25");
                instance2 = await _repository.GetByCodeAsync("25");
            }
            catch
            {
                _context.Database.EnsureDeleted();
            }

            return Ok(instance1);
        }
    }
}
