using System;
using System.ComponentModel;
using System.Windows;
using Microsoft.Xaml.Behaviors;
using WPF.Edu.Utils;

namespace WPF.Edu.Behaviors
{
    public class DataPipeBehavior : Behavior<FrameworkElement>
    {
        public string Source { get; set; }

        public object Target
        {
            get { return GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target",
                                        typeof(object),
                                        typeof(DataPipeBehavior),
                                        new FrameworkPropertyMetadata());
        private Type _associatedType;
        private DependencyPropertyDescriptor _descriptor;

        protected override void OnAttached()
        {
            if (string.IsNullOrEmpty(Source))
                return;

            _associatedType = AssociatedObject.GetType();
            _descriptor = DependencyPropertyExtensions.GetDependencyPropertyDescriptor(_associatedType, Source);
            _descriptor.AddValueChanged(AssociatedObject, OnSourceValueChanged);
        }

        protected override void OnDetaching()
        {
            if (_descriptor != null)
                _descriptor.RemoveValueChanged(AssociatedObject, OnSourceValueChanged);
        }

        private void OnSourceValueChanged(object sender, EventArgs e)
        {
            var value = _descriptor.GetValue(AssociatedObject);
            Target = value;
        }
    }
}
