using BLL.Models;
using BLL.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;


namespace wpfreg.View
{
    public partial class ChatView : UserControl
    {
        private ChatService _chatservice;
        private ChatModel Chat { get; set; }
        public ChatView()
        {
            InitializeComponent();
            _chatservice = App.AppHost.Services.GetRequiredService<ChatService>();
            btnload.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }
        private async void btnLoadClick(object sender, RoutedEventArgs e)
        {
            var chats = await _chatservice.GetChatsForUser(App.CurrentUser.Id);
            ChatList.ItemsSource = chats;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                
            }
        }
    }
}
