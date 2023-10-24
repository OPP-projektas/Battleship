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
            TextComponent textComponent = new ConcreteTextComponent();
            if (cbBold)
            {
                textComponent = new BoldDecorator(textComponent);
            }
            if (cbItalic)
            {
                textComponent = new ItalicDecorator(textComponent);
            }
            if (cbUnderline)
            {
                textComponent = new UnderlineDecorator(textComponent);
            }
            Run run = new Run(text);
            Run formattedRun = textComponent.GetFormattedText(run);
            TextBlock block = new TextBlock();
            block.Inlines.Add(formattedRun);
            return block;
        }
        public void Log(IPrototype message)
        {
            if (messageListBox != null)
            {
                //PROTOTYPE
                logEntry = (Message)message.DeepCopy();

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