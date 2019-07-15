using System.Windows;
using WPF.Ext.Interfaces;

namespace WPF.Ext.MarkupExtensions
{
    public class PropertyFilter : DependencyObject, IFilter
    {
        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register("PropertyName", typeof(string), typeof(PropertyFilter), new UIPropertyMetadata(null));

        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(PropertyFilter), new UIPropertyMetadata(null));
        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public bool Filter(object item)
        {
            var type = item.GetType();
            var itemValue = type.GetProperty(PropertyName).GetValue(item, null);

            return (object.Equals(itemValue, Value));            
        }
    }
}
