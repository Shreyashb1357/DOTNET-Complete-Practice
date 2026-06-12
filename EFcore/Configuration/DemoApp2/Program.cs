using Shopping.Data;
using Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

var db = new DbContextOptionsBuilder<SalesProduct>();
db.UseSqlite("Data source = shop.db");
using var shop = new SalesProduct(db.Options);
shop.Database.EnsureCreated();
int pno = int.Parse(args[0]);
var product = shop.Products.Find(pno);

if (product is null)
{
    foreach (var entry in shop.Products)
        System.Console.WriteLine("{0, -6} {1,12:0.00}", entry.Id, entry.Price);
}
else
{
    shop.Entry(product).Collection(p => p.Orders).Load();
    System.Console.WriteLine("Total Sales : {0:0.00}", product.GetSalesPrice());
}