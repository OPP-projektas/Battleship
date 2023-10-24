using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Observer;

namespace WPFClient.Entities.Command
{
    public class ReadyCommand : ICommand
    {
        private Subject _subject;
        private ConcreteObserver _observer;
        public ReadyCommand(Subject subject, ConcreteObserver observer) 
        {
            _subject = subject;
            _observer = observer;
            _subject.Attach(_observer);
        }
        public void Execute()
        {
            if (_subject != null && _observer != null)
                _subject.Ready();
        }
        public void Undo()
        {
            if (_subject != null && _observer != null)
                _subject.UnReady();
        }
    }
}
