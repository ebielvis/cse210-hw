// W05 Project: Mindfulness Program
//
//  - Menu system for choosing activities
//  - Base Activity class with shared start/end messages and helpers
//  - BreathingActivity, ReflectionActivity, ListingActivity derived from Activity
//  - Animations: spinner and countdown shown during pauses
//  - Activities prompt for duration (seconds), prepare, run, and conclude with end message
//  - Reflection: shows prompts and questions, pauses with spinner between questions
//  - Listing: gives a prompt, countdown to start, collects user entries until time expires
//
// Extra (exceeding requirements) - described here and also in comments:
//  1) Non-repeating selection within a session for prompts/questions (until all are used).
//  2) Simple logging: each completed activity appends a line to "mindfulness_log.txt" with timestamp,
//     activity name and duration (shows how the student exceeded requirements).
//


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        ShowMenu();
    }

    static void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Mindfulness Program ===");
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Enter choice (1-4): ");
            var choice = Console.ReadLine()?.Trim();

            Activity activity = null;
            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    Console.WriteLine("Goodbye. Stay mindful!");
                    Thread.Sleep(800);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press Enter to try again.");
                    Console.ReadLine();
                    continue;
            }

            activity.Run();
            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }
    }
}

/// <summary>
/// Base class for activities: contains shared attributes and behaviors (start/end messages, helpers).
/// </summary>
abstract class Activity
{
    private readonly string _name;
    private readonly string _description;
    protected int Duration { get; private set; } = 0; // in seconds
    private static readonly string LogFile = "mindfulness_log.txt";

    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    /// <summary>
    /// Template method: start -> run activity -> finish
    /// </summary>
    public void Run()
    {
        Console.Clear();
        DisplayStart();
        RunActivity(); // implemented by derived classes
        DisplayEnd();
        AppendLog();
    }

    protected abstract void RunActivity();

    #region Start/End and helpers

    private void DisplayStart()
    {
        Console.WriteLine($"--- {_name} ---\n");
        Console.WriteLine(_description + "\n");
        Duration = PromptForDuration();
        Console.WriteLine("\nGet ready...");
        Spinner(3);
        Console.WriteLine();
    }

    private void DisplayEnd()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!");
        Spinner(2);
        Console.WriteLine($"\nYou completed the {_name} for {Duration} seconds.");
        Spinner(2);
        Console.WriteLine();
    }

    private int PromptForDuration()
    {
        while (true)
        {
            Console.Write("How long, in seconds, would you like to do this activity? ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int seconds) && seconds > 0)
            {
                return seconds;
            }
            Console.WriteLine("Please enter a positive integer for seconds.");
        }
    }

    /// <summary>
    /// Spinner animation for the given number of seconds.
    /// </summary>
    protected void Spinner(int seconds)
    {
        char[] seq = new[] { '|', '/', '-', '\\' };
        int delay = 250; // ms
        int total = Math.Max(1, (seconds * 1000) / delay);
        for (int i = 0; i < total; i++)
        {
            Console.Write(seq[i % seq.Length]);
            Thread.Sleep(delay);
            Console.Write("\b \b"); // erase
        }
    }

    /// <summary>
    /// Countdown that prints numbers and erases them cleanly.
    /// </summary>
    protected void Countdown(int seconds)
    {
        int prevLen = 0;
        for (int i = seconds; i >= 1; i--)
        {
            string s = i.ToString();
            Console.Write(s);
            Thread.Sleep(1000);
            // erase exactly the characters we printed
            Console.Write(new string('\b', s.Length) + new string(' ', s.Length) + new string('\b', s.Length));
            prevLen = s.Length;
        }
    }

    /// <summary>
    /// Appends a simple log entry to a file for this session. (Extra feature)
    /// </summary>
    private void AppendLog()
    {
        try
        {
            string line = $"{DateTime.Now:u}\t{_name}\t{Duration} seconds";
            File.AppendAllText(LogFile, line + Environment.NewLine);
        }
        catch
        {
            // Silently ignore logging errors (won't crash main functionality)
        }
    }

    #endregion
}

/// <summary>
/// BreathingActivity: alternate breathe in/out until duration elapses.
/// </summary>
class BreathingActivity : Activity
{
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    { }

    protected override void RunActivity()
    {
        // We'll use a simple 4-second inhale / 4-second exhale pattern, adjusting the last partial cycle.
        int inhale = 4;
        int exhale = 4;
        var endTime = DateTime.Now.AddSeconds(Duration);

        while (DateTime.Now < endTime)
        {
            // Breathe in
            Console.WriteLine("\nBreathe in...");
            int remaining = Math.Max(0, (int)(endTime - DateTime.Now).TotalSeconds);
            int toWait = Math.Min(inhale, remaining);
            if (toWait > 0) Countdown(toWait);
            if (DateTime.Now >= endTime) break;

            // Breathe out
            Console.WriteLine("Breathe out...");
            remaining = Math.Max(0, (int)(endTime - DateTime.Now).TotalSeconds);
            toWait = Math.Min(exhale, remaining);
            if (toWait > 0) Countdown(toWait);
        }
    }
}

/// <summary>
/// ReflectionActivity: show a random prompt, then random questions until time elapses.
/// - Extra: questions and prompts are shown without repeating until all have been used once in the session.
/// </summary>
class ReflectionActivity : Activity
{
    private readonly List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private readonly List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    private List<int> _promptPool;
    private List<int> _questionPool;
    private static readonly Random _rand = new Random();

    public ReflectionActivity() : base(
        "Reflection Activity",
        "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
        // initialize pools
        _promptPool = Enumerable.Range(0, _prompts.Count).ToList();
        _questionPool = Enumerable.Range(0, _questions.Count).ToList();
        Shuffle(_promptPool);
        Shuffle(_questionPool);
    }

    protected override void RunActivity()
    {
        // show a prompt (non-repeating until all used)
        int promptIndex = PopFromPool(_promptPool, () => ShuffleAndRefill(_promptPool, _prompts.Count));
        Console.WriteLine("\nPrompt:");
        Console.WriteLine(_prompts[promptIndex]);
        Console.WriteLine("\nWhen you're ready, reflect on the following questions:");
        Spinner(3);

        var endTime = DateTime.Now.AddSeconds(Duration);

        while (DateTime.Now < endTime)
        {
            int qIndex = PopFromPool(_questionPool, () => ShuffleAndRefill(_questionPool, _questions.Count));
            Console.WriteLine("\n> " + _questions[qIndex]);
            // pause with spinner to let the user reflect for a short time
            Spinner(5);
        }
    }

    // Helpers for non-repeating selection
    private int PopFromPool(List<int> pool, Action refillIfEmpty)
    {
        if (pool.Count == 0) refillIfEmpty();
        int index = pool[0];
        pool.RemoveAt(0);
        return index;
    }

    private void ShuffleAndRefill(List<int> pool, int count)
    {
        pool.Clear();
        pool.AddRange(Enumerable.Range(0, count));
        Shuffle(pool);
    }

    private void Shuffle<T>(List<T> list)
    {
        // Fisher-Yates
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = _rand.Next(i + 1);
            var tmp = list[j];
            list[j] = list[i];
            list[i] = tmp;
        }
    }
}

/// <summary>
/// ListingActivity: show a prompt, countdown to start, then collect user-listed items until time expires.
/// </summary>
class ListingActivity : Activity
{
    private readonly List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private List<int> _promptPool;
    private static readonly Random _rand = new Random();

    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _promptPool = Enumerable.Range(0, _prompts.Count).ToList();
        Shuffle(_promptPool);
    }

    protected override void RunActivity()
    {
        int pIndex = PopFromPool(_promptPool, () => ShuffleAndRefill(_promptPool, _prompts.Count));
        Console.WriteLine("\nPrompt:");
        Console.WriteLine(_prompts[pIndex]);

        Console.WriteLine("\nYou will have a few seconds to think, then begin listing items. Press Enter after each item.");
        Console.Write("Starting in: ");
        Countdown(5);
        Console.WriteLine();

        var endTime = DateTime.Now.AddSeconds(Duration);
        var items = new List<string>();

        Console.WriteLine("Start listing (press Enter after each item):");

        // Use Task.Run to allow ReadLine with timeout
        while (DateTime.Now < endTime)
        {
            int remainingMs = (int)(endTime - DateTime.Now).TotalMilliseconds;
            if (remainingMs <= 0) break;

            var readTask = Task.Run(() => Console.ReadLine());
            bool finishedInTime = readTask.Wait(remainingMs);
            if (!finishedInTime)
            {
                // time expired while waiting for input
                Console.WriteLine("\nTime's up!");
                break;
            }

            string entry = readTask.Result;
            if (!string.IsNullOrWhiteSpace(entry))
            {
                items.Add(entry.Trim());
            }
            // continue until time is up
        }

        Console.WriteLine($"\nYou listed {items.Count} item(s).");
        if (items.Count > 0)
        {
            Console.WriteLine("Your entries:");
            foreach (var it in items)
            {
                Console.WriteLine("- " + it);
            }
        }
    }

    // Helper methods for prompt pool
    private int PopFromPool(List<int> pool, Action refillIfEmpty)
    {
        if (pool.Count == 0) refillIfEmpty();
        int index = pool[0];
        pool.RemoveAt(0);
        return index;
    }

    private void ShuffleAndRefill(List<int> pool, int count)
    {
        pool.Clear();
        pool.AddRange(Enumerable.Range(0, count));
        Shuffle(pool);
    }

    private void Shuffle<T>(List<T> list)
    {
        // Fisher-Yates
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = _rand.Next(i + 1);
            var tmp = list[j];
            list[j] = list[i];
            list[i] = tmp;
        }
    }
}
