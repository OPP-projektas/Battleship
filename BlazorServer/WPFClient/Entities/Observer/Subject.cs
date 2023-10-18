using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Prototype;

namespace WPFClient.Entities.Observer
{
    public class Subject : ISubject
    {
        Logger logger = Logger.GetInstance();
        public bool State { get; set; } = false;

        private List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);
            this._observers.Remove(observer);
        }

        public void Notify()
        {
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);
            foreach (var observer in _observers)
            {
                observer.Update(this);
            }         
        }

        public void Ready()
        {
            this.State = true;
            this.Notify();
        }
    }
}
