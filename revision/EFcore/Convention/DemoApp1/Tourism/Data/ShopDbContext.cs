namespace DemoApp.Tourism.Data;

using Microsoft.EntityFrameworkCore;

public class ShopDbContext : DbContext
{

    public DbSet<Traveller> Travellers { get; set; }

    public ShopDbContext()
    {
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source=Shop.db");
    }
}