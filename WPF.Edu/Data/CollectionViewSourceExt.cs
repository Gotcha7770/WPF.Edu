using System;
using System.Collections;
using System.Windows;
using System.Windows.Data;

namespace WPF.Edu.Data
{
    public class CollectionViewSourceExt : CollectionViewSource
    {
        private object _lock = new object();

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

        public bool IsSynchronized
        {
            get { return (bool)GetValue(IsSynchronizedProperty); }
            set { SetValue(IsSynchronizedProperty, value); }
        }

        public static readonly DependencyProperty IsSynchronizedProperty = DependencyProperty.Register("IsSynchronized",
                                                                                                       typeof(bool),
                                                                                                       typeof(CollectionViewSourceExt),
                                                                                                       new PropertyMetadata(false, OnIsSynchronizedChanged));

        protected override void OnSourceChanged(object oldSource, object newSource)
        {
            if(oldSource is IEnumerable oldEnumerable && IsSynchronized)
            {
                BindingOperations.DisableCollectionSynchronization(oldEnumerable);
            }


            if(newSource is IEnumerable newEnumerable && IsSynchronized)
            {
                BindingOperations.EnableCollectionSynchronization(newEnumerable, _lock);
            }
        }

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

        private static void OnIsSynchronizedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj is CollectionViewSourceExt cvs)
            {
                cvs.OnIsSynchronizedChanged(args);
            }
        }        

        private void OnFilterDescriptionChanged(DependencyPropertyChangedEventArgs args)
        {
            if (View != null && args.NewValue is Predicate<object> predicate)
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

        private void OnIsSynchronizedChanged(DependencyPropertyChangedEventArgs args)
        {
            if(View != null && args.NewValue is bool value)
            {
                if (value)
                {
                    BindingOperations.EnableCollectionSynchronization(View.SourceCollection, _lock);
                }
                else
                {
                    BindingOperations.DisableCollectionSynchronization(View.SourceCollection);
                }
            }            
        }
    }
}
