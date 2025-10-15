using System;
using System.Linq;

namespace MatrixCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Matrix Calculator";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== MATRIX CALCULATOR ===\n");
            Console.ResetColor();

            Console.WriteLine("Select operation:");
            Console.WriteLine("1 - Add matrices");
            Console.WriteLine("2 - Multiply matrices");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine();

            Console.WriteLine("\nMatrix A:");
            var matrixA = ReadMatrix();

            Console.WriteLine("\nMatrix B:");
            var matrixB = ReadMatrix();

            int[,] result;

            switch (choice)
            {
                case "1":
                    if (matrixA.GetLength(0) != matrixB.GetLength(0) || matrixA.GetLength(1) != matrixB.GetLength(1))
                    {
                        Console.WriteLine("Error: Matrices must have same dimensions for addition.");
                        return;
                    }
                    result = AddMatrices(matrixA, matrixB);
                    break;
                case "2":
                    if (matrixA.GetLength(1) != matrixB.GetLength(0))
                    {
                        Console.WriteLine("Error: Columns of A must equal rows of B for multiplication.");
                        return;
                    }
                    result = MultiplyMatrices(matrixA, matrixB);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nResult:");
            PrintMatrix(result);
            Console.ResetColor();
        }

        static int[,] ReadMatrix()
        {
            Console.Write("Enter number of rows: ");
            int rows = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter number of columns: ");
            int cols = int.Parse(Console.ReadLine() ?? "0");

            int[,] matrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine($"Enter row {i + 1} values separated by spaces:");
                var values = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse).ToArray();
                if (values == null || values.Length != cols)
                {
                    Console.WriteLine("Invalid row input. Try again.");
                    i--;
                    continue;
                }
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = values[j];
            }

            return matrix;
        }

        static int[,] AddMatrices(int[,] a, int[,] b)
        {
            int rows = a.GetLength(0), cols = a.GetLength(1);
            int[,] result = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = a[i, j] + b[i, j];
            return result;
        }

        static int[,] MultiplyMatrices(int[,] a, int[,] b)
        {
            int rows = a.GetLength(0), cols = b.GetLength(1), n = a.GetLength(1);
            int[,] result = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    for (int k = 0; k < n; k++)
                        result[i, j] += a[i, k] * b[k, j];
            return result;
        }

        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0), cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    Console.Write(matrix[i, j] + "\t");
                Console.WriteLine();
            }
        }
    }
}
