namespace Tax;
struct Worker(int jobs) : Interface
{
    decimal Interface.Annualincome()
    {
        return 144000 + 400 * jobs;
    }
}