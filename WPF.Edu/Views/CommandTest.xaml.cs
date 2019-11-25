using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ReactiveUI;
using WPF.Edu.ViewModels;

namespace WPF.Edu.Views
{
    /// <summary>
    /// Interaction logic for ContextMenuTest.xaml
    /// </summary>
    public partial class CommandTest : UserControl
    {
        public CommandTest()
        {
            Name = typeof(CommandTest).Name;
            DeleteCommand = ReactiveCommand.Create<object>(parameter => CommandBinding_Executed(parameter, null));

            var generator = Enumerable.Range(0, 25).Select(x => new TestViewModel { Name = $"TestVM{x}" });
            Items = new ObservableCollection<TestViewModel>(generator);

            InitializeComponent();
        }

        public TestViewModel FirstSelectedElement { get; set; }

        public TestViewModel SecondSelectedElement { get; set; }

        public ObservableCollection<TestViewModel> Items { get; }

        public ICommand DeleteCommand { get; }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            if(FirstSelectedElement != null)
            {
                Items.Remove(FirstSelectedElement);
            }

            if(SecondSelectedElement != null)
            {
                Items.Remove(SecondSelectedElement);
            }

            //if(args != null && !args.Handled)
            //    ApplicationCommands.Delete.Execute(null, Parent as UIElement);
        }
    }
}
