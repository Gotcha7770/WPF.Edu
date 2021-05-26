using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Reactive.Linq;
using System.Windows.Media;
using DynamicData;
using ReactiveUI;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace WPF.Edu.ViewModels
{
    public class DynamicDataExampleViewModel : ReactiveObject
    {
        private readonly SourceCache<string, int> _sourceCache;
        private readonly IDisposable _disposable;
        private ReadOnlyObservableCollection<Brush> _brushes;

        public DynamicDataExampleViewModel()
        {
            _sourceCache = new SourceCache<string, int>(x => x.GetHashCode());

            _disposable = _sourceCache.Connect()
                .ObserveOnDispatcher()
                .Transform(x => (Brush)new SolidColorBrush((Color)ColorConverter.ConvertFromString(x)))
                .StartWithItem(CreateDrawingBrush(), 123)
                .Bind(out _brushes)
                .Subscribe();
        }

        private Brush CreateDrawingBrush()
        {
            var result = new DrawingBrush();

            var group = new DrawingGroup();
            var background = new ImageDrawing();
            //var tile = new

            // Bitmap bmp = new Bitmap(100, 100);
            // Graphics g = Graphics.FromImage(bmp);
            // g.Clear(Color.Green);

            //group.Children.Add();


            //result.Drawing = group;

            return result;
        }

        public ReadOnlyObservableCollection<Brush> Brushes
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
