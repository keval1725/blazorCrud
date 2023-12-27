using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
