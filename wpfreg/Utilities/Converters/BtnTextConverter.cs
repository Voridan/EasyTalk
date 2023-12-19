using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace wpfreg.Utilities.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    internal class BtnTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool textflag = (bool)value;
            string textbtn = textflag ? "save" : "edit";
            return textbtn;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string textbtn = (string)value;
            bool textflag = textbtn == "save";
            return textflag;
        }
    }
}
