using Microsoft.EntityFrameworkCore;
namespace Tourism.Data;

public class SiteDbContext : DbContext
{
    public DbSet<Traveller> Treavellers { get; set; }

    public SiteDbContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
    {
        optionbuilder.UseSqlite("Data source = site.db");

    }
    
}