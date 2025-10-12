public class SimpleGoal : Goal
{
    private bool _isComplete = false;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        _isComplete = true;
        return _points;
    }

    public override bool IsComplete() => _isComplete;

    public override string GetDetails()
    {
        return $"[{(_isComplete ? "X" : " ")}] {_name} ({_description})";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{_name},{_description},{_points},{_isComplete}";
    }

    public static SimpleGoal FromString(string data)
    {
        string[] parts = data.Split(",");
        return new SimpleGoal(parts[0], parts[1], int.Parse(parts[2]))
        {
            _isComplete = bool.Parse(parts[3])
        };
    }
}
