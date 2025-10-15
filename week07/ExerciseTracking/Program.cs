using System;
using System.Collections.Generic;

namespace ExerciseTracking
{
    class Program
    {
        static void Main()
        {
            List<Activity> activities = new List<Activity>()
            {
                new Running("03 Nov 2022", 30, 4.8),
                new Cycling("03 Nov 2022", 45, 20),
                new Swimming("03 Nov 2022", 30, 40)
            };

            foreach (Activity activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
}
