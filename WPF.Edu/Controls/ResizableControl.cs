using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using WPF.Edu.Utils;

namespace WPF.Edu.Controls
{
    [TemplatePart(Name = ContentPresenterPartName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = ThumbPartName, Type = typeof(Thumb))]
    public class ResizableControl : ContentControl
    {
        private Point _startPoint;

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
            DependencyProperty.Register("ContentWidth",
                                        typeof(double),
                                        typeof(ResizableControl),
                                        new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnContentWidthChanged));

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

        public double ContentHeight
        {
            get { return (double)GetValue(ContentHeightProperty); }
            set { SetValue(ContentHeightProperty, value); }
        }

        public static readonly DependencyProperty ContentHeightProperty =
            DependencyProperty.Register("ContentHeight", typeof(double), typeof(ResizableControl), new FrameworkPropertyMetadata(OnContentHightChanged));

        private static void OnContentHightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ResizableControl resizer && e.NewValue is double value)
            {
                resizer.OnContentHightChanged(value);
            }
        }

        private void OnContentHightChanged(double value)
        {
            if (IsInitialized)
                _contentPresenter.Height = value;
        }

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
            EventManager.RegisterClassHandler(typeof(ResizableControl), Thumb.DragStartedEvent, new DragStartedEventHandler(OnDragStarted));
        }

        private static void OnDragStarted(object sender, DragStartedEventArgs args)
        {
            (sender as ResizableControl)?.OnDragStarted();
        }

        private void OnDragStarted()
        {
            _startPoint = PointToScreen(Mouse.GetPosition(this));
        }

        private static void OnDragDelta(object sender, DragDeltaEventArgs args)
        {
            (sender as ResizableControl)?.OnDragDelta();
        }

        private void OnDragDelta()
        {
            var currentPoint = PointToScreen(Mouse.GetPosition(this));

            if (Orientation == Orientation.Horizontal)
                _contentPresenter.Width = Math.Max(0, _contentPresenter.ActualWidth + (currentPoint.X - _startPoint.X));
            else
                _contentPresenter.Height = Math.Max(0, _contentPresenter.ActualHeight + (currentPoint.Y - _startPoint.Y));

            _startPoint = currentPoint;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _contentPresenter = GetTemplateChild(ContentPresenterPartName) as FrameworkElement;
            _thumb = GetTemplateChild(ThumbPartName) as FrameworkElement;

            var widthDescriptor = DependencyPropertyExtensions.FromProperty<ContentControl>(ActualWidthProperty);
            widthDescriptor.AddValueChanged(_contentPresenter, OnContentActualWidthChanged);

            if (ContentWidth > 0)
                _contentPresenter.Width = ContentWidth;
            if (ContentHeight > 0)
                _contentPresenter.Height = ContentHeight;
        }

        protected override void OnTemplateChanged(ControlTemplate oldTemplate, ControlTemplate newTemplate)
        {
            base.OnTemplateChanged(oldTemplate, newTemplate);
        }

        private void OnContentActualWidthChanged(object sender, EventArgs e)
        {           
            var binding = BindingOperations.GetBindingExpression(this, ContentWidthProperty);
            if (binding != null) //BindingMode == TwoWay???
            {
                ContentWidth = _contentPresenter.ActualWidth;
            }            
        }        
    }
}
