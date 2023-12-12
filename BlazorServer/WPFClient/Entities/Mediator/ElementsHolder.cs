using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFClient.Entities.Mediator
{
    public class ElementsHolder : BaseComponent
    {
        private List<object> _elements = new List<object>();

        public ElementsHolder() { }

        public void Add(object element)
        {
            this._elements.Add(element);
        }

        public void EnableAll()
        {
            foreach (var element in _elements)
            {
                Button btn = element as Button;
                Label lbl = element as Label;
                CheckBox cb = element as CheckBox;
                ComboBox cmBox = element as ComboBox;

                if (btn != null)
                {
                    btn.IsEnabled = true;
                    continue;
                }
                if (lbl != null)
                {
                    lbl.IsEnabled = true;
                    continue;
                }
                if (cb != null)
                {
                    cb.IsEnabled = true;
                    continue;
                }
                if (cmBox != null)
                {
                    cmBox.IsEnabled = true;
                }
            }
        }
        public void DisableAll()
        {
            foreach (var element in _elements)
            {
                Button btn = element as Button;
                Label lbl = element as Label;
                CheckBox cb = element as CheckBox;
                ComboBox cmBox = element as ComboBox;

                if (btn != null)
                {
                    btn.IsEnabled = false;
                    continue;
                }
                if (lbl != null)
                {
                    lbl.IsEnabled = false;
                    continue;
                }
                if (cb != null)
                {
                    cb.IsEnabled = false;
                    continue;
                }
                if (cmBox != null)
                {
                    cmBox.IsEnabled = false;
                }
            }
        }
    }
}
