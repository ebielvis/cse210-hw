using System;

namespace ExerciseTracking
{
    public class Activity
    {
        private string _date;
        private double _length; // in minutes

        public Activity(string date, double length)
        {
            _date = date;
            _length = length;
        }

        public string GetDate() => _date;
        public double GetLength() => _length;

        public virtual double GetDistance() { return 0; }
        public virtual double GetSpeed() { return 0; }
        public virtual double GetPace() { return 0; }

        public virtual string GetSummary()
        {
            return $"{_date} {GetType().Name} ({_length} min): " +
                   $"Distance {GetDistance():0.0} km, " +
                   $"Speed: {GetSpeed():0.0} kph, " +
                   $"Pace: {GetPace():0.00} min per km";
        }
    }
}
