namespace FitnessApp.LogicLibrary;

public class Workout
{
    public string Type { get; set; } = "";
    public double Duration { get; set; }
    public DateTime PerformedOn { get; set; }
    public int Calories { get; set; }
    public int AvgHeartRate { get; set; }
    public TimeSpan TimeSpanDuration => TimeSpan.FromSeconds(Duration);

    public override string? ToString()
    {
        return $"{Type} took: {TimeSpanDuration}" +
               $", burend calories: {Calories}" +
               $", average heart rate: {AvgHeartRate} ({PerformedOn})";
    }
}

