namespace Tax;
public interface Taxpayer
{
    decimal Annualincome();
    decimal Incometax()
    {
        decimal i = Annualincome() - 1200000;
        return i > 0 ? 0.15m * i : 0;
    }
}