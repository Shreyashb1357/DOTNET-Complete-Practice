namespace Tax;
interface Interface
{
    decimal Annualincome();

    decimal Incometax()
    {
        decimal i = Annualincome() - 120000;
        return i > 0 ? 0.15m * i : 0;
    }
}