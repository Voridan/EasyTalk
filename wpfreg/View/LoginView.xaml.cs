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
using BLL.Models;
using BLL.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace wpfreg.View
{
  
    public partial class LoginView : Window
    {

        private UserService _userService;
        private readonly MainWindow _mainwindow;
        private string nickNamePlaceholder = "Nickname";
        private string passwordPlaceholder = "Password";
        
      
        public LoginView(UserService userservice, MainWindow mainwindow)
        {
            InitializeComponent();
            _userService = userservice;
            _mainwindow = mainwindow;
            
        }
        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

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
            if (passwordBox.Password == passwordPlaceholder)
            {
                passwordBox.Password = "";
            }
            if (textBoxNickname.Text == "")
            {
                textBoxNickname.Text = nickNamePlaceholder;
            }
        }
        private void textBoxNickName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBoxNickname.Text == nickNamePlaceholder)
            {
                textBoxNickname.Text = "";
            }
            if (passwordBox.Password == "")
            {
                passwordBox.Password = passwordPlaceholder;
            }
        }
        private async void Login(object sender, RoutedEventArgs e)
        {
            string password = passwordBox.Password;
            string nickname = textBoxNickname.Text;
           
            if (nickname == "")
            {
                
                textBoxNickname.Focus();
            }
            else if (password == "")
            {
                
                passwordBox.Focus();
            }
            LoginUserModel userLogin = new LoginUserModel(nickname, password);

            Result<UserModel> resLogin = await _userService.LoginUserAsync(userLogin);

            errorMes.Text = resLogin.Message;
            
            if (!resLogin.IsError)
            {
                App.CurrentUser = resLogin.Value;
                if (App.CurrentUser?.NickName != null) App.Server.ConnectToServer(App.CurrentUser);
                this.Close();
                _mainwindow.ShowDialog();
                return;
            }
        }

        private void Signup_btn(object sender, RoutedEventArgs e)
        {
            this.Close();
            var registerForm = App.AppHost.Services.GetRequiredService<RegistrationView>();
             registerForm.ShowDialog();
        }

        private void textBoxNickname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxNickname.Text == nickNamePlaceholder)
            {
                textBoxNickname.Text = "";
            }
        }

        private void textBoxNickname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxNickname.Text == "")
            {
                textBoxNickname.Text = nickNamePlaceholder;
            }
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == passwordPlaceholder)
            {
                passwordBox.Password = "";
            }
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "")
            {
                passwordBox.Password = passwordPlaceholder;
            }
        }

    }
}
