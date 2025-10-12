public abstract class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent(); // returns points earned
    public abstract bool IsComplete();
    public abstract string GetDetails(); // [ ] Name (Desc)
    public abstract string GetStringRepresentation(); // For saving/loading
}
