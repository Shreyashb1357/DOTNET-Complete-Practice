namespace DemoApp.Tourism.Data;
public class Trip
{
    public int Id { get; set; }

    public DateTime Checkin { get; set; } = DateTime.Now;

    public Traveller Traveller{ get; set; }
}