using Audit.Consumer.Models;
using Microsoft.EntityFrameworkCore;

namespace Audit.Consumer.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
