using System;
using System.ComponentModel;
using System.Windows;

namespace WPF.Edu.Utils
{
    public static class DependencyPropertyExtensions
    {
        public static DependencyProperty GetDependencyPropertyByName(this DependencyObject obj, string name)
        {
            return GetDependencyPropertyDescriptor(obj.GetType(), name).DependencyProperty;
        }

        public static DependencyPropertyDescriptor GetDependencyPropertyDescriptor(Type objType, string name)
        {
            return DependencyPropertyDescriptor.FromName(name, objType, objType);
        }
    }
}
