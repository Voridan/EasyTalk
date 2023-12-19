using BLL.Models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfreg.ViewModel
{
    internal class SearchProfileViewModel : Utilities.ViewModelBase
    {
        public Guid ProfileId { get; set; }
        public UserModel user = App.SelectedUser!;

        private string _userNickname;
        public string UserNickname
        {
            get { return _userNickname; }
            set
            {
                if (_userNickname != value)
                {
                    _userNickname = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _userLastName;
        public string UserLastName
        {
            get { return _userLastName; }
            set
            {
                if (_userLastName != value)
                {
                    _userLastName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _userEmail;
        public string UserEmail
        {
            get { return _userEmail; }
            set
            {
                if (_userEmail != value)
                {
                    _userEmail = value;
                    OnPropertyChanged();
                }
            }
        }

        private byte[] _userPhoto;
        public byte[] UserPhoto
        {
            get { return _userPhoto; }
            set
            {
                if (_userPhoto != value)
                {
                    _userPhoto = value;
                    OnPropertyChanged();
                }
            }
        }


     
        public SearchProfileViewModel()
        {
            UserNickname = user.NickName!;
            UserName = user.FirstName!;
            UserLastName = user.LastName!;
            UserEmail = user.Email!;
            UserPhoto = user.Photo!;
        }
    }
}
