using Model;

string n = args[0].ToLower();
var serv = new Server(n);

string item =args[1].ToLower();
int quantity = int.Parse(args[2]);
var info = await serv.FetchItemInfo(item);
if (quantity <= info.Stock)
{
    System.Console.WriteLine("The Price will be : {0 : 0.00}", 1.05 * info.Stock * quantity);
}
else
{
    System.Console.WriteLine("No such Item");
}