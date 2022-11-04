using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    [Serializable]
    public class Node : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private bool isEditable = false;
        public bool IsEditable
        {
            get
            {
                return isEditable;
            }
            set
            {
                isEditable = value;
                OnPropertyChanged("IsEditable");
            }
        }
        public Node(Node parent)
        {
            Parent = parent;
        }
        public Node? Parent { get; set; }
        [field:NonSerialized]
        public event PropertyChangedEventHandler? PropertyChanged;
        protected private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        virtual public ObservableCollection<Node>? Items
        {
            get
            {
                return null;
            }
        }
        public void Delete()
        {
            Parent.Items.Remove(this);
            Parent.OnPropertyChanged("Items");
        }
    }
    [Serializable]
    public class Folder : Node
    {
        public Folder(Node parent) : base(parent)
        {
            Name = "New Folder";
        }
        public override ObservableCollection<Node> Items { get; } = new ObservableCollection<Node>();
        public void AddFolder()
        {
            Items.Add(new Folder(this));
            OnPropertyChanged("Items");
        }
        public void AddPasswordFile()
        {
            Items.Add(new PasswordFile(this));
            OnPropertyChanged("Items");
        }
        public void AddImageFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg";

            if (fileDialog.ShowDialog() == true)
            {
                string filepath = fileDialog.FileName;
                Items.Add(new ImageFile(this, filepath));
                OnPropertyChanged("Items");
            }
        }
    }

    [Serializable]
    public class File : Node
    {
        public File(Node parent) : base(parent) { }
    }
    [Serializable]
    public class ImageFile : File
    {
        public string Filename { get; set; }
        public ImageFile(Node parent, string filename) : base(parent)
        {
            Filename = filename;
            Name = "New Image";
        }
    }
    [Serializable]
    public class PasswordFile : File
    {
        public ObservableCollection<SinglePassword> Passwords { get; set; } = new ObservableCollection<SinglePassword>();
        public PasswordFile(Node parent) : base(parent)
        {
            Name = "New Passwords";
        }

        public void DeleteSinglePassword(SinglePassword password)
        {
            Passwords.Remove(password);
            OnPropertyChanged("Passwords");
        }
        public void AddSinglePassword(SinglePassword password)
        {
            Passwords.Add(password);
            OnPropertyChanged("Passwords");
        }
    }
}
