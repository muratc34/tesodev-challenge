using Order.Domain.Enumerations;
using Shared.Core.Common;

namespace Order.Domain.Entities
{
    public sealed class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public Product Product { get; set; }
        public Address Address { get; set; }
    }
}
