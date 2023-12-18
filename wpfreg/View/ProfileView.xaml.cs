using BLL.Models;
using BLL.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using wpfreg.ViewModel;

namespace wpfreg.View
{
    public partial class ProfileView : UserControl
    {
        public UserModel curUser = App.CurrentUser;
        public Image accImage = new Image();
        private UserService _userservice;
        private readonly ILogger<ProfileView> _logger;

        public ProfileView()
        {
            DataContext = new ProfileViewModel();
            InitializeComponent();
            _userservice = App.AppHost.Services.GetRequiredService<UserService>();
            _logger = App.AppHost.Services.GetRequiredService<ILogger<ProfileView>>();
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
                curUser.Photo = File.ReadAllBytes(selectedImagePath);
                App.CurrentUser.Photo = File.ReadAllBytes(selectedImagePath);
                UserImage.Source = LoadImageFromBytes((curUser.Photo));
                _logger.LogInformation("User upload photo.");
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

        private async void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            await _userservice.UpdateUser(curUser);
        }
    }


}
