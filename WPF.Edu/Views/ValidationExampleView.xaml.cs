using System.Windows.Controls;
using WPF.Edu.ViewModels;

namespace WPF.Edu.Views
{
    public partial class ValidationExampleView : UserControl
    {
        public ValidationExampleView()
        {
            Name = nameof(ValidationExampleView);

            InitializeComponent();
            DataContext = new ValidationExampleViewModel();
        }
    }
}