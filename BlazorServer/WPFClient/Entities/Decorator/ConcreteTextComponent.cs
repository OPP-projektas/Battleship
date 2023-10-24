using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPFClient.Entities.Decorator
{
    class ConcreteTextComponent : TextComponent
    {
        public override Run GetFormattedText(Run run)
        {
            return run;
        }
    }
}
