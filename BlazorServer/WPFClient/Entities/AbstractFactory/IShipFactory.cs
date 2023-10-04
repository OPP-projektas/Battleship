using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.AbstractFactory
{
    public interface IShipFactory
    {
        Ship CreateCarrier(string username);
        Ship CreateBattleship(string username);
        Ship CreateBoat(string username);
        Ship CreateSubmarine(string username);
    }
}
