using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Builder;

namespace WPFClient.Entities
{
    public static class Helper
    {
        public static Dictionary<TypesOfShips, int> ConcatDictionairies(Dictionary<TypesOfShips, int> a, Dictionary<TypesOfShips, int> b)
        {
            foreach (var kvp in b)
            {
                a[kvp.Key] += kvp.Value;
            }
            return a;
        }
        public static List<TypesOfShips> RemoveLastShip(List<TypesOfShips> shipList, BuilderAbstract boatBuilder, BuilderAbstract battleBuilder, BuilderAbstract subB, BuilderAbstract cB)
        {
            if (shipList.Count == 0)
                return shipList;

            switch (shipList.LastOrDefault())
            {
                case TypesOfShips.Boat:
                    boatBuilder.GetProduct().RemoveLast();
                    break;
                case TypesOfShips.Battleship:
                    battleBuilder.GetProduct().RemoveLast();
                    break;
                case TypesOfShips.Submarine:
                    subB.GetProduct().RemoveLast();
                    break;
                case TypesOfShips.Carrier:
                    cB.GetProduct().RemoveLast();
                    break;
            }
            shipList.RemoveAt(shipList.Count - 1);
            return shipList;
        }
    }
}
