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

namespace Wpfnew.View
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        private bool handle = true;
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
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
                case "1":
                    //Handle for the first combobox
                    break;
                case "2":
                    //Handle for the second combobox
                    break;
               
            }
        }
    }
}
