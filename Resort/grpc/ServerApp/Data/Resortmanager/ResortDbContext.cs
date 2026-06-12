using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
namespace Data.Resortmanager;

public class ResortDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Customer> customers {get; set;}

    public DbSet<Department>  departments {get; set;} 

    public DbSet<FoodCategory> foodcategories  {get; set;} 

    public DbSet<FoodMenu> foodmenu  {get; set;} 

    public DbSet<FoodOrder> foodorders  {get; set;} 

    public DbSet<Room> rooms  {get; set;} 

    public DbSet<RoomBooking> roombookings  {get; set;} 


    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Customer>()
            .HasKey(e=>e.CustId);

        modelBuilder.Entity<Department>()
            .HasKey(e=>e.departmentId);

        modelBuilder.Entity<FoodCategory>()
            .HasKey(e=>e.FoodCategoryId);

        modelBuilder.Entity<FoodOrder>()
            .HasKey(e=>e.orderId);

        modelBuilder.Entity<Room>()
            .HasKey(e=>e.rId);

        modelBuilder.Entity<RoomBooking>()
            .HasKey(e=>e.BId);

        modelBuilder.Entity<FoodMenu>()
            .HasKey(e=>e.foodId);
    }
}