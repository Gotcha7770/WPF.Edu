using System;
using System.Windows.Markup;

namespace WPF.Edu.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(bool))]
    public class TrueExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider) => true;
    }
}