using aduaba.api.Entities.ApplicationEntity;
using aduaba.api.Entities.ApplicationEntity.ApplicationUserModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aduaba.api.AppDbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> cart { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<WishListItem> wishListItems { get; set; }
        public DbSet<Address> addresses { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}