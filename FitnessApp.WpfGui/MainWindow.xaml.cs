using FitnessApp.LogicLibrary;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;

namespace FitnessApp.WpfGui
{
    public partial class MainWindow : Window
    {
        private CurrentUser? _currentUser;
        private ObservableCollection<Workout> _workouts;
        public MainWindow(ref CurrentUser currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _workouts = new ObservableCollection<Workout>();
            if (_currentUser is not null && _currentUser.CurrentProfile is not null) 
            {
                UserLabel.Content = _currentUser.CurrentProfile;
                SetWorkout(_currentUser.CurrentProfile.Username);
            }
            else 
            {
                MessageBox.Show("Something went wrong. User is null.");
            }
        }

        private void AddWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser is not null && _currentUser.CurrentProfile is not null) 
            { 
                AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow(_currentUser.CurrentProfile); 
                addWorkoutWindow.ShowDialog();
                if (addWorkoutWindow.DialogResult == true) 
                {
                    SetWorkout(_currentUser.CurrentProfile.Username);
                }
            }
        }

        private void SetWorkout(string username) 
        { 
            _workouts = new ObservableCollection<Workout>(
                Sorters.SortWorkouts(DBAccess.LoadWorkouts(username)));
            WorkoutsListBox.ItemsSource = _workouts;
        }

        private void WorkoutsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox? listBox = sender as ListBox;
            DietPrescriber prescriber = new DietPrescriber();

            if (listBox is not null && listBox.SelectedItem is not null 
                && _currentUser is not null && _currentUser.CurrentProfile is not null)
            {
                MealsListBox.ItemsSource = 
                    Sorters.SortFootItems(prescriber.Prescribe(
                        (Workout)listBox.SelectedItem, _currentUser.CurrentProfile));
            } 
        }
    }
}