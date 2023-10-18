using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFClient.Entities.Prototype;

namespace WPFClient.Entities.Builder
{
    public class Product
    {
        private List<TypesOfShips> _parts = new List<TypesOfShips>();
        Logger logger = Logger.GetInstance();
        public void Add(TypesOfShips shipType)
        {
            this._parts.Add(shipType);
        }

        public Dictionary<TypesOfShips,int> ListParts()
        {

            Dictionary<TypesOfShips, int> ret = new Dictionary<TypesOfShips, int>();
            foreach (TypesOfShips enumValue in Enum.GetValues(typeof(TypesOfShips)))
            {
                ret[enumValue] = 0;
            }
            foreach (var part in this._parts)
            {
                ret[part]++;
            }
            return ret;
        } 
    }
}
