// A code template for the category of things known as Resume.
// The responsibility of a Resume is to hold and display a person's name and their jobs.
public class Resume
{
    // Member variables
    public string _name;
    public List<Job> _jobs = new List<Job>();

    // Constructor
    public Resume()
    {
    }

    // Behavior: Display resume details
    public void Display()
    {
        Console.WriteLine($"Name: {_name}");
        Console.WriteLine("Jobs:");
        foreach (Job job in _jobs)
        {
            job.Display();
        }
    }
}
