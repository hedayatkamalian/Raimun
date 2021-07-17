using Microsoft.EntityFrameworkCore;

namespace Raimun.App.Entities.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TempData> TempDatas { get; set; }
    }
}