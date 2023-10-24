using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFClient.Entities.Decorator
{
    public class ItalicDecorator : Decorator
    {

        public ItalicDecorator(TextComponent component) : base(component)
        {
        }

        public override Run GetFormattedText(Run run)
        {
            run.FontStyle = FontStyles.Italic;
            return base.GetFormattedText(run);
        }
        public string GetText()
        {
            return base.GetText();
        }
    }
}
