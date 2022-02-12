using Benchmark.Benchmarks;
using BenchmarkDotNet.Running;
using System;

namespace Benchmark
{
	internal class Program
	{
		static void Main(string[] args)
		{
#if DEBUG


#else
			new BenchmarkSwitcher(typeof(BenchmarkBase).Assembly).Run(args);

			Console.WriteLine("Done!");
			Console.ReadKey();
#endif
		}
	}
}
