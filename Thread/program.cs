using Demo;

class Program
{
    static void Main()
    {
        Jointacc acc = new Jointacc();
        acc.Credit(10000);

        System.Console.WriteLine("The current balance  : {0}", acc.balance);

        var first = new Thread(() =>
        {
            System.Console.WriteLine("Shreyash is withdrawing 6000.......");
            if (acc.Debit(6000) == false)
                System.Console.WriteLine("Debit shreyash failed");

        });

        var second = new Thread(() =>
        {
            System.Console.WriteLine("Raj is withdrawing 7000.........");
            if (acc.Debit(7000) == false)
                System.Console.WriteLine("Debit Raj failed");
        });
        first.Start();
        second.Start();
        first.Join();
        second.Join();
        
        
        System.Console.WriteLine("Final balance : {0}", acc.balance);
    }
}