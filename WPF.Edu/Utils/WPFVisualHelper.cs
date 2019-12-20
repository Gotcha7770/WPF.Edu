using System;
using System.Windows;
using System.Windows.Media;

namespace WPF.Edu.Utils
{
    public static class WPFVisualHelper
    {
        public static DependencyObject FindVisualParent(this DependencyObject d, Func<DependencyObject, bool> condition)
        {
            return FindVisualParent<DependencyObject>(d, condition);
        }

        public static T FindVisualParent<T>(this DependencyObject d, Func<T, bool> condition)
            where T : DependencyObject
        {
            var item = VisualTreeHelper.GetParent(d);

            while (item != null)
            {
                var typedItem = item as T;
                if (condition != null)
                {
                    if (typedItem != null && condition(typedItem))
                        return typedItem;

                    item = VisualTreeHelper.GetParent(item);
                }
                else if (typedItem != null)
                    return typedItem;
            }

            return null;
        }
    }
}
