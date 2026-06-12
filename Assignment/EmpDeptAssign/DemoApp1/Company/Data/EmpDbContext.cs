namespace DemoApp.Company.Model;

using Microsoft.EntityFrameworkCore;
public class EmpDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=iitdac.met.edu;Database=Employee63;User Id=Dac4;Password=Dac4@1234;Encrypt=False");
    }
}