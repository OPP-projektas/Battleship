using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFClient.Entities.ChainOfResponsability
{
    public abstract class AbstractHandler : IHandler
    {
        protected int level = -1;
        public AbstractHandler(int level)
        {
            this.level = level;
        }

        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;

            return handler;
        }

        public virtual TextBlock Handle(TextBlock request, int toCheckLevel)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request, toCheckLevel);
            }
            else
            {
                return null;
            }
        }
    }
}
