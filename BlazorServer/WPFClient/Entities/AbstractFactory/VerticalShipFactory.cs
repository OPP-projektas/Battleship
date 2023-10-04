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
        public Ship CreateCarrier(string username)
        {
            return new VerticalCarrier(username);
        }

        public Ship CreateBattleship(string username)
        {
            return new VerticalBattleship(username);
        }

        public Ship CreateBoat(string username)
        {
            return new VerticalBoat(username);
        }

        public Ship CreateSubmarine(string username)
        {
            return new VerticalSubmarine(username);
        }
    }
}
