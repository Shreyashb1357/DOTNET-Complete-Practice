namespace Tourism.Data;

using Microsoft.EntityFrameworkCore;
public class SiteDbContext : DbContext
{
    public DbSet<Traveller> Travellers { get; set; }

    public SiteDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data source = ShopDb.cs");
    }
}