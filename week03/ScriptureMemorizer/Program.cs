using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    // Represents a single word in the scripture
    class Word
    {
        private string text;
        private bool hidden;

        public Word(string text)
        {
            this.text = text;
            hidden = false;
        }

        public void Hide()
        {
            hidden = true;
        }

        public bool IsHidden()
        {
            return hidden;
        }

        public string Display()
        {
            if (hidden)
                return new string('_', text.Length);
            else
                return text;
        }
    }

    // Represents the scripture reference
    class Reference
    {
        private string book;
        private int chapter;
        private int verseStart;
        private int? verseEnd;

        // Constructor for single verse
        public Reference(string book, int chapter, int verse)
        {
            this.book = book;
            this.chapter = chapter;
            this.verseStart = verse;
            this.verseEnd = null;
        }

        // Constructor for verse range
        public Reference(string book, int chapter, int verseStart, int verseEnd)
        {
            this.book = book;
            this.chapter = chapter;
            this.verseStart = verseStart;
            this.verseEnd = verseEnd;
        }

        public override string ToString()
        {
            if (verseEnd.HasValue)
                return $"{book} {chapter}:{verseStart}-{verseEnd}";
            else
                return $"{book} {chapter}:{verseStart}";
        }
    }

    // Represents a scripture
    class Scripture
    {
        private Reference reference;
        private List<Word> words;

        public Scripture(Reference reference, string text)
        {
            this.reference = reference;
            words = text.Split(' ').Select(w => new Word(w)).ToList();
        }

        public void Display()
        {
            Console.WriteLine(reference.ToString());
            Console.WriteLine(string.Join(" ", words.Select(w => w.Display())));
        }

        public void HideRandomWords(int count)
        {
            Random rnd = new Random();
            
            // Get the list of words that are not yet hidden
            var visibleWords = words.Where(w => !w.IsHidden()).ToList();

            // If there are fewer visible words than the requested count, adjust
            int wordsToHide = Math.Min(count, visibleWords.Count);

            for (int i = 0; i < wordsToHide; i++)
            {
                // Pick a random visible word
                int index = rnd.Next(visibleWords.Count);
                visibleWords[index].Hide();

                // Remove it from the list so it won't be picked again in this round
                visibleWords.RemoveAt(index);
            }
        }


        public bool AllWordsHidden()
        {
            return words.All(w => w.IsHidden());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Example scripture: John 3:16
            Reference reference = new Reference("John", 3, 16);
            Scripture scripture = new Scripture(reference, "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.");

            while (true)
            {
                Console.Clear();
                scripture.Display();

                if (scripture.AllWordsHidden())
                {
                    Console.WriteLine("\nAll words are hidden. Well done!");
                    break;
                }

                Console.WriteLine("\nPress Enter to hide some words or type 'quit' to exit.");
                string input = Console.ReadLine();
                if (input.ToLower() == "quit") break;

                // Hide 3 random words each iteration
                scripture.HideRandomWords(3);
            }
        }
    }
}
