using BenchmarkDotNet.Running;

namespace OnlySpans
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchi>();
        }
    }
}
