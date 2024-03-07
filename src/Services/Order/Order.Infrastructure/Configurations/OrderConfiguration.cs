using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.Infrastructure.Configurations
{
    internal sealed class OrderConfiguration
        : IEntityTypeConfiguration<Domain.Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Price).IsRequired();

            builder.Property(o => o.Quantity).IsRequired();

            builder.Property(o => o.Status).IsRequired();

            builder.Property(o => o.CustomerId).IsRequired();

            builder.HasOne(o => o.Product);

            builder.HasOne(o => o.Address);
        }
    }
}
