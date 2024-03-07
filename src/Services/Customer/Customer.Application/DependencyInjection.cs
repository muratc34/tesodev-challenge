using Customer.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;

namespace Customer.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
