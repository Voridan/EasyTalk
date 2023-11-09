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

namespace wpfreg.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    /// 

  
    public partial class LoginView : Window
    {

        private UserService _userService;
        private readonly MainWindow _mainwindow;

       
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
        private void Login(object sender, RoutedEventArgs e)
        {
            string password = passwordBox.Password;
            string nickname = textBoxNickname.Text;
            LoginUserModel userLogin = new LoginUserModel(nickname, password);
            Task<Result> res = _userService.LoginUserAsync(userLogin);
            if(!res.Result.IsError)
            {
                this.Close();
                _mainwindow.ShowDialog();

            }

        }

        //private void Signup_btn(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //    _registrationview.ShowDialog();
        //}
    }
}
