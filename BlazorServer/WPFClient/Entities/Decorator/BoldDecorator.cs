using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFClient.Entities.Decorator
{
    public class BoldDecorator : Decorator
    {

        public BoldDecorator(TextComponent component) : base (component)
        {
        }

        public override Run GetFormattedText(Run run)
        {
            run.FontWeight = FontWeights.Bold;
            return base.GetFormattedText(run);
        }
        public string GetText()
        {
            return base.GetText();
        }
    }
}
