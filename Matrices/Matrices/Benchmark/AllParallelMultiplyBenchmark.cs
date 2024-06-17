using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using System.Collections.Generic;

namespace Matrices
{
	[RPlotExporter]
	[ThreadingDiagnoser]
	[IterationCount(3)]
	[WarmupCount(5)]
	public class AllParallelMultiplyBenchmark
	{
		private Matrix a;
		private Matrix b;
		private int n;

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

		public AllParallelMultiplyBenchmark()
		{
			a = new Matrix(250, 250);
			b = new Matrix(250, 250);
			a.Rand();
			b.Rand();
			n = 50;
		}

		[Benchmark(Baseline = true)]
		public void Multiply()
		{
			Matrix.Multiply(a, b);
		}

		[Benchmark]
		public void MultiplyParallel()
		{
			Matrix.MultiplyParallel(a, b);
		}

		[Benchmark]
		public void MultiplyParallelBanded()
		{
			Matrix.MultiplyParallelBanded(a, b);
		}

		[Benchmark]
		public void MultiplyParallelTiled()
		{
			Matrix.MultiplyParallelTiled(a, b, n);
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