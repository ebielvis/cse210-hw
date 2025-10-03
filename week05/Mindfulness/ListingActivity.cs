using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "List as many things as you can that make you happy.",
        "List people who have influenced your life positively.",
        "List goals you want to achieve this year."
    };

    public ListingActivity()
        : base("Listing Activity", "This activity will help you reflect on positive things in your life by listing them.") { }

    public override void Run()
    {
        StartMessage();
        Random random = new Random();
        int duration = GetDuration();

        string prompt = _prompts[random.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        ShowSpinner(3);

        List<string> responses = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.Write(" > ");
            responses.Add(Console.ReadLine());
        }

        Console.WriteLine($"You listed {responses.Count} items!");
        EndMessage();

        SessionLogger.Log(GetName(), duration, $"Prompt: {prompt}\nResponses: {string.Join(", ", responses)}");
    }
}
