using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Repositories;

namespace Customer.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository<Domain.Entities.Customer>, Repository<Domain.Entities.Customer, DatabaseContext>>();

            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(@"Server=localhost;Port=5432;Database=Customer;Search Path=public;User Id=postgres;Password=12345");
            });
        }
    }
}
