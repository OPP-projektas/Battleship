using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFClient.Entities.Decorator
{
    public class PlainText : TextComponent
    {
        string text;
        public PlainText(string text)
        {
            this.text = text;
        }
        public Run GetFormattedText()
        {
            Run formattedText = new Run();
            formattedText.Text = GetText();
            return formattedText;
        }
        public string GetText()
        {
            return text;
        }
    }
}
