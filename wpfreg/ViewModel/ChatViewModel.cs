using BLL.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BLL.Services.Implementations;
using DAL.Models;
using Microsoft.Extensions.DependencyInjection;
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
        
        public ObservableCollection<MessageModel> Messages { get; set; }

        public ObservableCollection<MessageModel> MessagesFromDB { get; set; }

        public ObservableCollection<MessageModel> MessagesToSave { get; set; }

        public RelayCommand SendMessageCommand { get; set; }

        public RelayCommand SaveMessagesCommand { get; set; }

        public string Username { get; set; }
        
        public string Message { get; set; }

        private ChatService _chatService;
        
        public ChatViewModel() 
        {
            Username = App.CurrentUser?.NickName ?? "tyler";
            Users = new ObservableCollection<UserModel>();
            Chats = new ObservableCollection<ChatModel>();
            Messages = new ObservableCollection<MessageModel>();
            _server = App.Server;
            _server.msgRecieveEvent += MessageRecieved;
            _server.userDisconectEvent += RemoveUser;
            _server.connectedEvent += UserConnected;
            _chatService = App.AppHost.Services.GetRequiredService<ChatService>();
            SendMessageCommand = new RelayCommand(o =>
            {
                var messagemodel = new MessageModel() { Text = Message, SenderId = App.CurrentUser.Id, ChatId = App.SelectedChat.Id};
                _server.SendMessageToServer(messagemodel);
            }, o => !string.IsNullOrEmpty(Message));
            SaveMessagesCommand = new RelayCommand(SaveMasseges);

        }
        public ChatViewModel(Guid userid)
        {
            _userid = userid;
            Username = App.CurrentUser?.NickName??"tyler";
            Users = new ObservableCollection<UserModel>();
            Chats = new ObservableCollection<ChatModel>();
            Messages = new ObservableCollection<MessageModel>();
            _server = App.Server;
            _server.msgRecieveEvent += MessageRecieved;
            _server.userDisconectEvent += RemoveUser;
            _server.connectedEvent += UserConnected;
            _chatService = App.AppHost.Services.GetRequiredService<ChatService>();
            SendMessageCommand = new RelayCommand(o =>
            {
                var messagemodel = new MessageModel() { Text = Message, SenderId = App.CurrentUser.Id, ChatId = App.SelectedChat.Id };
                _server.SendMessageToServer(messagemodel);
            }, o => !string.IsNullOrEmpty(Message));
            SaveMessagesCommand = new RelayCommand(SaveMasseges);
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
            var messageModel = MessageModel.Deserialize(msg);
            Application.Current.Dispatcher.Invoke(() => Messages.Add(messageModel));
            Application.Current.Dispatcher.Invoke(() => MessagesToSave.Add(messageModel));
        }

        private void RemoveUser()
        {
            var uname = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.NickName == uname).FirstOrDefault();
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
        }

        private void SaveMasseges(object obj)
        {
            Task.Run(async () => { await _chatService.SaveMessages(App.SelectedChat.Id, App.CurrentUser.Id, MessagesToSave); });
        }
    }
}
