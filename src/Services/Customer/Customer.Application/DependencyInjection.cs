using Customer.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
