﻿using BLL.Models;
using BLL.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using wpfreg.Net;
using wpfreg.Utilities;

namespace wpfreg.ViewModel
{
    class ChatViewModel
    {
        private Server _server;

        public ObservableCollection<UserModel> Users { get; set; } = new();

        public ObservableCollection<ChatModel> Chats { get; set; } = new();

        public ObservableCollection<string> Messages { get; set; } = new();

        public ObservableCollection<MessageModel> MessagesFromDB { get; set; }

        public List<MessageModel> MessagesToSave { get; set; } = new ();

        public RelayCommand SendMessageCommand { get; set; }

        public RelayCommand SaveMessagesCommand { get; set; }

        public RelayCommand FetchMessagesCommand { get; set; }

        public string Username { get; set; }

        public string Message { get; set; }

        private ChatService _chatService;

        public ChatViewModel()
        {
            Username = App.CurrentUser!.NickName!;
            _server = App.Server!;
            _server.msgRecieveEvent += MessageRecieved;
            _server.userDisconectEvent += RemoveUser;
            _server.connectedEvent += UserConnected;
            _chatService = App.AppHost!.Services.GetRequiredService<ChatService>();

            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message!), o => !string.IsNullOrEmpty(Message));
            SaveMessagesCommand = new RelayCommand(SaveMasseges);
            FetchMessagesCommand = new RelayCommand(FetchMasseges);
        }
        public ChatViewModel(Guid userid) {}

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
            bool s = msg.Contains("\0");
            if(s)
            {
                msg = msg.Replace("\0", "");
            }

            MessagesToSave.Add(
                    new MessageModel()
                    {
                        Id = Guid.NewGuid(),
                        Text = msg,
                        SenderId = App.CurrentUser!.Id!,
                        ChatId = App.SelectedChat!.Id!,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                    }
                );

            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg));
        }

        private void RemoveUser()
        {
            var uname = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.NickName == uname).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user!));
        }

        private async void SaveMasseges(object obj)
        {
            await _chatService.SaveMessages(App.SelectedChat!.Id, App.CurrentUser!.Id, MessagesToSave);
            MessagesToSave.Clear();
        }

        private async void FetchMasseges(object obj)
        {
            var msgs = await _chatService.GetMessages(App.SelectedChat!.Id!);
            foreach (var msg in msgs)
            {
                Messages.Add(msg.Text!);
            }
        }
    }
}
