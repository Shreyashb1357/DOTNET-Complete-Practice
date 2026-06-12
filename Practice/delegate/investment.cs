delegate double Intrate(int years);
class Investment
{
    public double Installment { get; init; } = 50000;
    public int years { get; init; }

    public double Futurevalue(Intrate ss)
    {
        //ss.Invoke(years) = means call the method called ss and pass value years.
        double i = ss.Invoke(years) / 100;
        return (Installment / i) * (Math.Pow(1 + i, years) - 1);
    }
}