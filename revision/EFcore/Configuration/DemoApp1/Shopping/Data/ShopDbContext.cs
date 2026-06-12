using Microsoft.EntityFrameworkCore;


namespace DemoApp.Shopping.Data;


public class ShopDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=iitdac.met.edu;Database=Shop4;User Id=dac4;Password=Dac4@1234;Encrypt=False");
    }

}