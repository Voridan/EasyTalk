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

        private void Home(object obj) => CurrentView = new HomeViewModel();
        private void Chat(object obj)
        {
            CurrentView = new ChatViewModel();
            string? username = App.CurrentUser?.NickName?? "tyler";
            if (username != null) App.Server.ConnectToServer(username);
        }
        private void Profile(object obj) => CurrentView = new ProfileViewModel();

        public NavigationViewModel()
        {
            HomeCommand = new RelayCommand(Home);
            ProfileCommand = new RelayCommand(Profile);
            ChatCommand = new RelayCommand(Chat);
      

            // Startup Page
            CurrentView = new HomeViewModel();
        }
    }
}
