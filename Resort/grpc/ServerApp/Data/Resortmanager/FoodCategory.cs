using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Resortmanager;

[Table("food_category")]
public class FoodCategory {
    //| category_id | category_name |

    [Column("category_id")]
    public int FoodCategoryId {get; set;}

    [Column("category_name")]
    public string FoodCategoryName {get; set;}
    

}