using BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace wpfreg.ViewModel
{
    internal class RegistrationViewModel : INotifyPropertyChanged
    {
        private Industries _selectedIndustry;

        public Industries SelectedIndustry
        {
            get { return _selectedIndustry; }
            set
            {
                if (_selectedIndustry != value)
                {
                    _selectedIndustry = value;
                    OnPropertyChanged(nameof(SelectedIndustry));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}