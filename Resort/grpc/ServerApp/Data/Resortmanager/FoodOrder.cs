using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace Data.Resortmanager;

[Table("food_orders")]
public class FoodOrder {
    //| order_id | customer_id | food_id | quantity | total_price | order_date          |
    //   FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
    //  FOREIGN KEY (food_id) REFERENCES food_menu(food_id)
    
    [Column("order_id")]
    public int orderId  {get; set;}

    [Column("customer_id")]
    public int CustId{get;set;}

    [ForeignKey(nameof(CustId))]
    public Customer Customer {get; set;}

    [Column("food_id")]
    public int foodId {get; set;}

    [ForeignKey(nameof(foodId))]
    public FoodMenu FoodMenu {get; set;}

    public int quantity {get; set;}

    [Column("total_price")]
    public double totalPrice {get; set;}

    [Column("order_date")]
    public DateTime orderDate {get; set;} = DateTime.Now;
}