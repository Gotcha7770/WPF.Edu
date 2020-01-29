using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF.Edu.Views
{
    /// <summary>
    /// Interaction logic for ViewToFreezeApp.xaml
    /// </summary>
    public partial class ViewToFreezeApp : UserControl
    {
        private object _lock = new object();

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
                Application.Current.Dispatcher.Invoke(() => Background = new SolidColorBrush(Colors.Black));
            }
        }
    }
}
