using System.Windows;
using System.Windows.Controls;

namespace WPF.Edu.Controls
{
    public enum BadgePlacementMode
    {
        TopLeft,
        TopRight,
        BottomRight,
        BottomLeft,
    }

    [TemplatePart(Name = BadgeContainerPartName, Type = typeof(UIElement))]
    public class Badged : ContentControl
    {
        public const string BadgeContainerPartName = "PART_BadgeContainer";
        protected FrameworkElement _badgeContainer;

        public static readonly DependencyProperty BadgeProperty = DependencyProperty.Register("Badge",
                                                                                              typeof(object),
                                                                                              typeof(Badged),
                                                                                              new FrameworkPropertyMetadata(default(object), FrameworkPropertyMetadataOptions.AffectsArrange, OnBadgeChanged));

        public object Badge
        {
            get { return (object)GetValue(BadgeProperty); }
            set { SetValue(BadgeProperty, value); }
        }

        public static readonly DependencyProperty BadgePlacementModeProperty = DependencyProperty.Register("BadgePlacementMode",
                                                                                                           typeof(BadgePlacementMode),
                                                                                                           typeof(Badged),
                                                                                                           new PropertyMetadata(default(BadgePlacementMode)));

        public BadgePlacementMode BadgePlacementMode
        {
            get { return (BadgePlacementMode)GetValue(BadgePlacementModeProperty); }
            set { SetValue(BadgePlacementModeProperty, value); }
        }

        public static readonly RoutedEvent BadgeChangedEvent = EventManager.RegisterRoutedEvent("BadgeChanged",
                                                                                                RoutingStrategy.Bubble,
                                                                                                typeof(RoutedPropertyChangedEventHandler<object>),
                                                                                                typeof(Badged));

        public event RoutedPropertyChangedEventHandler<object> BadgeChanged
        {
            add { AddHandler(BadgeChangedEvent, value); }
            remove { RemoveHandler(BadgeChangedEvent, value); }
        }

        private static readonly DependencyPropertyKey IsBadgeSetPropertyKey = DependencyProperty.RegisterReadOnly("IsBadgeSet",
                                                                                                                  typeof(bool),
                                                                                                                  typeof(Badged),
                                                                                                                  new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsBadgeSetProperty = IsBadgeSetPropertyKey.DependencyProperty;

        public bool IsBadgeSet
        {
            get { return (bool)GetValue(IsBadgeSetProperty); }
            private set { SetValue(IsBadgeSetPropertyKey, value); }
        }
        
        private static void OnBadgeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (Badged)d;

            instance.IsBadgeSet = !string.IsNullOrWhiteSpace(e.NewValue as string) || (e.NewValue != null && !(e.NewValue is string));

            var args = new RoutedPropertyChangedEventArgs<object>(e.OldValue, e.NewValue) { RoutedEvent = BadgeChangedEvent };
            instance.RaiseEvent(args);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _badgeContainer = GetTemplateChild(BadgeContainerPartName) as FrameworkElement;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var result = base.ArrangeOverride(arrangeBounds);

            if (_badgeContainer == null)
                return result;

            var containerDesiredSize = _badgeContainer.DesiredSize;
            if ((containerDesiredSize.Width <= 0.0 || containerDesiredSize.Height <= 0.0)
                && !double.IsNaN(_badgeContainer.ActualWidth) && !double.IsInfinity(_badgeContainer.ActualWidth)
                && !double.IsNaN(_badgeContainer.ActualHeight) && !double.IsInfinity(_badgeContainer.ActualHeight))
            {
                containerDesiredSize = new Size(_badgeContainer.ActualWidth, _badgeContainer.ActualHeight);
            }

            var h = 0 - containerDesiredSize.Width / 2;
            var v = 0 - containerDesiredSize.Height / 2;
            _badgeContainer.Margin = new Thickness(0);
            _badgeContainer.Margin = new Thickness(h, v, h, v);

            return result;
        }
    }
}
