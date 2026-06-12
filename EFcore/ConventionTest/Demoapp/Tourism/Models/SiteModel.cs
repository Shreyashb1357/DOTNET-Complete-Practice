namespace Tourism.Model;
using Tourism.Data;
public class SiteModel
{
    public IEnumerable<Visitor> GetVisitor()
    {
        using var site = new SiteDbContext();
        var selection = from t in site.Treavellers
                        where t.Id.Length > 3
                        select new Visitor()
                        {
                            Name = t.Id,
                            Stars = new string('*', t.Rating),
                            Visits = t.Trips.Count,
                            Recent = t.Trips.Max(x => x.checkin)
                        };
        return selection.ToList();
    }

    public void HandleVisit(string VisitorName , int vrating)
    {
        using var site = new SiteDbContext();
        var traveller = site.Treavellers.Find(VisitorName);
        if (traveller is null)
        {
            traveller = new Traveller { Id = VisitorName };
            site.Treavellers.Add(traveller);
        }
        traveller.Trips.Add(new Trips());
        traveller.Rating = vrating;
        site.SaveChanges();
        
    }
}