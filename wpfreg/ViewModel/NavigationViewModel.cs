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

        private void Home(object obj) => CurrentView = new HomeViewModel();
        
      
        private void Chat(object obj)
        {
            CurrentView = new ChatViewModel();
            
        }
        private void Profile(object obj) => CurrentView = new ProfileViewModel();
        private void SearchList(object obj) => CurrentView = new SearchListViewModel();
        public NavigationViewModel()
        {
            HomeCommand = new RelayCommand(Home);
            ProfileCommand = new RelayCommand(Profile);
            ChatCommand = new RelayCommand(Chat);

            SearchistCommand = new RelayCommand(SearchList);
           
            // Startup Page
            CurrentView = new HomeViewModel();
        }
    }
}
