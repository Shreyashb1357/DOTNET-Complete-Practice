namespace DemoApp.Shopping.Data;

public class Product
{
    public int Id { get; set; }

    public decimal Price { get; set; }

    public ICollection<Order> orders { get; set; } = [];
}