using System;
using System.Collections.Generic;
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
using ReactiveUI;

namespace WPF.Edu.Views
{
    /// <summary>
    /// Interaction logic for ThumbVsButtonView.xaml
    /// </summary>
    public partial class ThumbVsButtonView : UserControl
    {
        public ThumbVsButtonView()
        {
            Name = nameof(ThumbVsButtonView);
            InitializeComponent();

            Keyboard.AddKeyDownHandler(this, OnKeyDown);
            //Mouse.AddPreviewMouseDownOutsideCapturedElementHandler(this, OnMouseDownOutsideCapturedElement);
        }

        public ICommand FocusOnControl { get; } = ReactiveCommand.Create<UIElement>((u) =>
         {
             u.Focus();
             //u.CaptureMouse();
             //Mouse.Capture(u);
             Mouse.Capture(u, CaptureMode.SubTree);
         });

        public ICommand LostFocus { get; } = ReactiveCommand.Create(() =>
        {
            var tmp = 2;
            //Mouse.Capture(u);
        });

        private void OnMouseDownOutsideCapturedElement(object sender, MouseButtonEventArgs e)
        {
            var tmp = 2;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}
