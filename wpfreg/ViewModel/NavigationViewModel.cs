using BLL.Models;
using BLL.Services.Implementations;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Input;
using wpfreg.Utilities;
using wpfreg.View;

namespace wpfreg.ViewModel
{
    internal class NavigationViewModel : ViewModelBase
    {
        private object _currentView;
        private readonly ILogger<NavigationViewModel> _logger;


        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
      
        public ICommand ProfileCommand { get; set; }

        public ICommand ChatCommand { get; set; }

        public ICommand SearchistCommand { get; set; }
        
        public ICommand ChatListCommand {  get; set; }

        public ICommand ChatInfoCommand { get; set; }

        public ICommand SearchToProfileCommand { get; set; }


        private async void EditChat(object parameter)
        {
            var _chatservice = App.AppHost!.Services.GetRequiredService<ChatService>();
           
            if (parameter is Guid id)
            {
                var chat =await _chatservice.GetChat(id);
                App.SelectedChat = chat;
                ChatInfoViewModel chatInfoViewModel = new ChatInfoViewModel();
                ChatInfo chatInfoWindow = new ChatInfo();
                chatInfoWindow.DataContext = chatInfoViewModel;

                // Show the ChatInfo window
                chatInfoWindow.Show();
                _logger.LogInformation("User open ChatInfo window.");
            }
        }

        private async void OpenProfile(object parameter)
        {
            if(parameter is Guid id)
            {
                var usrService = App.AppHost!.Services.GetRequiredService<UserService>();
                var user = await usrService.GetUserByIdAsync(id);
                UserModel usr = UserService.DALUserToBLLUser(user!);
                App.SelectedUser = usr;
                CurrentView = new SearchProfileViewModel();
                // Show or navigate to the SearchProfileView as needed
                _logger.LogInformation("User open ChatInfo window.");
            }
        }

        public async void OpenChat(object parameter)
        {
            if (parameter is Guid userId)
            {
                var usrService = App.AppHost!.Services.GetRequiredService<UserService>();
                var currUserId = App.CurrentUser!.Id;
                var chatExists = await usrService.ChatExists(currUserId, userId);
                if (chatExists)
                {
                    CurrentView = new ChatViewModel(userId);
                    _logger.LogInformation("User open existed chat.");
                }
                else
                {
                    var chatService = App.AppHost.Services.GetRequiredService<ChatService>();
                    var untitledChat = new ChatModel() { Name = "untitled", Description = "None" };
                    await chatService.CreateChat(untitledChat, currUserId, userId);
                    CurrentView = new ChatViewModel(userId);
                    _logger.LogInformation("User create new chat.");
                }
            }
        }

        private void Home(object obj)
        {
            CurrentView = new HomeViewModel();
            _logger.LogInformation("User open home window.");
        }

        private void Chat(object obj)
        {
            OpenChat(App.CurrentUser.Id);
            _logger.LogInformation("User open chat window.");
        }

        private void Profile(object obj)
        {
            CurrentView = new ProfileViewModel();
            _logger.LogInformation("User open profile window.");
        }

        private void SearchList(object obj)
        {
            CurrentView = new SearchListViewModel();
            _logger.LogInformation("User open SearchList window.");
        }

        public NavigationViewModel()
        {
            HomeCommand = new RelayCommand(Home);
            ProfileCommand = new RelayCommand(Profile);
            ChatCommand = new RelayCommand(Chat);
            ChatListCommand = new RelayCommand(OpenChat);
            SearchistCommand = new RelayCommand(SearchList);
            ChatInfoCommand = new RelayCommand(EditChat);
            SearchToProfileCommand = new RelayCommand(OpenProfile);
            // Startup Page
            CurrentView = new HomeViewModel();
            _logger = App.AppHost.Services.GetRequiredService<ILogger<NavigationViewModel>>();
        }
    }
}
