using Tax;
class Program
{
    // static void Doaudit(string name, int count)
    // {
    //     var a = new Auditor();
    //     if (count > 10)
    //         a.Audit(name, new Supervisor(count));
    //     else
    //         a.Audit(name, new Worker(count));
    // }

    static void Doaudit(string name, int count)
    {
        using (var a = new Auditor())
        {
            if (count > 10)
                a.Audit(name, new Supervisor(count));
            else
                a.Audit(name, new Worker(count));
        }
    }

    
    static void Main(string[] args)
    {
        try
        {
            string p = args[0].ToUpper();
            int q = int.Parse(args[1]);
            Doaudit(p, q);
        }
        catch(Exception e)
        {
            System.Console.WriteLine("Auditng error  : {0}", e);
        }
    }
}