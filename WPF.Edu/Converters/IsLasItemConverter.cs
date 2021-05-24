using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF.Edu.Converters
{
    public class IsLastItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DependencyObject obj && ItemsControl.ItemsControlFromItemContainer(obj) is { } owner)
            {
                return owner.ItemContainerGenerator.IndexFromContainer(obj) == owner.Items.Count - 1;
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}