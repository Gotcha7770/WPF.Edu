using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace WPF.Edu.Views
{
    /// <summary>
    /// Interaction logic for DynamicListViewTest.xaml
    /// </summary>
    public partial class DynamicListViewTest : UserControl, INotifyPropertyChanged
    {
        private bool _templateApplied;

        public DynamicListViewTest()
        {
            Name = nameof(DynamicListViewTest);
            InitializeComponent();
        }

        public ObservableCollection<DateTime> Items { get; } = new ObservableCollection<DateTime>
        {
                     new DateTime(1993, 1, 1, 12, 22, 2),
                     new DateTime(1993, 1, 2, 13, 2, 1),
                     new DateTime(1993, 1, 3, 2, 1, 6),
                     new DateTime(1993, 1, 4, 13, 6, 55),
                     new DateTime(1993, 2, 1, 12, 22, 2),
                     new DateTime(1993, 2, 2, 13, 2, 1),
                     new DateTime(1993, 2, 3, 2, 1, 6),
                     new DateTime(1993, 2, 4, 13, 6, 55),
                     new DateTime(1993, 3, 1, 12, 22, 2),
                     new DateTime(1993, 3, 2, 13, 2, 1),
                     new DateTime(1993, 3, 3, 2, 1, 6),
                     new DateTime(1993, 3, 4, 13, 6, 55)
        };

        public bool TemplateApplied
        {
            get => _templateApplied;
            set
            {
                if(_templateApplied != value)
                {
                    _templateApplied = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
