using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Entities;
using Shared.Core.Repositories;

namespace Order.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository<Domain.Entities.Order>, Repository<Domain.Entities.Order, DatabaseContext>>();
            services.AddScoped<IRepository<Product>, Repository<Product, DatabaseContext>>();
            services.AddScoped<IRepository<Address>, Repository<Address, DatabaseContext>>(); 

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Database"));
            });
        }
    }
}
