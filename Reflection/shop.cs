namespace demo;
record Item(string Name, string Brand);

readonly record struct Customer(string Name, decimal Purchase, int rating);

class Shop
{
    public static Item Getpopular()
    {
        return new Item("cpu", "Intel");
    }

    public static Customer Getbest()
    {
        return new Customer("Shreyash", 150000, 5);
    }
}