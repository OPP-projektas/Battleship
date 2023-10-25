using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFClient.Entities.Bridge
{
    public class ColorBackground : IColorPicker
    {
        public Button PickColor(Button button, SolidColorBrush color)
        {
            button.Background = color;
            return button;
        }
    }
}
