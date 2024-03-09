using Audit.Consumer.Context;
using Audit.Consumer.Models;
using MassTransit;
using Shared.Contracts;

namespace Audit.Consumer.Consumers
{
    public class AuditLogConsumer : IConsumer<AuditLogCreated>
    {
        private readonly DatabaseContext _databaseContext;

        public AuditLogConsumer(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task Consume(ConsumeContext<AuditLogCreated> context)
        {
            var data = new AuditLog(
                    context.Message.Id,
                    context.Message.OrderId,
                    context.Message.Action.ToString(),
                    context.Message.Message,
                    context.Message.Date);

            await _databaseContext.Set<AuditLog>().AddAsync(data);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
