namespace ExerciseTracking
{
    public class Running : Activity
    {
        private double _distance; // in kilometers

        public Running(string date, double length, double distance)
            : base(date, length)
        {
            _distance = distance;
        }

        public override double GetDistance() => _distance;
        public override double GetSpeed() => (_distance / GetLength()) * 60;
        public override double GetPace() => GetLength() / _distance;
    }
}
