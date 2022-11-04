using PasswordManager.PasswordLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager
{
    public partial class MainWindow : Window
    {
        private Login loginPage = new Login(new PasswordProvider());
        private ManagerPage? managerPage;
        private string sessionPassword;
        public MainWindow()
        {
            loginPage.OnLogin += LoginSuccess;

            InitializeComponent();
        }

        public void LoginSuccess(object sender, EventArgs e)
        {
            var loginArgs = e as LoginEventArgs;

            if (loginArgs == null)
                throw new ArgumentException("Invalid login args.");

            sessionPassword = loginArgs.Password;

            managerPage = new ManagerPage(sessionPassword);

            managerPage.OnLogout += logout;

            mainFrame.Navigate(managerPage);
        }

        private void logout(object sender, EventArgs e)
        {
            mainFrame.Navigate(loginPage);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(loginPage);
        }
    }
}
