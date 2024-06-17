using Matrices;
using System.Drawing;

namespace Matrices
{
	using System.Diagnostics;

	using BenchmarkDotNet.Running;
    using Matrices.Benchmark;
	using static Matrix;
	internal class Program
	{

		static void Main(string[] args)
		{
			Matrix a = new Matrix(10000, 10000);
			Matrix b = new Matrix(10000, 10000);
			a.Rand(); b.Rand();

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			Multiply(a, b);

			stopwatch.Stop();

			TimeSpan elapsedTime = stopwatch.Elapsed;
			Console.WriteLine($"Время выполнения функции Multiply: {elapsedTime}");

			//BenchmarkRunner.Run<MultiplyParallelTiledBenchmarkd>();
			//BenchmarkRunner.Run<MultiplyParallelBandedBenchmark>(); 
			//BenchmarkRunner.Run<MultiplyParallelBenchmark>();
			//BenchmarkRunner.Run<MultiplyBenchmark>();
			//BenchmarkRunner.Run<AllParallelMultiplyBenchmark>();
			//BenchmarkRunner.Run<BanchmarkTiledSize>();

		}

	}
}


