namespace ServerApp.Resources;

public readonly record struct FoodOrderEntry(
    int OrderId, int CustomerId , int FoodId , int Quantity , double TotalPrice , DateTime OrderDate
);