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

        public double ContentWidth
        {
            get { return (double)GetValue(ContentWidthProperty); }
            set { SetValue(ContentWidthProperty, value); }
        }

        public static readonly DependencyProperty ContentWidthProperty =
            DependencyProperty.Register("ContentWidth", typeof(double), typeof(ResizableControl), new PropertyMetadata(OnContentWidthChanged));

        private static void OnContentWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ResizableControl resizer && e.NewValue is double value)
            {
                resizer.OnContentWidthChanged(value);
            }
        }

        private void OnContentWidthChanged(double value)
        {
            if (IsInitialized)
                _contentPresenter.Width = value;
        }

        public double StartHeight
        {
            get { return (double)GetValue(StartHeightProperty); }
            set { SetValue(StartHeightProperty, value); }
        }

        public static readonly DependencyProperty StartHeightProperty =
            DependencyProperty.Register("StartHeight", typeof(double), typeof(ResizableControl), new PropertyMetadata());

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ResizableControl), new PropertyMetadata());


        static ResizableControl()
        {
            EventManager.RegisterClassHandler(typeof(ResizableControl), Thumb.DragDeltaEvent, new DragDeltaEventHandler(OnDragDelta));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentPresenter = GetTemplateChild(ContentPresenterPartName) as FrameworkElement;
            _thumb = GetTemplateChild(ThumbPartName) as FrameworkElement;

            if (ContentWidth > 0)
                _contentPresenter.Width = ContentWidth;
            if (StartHeight > 0)
                _contentPresenter.Height = StartHeight;
        }

        private static void OnDragDelta(object sender, DragDeltaEventArgs args)
        {
            (sender as ResizableControl)?.OnDragDelta(args);
        }

        private void OnDragDelta(DragDeltaEventArgs args)
        {
            if (Orientation == Orientation.Horizontal)
                _contentPresenter.Width = Math.Max(0, _contentPresenter.ActualWidth + args.HorizontalChange);
            else
                _contentPresenter.Height = Math.Max(0, _contentPresenter.ActualHeight + args.VerticalChange);
        }
    }
}
