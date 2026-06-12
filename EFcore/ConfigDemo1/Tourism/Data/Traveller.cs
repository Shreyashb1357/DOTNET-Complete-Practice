namespace Tourism.Data;

public class Traveller
{
    public string Id { get; set; }

    public int Rating { get; set; }

    public List<Trip> Trips { get; set; }
}