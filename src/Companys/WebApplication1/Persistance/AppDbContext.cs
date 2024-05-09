using Microsoft.EntityFrameworkCore;

namespace WebApplication1.API.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) => Database.Migrate();

        public DbSet<Companies> Companies { get; set; }
    }
}
