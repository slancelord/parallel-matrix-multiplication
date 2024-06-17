using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Matrices.Benchmark
{
	[RPlotExporter]
	[IterationCount(3)]
	[WarmupCount(5)]
	public class MultiplyBenchmark
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

		public MultiplyBenchmark()
		{
			a = new Matrix(Size, Size);
			b = new Matrix(Size, Size);
			a.Rand();
			b.Rand();
			n = 50;
		}

		[Benchmark]
		public void Multiply()
		{
			Matrix.Multiply(a, b);
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
