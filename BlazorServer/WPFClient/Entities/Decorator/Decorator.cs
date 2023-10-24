using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPFClient.Entities.Decorator
{
    public abstract class Decorator : TextComponent
    {
        protected TextComponent _textComponent;

        public Decorator(TextComponent textComponent)
        {
            _textComponent = textComponent;
        }

        public void SetTextComponent(TextComponent textComponent)
        {
            _textComponent = textComponent;
        }

        public override Run GetFormattedText(Run run)
        {
            return _textComponent.GetFormattedText(run);
        }

        public override string GetText()
        {
            return _textComponent.GetText();
        }
    }
}
