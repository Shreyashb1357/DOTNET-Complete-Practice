namespace DemoApp.Tourism.Model;
using DemoApp.Tourism.Data;

public class ShopModel
{
    public IEnumerable<Visitor> GetVisitors()
    {
        using var site = new ShopDbContext();
        var selection = from t in site.Travellers
                        where t.Id.Length > 3
                        select new Visitor
                        {
                            Name = t.Id,
                            Stars = t.Rating,
                            Visits = t.Trips.Count,
                            Recent = t.Trips.Max(a => a.Checkin)

                        };
        return selection.ToList();
    }

    public void HandleVisitor(string vname , int vrating)
    {
        using var site = new ShopDbContext();
        var tarveller = site.Travellers.Find(vname);
        if (tarveller is null)
        {
            tarveller = new Traveller { Id = vname, Rating = vrating };
            site.Travellers.Add(tarveller);
        }
        tarveller.Trips.Add(new Trip());
        site.SaveChanges();
    }
}