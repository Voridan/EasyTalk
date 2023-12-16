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
using BLL.Utils;
using wpfreg.ViewModel;

namespace wpfreg.View
{

    public partial class RegistrationView : Window
    {
        private UserService _userService;
        private readonly LoginView _loginView;
        private readonly MainWindow _mainwindow;
        private bool handle = true;
        private string nickNamePlaceholder = "NickName";
        private string firstNamePlaceholder = "FirstName";
        private string lastNamePlaceholder = "LastName";
        private string passwordPlaceholder = "Password";
        private string emailPlaceholder = "Email";
        private Industries SelectedIndustry { get; set; }
        
        public RegistrationView(UserService userservice, LoginView loginView, MainWindow mainwindow)
        {
            InitializeComponent();
            _userService = userservice;
            _loginView = loginView;
            _mainwindow = mainwindow;
            var enumValues = Enum.GetValues(typeof(BLL.Models.Industries));
            cmbSelect.ItemsSource = enumValues;

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
            if (textBoxLastname.Text == lastNamePlaceholder)
            {
                textBoxLastname.Text = "";
            }
            
        }



        private void textBoxPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBoxPassword.Text == passwordPlaceholder)
            {
                textBoxPassword.Text = "";
            }
        }

        private void textBoxEmailId_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBoxEmailId.Text == emailPlaceholder) 
            {
                textBoxEmailId.Text = "";
            }
        }

        private void textBoxFirstName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBoxFirstname.Text == firstNamePlaceholder) 
            {
                textBoxFirstname.Text = "";
            }
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
            if (textBoxNickName.Text == nickNamePlaceholder)
            {
                textBoxNickName.Text = "";
            }
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
            ((RegistrationViewModel)DataContext).SelectedIndustry = (Industries)cmbSelect.SelectedItem;
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
               
                textBoxEmailId.Focus();
            }
           
            else
            {
                string nickname = textBoxNickName.Text;
                
                string firstname = textBoxFirstname.Text;
                string lastname = textBoxLastname.Text;
                string email = textBoxEmailId.Text;
                string password = textBoxPassword.Text;
                UserModel user = new UserModel(Guid.Empty, nickname, firstname, lastname, email, password);
                if (textBoxPassword.Text.Length == 0)
                {
                   
                    textBoxPassword.Focus();
                }
               
                else
                {
                    Result<UserModel> resRegister = await _userService.RegisterUserAsync(user);
                    errorMes.Text = resRegister.Message;
                    if (!resRegister.IsError)
                    {
                        App.CurrentUser = resRegister.Value;
                        App.Server.ConnectToServer(App.CurrentUser);
                        this.Close();
                        _mainwindow.ShowDialog();
                        return;
                    }
                }
            }
        }
    }
}
