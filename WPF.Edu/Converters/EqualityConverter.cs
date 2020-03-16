using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF.Edu.Converters
{
    public class EqualityConverter : IValueConverter
    {
        public bool IsComparedByReference { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsComparedByReference ? ReferenceEquals(value, parameter) : value == parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
