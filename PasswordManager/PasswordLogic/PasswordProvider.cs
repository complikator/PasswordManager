using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.PasswordLogic
{
    
    public class PasswordProvider 
    {
        private static string passwordFilename = "Passwords.bin";

        public bool CheckPassword(string password)
        {
            var memento = EncryptedFileManager.ReadFile(passwordFilename);

            if (memento == null)
                return true;

            return memento.Password == password;
        }
        public static void Save(Memento memento)
        {
            EncryptedFileManager.WriteFile(passwordFilename, memento);
        }

        public static Memento Restore()
        {
            return EncryptedFileManager.ReadFile(passwordFilename);
        }
    }
}
