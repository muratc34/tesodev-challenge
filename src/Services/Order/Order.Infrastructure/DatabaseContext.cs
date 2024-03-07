using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;
using System.Reflection;

namespace Order.Infrastructure
{
    public sealed class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products{ get; set; }
        public DbSet<Domain.Entities.Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
