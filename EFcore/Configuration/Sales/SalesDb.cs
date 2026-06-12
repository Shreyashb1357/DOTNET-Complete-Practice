namespace Sales;
public static class SalesPrice 
{
    public static decimal GetSalesPrice(this Product item)
    {
        return item.Orders
            .Select(e => e.Quantity)
            .Sum() * item.Price;
    }

}