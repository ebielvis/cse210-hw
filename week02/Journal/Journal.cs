using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JournalApp
{
    public class Journal
    {
        private List<Entry> _entries = new List<Entry>();
        private const string Separator = "~|~";

        public void AddEntry(Entry newEntry)
        {
            _entries.Add(newEntry);
        }

        public void DisplayAll()
        {
            if (_entries.Count == 0)
            {
                Console.WriteLine("No entries in the journal.");
                return;
            }

            foreach (var e in _entries)
            {
                e.Display();
            }
        }

    
        public void SaveToFile(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentException("Filename is required.");

            if (Path.GetExtension(filename).Equals(".json", StringComparison.OrdinalIgnoreCase))
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(_entries, options);
                File.WriteAllText(filename, json);
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(filename))
                {
                    foreach (var e in _entries)
                    {
                        string date = (e.Date ?? string.Empty).Replace(Separator, " ");
                        string prompt = (e.PromptText ?? string.Empty).Replace(Separator, " ");
                        string text = (e.EntryText ?? string.Empty).Replace(Separator, " ");

                        writer.WriteLine($"{date}{Separator}{prompt}{Separator}{text}");
                    }
                }
            }

            Console.WriteLine($"Journal saved to {filename}");
        }

        public void LoadFromFile(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename)) throw new ArgumentException("Filename is required.");
            if (!File.Exists(filename))
            {
                Console.WriteLine($"File not found: {filename}");
                return;
            }

            if (Path.GetExtension(filename).Equals(".json", StringComparison.OrdinalIgnoreCase))
            {
                string json = File.ReadAllText(filename);
                try
                {
                    var entries = JsonSerializer.Deserialize<List<Entry>>(json);
                    _entries = entries ?? new List<Entry>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to parse JSON: " + ex.Message);
                    return;
                }
            }
            else
            {
                string[] lines = File.ReadAllLines(filename);
                var loaded = new List<Entry>();
                foreach (var line in lines)
                {
                    var parts = line.Split(new string[] { Separator }, StringSplitOptions.None);
                    if (parts.Length >= 3)
                    {
                        string date = parts[0];
                        string prompt = parts[1];
                        string text = parts.Length == 3 ? parts[2] : string.Join(Separator, parts, 2, parts.Length - 2);
                        loaded.Add(new Entry(date, prompt, text));
                    }
                }

                _entries = loaded;
            }

            Console.WriteLine($"Journal loaded from {filename}");
        }
    }
}
