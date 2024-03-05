using System.Diagnostics;
using TaskAsync.Extensions;
using TaskAsync.Tasks;

namespace TaskAsync
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var breakFast = new BreakFast();

            List<Task> breakFastTasks = new List<Task>();

            var semaphore = new SemaphoreSlim(100);

            var clientes = Enumerable.Range(1, 1000).ToList();

            breakFastTasks = clientes.Select(async b =>
            {
                await semaphore.WaitAsync();
                try
                {
                    await breakFast.RunAsync();
                }
                finally
                {

                    semaphore.Release();

                }


            }).ToList();


            breakFastTasks.Add(breakFast.RunAsync());



            await Task.WhenAll(breakFastTasks);
            stopWatch.Stop();
            Console.WriteLine($"Transcurrieron {stopWatch.Elapsed}");
        }


    }
}
