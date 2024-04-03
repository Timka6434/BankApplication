using System.Windows;

namespace BankApplication
{
    public partial class authorizationWindow : Window
    {

        public authorizationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(AuthenticationService.Authenticate(loginField.Text,passwordField.Password))
            {
                MainWindow mainWindow = new MainWindow(loginField.Text);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                 MessageBox.Show("Не правильный пароль!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
