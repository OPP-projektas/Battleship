using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFClient.Entities.Decorator
{
    public abstract class TextComponent
    {
        public abstract Run GetFormattedText(Run run);
    }
}
