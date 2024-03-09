namespace Shared.Contracts
{
    public sealed class AuditLogCreated
    {
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
