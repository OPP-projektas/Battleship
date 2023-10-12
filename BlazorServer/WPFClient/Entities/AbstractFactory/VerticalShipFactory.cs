using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.AbstractFactory.VerticalFactory;

namespace WPFClient.Entities.AbstractFactory
{
    public class VerticalShipFactory : IShipFactory
    {
        public Ship CreateShip(TypesOfShips shipType, string username)
        {
            switch (shipType)
            {
                case TypesOfShips.Battleship:
                    return new VerticalBattleship(username);
                case TypesOfShips.Boat:
                    return new VerticalBoat(username);
                case TypesOfShips.Carrier:
                    return new VerticalCarrier(username);
                case TypesOfShips.Submarine:
                    return new VerticalSubmarine(username);
                default: return null;
            }
        }
    }
}
