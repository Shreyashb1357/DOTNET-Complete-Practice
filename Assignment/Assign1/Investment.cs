namespace demo;
class Investment
{
    public double amount { get; set; }
    public double rate { get; private set; }
    public int period { get; set; }

    public Investment(double a = 50000, int p = 2)
    {
        amount = a;
        period = p;
    }

    ~Investment()
    {
        System.Console.WriteLine("It is finalised......");
    }

    public void Print()
    {
        System.Console.WriteLine("the invest amount : {0} and period will be : {1}", amount, period);
        System.Console.WriteLine("The amount after {0} years : {1}", period, Getinterest());
    }

    public double Getinterest()
    {
        rate = amount < 50000 ? 7 : 8.5;
        double sinter = amount * rate * period / 100;
        return amount + sinter;
    }
       

}