public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent() => _points;

    public override bool IsComplete() => false;

    public override string GetDetails()
    {
        return $"[∞] {_name} ({_description})";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{_name},{_description},{_points}";
    }

    public static EternalGoal FromString(string data)
    {
        string[] parts = data.Split(",");
        return new EternalGoal(parts[0], parts[1], int.Parse(parts[2]));
    }
}
