using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Ext.ViewModels;

namespace WPF.Ext.Views
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
