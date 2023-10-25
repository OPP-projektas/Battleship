using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFClient.Entities.Bridge
{
    public class LightTheme : Theme
    {
        public LightTheme(IColorPicker colorPicker) : base(colorPicker) 
        { 
        }

        public override Button ColorButton(Button button)
        {
            return _colorPicker.PickColor(button, Brushes.Green);
        }

    }
}
