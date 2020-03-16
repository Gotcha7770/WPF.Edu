using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace WPF.Edu.Converters
{
    public class CompositeConverter : List<ConverterContext>, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == DependencyProperty.UnsetValue)
                return value;

            return this.Aggregate(value, (current, context) => context.Converter.Convert(current, targetType, context.Parameter, culture));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Reverse<ConverterContext>()
                .Aggregate(value, (current, context) => context.Converter.ConvertBack(current, targetType, context.Parameter, culture));
        }
    }

    public class ConverterContext
    {
        public IValueConverter Converter { get; set; }

        public object Parameter { get; set; }
    }
}
