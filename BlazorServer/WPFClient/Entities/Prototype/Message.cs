using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Prototype
{
    class Message : IPrototype
    {
        public string Name = Player.Username;
        public Content Content = new Content();
        public DateTime Timestamp = DateTime.Now;

        public void SetMessage(string message)
        {
            Content.body = message;
        }
        public IPrototype DeepCopy()
        {
            Message clone = (Message)MemberwiseClone();
            clone.Name = string.Copy(Name);
            clone.Timestamp = Timestamp;
            clone.Content = new Content(Content.body);
            return clone;
        }       
        public IPrototype ShallowCopy()
        {
            return (Message)MemberwiseClone();
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
