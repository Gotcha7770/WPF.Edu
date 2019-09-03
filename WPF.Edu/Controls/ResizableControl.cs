using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace WPF.Edu.Controls
{
    [TemplatePart(Name = ThumbPartName, Type = typeof(Thumb))]
    public class ResizableControl : ContentControl
    {
        public const string ThumbPartName = "PART_Thumb";

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}
