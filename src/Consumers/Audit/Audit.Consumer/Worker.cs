using Audit.Consumer.Services;

namespace Audit.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                var nextMidnight = DateTime.UtcNow.Date.AddDays(1);
                var timeUntilMidnight = nextMidnight - now;

                _logger.LogInformation("Worker running at: {0}", now);
                _logger.LogInformation("Next time the worker will work at this time: {0}", nextMidnight);

                using var scope = _serviceProvider.CreateScope();
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<IAuditLogService>();

                await context.SendMailAsync();
                await Task.Delay(timeUntilMidnight, stoppingToken);
            }
        }
    }
}
