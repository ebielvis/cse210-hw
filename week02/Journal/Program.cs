using System;
using System.Collections.Generic;

namespace JournalApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var journal = new Journal();
            var promptGen = new PromptGenerator();
            bool running = true;

            Console.WriteLine("Welcome to the Journal Program!");

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("Menu:");
                Console.WriteLine("1) Write a new entry");
                Console.WriteLine("2) Display the journal");
                Console.WriteLine("3) Save the journal to a file");
                Console.WriteLine("4) Load the journal from a file");
                Console.WriteLine("5) Quit");
                Console.Write("Choose an option (1-5): ");

                string choice = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        WriteNewEntry(journal, promptGen);
                        break;
                    case "2":
                        journal.DisplayAll();
                        break;
                    case "3":
                        SaveJournal(journal);
                        break;
                    case "4":
                        LoadJournal(journal);
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number 1-5.");
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }

        static void WriteNewEntry(Journal journal, PromptGenerator promptGen)
        {
            string prompt = promptGen.GetRandomPrompt();
            Console.WriteLine("Prompt: " + prompt);
            Console.WriteLine("Enter your response. Press Enter on an empty line to finish:");

            var lines = new List<string>();
            while (true)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line)) break;
                lines.Add(line);
            }

            string entryText = string.Join(Environment.NewLine, lines);
            string dateText = DateTime.Now.ToShortDateString();

            var entry = new Entry(dateText, prompt, entryText);
            journal.AddEntry(entry);
            Console.WriteLine("Entry added.");
        }

        static void SaveJournal(Journal journal)
        {
            Console.Write("Enter filename to save (use .json for JSON): ");
            string filename = Console.ReadLine()?.Trim();
            try
            {
                journal.SaveToFile(filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save: " + ex.Message);
            }
        }

        static void LoadJournal(Journal journal)
        {
            Console.Write("Enter filename to load (use .json for JSON): ");
            string filename = Console.ReadLine()?.Trim();
            try
            {
                journal.LoadFromFile(filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load: " + ex.Message);
            }
        }
    }
}
