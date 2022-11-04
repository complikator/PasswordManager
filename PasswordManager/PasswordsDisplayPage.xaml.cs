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
    /// <summary>
    /// Interaction logic for PasswordsDisplayPage.xaml
    /// </summary>
    public partial class PasswordsDisplayPage : Page
    {
        private PasswordFile passwordsToDisplay;
        private SinglePassword currentPassword;
        public PasswordFile PasswordsToDisplay
        {
            get
            {
                return passwordsToDisplay;
            }
            set
            {
                passwordsToDisplay = value;

                this.DataContext = passwordsToDisplay;
            }
        }
        public PasswordsDisplayPage()
        {
            InitializeComponent();

            passwordList.SelectedItem += onPasswordSelected;
            passwordList.ItemAdded += addNewPassword;
        }
        private void onPasswordEditingStarted()
        {
            passwordList.IsEnabled = false;
        }

        private void onPasswordEditingFinished()
        {
            passwordList.IsEnabled = true;
        }
        private void addNewPassword(SinglePassword password)
        {
            passwordsToDisplay.AddSinglePassword(password);

            editPassword(password);
        }
        private void clearRightPanel()
        {
            rightSideFrame.Content = "";
        }

        private void onPasswordSelected(SinglePassword password)
        {
            currentPassword = password;

            showPassword(password);
        }

        private void showPassword(SinglePassword password)
        {
            var page = new SinglePasswordDisplayPage(password);
            page.PasswordDeleteRequested += deletePassword;
            page.PasswordEditRequested += editPassword;

            rightSideFrame.Navigate(page);
        }
        private void deletePassword(SinglePassword password)
        {
            PasswordsToDisplay.DeleteSinglePassword(password);

            clearRightPanel();
        }

        private void editPassword(SinglePassword password)
        {
            onPasswordEditingStarted();
            showPasswordEditionForm(password);
        }

        private void showPasswordEditionForm(SinglePassword password)
        {
            var page = new PasswordEditPage(password);
            page.ApplyPassword += changePassword;
            page.CancelEditableForm += cancelEdition;

            rightSideFrame.Navigate(page);
        }

        private void cancelEdition(SinglePassword password)
        {
            onPasswordEditingFinished();
            clearRightPanel();
        }
        private void changePassword(SinglePassword password)
        {
            onPasswordEditingFinished();
            showPassword(password);
        }
    }
}
