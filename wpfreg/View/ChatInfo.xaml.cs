using BLL.Models;
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

namespace wpfreg.View
{
    /// <summary>
    /// Interaction logic for ChatInfo.xaml
    /// </summary>
    public partial class ChatInfo : Window
    {
   
      
        public ChatInfo()
        {
            InitializeComponent();
 
        }

        private void editTitle_Click(object sender, RoutedEventArgs e)
        {
            TBtitle.IsReadOnly =!TBtitle.IsReadOnly;
        }

        private void editDesc_Click(object sender, RoutedEventArgs e)
        {
            TBdesc.IsReadOnly=!TBdesc.IsReadOnly;
        }
    }
}
