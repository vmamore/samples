using System.Threading;
using BenchmarkDotNet.Attributes;

namespace benchmark_overview_samples
{
    public class Sleeps
    {
        [Benchmark]
        public void Time50() => Thread.Sleep(50);
        
        [Benchmark(Baseline=true)]
        public void Time100() => Thread.Sleep(100);
        
        [Benchmark]
        public void Time150() => Thread.Sleep(150);
    }
}