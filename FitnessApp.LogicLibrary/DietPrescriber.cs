namespace FitnessApp.LogicLibrary
{
    public class DietPrescriber
    {
        private string[] _strengthWorkouts = [ "Pull", "Push", "Legs", "Skill training", "Weights" ];
        private string[] _cardioWorkouts = ["Cardio", "Run", "Swimming"];

        public DietPrescriber() { }
        
        public IEnumerable<FootItem> Prescribe(Workout workout, UserProfile user)
        {
            double calories = workout.Calories * 0.4;
            if (user.Age <= 40)
            {
                if (user.Height - user.Weight >= 100)
                {
                    calories = workout.Calories * 1.3;
                }
                else
                {
                    calories = workout.Calories * 0.7;
                }
            }
            List<FootItem> footItems = DBAccess.LoadFootItems((int)calories);
            if (_cardioWorkouts.Contains(workout.Type))
            {
                double sugar = AvgSugars(footItems);
                return from footItem in footItems
                       where footItem.Sugars >= sugar
                       select footItem;
            }
            if (_strengthWorkouts.Contains(workout.Type))
            {
                double protein = AvgProteins(footItems);
                return from footItem in footItems
                       where footItem.Proteins >= protein
                       select footItem;
            }
            return new List<FootItem>();
        }

        private double AvgProteins(List<FootItem> footItems) 
        {
            double maxProtein = 0;
            foreach (var item in footItems)
            {
                if (maxProtein < item.Proteins) 
                { 
                    maxProtein = item.Proteins;
                }
            }
            return maxProtein * 0.3;

        }

        private double AvgSugars(List<FootItem> footItems) 
        {
            double maxSugar = 0;
            foreach (var item in footItems) 
            {
                if (maxSugar < item.Sugars) 
                { 
                    maxSugar = item.Sugars;
                }
            }
            return maxSugar * 0.3;
        }
    }
}
