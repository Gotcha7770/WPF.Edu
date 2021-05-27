using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace WPF.Edu.Behaviors
{
    public class HandleEventBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register("EventName",
            typeof(string),
            typeof(HandleEventBehavior),
            new PropertyMetadata(default(string)));

        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source",
            typeof(object),
            typeof(HandleEventBehavior),
            new PropertyMetadata(default(object)));

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            //var sourceType = DependencyObjectType.FromSystemType(Source.GetType());
            var sourceType = Source.GetType();
            //var targetType = DependencyObjectType.FromSystemType(AssociatedObject.GetType());
            var targetType = AssociatedObject.GetType();

            var sourceEvent = EventManager.GetRoutedEvents()
                .Where(x => sourceType.IsSubclassOf(x.OwnerType))
                .FirstOrDefault(x => x.Name == EventName);

            if (sourceEvent != null)
            {
                var eventInfo = sourceEvent.OwnerType.GetEvent(EventName);
                eventInfo.RemoveEventHandler(Source, GetEventHandler(sourceType, eventInfo));
                eventInfo.AddEventHandler(Source, GetEventHandler(targetType, eventInfo));
            }
        }

        private Delegate GetEventHandler(Type ownerType, EventInfo eventInfo)
        {
            var delegateParameters = eventInfo.EventHandlerType.GetMethod("Invoke")
                .GetParametersTypes();

            MethodInfo eventHandler = ownerType.GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .FirstOrDefault(x => x.GetParametersTypes().SequenceEqual(delegateParameters));

            return Delegate.CreateDelegate(eventInfo.EventHandlerType, eventHandler);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }
    }

    public static class MethodHelper
    {
        public static Type[] GetParametersTypes(this MethodInfo method)
        {
            return  method.GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();
        }
    }
}