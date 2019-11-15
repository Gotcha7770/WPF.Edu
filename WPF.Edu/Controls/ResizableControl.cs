using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WPF.Edu.Controls
{
    [TemplatePart(Name = ContentPresenterPartName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = ThumbPartName, Type = typeof(Thumb))]
    public class ResizableControl : ContentControl
    {

        public const string ContentPresenterPartName = "PART_ContentPresenter";
        public const string ThumbPartName = "PART_Thumb";

        protected FrameworkElement _contentPresenter;
        protected FrameworkElement _thumb;

        static ResizableControl()
        {
            EventManager.RegisterClassHandler(typeof(ResizableControl), Thumb.DragStartedEvent, new DragStartedEventHandler(ResizableControl.OnDragStarted));
            EventManager.RegisterClassHandler(typeof(ResizableControl), Thumb.DragDeltaEvent, new DragDeltaEventHandler(ResizableControl.OnDragDelta));
            EventManager.RegisterClassHandler(typeof(ResizableControl), Thumb.DragCompletedEvent, new DragCompletedEventHandler(ResizableControl.OnDragCompleted));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentPresenter = GetTemplateChild(ContentPresenterPartName) as FrameworkElement;
            _thumb = GetTemplateChild(ThumbPartName) as FrameworkElement;
        }

        private static void OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
