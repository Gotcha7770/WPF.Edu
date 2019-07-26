using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WPF.Ext.Views;

namespace WPF.Ext
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<UserControl> ViewsCollection { get; }

        public MainWindow()
        {
            ViewsCollection = new ObservableCollection<UserControl>
            {
                new TabControlView(),
                new FilterableCollectionView(),
                new CheckBoxTestView(),
                new HistogramExampleView()
            };

            InitializeComponent();
        }
    }
}
