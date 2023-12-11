using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpfreg.Utilities;

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
        private ICommand _chatCommand;
        public ICommand ChatListCommand {  get; set; }

        private void OpenChat(object parameter)
        {
            if (parameter is Guid userId)
            {
                // Navigate to ChatView with the specified user ID
                CurrentView = new ChatViewModel(userId);
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
           
            // Startup Page
            CurrentView = new HomeViewModel();
        }
    }
}
