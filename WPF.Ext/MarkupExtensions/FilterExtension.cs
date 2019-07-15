using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using WPF.Ext.Interfaces;

namespace WPF.Ext.MarkupExtensions
{
    [ContentProperty("Filters")]
    class FilterExtension : MarkupExtension
    {
        private readonly Collection<IFilter> _filters = new Collection<IFilter>();

        public ICollection<IFilter> Filters => _filters;
        //public ICollection<IFilter> Filters { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new FilterEventHandler((s, e) =>
            {
                foreach (var filter in Filters)
                {
                    var res = filter.Filter(e.Item);
                    if (!res)
                    {
                        e.Accepted = false;
                        return;
                    }
                }
                e.Accepted = true;
            });
        }
    }
}
