namespace Tax;

struct Supervisor(int subordinates) : Interface
{
    decimal Interface.Annualincome()
    {
        return 480000 + 3000 * subordinates;
    }
}