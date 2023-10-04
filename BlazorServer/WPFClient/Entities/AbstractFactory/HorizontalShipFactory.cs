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
        public Ship CreateCarrier(string username)
        {
            return new HorizontalCarrier(username);
        }

        public Ship CreateBattleship(string username)
        {
            return new HorizontalBattleship(username);
    }

        public Ship CreateBoat(string username)
        {
            return new HorizontalBoat(username);
}

        public Ship CreateSubmarine(string username)
        {
            return new HorizontalSubmarine(username);
}
    }
}
