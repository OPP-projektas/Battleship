using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using WPFClient.Components;

namespace WPFClient.Entities.Flyweight
{
    public interface IFlyweight
    {
        Rectangle Operation();
    }
}
