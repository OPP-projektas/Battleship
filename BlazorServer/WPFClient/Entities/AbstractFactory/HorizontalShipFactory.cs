using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.AbstractFactory.VerticalFactory;

namespace WPFClient.Entities.AbstractFactory
{
    public class HorizontalShipFactory : IShipFactory
    {
        public Ship CreateShip(TypesOfShips shipType, string username)
        {
            switch(shipType)
            {
                case TypesOfShips.Battleship:
                    return new HorizontalBattleship(username);
                case TypesOfShips.Boat:
                    return new HorizontalBoat(username);
                case TypesOfShips.Carrier:
                    return new HorizontalCarrier(username);
                case TypesOfShips.Submarine:
                    return new HorizontalSubmarine(username);
                default: return null;
            }
        }
    }
}
