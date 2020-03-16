using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace WPF.Edu.Behaviors
{
    public class FocusOnAction : TargetedTriggerAction<UIElement>
    {
        protected override void Invoke(object parameter)
        {
            if (AssociatedObject == null || Target == null)
            {
                return;
            }

            FocusManager.SetFocusedElement(Target, Target as IInputElement);
        }
    }
}
