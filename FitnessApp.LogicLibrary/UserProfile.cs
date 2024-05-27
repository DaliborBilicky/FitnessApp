namespace FitnessApp.LogicLibrary
{
    public class UserProfile
    {
        public required string Username { get; set; }
        public string? Password { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Age { get; set; }

        public override string? ToString()
        {
            return $"User: {Username}, Height: {Height}, Weight: {Weight}, Age: {Age}";
        }
    }
}
