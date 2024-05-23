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

        public CreateProfileWindow()
        {
            InitializeComponent();

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
            UserProfile user = new UserProfile();
            string name = NameTextBox.Text;
            string password = PasswordTextBox.Text;
            string hieght = HeightTextBox.Text;
            string weight = WeightTextBox.Text;
            string age = AgeTextBox.Text;
            if (name == "")
            {
                MessageBox.Show("Forgot to insert name.");
                NameTextBox.Background = Brushes.Red;
            }
            else
            {
                user.Name = name;
            }
            if (password.Length < 5)
            {
                MessageBox.Show("Password to short. At least 6 chars.");
                PasswordTextBox.Background = Brushes.Red;
            }
            else
            {
                user.Password = password;
            }
            if (hieght == "")
            {
                MessageBox.Show("Forgot to insert height.");
                HeightTextBox.Background = Brushes.Red;
            }
            else 
            { 
                user.Height = int.Parse(hieght);
            }
            if (weight == "")
            {
                MessageBox.Show("Forgot to insert weight.");
                WeightTextBox.Background = Brushes.Red;
            }
            else 
            { 
                user.Weight = int.Parse(weight);
            }
            if (age == "")
            {
                MessageBox.Show("Forgot to insert age.");
                AgeTextBox.Background = Brushes.Red;
            }
            else 
            { 
                user.Age = int.Parse(age);
            }
            DBAccess.SaveUserProfile(user);
            MainWindow main = new MainWindow();
            main.Show();
            Close();
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
    }
}
