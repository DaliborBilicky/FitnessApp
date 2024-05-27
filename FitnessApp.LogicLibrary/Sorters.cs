namespace FitnessApp.LogicLibrary
{
    public class Sorters
    {
        static public IEnumerable<Workout> SortWorkouts(IEnumerable<Workout> workouts) 
        { 
            return from workout in workouts
                   orderby workout.PerformedOn descending
                   select workout;
        }

        static public IEnumerable<FootItem> SortFootItems(IEnumerable<FootItem> footItems) 
        { 
            return from footItem in footItems
                   orderby footItem.Calories
                   select footItem;
        }
    }
}
