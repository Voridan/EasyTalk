using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfreg.Net;
using wpfreg.Utilities;

namespace wpfreg.ViewModel
{
     class ChatViewModel
    {
        public RelayCommand ConnectToServerCommand { get; set; }
        public string Username { get; set; }
        private Server _server;
        public ChatViewModel()
        {
            _server = new Server();
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(Username), o => !string.IsNullOrEmpty(Username));
        }

           
    }
}
