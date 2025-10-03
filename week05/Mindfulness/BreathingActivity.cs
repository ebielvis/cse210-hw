using System;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing Activity", "This activity will help you relax by guiding you through slow breathing.") { }

    public override void Run()
    {
        StartMessage();
        int duration = GetDuration();
        DateTime endTime = DateTime.Now.AddSeconds(duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("Breathe in... ");
            ShowCountdown(3);
            Console.Write("Now breathe out... ");
            ShowCountdown(3);
        }

        EndMessage();
        SessionLogger.Log(GetName(), duration, "Relaxed breathing completed.");
    }
}
