using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Resortmanager;

[Table("food_menu")]
public class FoodMenu {
    //| food_id | food_name            | price  | category_id |

    [Column("food_id")]
    public int foodId {get; set;}

    [Column("food_name")]
    public string foodName {get;set;}

    [Column("category_id")]
    public int FoodCategoryId {get;set;}

    [ForeignKey(nameof(FoodCategoryId))]
    public FoodCategory FoodCategory {get; set;}

    public double price{get;set;}
}
