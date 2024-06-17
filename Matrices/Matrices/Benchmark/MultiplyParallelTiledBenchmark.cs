using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using System.Collections.Generic;

namespace Matrices
{
	[RPlotExporter]
	[ThreadingDiagnoser]
	[IterationCount(3)]
	[WarmupCount(5)]
	public class MultiplyParallelTiledBenchmarkd
	{
		private Matrix a;
		private Matrix b;


		[ParamsSource(nameof(GetMatrixSizes))]
		public int Size { get; set; }

		public int N { get; set; }

		[GlobalSetup]
		public void Setup()
		{
			a = new Matrix(Size, Size);
			b = new Matrix(Size, Size);
			a.Rand();
			b.Rand();
			N = 50;
		}

		public MultiplyParallelTiledBenchmarkd()
		{
			a = new Matrix(Size, Size);
			b = new Matrix(Size, Size);
			a.Rand();
			b.Rand();
			N = 50;
		}

		[Benchmark]
		public void MultiplyParallelTiled()
		{
			Matrix.MultiplyParallelTiled(a, b, N);
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