using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Simple Console Calculator";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== SIMPLE CALCULATOR ===\n");
            Console.ResetColor();

            while (true)
            {
                Console.Write("Enter first number (or 'q' to quit): ");
                string input1 = Console.ReadLine();
                if (input1?.ToLower() == "q") break;

                Console.Write("Enter operator (+, -, *, /): ");
                string op = Console.ReadLine();

                Console.Write("Enter second number: ");
                string input2 = Console.ReadLine();

                if (!double.TryParse(input1, out double num1) || !double.TryParse(input2, out double num2))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid numbers. Try again.\n");
                    Console.ResetColor();
                    continue;
                }

                double result = 0;
                bool validOp = true;

                switch (op)
                {
                    case "+": result = num1 + num2; break;
                    case "-": result = num1 - num2; break;
                    case "*": result = num1 * num2; break;
                    case "/":
                        if (num2 == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Cannot divide by zero.\n");
                            Console.ResetColor();
                            validOp = false;
                        }
                        else
                        {
                            result = num1 / num2;
                        }
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid operator. Use +, -, *, /.\n");
                        Console.ResetColor();
                        validOp = false;
                        break;
                }

                if (validOp)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Result: {result}\n");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }
}
