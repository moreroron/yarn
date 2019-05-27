using System.Data.Entity;
    
namespace yarn_rider.Models
{
    public class SiteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}