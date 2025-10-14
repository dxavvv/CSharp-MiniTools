using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TextAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Text Analyzer";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== TEXT ANALYZER ===\n");
            Console.ResetColor();

            string text = "";

            Console.WriteLine("Choose input method:");
            Console.WriteLine("1 - Enter text manually");
            Console.WriteLine("2 - Load from file");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine();

            if (choice == "2")
            {
                Console.Write("Enter file path: ");
                string path = Console.ReadLine();
                try
                {
                    text = File.ReadAllText(path);
                    Console.WriteLine("\nFile loaded successfully.\n");
                }
                catch
                {
                    Console.WriteLine("Failed to read file. Exiting.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Enter text (end with empty line):");
                string line;
                while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                {
                    text += line + " ";
                }
            }

            AnalyzeText(text);
        }

        static void AnalyzeText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("No text provided.");
                return;
            }

            int charCount = text.Length;
            int wordCount = Regex.Matches(text, @"\b\w+\b").Count;
            int sentenceCount = Regex.Matches(text, @"[.!?]").Count;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== Analysis Result ===\n");
            Console.ResetColor();

            Console.WriteLine($"Characters: {charCount}");
            Console.WriteLine($"Words: {wordCount}");
            Console.WriteLine($"Sentences: {sentenceCount}");

            // Most frequent words
            var words = Regex.Matches(text.ToLower(), @"\b\w+\b")
                             .Cast<Match>()
                             .Select(m => m.Value)
                             .Where(w => w.Length > 2) // ignore very short words
                             .ToList();

            var freq = words.GroupBy(w => w)
                            .OrderByDescending(g => g.Count())
                            .Take(10)
                            .Select(g => new { Word = g.Key, Count = g.Count() });

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nMost frequent words:");
            Console.ResetColor();
            foreach (var w in freq)
            {
                Console.WriteLine($"{w.Word} - {w.Count} times");
            }

            // Optionally save to file
            Console.Write("\nSave analysis to file? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                Console.Write("Enter file path to save: ");
                string savePath = Console.ReadLine();
                try
                {
                    using (StreamWriter sw = new StreamWriter(savePath))
                    {
                        sw.WriteLine("=== Analysis Result ===");
                        sw.WriteLine($"Characters: {charCount}");
                        sw.WriteLine($"Words: {wordCount}");
                        sw.WriteLine($"Sentences: {sentenceCount}");
                        sw.WriteLine("\nMost frequent words:");
                        foreach (var w in freq)
                        {
                            sw.WriteLine($"{w.Word} - {w.Count} times");
                        }
                    }
                    Console.WriteLine("Analysis saved successfully!");
                }
                catch
                {
                    Console.WriteLine("Failed to save file.");
                }
            }
        }
    }
}
