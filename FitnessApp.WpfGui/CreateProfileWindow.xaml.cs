using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using FitnessApp.LogicLibrary;

namespace FitnessApp.WpfGui
{
    /// <summary>
    /// Interaction logic for CreateProfileWindow.xaml
    /// </summary>
    public partial class CreateProfileWindow : Window
    {
        private Brush _originalTextBoxColor;
        private CurrentUser _currentUser;

        public CreateProfileWindow()
        {
            InitializeComponent();
            _currentUser = new CurrentUser();
            _originalTextBoxColor = PasswordTextBox.Background;
        }

        // Pomohol som si ChatGPT ale v podstate len kozmeticky doplnok
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        // Pomohol som si ChatGPT ale v podstate len kozmeticky doplnok
        private static bool IsTextNumeric(string text)
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

        private void CreateProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UserProfile user = new UserProfile() { Username = " " };
            string name = NameTextBox.Text;
            string password = PasswordTextBox.Text;
            string hieght = HeightTextBox.Text;
            string weight = WeightTextBox.Text;
            string age = AgeTextBox.Text;
            bool isValid = true;

            if (name == "")
            {
                MessageBox.Show("Forgot to insert name or it's already taken.");
                NameTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                if (IsLogginUnique(name)) 
                { 
                    user.Username = name;
                } else 
                { 
                    MessageBox.Show("Userame it's already taken.");
                    NameTextBox.Background = Brushes.Red;
                    isValid = false;
                }
            }
            if (password.Length < 5)
            {
                MessageBox.Show("Password to short. At least 6 chars.");
                PasswordTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                user.Password = password;
            }
            if (hieght == "")
            {
                MessageBox.Show("Forgot to insert height.");
                HeightTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else 
            { 
                user.Height = int.Parse(hieght);
            }
            if (weight == "")
            {
                MessageBox.Show("Forgot to insert weight.");
                WeightTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else 
            { 
                user.Weight = int.Parse(weight);
            }
            if (age == "")
            {
                MessageBox.Show("Forgot to insert age.");
                AgeTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else 
            { 
                user.Age = int.Parse(age);
            }
            if (isValid)
            {
                try
                {
                    DBAccess.SaveUserProfile(user);
                    _currentUser.CurrentProfile = user;
                    MainWindow main = new MainWindow(ref _currentUser);
                    main.Show();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "An error occurred while saving the user profile: " + ex.Message);
                }
            }
        }

        private void PasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Background = _originalTextBoxColor;
        }

        private void HeightTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            HeightTextBox.Background = _originalTextBoxColor;
        }

        private void WeightTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            WeightTextBox.Background = _originalTextBoxColor;
        }

        private void AgeTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            AgeTextBox.Background = _originalTextBoxColor;
        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            NameTextBox.Background = _originalTextBoxColor;
        }

        private bool IsLogginUnique(string username) 
        {
            List<UserProfile> users = DBAccess.LoadUsers();
            foreach (var user in users)
            {
                if (user.Username == username) 
                { 
                    return false; 
                }
            }
            return true;
        }
    }
}
