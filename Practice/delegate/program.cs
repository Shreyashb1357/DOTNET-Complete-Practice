class Program
{
    static double Safescheme(int period)
    {
        return period > 3 ? 7.2 : 5.2;
    }
    static void Main(string[] args)
    {
        var a = new Investment()
        {
            Installment = double.Parse(args[0]),
            years = int.Parse(args[1])
        };
        System.Console.WriteLine("The future  value  : {0 : 0.00}", a.Futurevalue(Safescheme));
        double x = 8.75;
        System.Console.WriteLine("The future  value  : {0 : 0.00}", a.Futurevalue(n => x + 0.25*n));

        
    }
}