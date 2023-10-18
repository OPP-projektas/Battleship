using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Strategy
{
    class AscendingAmount : IStrategy
    {
        public object StrategicSort(object data)
        {
            var dict = data as Dictionary<TypesOfShips, int>;
            var orderedDict = dict.OrderBy(kvp => kvp.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return orderedDict;
        }
    }
}
