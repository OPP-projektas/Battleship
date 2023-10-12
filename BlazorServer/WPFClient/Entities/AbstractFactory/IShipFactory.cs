using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.AbstractFactory
{
    public interface IShipFactory
    {
        Ship CreateShip(TypesOfShips shipType, string username);
    }
}
