using System;

namespace JournalApp
{
    public class Entry
    {
        public string Date { get; set; }
        public string PromptText { get; set; }
        public string EntryText { get; set; }

        public Entry() { }

        public Entry(string date, string prompt, string text)
        {
            Date = date;
            PromptText = prompt;
            EntryText = text;
        }

        // Display the entry to the console. Keeping this here enforces encapsulation:
        // other classes don't need to know how an entry is formatted.
        public void Display()
        {
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Prompt: {PromptText}");
            Console.WriteLine("Entry:");
            Console.WriteLine(EntryText);
            Console.WriteLine(new string('-', 40));
        }
    }
}
