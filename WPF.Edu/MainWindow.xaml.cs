using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ReactiveUI;
using WPF.Edu.Views;

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
                new HistogramExampleView(),
                new ControlsTestView(),
                new ResizerTest(),
                new CommandTest(),
                new ThumbVsButtonView(),
                new DynamicListViewTest()
            };

            DeleteCommand = ReactiveCommand.Create<object>(parameter => CommandBinding_Executed(parameter, null));

            InitializeComponent();
        }

        public ICommand DeleteCommand { get; }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            bool executed = true;
        }
    }
}
