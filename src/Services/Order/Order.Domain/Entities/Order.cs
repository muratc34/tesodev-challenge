using Order.Domain.Enumerations;
using Shared.Core.Common;

namespace Order.Domain.Entities
{
    public sealed class Order : BaseEntity
    {
        public Order()
        {
        }
        public Order(Guid customerId, int quantity, double price, Status status)
        {
            CustomerId = customerId;
            Quantity = quantity;
            Price = price;
            Status = status;
        }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public List<Product>? Products { get; set; }
    }
}
