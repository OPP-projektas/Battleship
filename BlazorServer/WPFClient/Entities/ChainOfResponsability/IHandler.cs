using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFClient.Entities.ChainOfResponsability
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        TextBlock Handle(TextBlock request, int toCheckLevel);
    }
}
