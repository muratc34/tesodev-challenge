using Audit.Consumer.Context;
using MassTransit;
using Shared.Contracts;

namespace Audit.Consumer.Consumers
{
    public class AuditLogConsumer : IConsumer<AuditLog>
    {
        private readonly DatabaseContext _databaseContext;

        public AuditLogConsumer(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task Consume(ConsumeContext<AuditLog> context)
        {
            var newAuditLog = new AuditLog()
            {
               Id = context.Message.Id, 
               OrderId = context.Message.OrderId, 
               Action = context.Message.Action, 
               Message = context.Message.Message, 
               Date = context.Message.Date
            };
            Enum.TryParse(newAuditLog.Action.ToString(), true, out Models.Action result);
            await _databaseContext.Set<Models.AuditLog>().AddAsync(
                new Models.AuditLog(
                    newAuditLog.Id, 
                    newAuditLog.OrderId, 
                    result, 
                    newAuditLog.Message, 
                    newAuditLog.Date));
            await _databaseContext.SaveChangesAsync();
        }
    }
}
