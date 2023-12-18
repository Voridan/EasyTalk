using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BLL.Models;
using BLL.Services.Implementations;
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

        public RegistrationView(UserService userservice, LoginView loginView, MainWindow mainwindow)
        {
            InitializeComponent();
            _userService = userservice;
            _loginView = loginView;
            _mainwindow = mainwindow;
            var enumValues = Enum.GetValues(typeof(BLL.Models.Industries));
            cmbSelect.ItemsSource = enumValues;
        }

        private Industries SelectedIndustry { get; set; }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void textBoxLastname_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBoxLastname.Text == lastNamePlaceholder)
            {
                textBoxLastname.Text = string.Empty;
            }

            if (textBoxNickName.Text == string.Empty)
            {
                textBoxNickName.Text = nickNamePlaceholder;
            }

            if (textBoxPassword.Text == string.Empty)
            {
                textBoxPassword.Text = passwordPlaceholder;
            }

            if (textBoxFirstname.Text == string.Empty)
            {
                textBoxFirstname.Text = firstNamePlaceholder;
            }

            if (textBoxEmailId.Text == string.Empty)
            {
                textBoxEmailId.Text = emailPlaceholder;
            }
        }

        private void textBoxPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBoxPassword.Text == passwordPlaceholder)
            {
                textBoxPassword.Text = string.Empty;
            }

            if (textBoxNickName.Text == string.Empty)
            {
                textBoxNickName.Text = nickNamePlaceholder;
            }

            if (textBoxLastname.Text == string.Empty)
            {
                textBoxLastname.Text = lastNamePlaceholder;
            }

            if (textBoxFirstname.Text == string.Empty)
            {
                textBoxFirstname.Text = firstNamePlaceholder;
            }

            if (textBoxEmailId.Text == string.Empty)
            {
                textBoxEmailId.Text = emailPlaceholder;
            }
        }

        private void textBoxEmailId_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBoxEmailId.Text == emailPlaceholder)
            {
                textBoxEmailId.Text = string.Empty;
            }

            if (textBoxNickName.Text == string.Empty)
            {
                textBoxNickName.Text = nickNamePlaceholder;
            }

            if (textBoxPassword.Text == string.Empty)
            {
                textBoxPassword.Text = passwordPlaceholder;
            }

            if (textBoxFirstname.Text == string.Empty)
            {
                textBoxFirstname.Text = firstNamePlaceholder;
            }

            if (textBoxLastname.Text == string.Empty)
            {
                textBoxLastname.Text = lastNamePlaceholder;
            }
        }

        private void textBoxFirstName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBoxFirstname.Text == firstNamePlaceholder)
            {
                textBoxFirstname.Text = string.Empty;
            }

            if (textBoxLastname.Text == string.Empty)
            {
                textBoxLastname.Text = lastNamePlaceholder;
            }

            if (textBoxNickName.Text == string.Empty)
            {
                textBoxNickName.Text = nickNamePlaceholder;
            }

            if (textBoxPassword.Text == string.Empty)
            {
                textBoxPassword.Text = passwordPlaceholder;
            }

            if (textBoxEmailId.Text == string.Empty)
            {
                textBoxEmailId.Text = emailPlaceholder;
            }
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            this.Close();
            _loginView.ShowDialog();
        }

        private void textBoxNickName_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBoxNickName.Text == nickNamePlaceholder)
            {
                textBoxNickName.Text = string.Empty;
            }

            if (textBoxLastname.Text == string.Empty)
            {
                textBoxLastname.Text = lastNamePlaceholder;
            }

            if (textBoxPassword.Text == string.Empty)
            {
                textBoxPassword.Text = passwordPlaceholder;
            }

            if (textBoxFirstname.Text == string.Empty)
            {
                textBoxFirstname.Text = firstNamePlaceholder;
            }

            if (textBoxEmailId.Text == string.Empty)
            {
                textBoxEmailId.Text = emailPlaceholder;
            }
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle)
            {
                Handle();
            }

            handle = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox? cmb = sender as ComboBox;
            handle = !cmb!.IsDropDownOpen;
            ((RegistrationViewModel)DataContext).SelectedIndustry = (Industries)cmbSelect.SelectedItem;
            Handle();
        }

        private void Handle()
        {
            switch (cmbSelect.SelectedItem?.ToString()?.Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Creative":
                    break;
                case "Education":
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
                        App.CurrentUser = resRegister.Value!;
                        App.Server.ConnectToServer(App.CurrentUser);
                        this.Close();
                        _mainwindow.ShowDialog();
                        return;
                    }
                }
            }
        }

        private void textBoxFirstname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxFirstname.Text == firstNamePlaceholder)
            {
                textBoxFirstname.Text = string.Empty;
            }
        }

        private void textBoxFirstname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxFirstname.Text == string.Empty)
            {
                textBoxFirstname.Text = firstNamePlaceholder;
            }
        }

        private void textBoxNickName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxNickName.Text == nickNamePlaceholder)
            {
                textBoxNickName.Text = string.Empty;
            }
        }

        private void textBoxNickName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxNickName.Text == string.Empty)
            {
                textBoxNickName.Text = nickNamePlaceholder;
            }
        }

        private void textBoxLastname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxLastname.Text == lastNamePlaceholder)
            {
                textBoxLastname.Text = string.Empty;
            }
        }

        private void textBoxLastname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxLastname.Text == string.Empty)
            {
                textBoxLastname.Text = lastNamePlaceholder;
            }
        }

        private void textBoxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxPassword.Text == passwordPlaceholder)
            {
                textBoxPassword.Text = string.Empty;
            }
        }

        private void textBoxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxPassword.Text == string.Empty)
            {
                textBoxPassword.Text = passwordPlaceholder;
            }
        }

        private void textBoxEmailId_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxEmailId.Text == emailPlaceholder)
            {
                textBoxEmailId.Text = string.Empty;
            }
        }

        private void textBoxEmailId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxEmailId.Text == string.Empty)
            {
                textBoxEmailId.Text = emailPlaceholder;
            }
        }
    }
}
