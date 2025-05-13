using Microsoft.EntityFrameworkCore;
using edx_briselle.Server.Models;

namespace edx_briselle.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        internal IEnumerable<object> DObjs;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Add parameterless constructor for design-time migration support
        public ApplicationDbContext() { }

        public DbSet<User> Users { get; set; }
    }
}
