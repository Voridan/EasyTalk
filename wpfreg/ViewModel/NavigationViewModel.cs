using BLL.Models;
using BLL.Services.Implementations;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Input;
using wpfreg.Utilities;
using wpfreg.View;

namespace wpfreg.ViewModel
{
    internal class NavigationViewModel : ViewModelBase
    {
        private object _currentView;
        
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
            var _chatservice = App.AppHost.Services.GetRequiredService<ChatService>();
           
            if (parameter is Guid id)
            {
                var chat =await _chatservice.GetChat(id);
                App.SelectedChat = chat;
                ChatInfoViewModel chatInfoViewModel = new ChatInfoViewModel();
                ChatInfo chatInfoWindow = new ChatInfo();
                chatInfoWindow.DataContext = chatInfoViewModel;

                // Show the ChatInfo window
                chatInfoWindow.Show();

            }
        }

        private async void OpenProfile(object parameter)
        {
            if(parameter is Guid id)
            {
                var usrService = App.AppHost.Services.GetRequiredService<UserService>();
                var user = await usrService.GetUserByIdAsync(id);
                UserModel usr = UserService.DALUserToBLLUser(user);
                App.SelectedUser = usr;
                CurrentView = new SearchProfileViewModel();
              
              
                
                // Show or navigate to the SearchProfileView as needed


            }
        }
        public async void OpenChat(object parameter)
        {
            if (parameter is Guid userId)
            {
                var usrService = App.AppHost.Services.GetRequiredService<UserService>();
                var currUserId = App.CurrentUser.Id;
                var chatExists = await usrService.ChatExists(currUserId, userId);
                if (chatExists)
                {
                    CurrentView = new ChatViewModel(userId);
                }
                else
                {
                    var chatService = App.AppHost.Services.GetRequiredService<ChatService>();
                    var untitledChat = new ChatModel() { Name = "untitled", Description = "None" };
                    await chatService.CreateChat(untitledChat, currUserId, userId);
                    CurrentView = new ChatViewModel(userId);
                }
            }
        }

        private void Home(object obj) => CurrentView = new HomeViewModel();
              
        private void Chat(object obj)
        {
            CurrentView = new ChatViewModel(App.CurrentUser.Id);
        }
        private void Profile(object obj) => CurrentView = new ProfileViewModel();
        private void SearchList(object obj) => CurrentView = new SearchListViewModel();
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
        }
    }
}
