using Audit.Consumer;
using Audit.Consumer.Consumers;
using Audit.Consumer.Context;
using Audit.Consumer.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        services.AddMassTransit(x =>
        {
            x.AddConsumer<AuditLogConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(configuration.GetSection("RabbitMq:ConnectionString").Value!), h =>
                {
                    h.Username(configuration.GetSection("RabbitMq:Username").Value!);
                    h.Password(configuration.GetSection("RabbitMq:Password").Value!);
                });
                cfg.ReceiveEndpoint("audit-log", e =>
                {
                    e.ConfigureConsumer<AuditLogConsumer>(context);
                });
            });
        });

        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
        });

        services.AddScoped<IAuditLogService, AuditLogService>();

        services.AddHostedService<Worker>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DatabaseContext>();
    if (context.Database.GetPendingMigrations().Any())
        context.Database.Migrate();
}

host.Run();
