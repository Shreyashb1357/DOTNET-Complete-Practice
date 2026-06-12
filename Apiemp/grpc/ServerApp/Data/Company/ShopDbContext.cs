using Microsoft.EntityFrameworkCore;

namespace ServerApp.Data.Company;

public class ShopDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .ToTable("Employees")
            .HasKey(e => e.EmpId);       

        modelBuilder.Entity<Employee>()
            .Property(e => e.EmpId)
            .HasColumnName("EmpId");
    }
}
