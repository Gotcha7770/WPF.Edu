using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF.Edu.Views
{
    /// <summary>
    /// Interaction logic for FilterableCollection.xaml
    /// </summary>
    public partial class FilterableCollectionView : UserControl, INotifyPropertyChanged
    {
        private Predicate<object> _filter;
        private IComparer _comparer;
        //private readonly object _lock = new object();

        public FilterableCollectionView()
        {
            Name = typeof(FilterableCollectionView).Name;
            Items = new ObservableCollection<string>
            {
                "Text1",
                "Text2",
                "Text1",
                "Text2",
                "Text1",
                "Text3",
                "Text1",
                "Text2",
                "Text1",
                "Text2",
                "Text1",
                "Text3"
            };

            //BindingOperations.EnableCollectionSynchronization(Items, _lock);

            InitializeComponent();

            Task.Run(() =>
            {
                Thread.Sleep(4000);
                Comparer = new CustomSorter();
            });

            Task.Run(() =>
            {
                Thread.Sleep(6000);
                //Dispatcher.Invoke(() => Items.Add("Text2"));
                Items.Add("Text2");

                Thread.Sleep(2000);
                Items.Add("Text3");

                Thread.Sleep(2000);
                Items.Add("Text4");
            });

            Task.Run(() =>
            {
                Thread.Sleep(8000);
                Filter = FilterMethod;
                //Filter = x => x is string s && s == "Text1";

                Thread.Sleep(2000);
                OnPropertyChanged(nameof(Filter));

                Thread.Sleep(2000);
                OnPropertyChanged(nameof(Filter));
            });
        }

        public bool FilterMethod(object value)
        {
            return value is string s && s == "Text1";
        }

        public ObservableCollection<string> Items { get; private set; }

        public Predicate<object> Filter
        {
            get => _filter;
            set
            {
                if(_filter != value)
                {
                    _filter = value;
                    OnPropertyChanged();
                }
            }
        }

        public IComparer Comparer
        {
            get => _comparer;
            set
            {
                if (_comparer != value)
                {
                    _comparer = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CustomSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            string first = x as string;
            string second = y as string;

            return string.Compare(first, second);
        }
    }
}
