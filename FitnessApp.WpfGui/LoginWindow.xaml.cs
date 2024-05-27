using FitnessApp.LogicLibrary;
using System.Windows;

namespace FitnessApp.WpfGui
{
    public partial class LoginWindow : Window
    {
        private CurrentUser _currentUser;

        public LoginWindow()
        {
            InitializeComponent();
            _currentUser = new CurrentUser();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = NameLoginTextBox.Text;
            string password = PasswordLoginBox.Password;
            UserProfile loginInUser = new UserProfile
            {
                Username = username,
                Password = password
            };

            if (_currentUser.IsLoginValid(loginInUser))
            {
                if (_currentUser is not null) 
                { 
                    MainWindow mainWindow = new MainWindow(ref _currentUser);
                    mainWindow.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateProfileWindow createProfileWindow = new CreateProfileWindow();
            createProfileWindow.Show();
            Close();
        }
    }
}
