using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF.Edu.Views
{
    /// <summary>
    /// Interaction logic for ComboboxTest.xaml
    /// </summary>
    public partial class ComboboxTest : UserControl
    {
        public ComboboxTest()
        {
            Name = typeof(ComboboxTest).Name;
            InitializeComponent();
        }

        public List<int> Items { get; } = Enumerable.Range(0, 50).ToList();

        private void FrameworkElement_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Mouse.Capture(sender as UIElement);
        }
    }
}
