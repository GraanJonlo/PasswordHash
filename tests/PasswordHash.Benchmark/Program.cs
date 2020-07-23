using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace PasswordHash.Benchmark
{
	[SimpleJob(RuntimeMoniker.Net472, baseline: true)]
	[SimpleJob(RuntimeMoniker.Net48)]
	[SimpleJob(RuntimeMoniker.NetCoreApp31)]
	[SimpleJob(RuntimeMoniker.NetCoreApp21)]
	public class PasswordHashPerformance
	{
		[Params(128u, 256u)]
		public uint HashSize;

		[Params(1000u, 10000u)]
		public uint Iterations;

		[Benchmark]
		public PasswordHash StaticFactory() => PasswordHash.From("Testing!234", Iterations, HashSize / 8);
	}

	public class Program
	{
		public static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run<PasswordHashPerformance>();
		}
	}
}
