using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace WPF.Edu.Behaviors
{
    public class MouseCaptureOnAction : TargetedTriggerAction<UIElement>
    {
        protected override void Invoke(object parameter)
        {
            if (AssociatedObject == null || Target == null)
            {
                return;
            }

            Mouse.Capture(Target, CaptureMode.SubTree);
        }
    }
}
