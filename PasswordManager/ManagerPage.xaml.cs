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
    /// <summary>
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        private string sessionPassword;
        private FolderDisplayPage FolderDisplayPage = new FolderDisplayPage();
        private ImageDisplayPage ImageDisplayPage = new ImageDisplayPage();
        private PasswordsDisplayPage PasswordsDisplayPage = new PasswordsDisplayPage();

        public event EventHandler OnLogout;

        public ManagerPage(string sessionPassword)
        {
            this.sessionPassword = sessionPassword;

            InitializeComponent();

            fileTree.FolderSelected += showFolder;
            fileTree.PasswordsSelected += showPasswords;
            fileTree.ImageSelected += showImage;

            restoreSession();
        }
        private void restoreSession()
        {
            var memento = PasswordProvider.Restore();

            if (memento != null)
                fileTree.VirtualRootFolder = memento.RootNode as Folder;
        }
        private void showFolder(Folder folder)
        {
            FolderDisplayPage.FolderToDisplay = folder;
            managerFrame.Navigate(FolderDisplayPage);
        }
        private void showPasswords(PasswordFile password)
        {
            PasswordsDisplayPage.PasswordsToDisplay = password;
            managerFrame.Navigate(PasswordsDisplayPage);
        }
        private void showImage(ImageFile image)
        {
            ImageDisplayPage.ImageToDisplay = image;
            managerFrame.Navigate(ImageDisplayPage);
        }

        public void Logout_Click(object sender, EventArgs e)
        {
            if (OnLogout != null)
            {
                OnLogout.Invoke(this, new EventArgs());
            }
        }

        public void Save_Click(object sender, EventArgs e)
        {
            Memento memento = new Memento(sessionPassword, fileTree.VirtualRootFolder);

            PasswordProvider.Save(memento);
        }
    }
}
