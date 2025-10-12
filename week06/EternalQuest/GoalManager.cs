using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void ListGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetails()}");
        }
    }

    public void RecordEvent(int index)
    {
        if (index >= 0 && index < _goals.Count)
        {
            int points = _goals[index].RecordEvent();
            _score += points;
            Console.WriteLine($"You earned {points} points! Total Score: {_score}");
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Total Score: {_score}");
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter output = new StreamWriter(filename))
        {
            output.WriteLine(_score);
            foreach (Goal goal in _goals)
            {
                output.WriteLine(goal.GetStringRepresentation());
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename)) return;

        string[] lines = File.ReadAllLines(filename);
        _score = int.Parse(lines[0]);
        _goals.Clear();

        foreach (string line in lines.Skip(1))
        {
            string[] parts = line.Split(":");
            string type = parts[0];
            string data = parts[1];

            switch (type)
            {
                case "SimpleGoal":
                    _goals.Add(SimpleGoal.FromString(data));
                    break;
                case "EternalGoal":
                    _goals.Add(EternalGoal.FromString(data));
                    break;
                case "ChecklistGoal":
                    _goals.Add(ChecklistGoal.FromString(data));
                    break;
            }
        }
    }
}
