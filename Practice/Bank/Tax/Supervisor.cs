namespace Tax;
struct Supervisor(int subordinates) : Taxpayer
{
    decimal Taxpayer.Annualincome()
    {
        return 480000 + 3000 * subordinates;
    }
}