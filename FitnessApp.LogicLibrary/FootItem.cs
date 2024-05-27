namespace FitnessApp.LogicLibrary
{
    public class FootItem
    {
        public string Name { get; set; } = "";
        public int Calories { get; set; }
        public double Proteins { get; set; }
        public double Sugars { get; set; }
        public double Fats { get; set; }

        public override string? ToString()
        {
            return $"{Name} - {Calories}, Proteins: {Proteins}, Sugars: {Sugars}, Fats: {Fats}";
        }
    }
}
