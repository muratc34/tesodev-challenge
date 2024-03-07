using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infrastructure.Configurations
{
    internal sealed class ProductConfiguration 
        : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Order)
                .WithMany(o => o.Products)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(p => p.Name).IsRequired();

            builder.Property(p => p.ImageUrl).IsRequired();
        }
    }
}
