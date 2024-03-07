namespace Order.Domain.Contracts
{
    public sealed class Customer
    {
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
    }
}
