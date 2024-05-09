using Microsoft.EntityFrameworkCore;

namespace Blog.API.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) => Database.Migrate();


        public DbSet<Models.Client> Client { get; set; }
    }
}
