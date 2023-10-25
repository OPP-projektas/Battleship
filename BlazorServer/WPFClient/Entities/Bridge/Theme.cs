using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFClient.Entities.Bridge
{
    public abstract class Theme
    {
        protected IColorPicker _colorPicker;
        protected Theme(IColorPicker colorPicker)
        {
            this._colorPicker = colorPicker ?? throw new ArgumentNullException(nameof(colorPicker));
        }

        public abstract Button ColorButton(Button button);
        //public abstract void Resize();
    }
}
