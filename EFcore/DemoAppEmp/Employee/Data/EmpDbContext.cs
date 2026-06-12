namespace DemoApp.Employee.Data;
using Microsoft.EntityFrameworkCore;



public class EmpDbContext : DbContext
{
    public DbSet<Emp> Emp {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
    {
        optionBuilder.UseSqlServer("Data Source=iitdac.met.edu;Database=Employee63;User Id=Dac4;Password=Dac4@1234;Encrypt=False");
    }

}