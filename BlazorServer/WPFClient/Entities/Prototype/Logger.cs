using System;
using System.Windows.Controls;
using System.Diagnostics;
using System.Collections.Generic;

namespace WPFClient.Entities.Prototype
{
    class Logger
    {
        private static Logger instance;
        private ListBox messageListBox;
        private Message logEntry = new Message();

        private Logger()
        {
        }

        // Public method to get the singleton instance of the Logger class
        public static Logger GetInstance()
        {
            if (instance == null)
            {
                instance = new Logger();
            }
            return instance;
        }

        // Set the ListBox control for logging
        public void SetMessageListBox(ListBox listBox)
        {
            messageListBox = listBox;
        }

        public void Log(Message message)
        {
            // Check if the ListBox is set, then add the message to it
            if (messageListBox != null)
            {
                logEntry = message.DeepCopy();
                messageListBox.Items.Add("[" + logEntry.Timestamp.ToString() + "] Username: " + logEntry.Name + " | Body: " +
                    logEntry.Content.body);
                //logEntries.Add(message);
                messageListBox.ScrollIntoView(messageListBox.Items[messageListBox.Items.Count - 1]);
            }
        }
    }
}