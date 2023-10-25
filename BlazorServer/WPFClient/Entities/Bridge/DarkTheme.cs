using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFClient.Entities.Bridge
{
    public class DarkTheme : Theme
    {
        public DarkTheme(IColorPicker colorPicker) : base(colorPicker)
        {
        }

        public override Button ColorButton(Button button)
        {
            return _colorPicker.PickColor(button, Brushes.DarkRed);
        }

    }
}
