namespace Tourism.Data;

public class Trip
{
    public int Id { get; set; }
    public DateTime Checkin { get; set; }

    public Traveller Traveller { get; set; }
}