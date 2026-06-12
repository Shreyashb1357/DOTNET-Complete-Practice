using Microsoft.EntityFrameworkCore;
using DemoApp.Shopping.Data;

var db = new DbContextOptionsBuilder<ShopDbContext>();
db.UseSqlite("Data source=Shop.db");


using var shop = new ShopDbContext(db.Options);


shop.products.AddRange(
    new Product{ Id = 1, Price = 120 },
    new Product{ Id = 2, Price = 340 },
    new Product{ Id = 3, Price = 450 }
);
shop.SaveChanges();





int pno = int.Parse(args[0]);
var product = shop.products.Find(pno);

if (product is null)
{
    foreach (var entry in shop.products)
        System.Console.WriteLine("{0, -6}{1, 12:0.00}", entry.Id, entry.Price);
}

else
{
    shop.Entry(product).Collection(p => p.orders).Load();
    System.Console.WriteLine("Total sales : {0:0.00}", product.GetTotalSales());
}