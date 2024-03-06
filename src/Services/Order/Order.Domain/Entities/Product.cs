using Shared.Core.Common;

namespace Order.Domain.Entities
{
    public sealed class Product : IBaseEntity
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public int Name { get; set; }
    }
}
