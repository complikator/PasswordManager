using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    public class ReversedStringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value as string) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringNullOrEmptyToVisibilityConverter : IValueConverter
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

    public class PasswordToStrengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == null)
                return null;

            PasswordStrength strength = PasswordStrengthUtils.CalculatePasswordStrength(value.ToString());
            var converter = new StrengthToIntConverter();

            var ret = converter.Convert(strength, targetType, parameter, culture);
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PasswordToStrengthColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == null)
                return null;

            PasswordStrength strength = PasswordStrengthUtils.CalculatePasswordStrength(value as string);
            PasswordStrengthToColorConverter converter = new PasswordStrengthToColorConverter();
            return converter.Convert(strength, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Interaction logic for PasswordEditPage.xaml
    /// </summary>
    public partial class PasswordEditPage : Page, INotifyPropertyChanged
    {
        private string editableIconPath;
        private string editableName;
        private string editableEmail;
        private string editableLogin;
        private string editablePassword;
        private string editableWebsite;
        private string editableNotes;


        public SinglePassword Password { get; set; }
        public string EditableIconPath
        {
            get
            {
                return editableIconPath;
            }
            set
            {
                editableIconPath = value;
                OnPropertyChanged("EditableIconPath");
            }
        }
        public string EditableName
        {
            get
            {
                return editableName;
            }
            set
            {
                editableName = value;

                OnPropertyChanged("EditableName");
            }
        }
        public string EditableEmail
        {
            get
            {
                return editableEmail;
            }
            set
            {
                editableEmail = value;

                OnPropertyChanged("EditableEmail");
            }

        }
        public string EditableLogin
        {
            get
            {
                return editableLogin;
            }
            set
            {
                editableLogin = value;

                OnPropertyChanged("EditableLogin");
            }

        }
        public string EditablePassword
        {
            get
            {
                return editablePassword;
            }
            set
            {
                editablePassword = value;

                OnPropertyChanged("EditablePassword");
            }

        }
        public string EditableWebsite
        {
            get
            {
                return editableWebsite;
            }
            set
            {
                editableWebsite = value;

                OnPropertyChanged("EditableWebsite");
            }

        }
        public string EditableNotes
        {
            get
            {
                return editableNotes;
            }
            set
            {
                editableNotes = value;

                OnPropertyChanged("EditableNotes");
            }

        }
        public delegate void SinglePasswordHandler(SinglePassword password);
        public event SinglePasswordHandler CancelEditableForm;
        public event SinglePasswordHandler ApplyPassword;
        public PasswordEditPage(SinglePassword password)
        {
            InitializeComponent();

            this.DataContext = this;
            Password = password;

            updateEditableValuesWithPassword();
        }

        protected private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void onApplyPassword()
        {
            if (ApplyPassword != null)
                ApplyPassword.Invoke(Password);
        }

        private void onCancel()
        {
            if (CancelEditableForm != null)
                CancelEditableForm.Invoke(Password);
        }
        private void updateEditableValuesWithPassword()
        {
            EditableIconPath = Password.IconPath;
            EditableName = Password.Name;
            EditableEmail = Password.Email;
            EditableLogin = Password.Login;
            EditablePassword = Password.Password;
            EditableWebsite = Password.Website;
            EditableNotes = Password.Notes;
        }
        private void updatePasswordWithEditedValues()
        {
            Password.IconPath = EditableIconPath;
            Password.Name = EditableName;
            Password.Email = EditableEmail;
            Password.Login = EditableLogin;
            Password.Password = EditablePassword;
            Password.Website = EditableWebsite;
            Password.Notes = editableNotes;

            Password.LastChangeTime = DateTime.Now;
        }

        private void applyChanges(object sender, RoutedEventArgs e)
        {
            updatePasswordWithEditedValues();

            onApplyPassword();
        }
        private void cancel(object sender, RoutedEventArgs e)
        {
            onCancel();
        }
        private void selectIcon(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg";

            if (fileDialog.ShowDialog() == true)
            {
                EditableIconPath = fileDialog.FileName;
            }
        }
    }
}
