namespace FitnessApp.LogicLibrary
{
    public class CurrentUser
    {
        public UserProfile? CurrentProfile { get; set; }

        public bool IsLoginValid(UserProfile logingInUser) 
        {
            List<UserProfile> users = DBAccess.LoadUsers();
            foreach (var user in users)
            {
                if (user.Username == logingInUser.Username && user.Password == logingInUser.Password) 
                {
                    CurrentProfile = user; 
                    return true;
                }
            }
            return false;
        }
    }
}
