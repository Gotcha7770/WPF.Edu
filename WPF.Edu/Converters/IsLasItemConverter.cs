using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF.Edu.Converters
{
    public class IsLastItemConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2
                || !(values[0] is ContentControl item)
                || !(values[1] is int count))
                return DependencyProperty.UnsetValue;

            ItemsControl owner = ItemsControl.ItemsControlFromItemContainer(item);
            if (owner is null)
                return DependencyProperty.UnsetValue;

            return owner.ItemContainerGenerator.IndexFromContainer(item) == owner.Items.Count - 1;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}