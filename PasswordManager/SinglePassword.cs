using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    [Serializable]
    public class SinglePassword : INotifyPropertyChanged
    {
        private string iconPath;
        private string name;
        private string email;
        private string login;
        private string password;
        private string website;
        private string notes;
        private DateTime lastChangeTime;

        [field:NonSerialized]
        public event PropertyChangedEventHandler? PropertyChanged;

        public string IconPath
        {
            get
            {
                return iconPath;
            }
            set
            {
                iconPath = value;

                OnPropertyChanged("IconPath");
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;

                OnPropertyChanged("Email");
            }
        }
        public string Login
        {

            get
            {
                return login;
            }
            set
            {
                login = value;

                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;

                PasswordStrength = PasswordStrengthUtils.CalculatePasswordStrength(value);


                OnPropertyChanged("PasswordStrength");
                OnPropertyChanged("Password");
            }
        }
        public PasswordStrength PasswordStrength { get; set; }
        public string Website
        {
            get
            {
                return website;
            }
            set
            {
                website = value;

                OnPropertyChanged("Website");
            }
        }
        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;

                OnPropertyChanged("Notes");
            }
        }
        public DateTime CreationTime { get; set; }

        public DateTime LastChangeTime
        {
            get
            {
                return lastChangeTime;
            }
            set
            {
                lastChangeTime = value;

                OnPropertyChanged("LastChangeTime");
            }
        }

        public SinglePassword(string iconPath = "", string name = "Account Name", string email = "", string login = "", string password = "", PasswordStrength strength = PasswordStrength.Invalid, string website = "", string notes = "")
        {
            IconPath = iconPath;
            Name = name;
            Email = email;
            Login = login;
            Password = password;
            PasswordStrength = strength;
            Website = website;
            Notes = notes;
            CreationTime = DateTime.Now;
            LastChangeTime = DateTime.Now;
        }

        protected private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
