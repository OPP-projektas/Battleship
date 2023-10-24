using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Prototype
{
    public interface IPrototype
    {
        IPrototype ShallowCopy();
        IPrototype DeepCopy();
    }
}
