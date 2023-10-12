using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Strategy
{
    class DescedingLength : IStrategy
    {
        public object StrategicSort(object data)
        {
            var dict = data as Dictionary<TypesOfShips, int>;
            Dictionary<TypesOfShips, int> enumToValueMapping = new Dictionary<TypesOfShips, int>
            {
                { TypesOfShips.Boat, 1 },
                { TypesOfShips.Battleship, 2 },
                { TypesOfShips.Submarine, 3 },
                { TypesOfShips.Carrier, 4 },
            };
            var orderedDict = dict.OrderBy(kvp => enumToValueMapping[kvp.Key]).Reverse().ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return orderedDict;
        }
    }
}
