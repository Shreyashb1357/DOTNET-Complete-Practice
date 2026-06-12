namespace Tourism.Data;
public class Trips
{
    public int Id { get; set; }

    public DateTime checkin { get; set; } = DateTime.Now;

    public Traveller Traveller { get; set; }
}
