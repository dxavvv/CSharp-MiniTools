using System;
using System.Linq;

namespace VigenereCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Vigenère Cipher";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== VIGENÈRE CIPHER ===\n");
            Console.ResetColor();

            Console.Write("Enter text: ");
            string input = Console.ReadLine() ?? "";

            Console.Write("Enter key (letters only): ");
            string key = (Console.ReadLine() ?? "").ToUpper();

            if (string.IsNullOrWhiteSpace(key) || !key.All(char.IsLetter))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid key. Letters only!");
                Console.ResetColor();
                return;
            }

            Console.Write("Mode (E)ncode / (D)ecode: ");
            bool encode = (Console.ReadLine()?.Trim().ToUpper() ?? "E") == "E";

            string result = Process(input, key, encode);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nResult: {result}");
            Console.ResetColor();
        }

        // ⚙️ Core Vigenère Cipher logic
        static string Process(string text, string key, bool encode)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            key = key.ToUpper();
            int keyIndex = 0;
            int direction = encode ? 1 : -1;

            char Shift(char c)
            {
                if (!char.IsLetter(c)) return c;
                bool upper = char.IsUpper(c);
                char offset = upper ? 'A' : 'a';
                int shift = key[keyIndex++ % key.Length] - 'A';
                int newChar = (c - offset + direction * shift + 26) % 26 + offset;
                return (char)newChar;
            }

            return new string(text.Select(Shift).ToArray());
        }
    }
}
