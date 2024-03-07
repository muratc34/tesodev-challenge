using Shared.Core.Common;

namespace Order.Domain.Entities
{
    public sealed class Product : IBaseEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public string ImageUrl { get; set; }
        public int Name { get; set; }
    }
}
