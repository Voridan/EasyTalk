using BLL.Models;
using BLL.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
namespace wpfreg.View
{
    /// <summary>
    /// Interaction logic for testview.xaml
    /// </summary>
    public partial class testview : UserControl
    {

        private UserService _userservice;
        private ChatService _chatservice;

        public testview()
        {
            InitializeComponent();
            _userservice = App.AppHost.Services.GetRequiredService<UserService>();
            _chatservice = App.AppHost.Services.GetRequiredService<ChatService>();
            btnload.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private async void btnLoadClick(object sender, RoutedEventArgs e)
        {

            var users = await _userservice.GetAllUsersAsync();

            UsersList.ItemsSource = users;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_Users();
        }

        private async void Update_Users()
        {

            var curusers = await _userservice.GetAllUsersAsync();
            curusers = curusers.Where(p => p.NickName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            UsersList.ItemsSource = curusers;

        }

        private async void ShowChat(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedBtn)
            {
                Guid userBtnId = Guid.Parse(clickedBtn.Name);
                bool usersHaveChat = await _chatservice.HasChat(App.CurrentUser.Id, userBtnId);
                if (!usersHaveChat)
                {
                    UserModel? chosenUser = await _userservice.GetUserByIdAsync(userBtnId);
                    if (chosenUser != null)
                        await _chatservice.CreateChat(App.CurrentUser, chosenUser);
                }

                var chatWindow = App.AppHost.Services.GetRequiredService<ChatWindow>();
                chatWindow.ShowDialog();
            }
        }
    }
}
