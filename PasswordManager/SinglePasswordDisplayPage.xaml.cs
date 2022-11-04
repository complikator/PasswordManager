using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class EmailToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string email = value as string;

            if (email == null)
                return null;

            return $"mailto:{email}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PasswordToDotsConverter : IValueConverter
    {
        private char dot = '●';
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == null)
                return null;
            return new string(dot, value.ToString().Length);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StrengthToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringNullOrEmptyToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value as string) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Interaction logic for SinglePasswordDisplayPage.xaml
    /// </summary>
    public partial class SinglePasswordDisplayPage : Page
    {
        public SinglePassword Password { get; set; }
        public delegate void SinglePasswordHandler(SinglePassword password);
        public event SinglePasswordHandler PasswordDeleteRequested;
        public event SinglePasswordHandler PasswordEditRequested;

        public SinglePasswordDisplayPage(SinglePassword password)
        {
            InitializeComponent();

            this.DataContext = password;
            Password = password;
        }

        private void copyButtonClick(object sender, RoutedEventArgs e)
        {
            string text = (sender as Button).CommandParameter as string;

            if (string.IsNullOrEmpty(text))
                return;

            Clipboard.SetText(text);
        }

        private void onPasswordDeleteRequested()
        {
            if (PasswordDeleteRequested != null)
                PasswordDeleteRequested.Invoke(Password);
        }

        private void onPasswordEditRequested()
        {
            if (PasswordEditRequested != null)
                PasswordEditRequested.Invoke(Password);
        }

        private void deletePassword(object sender, RoutedEventArgs e)
        {
            onPasswordDeleteRequested();
        }

        private void editPassword(object sender, RoutedEventArgs e)
        {
            onPasswordEditRequested();
        }

        private void openWebsiteLink(object sender, RequestNavigateEventArgs e)
        {
            if (!string.IsNullOrEmpty(Password.Website))
            {
                try
                {

                    var sInfo = new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri)
                    {
                        UseShellExecute = true,
                    };
                    System.Diagnostics.Process.Start(sInfo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                e.Handled = true;
            }
        }
    }
}
