using DynamicData;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WPF.Edu.ViewModels;

namespace WPF.Edu.Views
{
    /// <summary>
    /// Interaction logic for CheckBoxTest.xaml
    /// </summary>
    public partial class CheckBoxTestView : UserControl
    {
        public ObservableCollection<ITreeViewItemModel> Items { get; }

        public CheckBoxTestView()
        {
            Name = typeof(CheckBoxTestView).Name;

            var parent = new TreeViewItemViewModelBase("Parent", null);
            var firstLevelChild1 = new TreeViewItemViewModelBase("FirstLevelChild1", parent);
            var firstLevelChild2 = new TreeViewItemViewModelBase("FirstLevelChild2", parent);
            var secondLevelChildrens1 = new[]
            {
                new TreeViewItemViewModelBase("SecondLevelChild1", firstLevelChild1),
                new TreeViewItemViewModelBase("SecondLevelChild2", firstLevelChild1)
            };

            var secondLevelChild3 = new TreeViewItemViewModelBase("SecondLevelChild3", firstLevelChild2);
            var thirdLevelChild = new TreeViewItemViewModelBase("ThirdLevelChild", secondLevelChild3);
            secondLevelChild3.Children.Add(thirdLevelChild);

            var secondLevelChildrens2 = new[]
            {
                secondLevelChild3,
                new TreeViewItemViewModelBase("SecondLevelChild4", firstLevelChild2)
            };

            firstLevelChild1.Children.AddRange(secondLevelChildrens1);
            firstLevelChild2.Children.AddRange(secondLevelChildrens2);
            parent.Children.Add(firstLevelChild1);
            parent.Children.Add(firstLevelChild2);

            Items = new ObservableCollection<ITreeViewItemModel>
            {
                parent
            };

            InitializeComponent();
        }
    }
}
