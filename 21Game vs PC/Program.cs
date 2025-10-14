using System;

namespace Game21
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Game 21 - Easy/Hard AI";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== GAME 21 ===\n");
            Console.ResetColor();

            int total = 0;
            const int maxTake = 3;
            bool playerTurn = true;
            bool hardMode = false;

            // Mode selection
            Console.WriteLine("Select difficulty:");
            Console.WriteLine("1 - Easy (AI plays randomly)");
            Console.WriteLine("2 - Hard (AI starts and plays perfectly)");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine();

            Random rand = new Random();

            if (choice == "2")
            {
                hardMode = true;
                playerTurn = false; // AI starts first
            }

            Console.WriteLine("\nRules: Players take turns adding 1-3 to the total. Reach 21 to win.\n");

            while (total < 21)
            {
                Console.WriteLine($"Current total: {total}");
                int move = 0;

                if (playerTurn)
                {
                    // Player move
                    Console.Write("Your turn. Enter a number (1-3): ");
                    string input = Console.ReadLine();
                    if (!int.TryParse(input, out move) || move < 1 || move > maxTake)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Try again.\n");
                        Console.ResetColor();
                        continue;
                    }
                }
                else
                {
                    // AI move
                    if (hardMode)
                    {
                        // AI starts at 1, potem daje sumę = 1 mod 4
                        if (total == 0)
                        {
                            move = 1; // first move
                        }
                        else
                        {
                            move = (4 + 1) - (total % 4); // ensure (total + move) % 4 == 1
                            if (move < 1) move = 1;
                            if (move > maxTake) move = maxTake;
                        }
                    }
                    else
                    {
                        // Easy mode: random
                        move = rand.Next(1, maxTake + 1);
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"AI chooses: {move}");
                    Console.ResetColor();
                }

                total += move;

                if (total >= 21)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(playerTurn ? "\nYou win!" : "\nAI wins!");
                    Console.WriteLine($"Final total: {total}\n");
                    Console.ResetColor();
                    break;
                }

                playerTurn = !playerTurn;
            }

            Console.WriteLine("Game over. Thanks for playing!");
        }
    }
}
