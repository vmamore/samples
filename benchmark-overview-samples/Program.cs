using System;
using BenchmarkDotNet.Running;

namespace benchmark_overview_samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Md5VsSha256>();
        }
    }
}
