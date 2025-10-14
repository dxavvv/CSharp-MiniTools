using System;
using System.Text.RegularExpressions;

namespace PasswordStrengthChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Password Strength Checker";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== PASSWORD STRENGTH CHECKER ===\n");
            Console.ResetColor();

            Console.Write("Enter your password: ");
            string password = ReadPassword();

            var result = EvaluatePassword(password);

            Console.WriteLine();
            Console.WriteLine("Password Analysis:");
            Console.WriteLine("------------------");
            foreach (var feedback in result.Feedback)
            {
                if (feedback.StartsWith("✅")) Console.ForegroundColor = ConsoleColor.Green;
                else if (feedback.StartsWith("⚠️")) Console.ForegroundColor = ConsoleColor.Yellow;
                else Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(feedback);
            }

            Console.ResetColor();
            Console.WriteLine("\nScore: " + result.Score + "/5");

            // Visual strength bar
            Console.Write("Strength: ");
            DrawStrengthBar(result.Score);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nOverall: {result.StrengthDescription}");
            Console.ResetColor();
        }

        /// <summary>
        /// Reads password from console input, masking with '*'.
        /// </summary>
        static string ReadPassword()
        {
            string pass = "";
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass.Substring(0, pass.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    pass += keyInfo.KeyChar;
                    Console.Write("*");
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine();
            return pass;
        }

        /// <summary>
        /// Evaluates password strength and returns score with feedback.
        /// </summary>
        static (int Score, string[] Feedback, string StrengthDescription) EvaluatePassword(string password)
        {
            var feedback = new System.Collections.Generic.List<string>();
            int score = 0;

            // Length check
            if (password.Length >= 12)
            {
                score++;
                feedback.Add("✅ Length is good (12+ characters)");
            }
            else if (password.Length >= 8)
            {
                score++;
                feedback.Add("⚠️ Length is moderate (8-11 characters)");
            }
            else
            {
                feedback.Add("❌ Too short (less than 8 characters)");
            }

            // Uppercase
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
                feedback.Add("✅ Contains uppercase letters");
            }
            else
            {
                feedback.Add("❌ Missing uppercase letters");
            }

            // Lowercase
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
                feedback.Add("✅ Contains lowercase letters");
            }
            else
            {
                feedback.Add("❌ Missing lowercase letters");
            }

            // Digits
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
                feedback.Add("✅ Contains numbers");
            }
            else
            {
                feedback.Add("❌ Missing numbers");
            }

            // Special characters
            if (Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                score++;
                feedback.Add("✅ Contains special characters");
            }
            else
            {
                feedback.Add("❌ Missing special characters");
            }

            // Cap the score at 5
            if (score > 5) score = 5;

            string desc = score switch
            {
                <= 2 => "Weak 😬",
                3 => "Moderate 😐",
                4 => "Strong 💪",
                5 => "Very Strong 🔥",
                _ => "Unknown"
            };

            return (score, feedback.ToArray(), desc);
        }

        /// <summary>
        /// Draws a colored progress bar based on strength score.
        /// </summary>
        static void DrawStrengthBar(int score)
        {
            ConsoleColor[] colors = {
                ConsoleColor.Red,
                ConsoleColor.Yellow,
                ConsoleColor.DarkYellow,
                ConsoleColor.Green,
                ConsoleColor.Cyan
            };

            ConsoleColor color = colors[Math.Clamp(score - 1, 0, colors.Length - 1)];
            Console.ForegroundColor = color;

            int total = 5;
            for (int i = 0; i < total; i++)
            {
                Console.Write(i < score ? "█" : "░");
            }

            Console.ResetColor();
        }
    }
}
