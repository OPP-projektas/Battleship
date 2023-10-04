using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Facotries
{
    public abstract class ShipFactory
    {
        public abstract Ship CreateShip(string username);
    }
}
