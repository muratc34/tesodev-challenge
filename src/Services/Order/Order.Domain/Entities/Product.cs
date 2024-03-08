using Shared.Core.Common;

namespace Order.Domain.Entities
{
    public sealed class Product : IBaseEntity
    {
        public Product() 
        {
        }
        public Product(string imageUrl, string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            ImageUrl = imageUrl;
        }
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}
