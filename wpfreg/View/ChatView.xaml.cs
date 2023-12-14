using BLL.Models;
using BLL.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfreg.View
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class ChatView : UserControl
    
    {
        private UserService _userservice;
        private ChatService _chatservice;
        private ChatModel Chat { get; set; }
        public ChatView()
        {
            InitializeComponent();
            _userservice = App.AppHost.Services.GetRequiredService<UserService>();
            _chatservice = App.AppHost.Services.GetRequiredService<ChatService>();
            btnload.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }
        private async void btnLoadClick(object sender, RoutedEventArgs e)
        {
            var chats = await _chatservice.GetChatsForUser(App.CurrentUser.Id);
            ChatList.ItemsSource = chats;
        }

      
    }
}
