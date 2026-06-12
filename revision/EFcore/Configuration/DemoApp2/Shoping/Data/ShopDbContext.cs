namespace DemoApp.Shopping.Data;

using Microsoft.EntityFrameworkCore;

public class ShopDbContext : DbContext
{
    public DbSet<Order> orders { get; set; }
    public DbSet<Product> products { get; set; }

    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
    }

    // 👇 this constructor is needed by EF CLI tools
    public ShopDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // 👇 this ensures migrations know where your DB is
            optionsBuilder.UseSqlite("Data Source=Shop.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .ToTable("OrderInfo")
            .Property(p => p.Id)
            .HasColumnName("OrderNo");

        modelBuilder.Entity<Product>()
            .ToTable("ProductInfo")
            .Property(p => p.Id)
            .HasColumnName("ProductNo");

        modelBuilder.Entity<Order>()
            .Property(p => p.ProductId)
            .HasColumnName("ProductNo");
    }
}
