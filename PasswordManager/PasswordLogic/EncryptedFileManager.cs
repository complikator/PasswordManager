using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace PasswordManager.PasswordLogic
{
    [Serializable]
    public class Memento
    {
        public string Password { get; set; }
        public Node RootNode { get; set; }
        public Memento(string password, Node root)
        {
            Password = password;
            RootNode = root;
        }
    }
    internal class EncryptedFileManager
    {
        public static bool FileExist(string path)
        {
            return System.IO.File.Exists(path);
        }

        public static void WriteFile(string path, Memento memento)
        {
            if (FileExist(path))
            {
                System.IO.File.Delete(path);
            }

            ;
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                try
                {
                    formatter.Serialize(fileStream, memento);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        public static Memento ReadFile(string path)
        {
            if (!FileExist(path))
                return null;


            Memento memento = null;
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                try
                {
                    memento = (Memento)formatter.Deserialize(fileStream);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return memento;
        }
    }
}
