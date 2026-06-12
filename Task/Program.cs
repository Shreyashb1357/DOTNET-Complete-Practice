using System.Diagnostics;

namespace Demo;

class Program
{
    public class Computation
    {
        private Stopwatch clock = new();

        public long Compute(int first, int count)
        {
            clock.Start();
            return Enumerable.Range(first, count)
                .AsParallel()
                .Select(Activity.Perform)
                .Sum();
        }

        public Task<long> ComputeAsync(int first, int count)
        {
            return Task<long>.Run(() => Compute(first, count));
        }

        public double Time()
        {
            clock.Stop();
            return clock.Elapsed.TotalSeconds;
        }
    }
    
    static async Task Handlejob(int jno)
    {
        System.Console.Write("Start Computing....");
        var a = new Computation();
        long r = await a.ComputeAsync(1, jno);
        System.Console.WriteLine("Done!!");
        System.Console.WriteLine("The Result = {0} and computed in {1}", r, a.Time());
    }


    static void Main(string[] args)
    {
        int n = int.Parse(args[0]);
        var job = Handlejob(n);
        while (!job.IsCompleted)
        {
            System.Console.Write('.');
            Thread.Sleep(100);
        }
    }
}