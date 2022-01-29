using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
using WPF.Edu.Utils;

namespace WPF.Edu.Behaviors
{
    public class DataPipeBehavior : Behavior<FrameworkElement>
    {
        private DependencyPropertyDescriptor _descriptor;

        public Type FromType { get; set; }

        public string FromProperty { get; set; }

        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target",
            typeof(object),
            typeof(DataPipeBehavior),
            new FrameworkPropertyMetadata());

        public object Target
        {
            get { return GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        protected override void OnAttached()
        {
            if (string.IsNullOrEmpty(FromProperty))
                return;

            var type = AssociatedObject.GetType();

            _descriptor = FromType is not null
                ? DependencyPropertyDescriptor.FromName(FromProperty, FromType, type)
                : DependencyPropertyExtensions.GetDependencyPropertyDescriptor(type, FromProperty);

            _descriptor.AddValueChanged(AssociatedObject, OnSourceValueChanged);
        }

        protected override void OnDetaching()
        {
            _descriptor?.RemoveValueChanged(AssociatedObject, OnSourceValueChanged);
        }

        private void OnSourceValueChanged(object sender, EventArgs e)
        {
            Target = _descriptor.GetValue(AssociatedObject);
        }
    }
}
