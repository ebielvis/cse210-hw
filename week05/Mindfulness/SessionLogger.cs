using System;
using System.IO;

public static class SessionLogger
{
    private static string filePath = "session_log.txt";

    public static void Log(string activityName, int duration, string details)
    {
        using (StreamWriter sw = new StreamWriter(filePath, true))
        {
            sw.WriteLine("=== Session Log ===");
            sw.WriteLine($"Date: {DateTime.Now}");
            sw.WriteLine($"Activity: {activityName}");
            sw.WriteLine($"Duration: {duration} seconds");
            sw.WriteLine($"Details: {details}");
            sw.WriteLine("--------------------\n");
        }
    }
}
