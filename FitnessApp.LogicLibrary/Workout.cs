namespace FitnessApp.LogicLibrary
{
    public class Workout
    {
        private TimeSpan _timeSpanDuration;
        public string Type { get; set; } = "";
        public double Duration { get; set; }
        public DateTime PerformedOn { get; set; }
        public int Calories { get; set; }
        public int AvgHeartRate { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = "admin";
        public TimeSpan TimeSpanDuration
        {
            get
            {
                return TimeSpan.FromSeconds(Duration);
            }
            set
            {
                _timeSpanDuration = TimeSpan.FromSeconds(Duration);
            }
        }

        public override string? ToString()
        {
            return $"{Type} took: {TimeSpanDuration}" +
                   $", burend calories: {Calories}" +
                   $", average heart rate: {AvgHeartRate} ({PerformedOn})";
        }
    }
}