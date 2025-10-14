using System;

namespace Game21
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game 21";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== GAME 21 ===\n");
            Console.ResetColor();

            int total = 0;
            int currentPlayer = 1;

            Console.WriteLine("Rules: Players take turns adding 1, 2, or 3 to the total. Reach 21 to win!\n");

            while (total < 21)
            {
                Console.WriteLine($"Current total: {total}");
                Console.Write($"Player {currentPlayer}, enter a number (1-3): ");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int number) || number < 1 || number > 3)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Enter 1, 2, or 3.\n");
                    Console.ResetColor();
                    continue;
                }

                total += number;

                if (total >= 21)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nPlayer {currentPlayer} wins! Total reached {total}.\n");
                    Console.ResetColor();
                    break;
                }

                // Switch player
                currentPlayer = currentPlayer == 1 ? 2 : 1;
            }

            Console.WriteLine("Game over. Thanks for playing!");
        }
    }
}
