using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using wpfreg.Model;
using wpfreg.Net;
using wpfreg.Utilities;

namespace wpfreg.ViewModel
{
     class ChatViewModel
    {

        public ObservableCollection<UserModel> Users { get; set; }  
        public RelayCommand ConnectToServerCommand { get; set; }
        public string Username { get; set; }
        private Server _server;
        public ChatViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            _server = new Server();
            _server.connectedEvent += UserConnected;
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));
        }

           private void UserConnected()
        {
            var user = new UserModel
            {
                UserName = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage()
            };
            if(!Users.Any(x => x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(()=> Users.Add(user));
            }
        }
    }
}
