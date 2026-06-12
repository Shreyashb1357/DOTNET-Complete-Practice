namespace ServerApp.Resources;

public readonly record struct FoodMenuEntry(
    int FoodId , string FoodName , double Price , int CategoryId
);