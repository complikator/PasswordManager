using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    public class FontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEditable = (value as bool?) ?? false;

            return isEditable ? "Normal" : "Bold";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class FontStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEditable = (value as bool?) ?? false;

            return isEditable ? "Normal" : "Italic";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class BorderThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEditable = (value as bool?) ?? false;

            return isEditable ? 1 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NegationBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (!(value is bool))
                throw new ArgumentException();

            return !(value as bool?);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ContextMenuConverter : IValueConverter
    {
        public ContextMenu FolderMenu { get; set; }
        public ContextMenu FileMenu { get; set; }
        public ContextMenu BaseMenu { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type type = value.GetType();

            ContextMenu selected = BaseMenu;

            if (type == typeof(Folder))
                selected = FolderMenu;
            if (type.IsSubclassOf(typeof(File)))
                selected = FileMenu;

            return selected;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class TreeView : UserControl
    {
        public delegate void SelectedFolderHandler(Folder choosenNode);
        public delegate void SelectedImageHandler(ImageFile choosenNode);
        public delegate void SelectedPasswordsHandler(PasswordFile choosenNode);
        public event SelectedFolderHandler FolderSelected;
        public event SelectedPasswordsHandler PasswordsSelected;
        public event SelectedImageHandler ImageSelected;

        private void onFolderSelected(Folder sender)
        {
            if (FolderSelected != null)
            {
                FolderSelected.Invoke(sender);
            }
        }
        private void onPasswordsSelected(PasswordFile sender)
        {
            if (PasswordsSelected != null)
            {
                PasswordsSelected.Invoke(sender);
            }
        }
        private void onImageSelected(ImageFile sender)
        {
            if (ImageSelected != null)
            {
                ImageSelected.Invoke(sender);
            }
        }

        private Folder virtualRootFolder = new Folder(null);
        public Folder VirtualRootFolder
        {
            get
            {
                return virtualRootFolder;
            }
            set
            {
                virtualRootFolder = value;

                this.DataContext = virtualRootFolder.Items;
            }
        }
        public TreeView()
        {
            InitializeComponent();

            this.DataContext = VirtualRootFolder.Items;
        }

        private void AddDirectory(object sender, RoutedEventArgs e)
        {
            Folder folder = ((sender as MenuItem).CommandParameter as Folder) ?? VirtualRootFolder;

            folder.AddFolder();
        }
        private void AddPasswords(object sender, RoutedEventArgs e)
        {
            Folder folder = ((sender as MenuItem).CommandParameter as Folder) ?? VirtualRootFolder;

            folder.AddPasswordFile();
        }
        private void AddImage(object sender, RoutedEventArgs e)
        {
            Folder folder = ((sender as MenuItem).CommandParameter as Folder) ?? VirtualRootFolder;

            folder.AddImageFile();
        }
        private void RenameNode(object sender, RoutedEventArgs e)
        {
            Node node = (sender as MenuItem).CommandParameter as Node;
            node.IsEditable = true;
        }
        private void DeleteNode(object sender, RoutedEventArgs e)
        {
            ((sender as MenuItem).CommandParameter as Node).Delete();
        }
        private void finishRenaming(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            Node node = TextBoxExtension.GetNodeBinding(box);

            node.Name = box.Text;
            node.IsEditable = false;
        }
        private void onRenamingKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                finishRenaming(sender, e);
            }
        }
        private void selectNode(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            Node node = TextBoxExtension.GetNodeBinding(box);

            if (node.GetType() == typeof(Folder))
                onFolderSelected(node as Folder);
            if (node.GetType() == typeof(PasswordFile))
                onPasswordsSelected(node as PasswordFile);
            if (node.GetType() == typeof(ImageFile))
                onImageSelected(node as ImageFile);
        }
    }
}
