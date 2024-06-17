using System.Diagnostics;
using System.Text;


namespace Matrices
{
	internal class Matrix
	{
		private int rowsCount;
		private int columnsCount;
		private int[,] mass;

		public Matrix(int n, int m)
		{
			this.rowsCount = n;
			this.columnsCount = m;
			this.mass = new int[this.rowsCount, this.columnsCount];
		}

		public Matrix(int[,] mass)
		{
			this.mass = mass;
			this.rowsCount = mass.GetLength(0);
			this.columnsCount = mass.GetLength(1);
		}

		public int RowsCount
		{
			get { return this.rowsCount; }
			set { this.rowsCount = value; }
		}

		public int ColumnsCount
		{
			get { return this.columnsCount; }
			set { this.columnsCount = value; }
		}

		public int[,] Mass
		{
			get { return this.mass; }
			set { this.mass =  value; }
		}

		public int this [int i, int j]
		{
			get { return mass[i, j]; }
			set { this.mass[i, j] = value; }
		}

		public void Rand(int a = 0, int b = 10)
		{
			Random random = new();

			for (int i = 0; i < rowsCount; i++)
			{
				for (int j = 0; j < columnsCount; j++)
				{
					mass[i, j] = random.Next(a, b);
				}
			}
		}

		public static Matrix Multiply(Matrix matrix1, Matrix matrix2)
		{
			if (matrix1.columnsCount != matrix2.rowsCount)
				throw new ArgumentException("Матрицы должны быть согласованными для умножения");

			int rows = matrix1.rowsCount;
			int columns = matrix2.columnsCount;
			int inner = matrix1.columnsCount;

			Matrix result = new Matrix(rows, columns);

			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					for (int k = 0; k < inner; k++)
					{
						result.mass[i, j] += matrix1.mass[i, k] * matrix2.mass[k, j];
					}
				}
			}

			return result;
		}

		public static Matrix MultiplyParallelThreadsCount(Matrix matrix1, Matrix matrix2, int threadsCount)
		{
			if (matrix1.columnsCount != matrix2.rowsCount)
				throw new ArgumentException("Матрицы должны быть согласованными для умножения");

			int rows = matrix1.rowsCount;
			int columns = matrix2.columnsCount;
			int inner = matrix1.columnsCount;

			Matrix result = new Matrix(rows, columns);
			ParallelOptions options = new ParallelOptions();
			options.MaxDegreeOfParallelism = threadsCount;

			Parallel.For(0, rows, options, i =>
			{
				for (int j = 0; j < columns; j++)
				{
					for (int k = 0; k < inner; k++)
					{
						result.mass[i, j] += matrix1.mass[i, k] * matrix2.mass[k, j];
					}
				}
			});

			return result;
		}


		public static Matrix MultiplyParallel(Matrix matrix1, Matrix matrix2)
		{
			if (matrix1.columnsCount != matrix2.rowsCount)
				throw new ArgumentException("Матрицы должны быть согласованными для умножения");

			int rows = matrix1.rowsCount;
			int columns = matrix2.columnsCount;
			int inner = matrix1.columnsCount;

			Matrix result = new Matrix(rows, columns);

			Parallel.For(0, rows, i =>
			{
				for (int j = 0; j < columns; j++)
				{
					for (int k = 0; k < inner; k++)
					{
						result.mass[i, j] += matrix1.mass[i, k] * matrix2.mass[k, j];
					}
				}
			});

			return result;
		}

		public static Matrix MultiplyParallelBanded(Matrix matrix1, Matrix matrix2)
		{

			if (matrix1.columnsCount != matrix2.rowsCount)
				throw new ArgumentException("Матрицы должны быть согласованными для умножения");

			int rows = matrix1.rowsCount;
			int columns = matrix2.columnsCount;
			int inner = matrix1.columnsCount;

			Matrix result = new Matrix(rows, columns);

			Parallel.For(0, rows, i =>
			{
				Parallel.For(0, columns, j =>
				{
					for (int k = 0; k < inner; k++)
					{
						result.mass[i, j] += matrix1.mass[i, k] * matrix2.mass[k, j];
					}
				});
			});

			return result;
		}

		public static Matrix MultiplyParallelTiled(Matrix matrix1, Matrix matrix2, int tileSize)
		{

			if (matrix1.columnsCount != matrix2.rowsCount)
				throw new ArgumentException("Матрицы должны быть согласованными для умножения");

			int rows = matrix1.rowsCount;
			int columns = matrix2.columnsCount;
			int inner = matrix1.columnsCount;

			Matrix result = new Matrix(rows, columns);

			int numRowTiles = (rows + tileSize - 1) / tileSize;
			int numColTiles = (columns + tileSize - 1) / tileSize;
			int numInnerTiles = (inner + tileSize - 1) / tileSize;

			Parallel.For(0, numRowTiles * numColTiles * numInnerTiles, tileIndex =>
			{
				int tileRow = tileIndex / (numColTiles * numInnerTiles);
				int tileCol = (tileIndex / numInnerTiles) % numColTiles;
				int tileInner = tileIndex % numInnerTiles;

				int tileRowStart = tileRow * tileSize;
				int tileRowEnd = Math.Min(tileRowStart + tileSize, rows);
				int tileColStart = tileCol * tileSize;
				int tileColEnd = Math.Min(tileColStart + tileSize, columns);
				int tileInnerStart = tileInner * tileSize;
				int tileInnerEnd = Math.Min(tileInnerStart + tileSize, inner);

				for (int i = tileRowStart; i < tileRowEnd; i++)
				{
					for (int j = tileColStart; j < tileColEnd; j++)
					{
						for (int k = tileInnerStart; k < tileInnerEnd; k++)
						{
							result.mass[i, j] += matrix1.mass[i, k] * matrix2.mass[k, j];
						}
					}
				}
			});

			return result;
		}

		public override string ToString()
		{
			StringBuilder sb = new();

			for (int i = 0; i < rowsCount; i++)
			{
				for (int j = 0; j < columnsCount; j++)
				{
					sb.Append(mass[i, j] + " ");
				}

				sb.AppendLine();
			}

			return sb.ToString();
		}
	}
}
