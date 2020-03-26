using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF.Edu.ViewModels;

namespace WPF.Edu.Views
{
    /// <summary>
    /// Interaction logic for DynamicDataExampleView.xaml
    /// </summary>
    public partial class DynamicDataExampleView : UserControl
    {
        DynamicDataExampleViewModel ViewModel { get; } = new DynamicDataExampleViewModel();

        public DynamicDataExampleView()
        {
            Name = nameof(DynamicDataExampleView);

            InitializeComponent();
            DataContext = ViewModel;

            Loaded += DynamicDataExampleView_Loaded;
        }

        private void DynamicDataExampleView_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                AddColor("Red");
                AddColor("Blue");
                AddColor("Green");
                AddColor("Yellow");
                Remove("Red");
            });
        }        

        private void AddColor(string value)
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));

            ViewModel.Add(value);
        }

        private void Remove(string value)
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));

            ViewModel.Remove(value);
        }
    }
}
