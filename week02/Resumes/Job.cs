// A code template for the category of things known as Job.
// The responsibility of a Job is to hold and display information about a job position.
public class Job
{
    // Member variables (state)
    public string _jobTitle;
    public string _company;
    public int _startYear;
    public int _endYear;

    // Constructor
    public Job()
    {
    }

    // Behavior: Display job details in the specified format
    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
}
