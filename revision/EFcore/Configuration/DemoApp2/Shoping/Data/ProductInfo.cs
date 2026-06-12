namespace DemoApp.Shopping.Data;

public static class ProductInfo
{
    public static decimal GetTotalSales(this Product item)
    {
        return item.orders
            .Select(e => e.Qty)
            .Sum() * item.Price;
    }
}