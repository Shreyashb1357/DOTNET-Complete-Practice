namespace Tax;
struct Worker(int jobs) : Taxpayer
{
    decimal Taxpayer.Annualincome()
    {
        return 144000 + 400 * jobs;
    }
}