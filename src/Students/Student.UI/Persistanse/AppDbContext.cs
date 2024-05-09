using Microsoft.EntityFrameworkCore;

namespace Student.UI.Persistanse
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
            => Database.Migrate();


        public DbSet<Models.Student> Students { get; set; }

    }
}
