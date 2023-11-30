using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.Services;
using BLL.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using wpfreg.ViewModel;
namespace wpfreg.View
{
    /// <summary>
    /// Interaction logic for testview.xaml
    /// </summary>
    public partial class testview : UserControl
    {
      
        private UserService _userservice;

        public testview( )
        {
            InitializeComponent();
            _userservice = App.AppHost.Services.GetRequiredService<UserService>();
            btnload.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private async void btnLoadClick(object sender, RoutedEventArgs e)
        {
            
            var users = await _userservice.GetAllUsersAsync();
            
            UsersList.ItemsSource = users;
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update_Users();
        }

        private async void Update_Users()
        {
           
            var curusers = await _userservice.GetAllUsersAsync();
            curusers= curusers.Where(p=>p.NickName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            UsersList.ItemsSource = curusers; 
                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
