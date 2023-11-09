using BLL.Services.Implementations;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.MVVM.ViewModel
{
    internal class RegisterViewModel : ViewModelBase
    {
        private string _username;
    private string _password;
    private string errorMessage;
    private bool _isViewVisible = true;
    UserService _userService;
    public string Username
    {
        get
        {
            return _username;
        }
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }
    public string Password
    {
        get { return _password; }
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }
    public string ErrorMessage
    {
        get
        {
            return errorMessage;
        }
        set
        {
            errorMessage = value;
            OnPropertyChanged(nameof(ErrorMessage));
        }
    }
    public bool IsViewVisible
    {
        get
        {
            return _isViewVisible;
        }
        set
        {
            _isViewVisible = value;
            OnPropertyChanged(nameof(IsViewVisible));
        }
    }


    public ICommand RegisterCommand { get; }
    
    public ICommand ShowPasswordCommand { get; }
    public ICommand RememberPasswordCommand { get; }


    public RegisterViewModel()
    {
        RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
       
        
    }

    private async void ExecuteRegisterCommand(object obj)
    {
        LoginUser user = new LoginUser() { NickName = _username, Password = _password };
        await _userService.LoginUserAsync(user);
    }
    private bool CanExecuteRegisterCommand(object obj)
    {
        bool validData;

        if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 || Password == null || Password.Length < 3)
            validData = false;
        else validData = true;
        return validData;
    }
    private void ExecuteRecoverPassCommand(string username, string email)
    {
        throw new NotImplementedException();
    }
}
}
