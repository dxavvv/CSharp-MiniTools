using System;
using System.Linq;

namespace CaesarCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Caesar Cipher";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== CAESAR CIPHER ===\n");
            Console.ResetColor();

            Console.Write("Enter text: ");
            string input = Console.ReadLine() ?? "";

            Console.Write("Enter shift (e.g. 3): ");
            if (!int.TryParse(Console.ReadLine(), out int shift))
            {
                Console.WriteLine("Invalid shift. Exiting.");
                return;
            }

            Console.Write("Mode (E)ncode / (D)ecode: ");
            bool encode = (Console.ReadLine()?.Trim().ToUpper() ?? "E") == "E";

            string result = Process(input, shift, encode);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nResult: {result}");
            Console.ResetColor();
        }

        static string Process(string text, int shift, bool encode)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            shift = ((encode ? shift : -shift) % 26 + 26) % 26;

            return new string(text.Select(c =>
            {
                if (!char.IsLetter(c)) return c;
                char offset = char.IsUpper(c) ? 'A' : 'a';
                return (char)(offset + ((c - offset + shift) % 26));
            }).ToArray());
        }
    }
}
