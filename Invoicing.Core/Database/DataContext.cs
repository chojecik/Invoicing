using Invoicing.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoicing.Core
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
