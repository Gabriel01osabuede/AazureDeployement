using aduaba.data.Entities.ApplicationEntity;
using Microsoft.EntityFrameworkCore;

namespace aduaba.data.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base (options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        
        
        
        
    }
}