using System;
using System.Collections.Generic;

public class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time you helped someone.",
        "Recall a moment you overcame a challenge.",
        "Think of a time you showed kindness to yourself."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this experience meaningful?",
        "What did you learn from it?",
        "How can you apply this lesson today?"
    };

    public ReflectionActivity()
        : base("Reflection Activity", "This activity will help you reflect on times when you have shown strength or resilience.") { }

    public override void Run()
    {
        StartMessage();
        Random random = new Random();
        int duration = GetDuration();
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        string prompt = _prompts[random.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        ShowSpinner(3);

        while (DateTime.Now < endTime)
        {
            string question = _questions[random.Next(_questions.Count)];
            Console.WriteLine(question);
            ShowSpinner(5);
        }

        EndMessage();
        SessionLogger.Log(GetName(), duration, $"Reflected on: {prompt}");
    }
}
