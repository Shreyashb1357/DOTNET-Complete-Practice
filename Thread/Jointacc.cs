using System.Diagnostics;

namespace Demo;
class Jointacc
{
    public int balance { get; set; }
    public bool Debit(int amount)
    {
        bool success = false;
        Monitor.Enter(this);
        if (balance >= amount)
        {
            balance = Activity.Perform(balance, amount, -1);
            success = true;
        }
        
        return success;
    }
    
    public void Credit(int amount)
    {
        lock (this) {
            balance = Activity.Perform(balance, amount, 1);
        } 
    }
}