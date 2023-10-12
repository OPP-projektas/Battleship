using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Observer
{
    public class Subject : ISubject
    {
        Logger logger = Logger.GetInstance();
        public bool State { get; set; } = false;

        private List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            logger.Log($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            logger.Log($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            this._observers.Remove(observer);
        }

        public void Notify()
        {
            logger.Log($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            foreach(var observer in _observers)
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
