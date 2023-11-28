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
    public class FlyweightFactory
    {
        private Dictionary<string, IFlyweight> flyweights = new Dictionary<string, IFlyweight>();

        private bool Has(string key)
        {
            bool ret = false;

            foreach(var fw in flyweights.Keys)
            {
                if (fw == key)
                    ret = true;
            }
            return ret;

        }

        public IFlyweight GetFlyweight(Rectangle rect)
        {
            string key = new string($"{rect.Width}-{rect.Height}-{rect.Fill}");
            if (!Has(key))
            {
                Flyweight fw = new Flyweight(rect);
                flyweights[key] = fw;
            }

            return flyweights[key];
        }
        public List<string> ListFlyweights()
        {
            List<string> ret = new List<string>();
            foreach(var key in flyweights.Values)
            {
                
                ret.Add($"Base parameters: Width={key.Operation().Width}, Height={key.Operation().Height}, Color={key.Operation().Fill}");
            }
            return ret;
        }
    }
}
