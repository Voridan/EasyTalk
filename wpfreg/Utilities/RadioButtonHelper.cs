using System.Windows;

namespace wpfreg.Utilities
{
    public static class RadioButtonHelper
    {
        public static readonly DependencyProperty UniqueNameProperty =
            DependencyProperty.RegisterAttached("UniqueName", typeof(string), typeof(RadioButtonHelper));

        public static void SetUniqueName(DependencyObject element, string value)
        {
            element.SetValue(UniqueNameProperty, value);
        }

        public static string GetUniqueName(DependencyObject element)
        {
            return (string)element.GetValue(UniqueNameProperty);
        }
    }
}
