using FitnessApp.LogicLibrary;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FitnessApp.WpfGui
{
    public partial class AddWorkoutWindow : Window
    {
        private UserProfile _userProfile;
        private Brush _originalTextBoxColor;
        private string[] _items;
        public Workout Workout { get; set; }

        public AddWorkoutWindow(UserProfile userProfile)
        {
            _items = [ "Pull", "Push", "Legs", "Skill training",
                "Run", "Weights", "Cardio", "Swimming" ];
            InitializeComponent();
            WorkoutTypeComboBox.ItemsSource = _items;
            _userProfile = userProfile;
            _originalTextBoxColor = CaloriesTextBox.Background;
            Workout = new Workout();
        }

        // Pomohol som si ChatGPT ale v podstate len kozmeticky doplnok
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        // Pomohol som si ChatGPT ale v podstate len kozmeticky doplnok
        private bool IsTextNumeric(string text)
        {
            Regex regex = new Regex("[^0-9]+"); // regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        // Pomohol som si ChatGPT ale v podstate len kozmeticky doplnok
        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextNumeric(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void AddWorkoutButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;
            int hours = int.TryParse(HoursTextBox.Text, out int result) ? result : 0;
            int minutes = int.TryParse(MinutesTextBox.Text, out int result1) ? result1 : 0;
            int seconds = int.TryParse(SecondsTextBox.Text, out int result2) ? result2 : 0;
            int calories = int.TryParse(CaloriesTextBox.Text, out int result3) ? result3 : 0;
            int avgHeartRate = int.TryParse(AvgHeartRateTextBox.Text, out int result4) ? result4 : 0;

            if (WorkoutTypeComboBox.SelectedItem is not null)
            {
                for (int i = 0; i < _items.Length; i++)
                {
                    if (WorkoutTypeComboBox.SelectedIndex == i) 
                    { 
                        Workout.Type = _items[i];
                        break;
                    } 
                }
            }
            else
            { 
                MessageBox.Show("Forgot to pick workout type.");
                isValid = false;
            }
            if ((0 <= hours && hours <= 24) && 
                (0 <= minutes && minutes <= 60) && 
                (0 <= seconds && seconds <= 60))
            {
                Workout.Duration = (3600 * hours) + (60 * minutes) + seconds;
            }
            else
            {
                MessageBox.Show("Duration is not valid. Input real time.");
                DurationBackroundRed();
                isValid = false;
            }
            if (calories > 0)
            {
                Workout.Calories = calories;
            }
            else
            {
                MessageBox.Show("Calories were not set.");
                CaloriesTextBox.Background = Brushes.Red;
                isValid = false;
            }
            if (avgHeartRate > 0)
            {
                Workout.AvgHeartRate = avgHeartRate;
            }
            else
            {
                MessageBox.Show("Average heart rate was not set.");
                AvgHeartRateTextBox.Background = Brushes.Red;
                isValid = false;
            }

            if (isValid)
            {
                try
                {
                    Workout.Username = _userProfile.Username;
                    DBAccess.SaveWorkout(Workout);
                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while adding the workout: " + ex.Message);
                }
            }
        }

        private void AvgHeartRateTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            AvgHeartRateTextBox.Background = _originalTextBoxColor;
        }

        private void CaloriesTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CaloriesTextBox.Background = _originalTextBoxColor;
        }

        private void HoursTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DurationBackroundReset();
        }

        private void MinutesTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DurationBackroundReset();
        }

        private void SecondsTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            DurationBackroundReset();
        }

        private void DurationBackroundReset() 
        { 
            HoursTextBox.Background = _originalTextBoxColor;
            MinutesTextBox.Background = _originalTextBoxColor;
            SecondsTextBox.Background = _originalTextBoxColor;
        }

        private void DurationBackroundRed() 
        { 
            HoursTextBox.Background = Brushes.Red;
            MinutesTextBox.Background = Brushes.Red;
            SecondsTextBox.Background = Brushes.Red;
        }
    }
}
