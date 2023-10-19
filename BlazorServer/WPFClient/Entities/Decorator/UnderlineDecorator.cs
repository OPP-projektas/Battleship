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
    public class UnderlineDecorator : TextComponent
    {
        private TextComponent component;

        public UnderlineDecorator(TextComponent component)
        {
            this.component = component;
        }

        public Run GetFormattedText()
        {
            Run formattedText = component.GetFormattedText();
            formattedText.TextDecorations = TextDecorations.Underline;
            return formattedText;
        }

        public string GetText()
        {
            return component.GetText();
        }
    }
}
