using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordManager
{
    public class PasswordStrengthCorectnessToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (PasswordStrength)value == PasswordStrength.Invalid ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringPrefixValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string s = value.ToString();
            int prefixLength = 0;

            if (!int.TryParse(parameter.ToString(), out prefixLength) || prefixLength == 0)
            {
                prefixLength = 1;
            }

            if (prefixLength > s.Length)
                return s;

            return s.Substring(0, prefixLength);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class PasswordsListControl : UserControl
    {
        public delegate void SelectedItemEventHandler(SinglePassword item);
        public event SelectedItemEventHandler SelectedItem;
        public event SelectedItemEventHandler ItemAdded;
        public PasswordsListControl()
        {
            InitializeComponent();
        }

        private void passwordSelected(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItem != null)
            {
                SelectedItem.Invoke(passwordNamesList.SelectedItem as SinglePassword);
            }
        }

        private bool passwordFilter(object item)
        {
            if (string.IsNullOrEmpty(searchBox.Text))
                return true;

            return (item as SinglePassword).Name.IndexOf(searchBox.Text) > -1;
        }

        private void sortPasswords()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(passwordNamesList.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));
        }

        public void Refresh()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(passwordNamesList.ItemsSource);
            view.Refresh();
        }

        private void filterPasswords(object sender, KeyEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(passwordNamesList.ItemsSource);
            view.Filter = passwordFilter;
            view.Refresh();
        }
        private void onItemAdded(SinglePassword password)
        {
            if (ItemAdded != null)
                ItemAdded.Invoke(password);
        }
        private void addNewPassword(object sender, RoutedEventArgs e)
        {
            onItemAdded(new SinglePassword());
        }
    }
}
