using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
namespace wpfreg.View
{

    public partial class RegistrationView : Window
    {
        private UserService _userService;
        private readonly LoginView _loginView;
        private readonly MainWindow _mainwindow;
        private bool handle = true;
        public RegistrationView(UserService userservice, LoginView loginView, MainWindow mainwindow)
        {
            InitializeComponent();
            _userService = userservice;
            _loginView = loginView;
            _mainwindow = mainwindow;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void textBoxLastname_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxLastname.Text = " ";
        }



        private void passwordBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Password = " ";
        }

        private void textBoxEmailId_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxEmailId.Text = " ";
        }

        private void textBoxFirstName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxFirstname.Text = " ";
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            this.Close();
            _loginView.ShowDialog();
                
        }

        private void Register(object sender, RoutedEventArgs e)
        {

        }

        private void textBoxNickName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            textBoxNickName.Text = " ";
        }
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }

        private void Handle()
        {
            switch (cmbSelect.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Creative":
                    //Handle for the first combobox
                    break;
                case "Education":
                    //Handle for the second combobox
                    break;

            }
        }
        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmailId.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmailId.Focus();
            }
            //else if (!Regex.IsMatch(textBoxEmailId.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            //{
            //    errormessage.Text = "Enter a valid email.";
            //    textBoxEmailId.Select(0, textBoxEmailId.Text.Length);
            //    textBoxEmailId.Focus();
            //}
            else
            {
                string nickname = textBoxNickName.Text;
                string firstname = textBoxFirstname.Text;
                string lastname = textBoxLastname.Text;
                string email = textBoxEmailId.Text;
                string password = passwordBox.Password;
                UserModel user = new UserModel(nickname, firstname, lastname, email, password);
                if (passwordBox.Password.Length == 0)
                {
                    errormessage.Text = "Enter password.";
                    passwordBox.Focus();
                }
                //else if (passwordBoxConfirm.Password.Length == 0)
                //{
                //    errormessage.Text = "Enter Confirm password.";
                //    passwordBoxConfirm.Focus();
                //}
                //else if (passwordBox1.Password != passwordBoxConfirm.Password)
                //{
                //    errormessage.Text = "Confirm password must be same as password.";
                //    passwordBoxConfirm.Focus();
                //}
                else
                {
                    await _userService.RegisterUserAsync(user);
                    this.Close();
                    _mainwindow.ShowDialog();

                }
            }
        }
    }
}
