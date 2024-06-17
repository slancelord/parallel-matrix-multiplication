using BenchmarkDotNet.Attributes;

namespace Matrices.Benchmark
{
	[RPlotExporter]
	[ThreadingDiagnoser]
	[IterationCount(3)]
	[WarmupCount(5)]
	public class BanchmarkTiledSize
	{
		private Matrix a;
		private Matrix b;


		[ParamsSource(nameof(GetMatrixSizes))]
		public int Size { get; set; }

		[ParamsSource(nameof(GetMatrixTiledSize))]
		public int n;

		[GlobalSetup]
		public void Setup()
		{
			a = new Matrix(Size, Size);
			b = new Matrix(Size, Size);
			a.Rand();
			b.Rand();
		}

		public BanchmarkTiledSize()
		{
			a = new Matrix(250, 250);
			b = new Matrix(250, 250);
			a.Rand();
			b.Rand();
			n = 50;
		}

		[Benchmark]
		public void MultiplyParallelTiled()
		{
			Matrix.MultiplyParallelTiled(a, b, n);
		}

		public static IEnumerable<int> GetMatrixSizes()
		{
			yield return 2000;
		}

		public static IEnumerable<int> GetMatrixTiledSize()
		{
			yield return 25;
			yield return 50;
			yield return 75;
			yield return 100;
			yield return 150;
			yield return 300;
		}
	}
}
