using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using BLL.Models;
using System.IO;

namespace wpfreg.View
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        public ProfileView()
        {
            InitializeComponent();
            DataContext = new UserProfile
            {
                UserName = "Ім'я Користувача",
                UserLastName = "Прізвище Користувача",
                UserEmail = "user@example.com",
                UserNickname = "user123",
                // Зображення користувача можна ініціалізувати з файлу або іншим шляхом
                
            };

        }
        private void UploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            // Діалог вибору файлу для вибору фотографії
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif|All Files|*.*",
                Title = "Виберіть фотографію користувача"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Отримання вибраного файлу та оновлення фотографії користувача
                string selectedImagePath = openFileDialog.FileName;
                ((UserProfile)DataContext).Photo = File.ReadAllBytes(selectedImagePath);
                UserImage.Source = LoadImageFromBytes(((UserProfile)DataContext).Photo);
            }
        }
        private ImageSource LoadImageFromBytes(byte[] imageData)
        {
            BitmapImage image = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(imageData))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }
            return image;
        }
    }
    public class UserProfile
    {
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserNickname { get; set; }
        public byte[] Photo { get; set; }
    }
}
