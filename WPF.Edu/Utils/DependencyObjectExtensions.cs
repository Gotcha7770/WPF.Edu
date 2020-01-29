using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace WPF.Edu.Utils
{
    public static class DependencyObjectExtensions
    {
        public static T Clone<T>(this T source) where T : DependencyObject
        {
            //Argument.IsNotNull(() => source);

            T target = null;
            var objXaml = XamlWriter.Save(source);
            var stringReader = new StringReader(objXaml);
            using (var xmlReader = XmlReader.Create(stringReader))
            {
                target = (T)XamlReader.Load(xmlReader);
            }

            
            return target;
        }
    }
}
