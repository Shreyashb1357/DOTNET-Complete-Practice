using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Sales;
namespace Shopping.Data;

public class SalesProduct(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .ToTable("ProductInfo")
            .Property(e => e.Id)
            .HasColumnName("ProductNo");

        modelBuilder.Entity<Order>()
            .ToTable("Order")
            .Property(e => e.Id)
            .HasColumnName("OrderNo");

        modelBuilder.Entity<Order>()
            .Property(e => e.ProductId)
            .HasColumnName("ProductNo");

    }

}