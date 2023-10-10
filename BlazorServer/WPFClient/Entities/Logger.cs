using System;
using System.Windows.Controls;
using System.Diagnostics;

namespace WPFClient.Entities
{
    class Logger
    {
        private static Logger instance;
        private ListBox messageListBox;

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
        }

        // Log a message and add it to the ListBox
        public void Log(string message)
        {
            // Implement your logging logic here
            Console.WriteLine($"Log: {message}");

            // Check if the ListBox is set, then add the message to it
            if (messageListBox != null)
            {
                messageListBox.Items.Add(message);
                messageListBox.ScrollIntoView(messageListBox.Items[messageListBox.Items.Count - 1]);
            }
        }
    }
}