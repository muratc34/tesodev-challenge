using Audit.Consumer.Services;

namespace Audit.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<IAuditLogService>();

                await context.SendMailAsync();
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
