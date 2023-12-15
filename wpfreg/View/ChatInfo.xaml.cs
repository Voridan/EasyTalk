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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using wpfreg.ViewModel;

namespace wpfreg.View
{
    public partial class ChatInfo : Window
    {
       
        
        private ChatService _chatservice;

        private ChatInfoViewModel _chatinfoviewModel;
        public ChatInfo()
        {
           
            InitializeComponent();
            _chatservice = App.AppHost.Services.GetRequiredService<ChatService>();
            _chatinfoviewModel = new ChatInfoViewModel();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            TBtitle.IsReadOnly =!TBtitle.IsReadOnly;
            TBdesc.IsReadOnly =!TBdesc.IsReadOnly;
            
        }

        private async void save_Click(object sender, RoutedEventArgs e)
        {
            await _chatservice.UpdateChat(App.SelectedChat);
            this.Close();
        }
    }
}
