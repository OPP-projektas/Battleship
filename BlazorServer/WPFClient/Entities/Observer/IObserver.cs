using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Observer
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }
}
