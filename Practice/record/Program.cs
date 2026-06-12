using Demo;

if (args[0] == "Item")
{
    var selection = Shop.Getitems()
        .Where(i => i.Brand == args[0])
        .Select(i => i.Name);
    foreach (var a in selection)
        System.Console.WriteLine(a);
}
else if (args[0] == "Customer")
{
    decimal money = decimal.Parse(args[1]);
    var selection = from c in Shop.Getcust()
        where c.purchase > money
        select (c.Name, c.Ratin);
    foreach (var a in selection)
        System.Console.WriteLine(a);

}