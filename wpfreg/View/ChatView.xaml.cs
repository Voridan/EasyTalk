using BLL.Models;
using BLL.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using wpfreg.ViewModel;


namespace wpfreg.View
{
    public partial class ChatView : UserControl
    {
        private ChatService _chatservice;
        private NavigationViewModel _navViewModel;
        private ChatModel Chat { get; set; }
        private readonly ILogger<ChatView> _logger;

        public ChatView()
        {
            InitializeComponent();
            _chatservice = App.AppHost.Services.GetRequiredService<ChatService>();
            _navViewModel = App.AppHost.Services.GetRequiredService<NavigationViewModel>();
            btnload.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            _logger = App.AppHost.Services.GetRequiredService<ILogger<ChatView>>();
        }
        private async void btnLoadClick(object sender, RoutedEventArgs e)
        {
            var chats = await _chatservice.GetChatsForUser(App.CurrentUser!.Id);
            ChatList.ItemsSource = chats;
        }

        private async void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                App.SelectedChat = item.Content as ChatModel;
                IEnumerable<UserModel> users = await _chatservice.GetUsersForChat(App.SelectedChat!.Id);
                Guid otherUserId = users!.Where(u => u.Id != App.CurrentUser.Id).FirstOrDefault().Id;
                _navViewModel.OpenChat(otherUserId);
                MsgInput.IsReadOnly = false;
                _logger.LogInformation($"User select {App.SelectedChat.Name} chat.");
            }
        }

        private void SendClick(object sender, RoutedEventArgs e)
        {
            MsgInput.Text = string.Empty;
        }
    }
}
