using WPF.Core;

namespace WPF.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand MyProjectsCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }
        public MyProjectsModel MyProjectsVM { get; set; }
        public DiscoveryViewModel DiscoveryVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            MyProjectsVM = new MyProjectsModel();
            DiscoveryVM = new DiscoveryViewModel();
            CurrentView = MyProjectsVM;

            MyProjectsCommand = new RelayCommand(o =>
            {
                CurrentView = MyProjectsVM;
            });
            DiscoveryViewCommand = new RelayCommand(o =>
            {
                CurrentView = DiscoveryVM;
            });
        }
    }
}