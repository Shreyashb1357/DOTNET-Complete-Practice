using demo;
class Program
{
    static void PresentRecord(object info)
    {
        Type t = info.GetType();
        System.Console.WriteLine("<{0}>", t.Name);
        foreach(var p in t.GetProperties())
            System.Console.WriteLine("      <{0}>{1}</{0}>", p.Name, p.GetValue(info));

        System.Console.WriteLine("</{0}>", t.Name);
        System.Console.WriteLine();
    }
    static void Main()
    {
        Item i = Shop.Getpopular();
        PresentRecord(i);
        Customer c = Shop.Getbest();
        PresentRecord(c);
        
    }
}