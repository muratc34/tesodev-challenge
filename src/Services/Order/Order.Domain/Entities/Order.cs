using Order.Domain.Enumerations;
using Shared.Core.Common;

namespace Order.Domain.Entities
{
    public sealed class Order : BaseEntity
    {
        public Order()
        {
        }
        public Order(Guid customerId, int quantity, double price, Status status, Guid productId, Guid addressId)
        {
            CustomerId = customerId;
            Quantity = quantity;
            Price = price;
            Status = status;
            ProductId = productId;
            AddressId = addressId;
        }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
