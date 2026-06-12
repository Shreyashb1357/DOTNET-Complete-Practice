using DemoApp.Tourism.Model;
using DemoApp.Tourism.Data;


var model = new ShopModel();
if(args.Length > 0)
{
    model.HandleVisitor(args[0], 5);
    Console.WriteLine("Welcome {0}", args[0]);
}
else
{
    foreach(var entry in model.GetVisitors())
        Console.WriteLine("{0, -20}{1, -6}{2, 16}", entry.Name, entry.Visits, entry.Recent);
}