public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonus)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonus = bonus;
    }

    public override int RecordEvent()
    {
        _currentCount++;
        if (_currentCount >= _targetCount)
            return _points + _bonus;
        return _points;
    }

    public override bool IsComplete() => _currentCount >= _targetCount;

    public override string GetDetails()
    {
        return $"[{(_currentCount >= _targetCount ? "X" : " ")}] {_name} ({_description}) -- Completed {_currentCount}/{_targetCount}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{_name},{_description},{_points},{_targetCount},{_bonus},{_currentCount}";
    }

    public static ChecklistGoal FromString(string data)
    {
        string[] parts = data.Split(",");
        return new ChecklistGoal(parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]))
        {
            _currentCount = int.Parse(parts[5])
        };
    }
}
