using Microsoft.EntityFrameworkCore;
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
            modelBuilder.Entity<WebSiteClassLibrary.Models.Shop>()
                .HasMany(s => s.Products)
                .WithOne(p => p.ProductShop)
                .HasForeignKey(p => p.ShopId);

            modelBuilder.Entity<WebSiteClassLibrary.Models.SubCategory>()
                .HasOne(s => s.Category)
                .WithMany(p => p.SubCategories)
                .HasForeignKey(fk => fk.CategoryId);

            modelBuilder.Entity<Cart>()
            .Property(c => c.Created_at)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.cartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.AddedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany() // Если Product имеет коллекцию CartItems
                .HasForeignKey(ci => ci.ProductId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<WebSiteClassLibrary.Models.User> users { get; set; }
        public DbSet<WebSiteClassLibrary.Models.Category> categories { get; set; }
        public DbSet<WebSiteClassLibrary.Models.SubCategory> subCategories { get; set; }
        public DbSet<WebSiteClassLibrary.Models.Product> products { get; set; }
        public DbSet<WebSiteClassLibrary.Models.Shop> shops { get; set; }
        public DbSet<WebSiteClassLibrary.Models.Cart> Cart { get; set; }
        public DbSet<WebSiteClassLibrary.Models.CartItem> CartItems { get; set; }

    }
}
