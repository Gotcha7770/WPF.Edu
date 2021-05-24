using System;
using System.Windows.Markup;

namespace WPF.Edu.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(string))]
    public class TypeNameExtension : MarkupExtension
    {
        public Type Type { get; set; }

        public bool FullName { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return FullName ? Type.FullName : Type.Name;
        }
    }
}