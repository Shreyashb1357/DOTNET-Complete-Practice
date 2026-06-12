namespace ServerApp.data.shopping;

public class Orders
{
    public int OrderNo { get; set; }

    public DateOnly OrderDate { get; set; }

    public string CustomerId { get; set; }

    public int ProductNo { get; set; }

    public int Quantity { get; set; }
}