using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Component
{
    public abstract class Componentumas
    {
        public Componentumas() { }

        public abstract int GetLength();

        public virtual void Add(Componentumas component)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove()
        {
            throw new NotImplementedException();
        }

        public virtual bool IsComposite()
        {
            return true;
        }
    }
}
