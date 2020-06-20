using System.Threading;
using BenchmarkDotNet.Attributes;

namespace benchmark_overview_samples
{
    public class IntroParams
    {
        [Params(100, 200)]
        public int A { get; set; }

        [Params(10, 20)]
        public int B { get; set; }

        [Benchmark]
        public void Benchmark()
        {
            Thread.Sleep(A + B + 5);
        }
    }
}