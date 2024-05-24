using Dapper;
using System.Data;
using System.Data.SQLite;

namespace FitnessApp.LogicLibrary;

public class DBAccess
{
    private const string ConnectionString = "Data Source=./FitnessAppDB.db;Version=3;";

    public static List<Workout> LoadWorkouts(string username)
    {
        try
        {
            using (IDbConnection connection = new SQLiteConnection(ConnectionString))
            {
                string query = "SELECT * FROM Workout WHERE Username = @Username";
                var output = connection.Query<Workout>(query, new { Username = username });
                return output.ToList();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error loading workouts.", ex);
        }
    }

    public static List<UserProfile> LoadUsers()
    {
        try
        {
            using (IDbConnection connection = new SQLiteConnection(ConnectionString))
            {
                var query = "SELECT * FROM User";
                var output = connection.Query<UserProfile>(query);
                return output.ToList();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error loading users.", ex);
        }
    }

    public static int SaveWorkout(Workout workout)
    {
        try
        {
            using (IDbConnection connection = new SQLiteConnection(ConnectionString))
            {
                string query = 
                    "INSERT INTO Workout (Type, Duration, Calories, AvgHeartRate, PerformedOn) " +
                    "VALUES (@Type, @Duration, @Calories, @AvgHeartRate, @PerformedOn)";
                return connection.Execute(query, workout);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error saving workout.", ex);
        }
    }

    public static int SaveUserProfile(UserProfile user)
    {
        try
        {
            using (IDbConnection connection = new SQLiteConnection(ConnectionString))
            {
                string query = 
                    "INSERT INTO User (Username, Password, Height, Weight, Age) " +
                    "VALUES (@Username, @Password, @Height, @Weight, @Age)";
                return connection.Execute(query, user);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error saving user profile.", ex);
        }
    }
}
