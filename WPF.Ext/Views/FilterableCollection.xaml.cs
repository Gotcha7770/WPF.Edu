using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WPF.Ext.Views
{
    /// <summary>
    /// Interaction logic for FilterableCollection.xaml
    /// </summary>
    public partial class FilterableCollectionView : UserControl
    {

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

            InitializeComponent();
        }

        public ObservableCollection<string> Items { get; private set; }
    }
}
