using Dapper;
using System.Data;
using System.Data.SQLite;

namespace FitnessApp.LogicLibrary
{
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
                Console.Error.WriteLine("Error loading workouts." + ex.Message);
            }
            return new List<Workout>();
        }

        public static List<FootItem> LoadFootItems(int calories)
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(ConnectionString))
                {
                    var query = $"SELECT * FROM FootItem WHERE Calories <= @Calories";
                    var output = connection.Query<FootItem>(query, new { Calories = calories });
                    return output.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error loading users." + ex.Message);
            }
            return new List<FootItem>();
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
                Console.Error.WriteLine("Error loading users." + ex.Message);
            }
            return new List<UserProfile>();
        }

        public static void SaveWorkout(Workout workout)
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(ConnectionString))
                {
                    string query =
                        "INSERT INTO Workout (Type, Duration, Calories, AvgHeartRate, Username) " +
                        "VALUES (@Type, @Duration, @Calories, @AvgHeartRate, @Username)";
                    connection.Execute(query, workout);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error saving workout." + ex.Message);
            }
        }

        public static void SaveUserProfile(UserProfile user)
        {
            try
            {
                using (IDbConnection connection = new SQLiteConnection(ConnectionString))
                {
                    string query =
                        "INSERT INTO User (Username, Password, Height, Weight, Age) " +
                        "VALUES (@Username, @Password, @Height, @Weight, @Age)";
                    connection.Execute(query, user);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error saving user profile." + ex.Message);
            }
        }
    }
}
