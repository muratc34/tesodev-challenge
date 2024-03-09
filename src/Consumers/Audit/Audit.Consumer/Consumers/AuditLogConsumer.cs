using Audit.Consumer.Models;
using Audit.Consumer.Services;
using MassTransit;
using Shared.Contracts;

namespace Audit.Consumer.Consumers
{
    public class AuditLogConsumer : IConsumer<AuditLogCreated>
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogConsumer(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        public async Task Consume(ConsumeContext<AuditLogCreated> context)
        {
            var data = new AuditLog(
                    context.Message.Id,
                    context.Message.OrderId,
                    context.Message.Action.ToString(),
                    context.Message.Message,
                    context.Message.Date);

            await _auditLogService.CreateAuditLog(data);
        }
    }
}
