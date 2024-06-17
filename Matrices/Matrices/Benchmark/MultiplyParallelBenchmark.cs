using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using System.Collections.Generic;

namespace Matrices
{
	[RPlotExporter]
	[ThreadingDiagnoser]
	[IterationCount(3)]
	[WarmupCount(5)]
	public class MultiplyParallelBenchmark
	{
		private Matrix a;
		private Matrix b;

		[ParamsSource(nameof(GetMatrixSizes))]
		public int Size { get; set; }

		[GlobalSetup]
		public void Setup()
		{
			a = new Matrix(Size, Size);
			b = new Matrix(Size, Size);
			a.Rand();
			b.Rand();
		}

		public MultiplyParallelBenchmark()
		{
			a = new Matrix(Size, Size);
			b = new Matrix(Size, Size);
			a.Rand();
			b.Rand();
		}

		[Benchmark]
		public void MultiplyParallel()
		{
			Matrix.MultiplyParallel(a, b);
		}

		public static IEnumerable<int> GetMatrixSizes()
		{
			yield return 250;
			yield return 500;
			yield return 1000;
			yield return 1500;
			yield return 2000;
		}
	}

}