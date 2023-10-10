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
        public Ship CreateShip(IShipFactory.ShipType shipType, string username)
        {
            switch(shipType)
            {
                case IShipFactory.ShipType.Battleship:
                    return new HorizontalBattleship(username);
                case IShipFactory.ShipType.Boat:
                    return new HorizontalBoat(username);
                case IShipFactory.ShipType.Carrier:
                    return new HorizontalCarrier(username);
                case IShipFactory.ShipType.Submarine:
                    return new HorizontalSubmarine(username);
                default: return null;
            }
        }
    }
}
