using System.Windows;

namespace FitnessApp.WpfGui
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = NameLoginTextBox.Text;
            string password = PasswordLoginBox.Password;

            if (username == "admin" && password == "password") // Simple credential check
            {
                MessageBox.Show("Succes");
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
    }
}
