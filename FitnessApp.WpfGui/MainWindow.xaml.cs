using FitnessApp.LogicLibrary;
using System.Collections.ObjectModel;
using System.Windows;

namespace FitnessApp.WpfGui
{
    public partial class MainWindow : Window
    {
        private CurrentUser? _currentUser;
        private List<Workout>? _workouts;
        public MainWindow(ref CurrentUser currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            if (_currentUser is not null && _currentUser.CurrentProfile is not null) 
            {
                UserLabel.Content = _currentUser.CurrentProfile;
                _workouts = DBAccess.LoadWorkouts(_currentUser.CurrentProfile.Username);
            }
            else 
            {
                MessageBox.Show("Something went wrong. User is null.");
            }
        }

        private void AddWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow(); 
            addWorkoutWindow.Show();
        }
    }
}