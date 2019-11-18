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
            EventManager.RegisterClassHandler(typeof(ResizableControl), Thumb.DragStartedEvent, new DragStartedEventHandler(OnDragStarted));
            EventManager.RegisterClassHandler(typeof(ResizableControl), Thumb.DragDeltaEvent, new DragDeltaEventHandler(OnDragDelta));
            EventManager.RegisterClassHandler(typeof(ResizableControl), Thumb.DragCompletedEvent, new DragCompletedEventHandler(OnDragCompleted));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentPresenter = GetTemplateChild(ContentPresenterPartName) as FrameworkElement;
            _thumb = GetTemplateChild(ThumbPartName) as FrameworkElement;
        }

        private static void OnDragCompleted(object sender, DragCompletedEventArgs args)
        {
            //(sender as ResizableControl)?.OnDragCompleted(args);
        }

        private static void OnDragDelta(object sender, DragDeltaEventArgs args)
        {
            (sender as ResizableControl)?.OnDragDelta(args);
        }

        private void OnDragDelta(DragDeltaEventArgs args)
        {
            _contentPresenter.Width = Math.Max(0, _contentPresenter.ActualWidth + args.HorizontalChange);
        }

        private static void OnDragStarted(object sender, DragStartedEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
