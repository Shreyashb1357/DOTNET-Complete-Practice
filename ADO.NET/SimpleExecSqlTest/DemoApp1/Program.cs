using Microsoft.Data.Sqlite;

using var connection = new SqliteConnection("Data source = shop.db");
connection.Open();

using var command = connection.CreateCommand();
if (args.Length == 0)
{
    command.CommandText = "Select UserName , Address , Credit from Customerinfo";
    using var reader = command.ExecuteReader();
    while (reader.Read())
        System.Console.WriteLine("{0}\t{1}\t{2}", reader.GetString(0), reader.GetString(1), reader.GetDecimal(2));
}
else if(args[0].Length == 5)
{
    command.CommandText = $"Update Customerinfo set Credit = Credit + 100 where UserName = '{args[0]}'";
    int n = command.ExecuteNonQuery();
    if (n == 0)
        System.Console.WriteLine("No such Customer");
}