using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;

namespace WPF.Edu.Data
{
    public class CollectionViewSourceExt : CollectionViewSource
    {        
        public Predicate<object> FilterDescription
        {
            get { return (Predicate<object>)GetValue(FilterDescriptionProperty); }
            set { SetValue(FilterDescriptionProperty, value); }
        }

        public static readonly DependencyProperty FilterDescriptionProperty = DependencyProperty.Register("FilterDescription",
                                                                                                          typeof(Predicate<object>),
                                                                                                          typeof(CollectionViewSourceExt),
                                                                                                          new PropertyMetadata(null, OnFilterDescriptionChanged));

        public IComparer OrderDescription
        {
            get { return (IComparer)GetValue(OrderDescriptionProperty); }
            set { SetValue(OrderDescriptionProperty, value); }
        }

        public static readonly DependencyProperty OrderDescriptionProperty = DependencyProperty.Register("OrderDescription",
                                                                                                   typeof(IComparer),
                                                                                                   typeof(CollectionViewSourceExt),
                                                                                                   new PropertyMetadata(null, OnOrderDescriptionChanged));
        
        private static void OnFilterDescriptionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj is CollectionViewSourceExt cvs)
            {
                cvs.OnFilterDescriptionChanged(args);
            }
        }

        private static void OnOrderDescriptionChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj is CollectionViewSourceExt cvs)
            {
                cvs.OnOrderDescriptionChanged(args);
            }
        }

        private void OnFilterDescriptionChanged(DependencyPropertyChangedEventArgs args)
        {
            if (args.NewValue is Predicate<object> predicate)
            {
                View.Filter = predicate;
                View.Refresh();
            }
        }

        private void OnOrderDescriptionChanged(DependencyPropertyChangedEventArgs args)
        {
            if (View is ListCollectionView listView && args.NewValue is IComparer comparer)
            {
                listView.CustomSort = comparer;
            }
        }
    }
}
