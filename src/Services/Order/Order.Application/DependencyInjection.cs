using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Core.Behaviours;
using System.Reflection;

namespace Order.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddMassTransit(options => {

                options.UsingRabbitMq((context, cfg) => {

                    cfg.Host(new Uri(configuration.GetSection("RabbitMq:ConnectionString").Value!), h =>
                    {
                        h.Username(configuration.GetSection("RabbitMq:Username").Value!);
                        h.Password(configuration.GetSection("RabbitMq:Password").Value!);
                    });
                });
            });
        }
    }
}
