using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrices.Benchmark
{
	[RPlotExporter]
	[ThreadingDiagnoser]
	[IterationCount(3)]
	[WarmupCount(5)]
	public class MultiplyParallelBandedBenchmark
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

		public MultiplyParallelBandedBenchmark()
		{
			a = new Matrix(Size, Size);
			b = new Matrix(Size, Size);
			a.Rand();
			b.Rand();
		}

		[Benchmark]
		public void MultiplyParallelBanded()
		{
			Matrix.MultiplyParallelBanded(a, b);
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
