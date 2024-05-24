﻿using FitnessApp.LogicLibrary;
using System.Windows;

namespace FitnessApp.WpfGui
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public CurrentUser CurrentUser { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
            CurrentUser = new CurrentUser();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = NameLoginTextBox.Text;
            string password = PasswordLoginBox.Password;
            UserProfile loginInUser = new UserProfile();
            loginInUser.Username = username;
            loginInUser.Password = password;

            if (CurrentUser.IsLoginValid(loginInUser)) // Simple credential check
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
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
