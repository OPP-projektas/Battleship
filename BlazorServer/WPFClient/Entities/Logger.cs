using System;
using System.Windows.Controls;
using System.Diagnostics;
using System.Collections.Generic;

namespace WPFClient.Entities
{
    class Logger
    {
        private static Logger instance;
        private ListBox messageListBox;
        private List<string> logEntries = new List<string>();

        private Logger()
        {
        }

        // Public method to get the singleton instance of the Logger class
        public static Logger GetInstance()
        {           
            if (instance == null)
            {
                Debug.WriteLine(" Logger created");
                instance = new Logger();
            }
            return instance;
        }

        // Set the ListBox control for logging
        public void SetMessageListBox(ListBox listBox)
        {
            messageListBox = listBox;
            foreach (string entry in logEntries)
            {
                messageListBox.Items.Add(entry);
            }
        }

        // Log a message and add it to the ListBox
        public void Log(string message)
        {

            // Check if the ListBox is set, then add the message to it
            if (messageListBox != null)
            {
                messageListBox.Items.Add(message);
                logEntries.Add(message);
                messageListBox.ScrollIntoView(messageListBox.Items[messageListBox.Items.Count - 1]);
            }
        }
    }
}