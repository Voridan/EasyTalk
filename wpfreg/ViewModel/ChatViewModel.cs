﻿using BLL.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using wpfreg.Net;
using wpfreg.Utilities;

namespace wpfreg.ViewModel
{
    class ChatViewModel
    {
        private Guid _userid;

        private Server _server;

        public ObservableCollection<UserModel> Users { get; set; }
        
        public ObservableCollection<ChatModel> Chats { get; set; }
        
        public ObservableCollection<string> Messages { get; set; }
        
        public RelayCommand SendMessageCommand { get; set; }
        
        public string Username { get; set; }
        
        public string Message { get; set; }
        
        public ChatViewModel() 
        {
            Username = App.CurrentUser?.NickName ?? "tyler";
            Users = new ObservableCollection<UserModel>();
            Chats = new ObservableCollection<ChatModel>();
            Messages = new ObservableCollection<string>();
            _server = App.Server;
            _server.msgRecieveEvent += MessageRecieved;
            _server.userDisconectEvent += RemoveUser;
            _server.connectedEvent += UserConnected;
            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));
        }
        public ChatViewModel(Guid userid)
        {
            _userid = userid;
            Username = App.CurrentUser?.NickName??"tyler";
            Users = new ObservableCollection<UserModel>();
            Chats = new ObservableCollection<ChatModel>();
            Messages = new ObservableCollection<string>();
            _server = App.Server;
            _server.msgRecieveEvent += MessageRecieved;
            _server.userDisconectEvent += RemoveUser;
            _server.connectedEvent += UserConnected;
            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));
        }

        private void UserConnected()
        {
            var user = new UserModel
            {
                NickName = _server.PacketReader.ReadMessage()
            };
            if (!Users.Any(x => x.NickName == user.NickName))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }

        private void MessageRecieved()
        {
            var msg = _server.PacketReader.ReadMessage();
            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg));
        }
        
        private void RemoveUser()
        {
            var uname = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.NickName == uname).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
        }
    }
}
