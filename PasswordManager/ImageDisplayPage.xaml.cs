using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ImageDisplayPage.xaml
    /// </summary>
    public partial class ImageDisplayPage : Page
    {
        private ImageFile imageToDisplay;
        public ImageFile ImageToDisplay
        {
            get
            {
                return imageToDisplay;
            }
            set
            {
                imageToDisplay = value;

                this.DataContext = imageToDisplay.Filename;
            }
        }
        public ImageDisplayPage()
        {
            InitializeComponent();
        }

        private void saveImage(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "PNG Image|*.png";
            saveFileDialog.Title = "Save image";

            if (saveFileDialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(ImageToDisplay.Filename);
                byte[] data = new byte[fileInfo.Length];


                using (FileStream reader = fileInfo.OpenRead())
                {
                    reader.Read(data);

                    using (FileStream writer = (FileStream)saveFileDialog.OpenFile())
                    {
                        writer.Write(data);
                    }
                }
            }
        }
    }
}
