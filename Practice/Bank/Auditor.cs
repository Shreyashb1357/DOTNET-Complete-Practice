using Tax;
class Auditor : IDisposable
{
    public Auditor()
    {
        System.Console.WriteLine("Auditing : {0}", DateTime.Now);
        System.Console.WriteLine("=====================================");
    }

    public void Audit(string id, Taxpayer emp)
    {
        System.Console.WriteLine("Auditong of the {0}", id);
        if (id.Length > 4)
            throw new ArgumentException("Invalid ID");
        decimal payment = emp.Annualincome() + 500;
        System.Console.WriteLine("The to total payemnt : {0 : 0.00}", payment);
    }

    public void Dispose()
    {
        System.Console.WriteLine("Auding lo {0} is now ending.", DateTime.Now);
    }
}