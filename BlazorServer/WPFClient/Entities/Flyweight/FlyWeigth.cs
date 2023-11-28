using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using WPFClient.Components;

namespace WPFClient.Entities.Flyweight
{
    public class Flyweight : IFlyweight
    {
        private Rectangle state;
        public Flyweight(Rectangle state)
        {
            this.state = state;
        }
        Rectangle IFlyweight.Operation()
        {
            return state;
        }
    }
}
