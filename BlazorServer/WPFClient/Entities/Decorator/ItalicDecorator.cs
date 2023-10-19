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
    public class ItalicDecorator : TextComponent
    {
        private TextComponent component;

        public ItalicDecorator(TextComponent component)
        {
            this.component = component;
        }

        public Run GetFormattedText()
        {
            Run formattedText = component.GetFormattedText();
            formattedText.FontStyle = FontStyles.Italic;
            return formattedText;
        }

        public string GetText()
        {
            return component.GetText();
        }
    }
}
