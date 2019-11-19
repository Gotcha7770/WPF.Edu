using System.Collections.ObjectModel;
using System.Windows.Controls;
using WPF.Edu.ViewModels;

namespace WPF.Edu.Views
{
    /// <summary>
    /// Interaction logic for ResizerTest.xaml
    /// </summary>
    public partial class ResizerTest : UserControl
    {
        public ResizerTest()
        {
            Name = typeof(ResizerTest).Name;
            InitializeComponent();
        }

        public ObservableCollection<TestViewModel> Items { get; } = new ObservableCollection<TestViewModel>
        {
            new TestViewModel{ Name = "TestVM1", Width = 100, Height = 100},
            new TestViewModel{ Name = "TestVM2", Width = 200, Height = 200},
            new TestViewModel{ Name = "TestVM3", Width = 300, Height = 300}
        };
    }
}
