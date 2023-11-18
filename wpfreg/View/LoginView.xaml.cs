using BLL.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DAL.Models;
using BLL.Services.Implementations;
using BLL.Utils;
using BLL.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace wpfreg.View
{
  
    public partial class LoginView : Window
    {

        private UserService _userService;
        private readonly MainWindow _mainwindow;
        //private readonly ILogger logger;

        public LoginView(UserService userservice, MainWindow mainwindow)
        {
            InitializeComponent();
            _userService = userservice;
            _mainwindow = mainwindow;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void passwordBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Password = " ";
        }
        private void textBoxNickName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxNickname.Text = " ";
        }
        private async void Login(object sender, RoutedEventArgs e)
        {
            string password = passwordBox.Password;
            string nickname = textBoxNickname.Text;
            if (nickname == "")
            {
                errormessage.Text = "Enter a nickname.";
                textBoxNickname.Focus();
            }
            else if (password == "")
            {
                errormessage.Text = "Enter a password.";
                passwordBox.Focus();
            }
            LoginUserModel userLogin = new LoginUserModel(nickname, password);
            Result<UserModel> resLogin = await _userService.LoginUserAsync(userLogin);
            if (!resLogin.IsError)
            {
                App.CurrentUser = resLogin.Value;
                this.Close();
                _mainwindow.ShowDialog();
                return;
            }
        }

        private void Signup_btn(object sender, RoutedEventArgs e)
        {
            this.Close();
            var logger = App.AppHost.Services.GetRequiredService<ILogger>();
            logger.LogInformation($"User redirected to register view");
            var registerForm = App.AppHost.Services.GetRequiredService<RegistrationView>();
            registerForm.ShowDialog();
        }
    }
}
