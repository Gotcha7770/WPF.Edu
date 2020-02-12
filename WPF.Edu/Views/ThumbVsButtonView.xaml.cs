using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
