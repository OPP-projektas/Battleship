using System;
using System.Windows.Controls;
using System.Diagnostics;
using System.Collections.Generic;
using WPFClient.Entities.Decorator;
using System.Windows.Media;
using Microsoft.Extensions.Logging;
using System.Windows.Documents;
using WPFClient.Entities.ChainOfResponsability;

namespace WPFClient.Entities.Prototype
{
    class Logger
    {
        private static Logger instance;
        private ListBox messageListBox;
        private Message logEntry = new Message();
        private Message logEntryShallow = new Message();
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
        public void Log(IPrototype message, int level = 1)
        {
            if (messageListBox != null)
            {
                //PROTOTYPE
                logEntry = (Message)message.DeepCopy();
                //logEntryShallow = (Message)message.ShallowCopy();

                string plainText = "DeepCopy HASHCODE =" + logEntry.GetHashCode() + "[" + logEntry.Timestamp.ToString() + "] Username: " + logEntry.Name + " | Body: " + logEntry.Content.body;
                //string plainTextOG = "OG HASHCODE ="+message.GetHashCode()+"[" + logEntry.Timestamp.ToString() + "] Username: " + logEntry.Name + " | Body: " + logEntry.Content.body;
                //string plainTextShallow = "ShallowCopy HASHCODE ="+logEntryShallow.GetHashCode()+"[" + logEntry.Timestamp.ToString() + "] Username: " + logEntry.Name + " | Body: " + logEntry.Content.body;

                //DECORATOR
                RedHandler red = new RedHandler(4);
                WhiteHandler white = new WhiteHandler(2);
                BlackHandler black = new BlackHandler(1);
                PurpleHandler purple = new PurpleHandler(3);
                red.SetNext(white).SetNext(black).SetNext(purple);

                TextBlock formattedText = SetTextComponent(plainText);

                TextBlock coloredText = red.Handle(formattedText, level);

                //TextBlock formattedTextOG = SetTextComponent(plainTextOG);
                //TextBlock formattedTextShallow = SetTextComponent(plainTextShallow);

                messageListBox.Items.Add(coloredText);
                //messageListBox.Items.Add(formattedTextOG);
                //messageListBox.Items.Add(formattedTextShallow);
                messageListBox.ScrollIntoView(messageListBox.Items[messageListBox.Items.Count - 1]);
            }
        }

        public void ClearLogger()
        {           
            messageListBox.Items.Clear();
        }

    }
}