using Api.Infrastructure;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CustomerContext>(options => options.UseSqlServer());

            services.AddMemoryCache();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<MemoryCacheService>();
            services.AddScoped<RedisCacheService>();
            services.AddScoped<ICacheService>(sp =>
            {
                var ambiente = sp.GetRequiredService<IWebHostEnvironment>();

                return ambiente.IsDevelopment()
                    ? sp.GetRequiredService<MemoryCacheService>()
                    : sp.GetRequiredService<RedisCacheService>();
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
