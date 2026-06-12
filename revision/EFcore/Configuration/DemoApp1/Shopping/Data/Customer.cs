namespace DemoApp.Shopping.Data;

using System.ComponentModel.DataAnnotations.Schema;


public class Customer
{
    
    public string Id { get; set; }
    public decimal Credit { get; set; }

    public List<Order> Orders { get; set; } = [];

}