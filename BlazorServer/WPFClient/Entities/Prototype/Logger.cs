using System;
using System.Windows.Controls;
using System.Diagnostics;
using System.Collections.Generic;
using WPFClient.Entities.Decorator;
using System.Windows.Media;
using Microsoft.Extensions.Logging;
using System.Windows.Documents;

namespace WPFClient.Entities.Prototype
{
    class Logger
    {
        private static Logger instance;
        private ListBox messageListBox;
        private Message logEntry = new Message();
        private TextComponent textComponent;
        public bool cbBold = false;
        public bool cbItalic = false;
        public bool cbUnderline = false;
        private Logger()
        {
        }

        public static Logger GetInstance()
        {
            if (instance == null)
            {
                instance = new Logger();
            }
            return instance;
        }

        public void SetMessageListBox(ListBox listBox)
        {
            messageListBox = listBox;
        }
        public void SetTextComponent(TextComponent textComponent)
        {
            this.textComponent = textComponent;
        }
        private TextBlock SetTextComponent(string text)
        {
            TextComponent decorator = new PlainText(text);
            if (cbBold)
            {
                decorator = new BoldDecorator(decorator);
            }
            if (cbItalic)
            {
                decorator = new ItalicDecorator(decorator);
            }
            if (cbUnderline)
            {
                decorator = new UnderlineDecorator(decorator);
            }
            TextBlock block = new TextBlock();
            block.Inlines.Add(decorator.GetFormattedText());
            return block;
        }
        public void Log(Message message)
        {
            if (messageListBox != null)
            {
                //PROTOTYPE
                logEntry = message.DeepCopy();

                string plainText = "[" + logEntry.Timestamp.ToString() + "] Username: " + logEntry.Name + " | Body: " + logEntry.Content.body;

                //DECORATOR
                TextBlock formattedText = SetTextComponent(plainText);

                messageListBox.Items.Add(formattedText);
                //messageListBox.Items.Add(formattedText);
                messageListBox.ScrollIntoView(messageListBox.Items[messageListBox.Items.Count - 1]);
            }
        }

    }
}