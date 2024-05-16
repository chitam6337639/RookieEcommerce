using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Data.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasOne(c => c.Parent).WithMany(c => c.SubCategories).HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.NoAction);
            
            var MEN = new Category { CategoryId = 1, CategoryName = "MEN" };
            var WOMEN = new Category { CategoryId = 2, CategoryName = "WOMEN" };
            var BAGS = new Category { CategoryId = 3, CategoryName = "Bags", ParentId = MEN.CategoryId };
            var WALLETS = new Category { CategoryId = 4, CategoryName = "Wallets", ParentId = MEN.CategoryId };
            var HANDBAGW = new Category { CategoryId = 5, CategoryName = "Handbags", ParentId = WOMEN.CategoryId };
            var SHOESW = new Category { CategoryId = 6, CategoryName = "Shoes", ParentId = WOMEN.CategoryId };
            var WALLETSW = new Category { CategoryId = 7, CategoryName = "Wallets", ParentId = WOMEN.CategoryId };

            builder.Entity<Category>().HasData(MEN, WOMEN, BAGS, WALLETS, SHOESW, WALLETSW, HANDBAGW);

            var Bag = new Product { ProductId = 1, ProductName= "Ethan Backpack In Signature Canvas", Price=200, ImageURL ="", ProductDescription= "",  };

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",

                },
                new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "USER",

                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
