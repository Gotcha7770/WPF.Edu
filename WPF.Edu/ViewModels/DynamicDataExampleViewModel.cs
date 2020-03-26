using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Media;
using DynamicData;
using ReactiveUI;

namespace WPF.Edu.ViewModels
{
    public class DynamicDataExampleViewModel : ReactiveObject
    {
        private readonly SourceCache<string, int> _sourceCache;
        private readonly IDisposable _disposable;
        private ReadOnlyObservableCollection<SolidColorBrush> _brushes;

        public DynamicDataExampleViewModel()
        {
            _sourceCache = new SourceCache<string, int>(x => x.GetHashCode());

            _disposable = _sourceCache.Connect()
                .ObserveOnDispatcher()
                .Transform(x => new SolidColorBrush((Color)ColorConverter.ConvertFromString(x)))                
                .Bind(out _brushes)
                .Subscribe();            
        }        

        public ReadOnlyObservableCollection<SolidColorBrush> Brushes
        {
            get => _brushes;
        }

        internal void Add(string value)
        {
            _sourceCache.AddOrUpdate(value);
        }

        internal void Remove(string value)
        {
            _sourceCache.Remove(value.GetHashCode());
        }
    }
}
