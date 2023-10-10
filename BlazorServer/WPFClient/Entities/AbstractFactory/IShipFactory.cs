using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.AbstractFactory
{
    public interface IShipFactory
    {
        public enum ShipType
        {
            Submarine,
            Carrier,
            Battleship,
            Boat
        }
        Ship CreateShip(ShipType shipType, string username);
    }
}
