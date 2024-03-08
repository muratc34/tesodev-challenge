using System.ComponentModel.DataAnnotations;

namespace Audit.Consumer.Models
{
    public sealed class AuditLog
    {
        public AuditLog()
        {
        }
        public AuditLog(Guid id, Guid? orderId, Action action, string message, DateTime date)
        {
            Id = id;
            OrderId = orderId;
            Action = action;
            Message = message;
            Date = date;
        }
        [Key]
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Action Action { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }

    public enum Action
    {
        Create = 0,
        Update = 1,
        Delete = 2,
        Get = 3
    }
}
