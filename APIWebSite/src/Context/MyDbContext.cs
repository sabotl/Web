using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebSiteClassLibrary.Models;

namespace APIWebSite.src.Context
{
    public class MyDbContext: DbContext
    {
        public MyDbContext() { }
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=webShop;user id=postgres;Password=root;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Shop>()
                .HasMany(s => s.Products)
                .WithOne(p => p.ProductShop)
                .HasForeignKey(p => p.ShopId);
            modelBuilder.Entity<SubCategory>()
                .HasOne(s => s.Category)
                .WithMany(p => p.SubCategories)
                .HasForeignKey(fk => fk.CategoryId);
        }
        public DbSet<WebSiteClassLibrary.Models.User> users { get; set; }
        public DbSet<WebSiteClassLibrary.Models.Category> categories { get; set; }
        public DbSet<WebSiteClassLibrary.Models.SubCategory> subCategories { get; set; }
        public DbSet<WebSiteClassLibrary.Models.Product> products { get; set; }
        public DbSet<WebSiteClassLibrary.Models.Shop> shops { get; set; }

    }
}
