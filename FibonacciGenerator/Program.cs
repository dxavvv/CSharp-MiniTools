using System;
using System.Numerics;
using System.Linq;

namespace FibonacciGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Fibonacci Generator";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== FIBONACCI GENERATOR ===\n");
            Console.ResetColor();

            Console.Write("How many terms to generate? ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid positive integer.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("\nSelect output mode:");
            Console.WriteLine("1 - Single line");
            Console.WriteLine("2 - One number per line");
            Console.Write("Choice: ");
            string mode = Console.ReadLine()?.Trim() ?? "1";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nGenerating Fibonacci sequence...\n");
            Console.ResetColor();

            var sequence = GenerateFibonacci(n).ToArray();

            if (mode == "1")
                Console.WriteLine(string.Join(", ", sequence));
            else
                for (int i = 0; i < sequence.Length; i++)
                    Console.WriteLine($"{i + 1,2}: {sequence[i]}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nDone!");
            Console.ResetColor();
        }

        static IEnumerable<BigInteger> GenerateFibonacci(int count)
        {
            BigInteger a = 0, b = 1;
            for (int i = 0; i < count; i++)
            {
                yield return a;
                (a, b) = (b, a + b);
            }
        }
    }
}
