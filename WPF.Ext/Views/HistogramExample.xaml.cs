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
using LiveCharts;
using LiveCharts.Wpf;

namespace WPF.Ext.Views
{
    public class ItemModel
    {

        public List<double> Values { get; set; }

        public double Total => Values?.Any() == true ? Values.Sum() : 0;

        public ItemModel(params double[] values)
        {
            Values = new List<double>(values);
        }
    }

    /// <summary>
    /// Interaction logic for HistogramExample.xaml
    /// </summary>
    public partial class HistogramExampleView : UserControl
    {
        public double Total => Items?.Any() == true ? Items.SelectMany(x => x.Values).Sum() : 0;

        public HistogramExampleView()
        {
            Name = typeof(HistogramExampleView).Name;

            Items = new ObservableCollection<ItemModel>
            {
                new ItemModel(.5,1,2.22),
                new ItemModel(15,50.01,7),
                new ItemModel(.2,20,6.2),
                new ItemModel(3,1,.03),
            };

            InitializeComponent();

            Formatter = value => value.ToString("P");
        }

        public ObservableCollection<ItemModel> Items { get; set; }

        public ChartValues<double> Values1 => new ChartValues<double>(Items.SelectMany(x => x.Values));

        public ChartValues<double> Values2 => new ChartValues<double>(Items.Select(x => x.Total));

        public Func<double, string> Formatter { get; set; }
    }
}
