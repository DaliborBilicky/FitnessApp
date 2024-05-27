namespace FitnessApp.LogicLibrary
{
    public class DietPrescriber
    {
        private static string[] _strengthWorkouts = [ "Pull", "Push", "Legs", "Skill training", "Weights" ];
        private static string[] _cardioWorkouts = ["Cardio", "Run", "Swimming"];

        public DietPrescriber() { }
        
        public IEnumerable<FootItem> Prescribe(Workout workout, UserProfile user)
        {
            int calories = 0;
            if (user.Age <= 40)
            {
                if (user.Height - user.Weight <= 100)
                {
                    calories = workout.Calories;
                }
            }
            else
            {
                calories = (int)(workout.Calories * 0.3);
            }
            List<FootItem> footItems = DBAccess.LoadFootItems("<=", calories);
            if (_cardioWorkouts.Contains(workout.Type))
            {
                double sugar = avgSugars(footItems);
                return from footItem in footItems
                       where footItem.Sugars >= sugar
                       select footItem;
            }
            if (_strengthWorkouts.Contains(workout.Type))
            {
                double protein = avgProteins(footItems);
                return from footItem in footItems
                       where footItem.Proteins >= protein
                       select footItem;
            }
            return new List<FootItem>();
        }

        private double avgProteins(List<FootItem> footItems) 
        {
            double maxProtein = 0;
            foreach (var item in footItems)
            {
                if (maxProtein < item.Proteins) 
                { 
                    maxProtein = item.Proteins;
                }
            }
            return maxProtein / 2;

        }

        private double avgSugars(List<FootItem> footItems) 
        {
            double maxSugar = 0;
            foreach (var item in footItems) 
            {
                if (maxSugar < item.Sugars) 
                { 
                    maxSugar = item.Sugars;
                }
            }
            return maxSugar / 2;
        }
    }
}
