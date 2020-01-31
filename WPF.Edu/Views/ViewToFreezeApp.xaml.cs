using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ReactiveUI;

namespace WPF.Edu.Views
{
    /// <summary>
    /// Interaction logic for ViewToFreezeApp.xaml
    /// </summary>
    public partial class ViewToFreezeApp : UserControl
    {
        private object _lock = new object();

        private static readonly Func<IScheduler, Control, IDisposable> _func = (s, c) =>
        {
            c.Background = new SolidColorBrush(Colors.Black);

            return Disposable.Empty;
        };

        public ViewToFreezeApp()
        {
            Name = typeof(ViewToFreezeApp).Name;

            InitializeComponent();
            Loaded += ViewToFreezeApp_Loaded;
        }

        private void ViewToFreezeApp_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(BackgroundAction);

            Thread.Sleep(50);

            lock (_lock)
            {
                var tmp = Background;
            }
        }

        private void BackgroundAction()
        {
            lock (_lock)
            {
                Thread.Sleep(100);
                RxApp.MainThreadScheduler.Schedule(this, (sd, st) => Background = new SolidColorBrush(Colors.Black));
                //Application.Current.Dispatcher.Invoke(() => Background = new SolidColorBrush(Colors.Black));

            }
        }
    }
}
