using BLL.Models;
using System;

namespace wpfreg.ViewModel
{
    internal class ProfileViewModel : Utilities.ViewModelBase
    {
        public Guid ProfileId { get; set; }
        public UserModel curUser = App.CurrentUser!;

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

        private bool _hasPhoto = false;

        public bool HasPhoto
        {
            get { return _hasPhoto; }
            set
            {
                if (_hasPhoto != value)
                {
                    _hasPhoto = value;
                    OnPropertyChanged(nameof(HasPhoto));
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
                    HasPhoto = (_userPhoto != null);
                }
            }
        }


        public ProfileViewModel(UserModel user)
        {
            UserNickname = user.NickName!;
            UserName = user.FirstName!;
            UserLastName = user.LastName!;
            UserEmail = user.Email!;
            UserPhoto = user.Photo!;

        }
        public ProfileViewModel()
        {
            UserNickname = curUser.NickName!;
            UserName = curUser.FirstName!;
            UserLastName = curUser.LastName!;
            UserEmail = curUser.Email!;
            UserPhoto = curUser.Photo!;
        }
    }
}
