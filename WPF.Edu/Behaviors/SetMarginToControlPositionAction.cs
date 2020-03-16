using System;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace WPF.Edu.Behaviors
{
    public class SetMarginToControlPositionAction : TargetedTriggerAction<FrameworkElement>
    {
        protected override void Invoke(object parameter)
        {
            if (!(AssociatedObject is UIElement control) || Target == null)
            {
                return;
            }

            if (Target.Margin != default)
            {
                Target.Margin = default;
                Target.Arrange(new Rect(default, Target.DesiredSize));
            }

            var point = control.TranslatePoint(new Point(0, 0), Target);

            Target.Margin = new Thickness(point.X, point.Y, 0, 0);
        }
    }
}
