using System;
using System.Globalization;

namespace UnitConverter
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Unit Converter";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== UNIT CONVERTER ===\n");
            Console.ResetColor();

            while (true)
            {
                Console.WriteLine("Select category:");
                Console.WriteLine("1 - Length");
                Console.WriteLine("2 - Mass");
                Console.WriteLine("3 - Temperature");
                Console.WriteLine("0 - Exit");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine()?.Trim();

                if (choice == "0")
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }

                Console.Write("Enter value to convert: ");
                if (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
                {
                    Console.WriteLine("Invalid number.\n");
                    continue;
                }

                double result = choice switch
                {
                    "1" => ConvertLength(value),
                    "2" => ConvertMass(value),
                    "3" => ConvertTemperature(value),
                    _ => double.NaN
                };

                if (double.IsNaN(result))
                    Console.WriteLine("Invalid selection.\n");
                else
                    Console.WriteLine($"\nConverted value: {result:F2}\n");
            }
        }

        static double ConvertLength(double value)
        {
            Console.WriteLine("Length conversions:");
            Console.WriteLine("1 - Kilometers → Miles");
            Console.WriteLine("2 - Miles → Kilometers");
            Console.Write("Choice: ");
            return Console.ReadLine()?.Trim() switch
            {
                "1" => value * 0.621371,   // km → mi
                "2" => value / 0.621371,   // mi → km
                _ => double.NaN
            };
        }

        static double ConvertMass(double value)
        {
            Console.WriteLine("Mass conversions:");
            Console.WriteLine("1 - Kilograms → Pounds");
            Console.WriteLine("2 - Pounds → Kilograms");
            Console.Write("Choice: ");
            return Console.ReadLine()?.Trim() switch
            {
                "1" => value * 2.20462,   // kg → lb
                "2" => value / 2.20462,   // lb → kg
                _ => double.NaN
            };
        }

        static double ConvertTemperature(double value)
        {
            Console.WriteLine("Temperature conversions:");
            Console.WriteLine("1 - Celsius → Fahrenheit");
            Console.WriteLine("2 - Fahrenheit → Celsius");
            Console.Write("Choice: ");
            return Console.ReadLine()?.Trim() switch
            {
                "1" => (value * 9 / 5) + 32,  // °C → °F
                "2" => (value - 32) * 5 / 9,  // °F → °C
                _ => double.NaN
            };
        }
    }
}
