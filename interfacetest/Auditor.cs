using Tax;

class Auditor : IDisposable
{
    public Auditor()
    {
        System.Console.WriteLine("Audit log {0} , - The audit started", DateTime.Now);
    }

    public void Audit(string s, Interface i)
    {
        System.Console.WriteLine("Audit {0}", s);
        if (s.Length < 4)
            throw new ArgumentException("Invalid ID");
        decimal payment = i.Incometax() + 500;
        System.Console.WriteLine("Total tax payment : {0:0.00}", payment);
    }

    public void Dispose()
    {
        Console.WriteLine("Auditor Log [{0}] - ending audit session", DateTime.Now);
    }
}