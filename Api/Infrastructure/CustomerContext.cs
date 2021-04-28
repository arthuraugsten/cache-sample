using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Api.Infrastructure
{
    public sealed class CustomerContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

        public DbSet<Customer> Customer => Set<Customer>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLoggerFactory(_logger).EnableSensitiveDataLogging()
                .UseSqlServer("Server=tcp:127.0.0.1,5433;Database=ApiCache;User Id=sa;Password=4Jgz2HmDKS;",
                     p => p.EnableRetryOnFailure(
                        maxRetryCount: 2,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null
                     )
                );
        }

        protected override void OnModelCreating(ModelBuilder builder) =>
            _ = builder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);
    }
}
