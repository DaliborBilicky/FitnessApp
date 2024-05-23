using Dapper;
using System.Data;
using System.Data.SQLite;

namespace FitnessApp.LogicLibrary;

public class DBAccess
{
    public static List<Workout> LoadWorkouts(int userId)
    {
        using (IDbConnection connection = new SQLiteConnection("Data Source=./FitnessAppDB.db;Version=3;"))
        {
            var output = connection.Query<Workout>("select * from Workout " +
                "where UserId = @UserId", new { UserId = userId }); ;
            return output.ToList();
        }
    }

    public static void SaveWorkout(Workout workout)
    {
        using (IDbConnection connection = new SQLiteConnection("Data Source=FitnessAppDB.db;Version=3;"))
        {
            connection.Execute("insert into Workout (Type, Duration, Calories, AvgHeartRate, PerformedOn) " +
                "values (@Type, @Duration, @Calories, @AvgHeartRate, @PerformedOn)", workout);
        }
    }

}
