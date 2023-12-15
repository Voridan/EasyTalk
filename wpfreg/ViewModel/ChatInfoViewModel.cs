using BLL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Primitives;

namespace wpfreg.ViewModel
{

    internal class ChatInfoViewModel : Utilities.ViewModelBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    App.SelectedChat.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    App.SelectedChat.Description = value;
                    OnPropertyChanged();
                }
            }
        }
        public ChatModel Chat { get; set; }

        
        public ChatInfoViewModel()
        {
            Chat =App.SelectedChat;
            Title = App.SelectedChat.Name;
            Description = App.SelectedChat.Description;

        }
  


    }
}
