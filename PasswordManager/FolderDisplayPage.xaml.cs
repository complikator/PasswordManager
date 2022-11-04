using System;
using System.Collections.Generic;
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
    public class FolderToDisplayConverter : IMultiValueConverter
    {
       
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Folder folder = values[0] as Folder;

            if (folder == null)
                return null;

            return $"{folder.Name}({folder.Items.Count})";
        }

       
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class FolderDisplayPage : Page
    {
        private Folder folderToDisplay;
        public Folder FolderToDisplay {
            get
            {
                return folderToDisplay;
            }
            set
            {
                folderToDisplay = value;
                this.DataContext = folderToDisplay;
            }
        }
        public FolderDisplayPage()
        {
            InitializeComponent();

        }
    }
}
