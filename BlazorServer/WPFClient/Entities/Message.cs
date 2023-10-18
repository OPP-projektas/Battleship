using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities
{
    class Message
    {
        public string Name = "";
        public Content Content = new Content();
        public DateTime Timestamp = DateTime.Now;

        public void SetMessage(string message)
        {
            Content.body = message;
        }

        public Message ShallowCopy()
        {
            return (Message)this.MemberwiseClone();
        }

        public Message DeepCopy()
        {
            Message clone = (Message)this.MemberwiseClone();
            clone.Name = String.Copy(Name);
            clone.Timestamp = this.Timestamp;
            clone.Content = new Content(Content.body);
            return clone;
        }
    }

    class Content
    {
        public string body = "";

        public Content() { }
            
        public Content(string body)
        {
            this.body = body;
        }
    }
}
