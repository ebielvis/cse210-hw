namespace ExerciseTracking
{
    public class Cycling : Activity
    {
        private double _speed; // in km/h

        public Cycling(string date, double length, double speed)
            : base(date, length)
        {
            _speed = speed;
        }

        public override double GetSpeed() => _speed;
        public override double GetDistance() => (_speed * GetLength()) / 60;
        public override double GetPace() => 60 / _speed;
    }
}
