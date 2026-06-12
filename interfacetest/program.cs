using Tax;

class Program 
{
    static void Doaudit(string str, int cout)
    {
        using(var a = new Auditor())
        {
            if(cout <= 10)
                a.Audit(str, new Supervisor(cout));
            else
                a.Audit(str, new Worker(cout));
        }
    }



    static void Main(string[] args)
    {
        try
        {
            string a = args[0].ToUpper();
            int b = int.Parse(args[1]);
            Doaudit(a, b);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine("Invalid Error: {0}", ex.Message);
        }
    }
}