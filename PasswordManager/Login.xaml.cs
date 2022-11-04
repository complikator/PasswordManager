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
    public class LoginEventArgs : EventArgs
    {
        public string Password { get; }

        public LoginEventArgs(string password)
        {
            this.Password = password;
        }
    }
    public partial class Login : Page
    {
        private PasswordProvider provider;

        public event EventHandler OnLogin;
        private string getPassword()
        {
            return passwordBox.Password;
        }

        public Login(PasswordProvider provider)
        {
            this.provider = provider;

            InitializeComponent();
        }

        private void onLoginRequested()
        {
            string password = getPassword();
            if (provider.CheckPassword(password))
            {
                if (OnLogin != null)
                    OnLogin.Invoke(this, new LoginEventArgs(password));
            }
            else
            {
                MessageBox.Show("Invalid Password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Login_Click(object sender, EventArgs e)
        {
            onLoginRequested();
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                onLoginRequested();
            }
        }

    }
}
