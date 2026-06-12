namespace DemoApp.Model;

using Tourism.Data;
public class SiteModels
{
    public IEnumerable<Visitor> GetVisitors()
    {
        using var site = new SiteDbContext();
        var selection = from t in site.Travellers
                        where t.Id.Length > 3
                        select new Visitor
                        {
                            Name = t.Id,
                            Stars = new string('*', t.Rating),
                            Visits = t.Trips.Count,
                            Recent = t.Trips.Max(x => x.Checkin)
                        };
        return selection.ToList();
    }
    
    public void HandleVisitor(string vname ,  int vrating)
    {
        using var site = new SiteDbContext();
        var traveller = site.Travellers.Find(vname);
        if (traveller is null)
        {
            traveller = new Traveller { Id = vname };
            site.Travellers.Add(traveller);
        }
        traveller.Trips.Add(new Trip());
        traveller.Rating = vrating;
        site.SaveChanges();
    }
}