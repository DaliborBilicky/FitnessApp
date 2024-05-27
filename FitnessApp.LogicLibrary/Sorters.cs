namespace FitnessApp.LogicLibrary
{
    public class Sorters
    {
        static public IEnumerable<Workout> SortWorkouts(IEnumerable<Workout> workouts) 
        { 
            IEnumerable<Workout> sortedWorkouts =  
                from workout in workouts
                orderby workout.PerformedOn descending
                select workout;
            return sortedWorkouts;
        }

        static public IEnumerable<FootItem> SortFootItems(IEnumerable<FootItem> footItems) 
        { 
            IEnumerable<FootItem> sortedFootItems =  
                from footItem in footItems
                orderby footItem.Calories
                select footItem;
            return sortedFootItems;
        }
    }
}
