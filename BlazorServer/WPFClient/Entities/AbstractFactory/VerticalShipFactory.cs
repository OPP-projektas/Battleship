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
        public Ship CreateShip(IShipFactory.ShipType shipType, string username)
        {
            switch (shipType)
            {
                case IShipFactory.ShipType.Battleship:
                    return new VerticalBattleship(username);
                case IShipFactory.ShipType.Boat:
                    return new VerticalBoat(username);
                case IShipFactory.ShipType.Carrier:
                    return new VerticalCarrier(username);
                case IShipFactory.ShipType.Submarine:
                    return new VerticalSubmarine(username);
                default: return null;
            }
        }
    }
}
