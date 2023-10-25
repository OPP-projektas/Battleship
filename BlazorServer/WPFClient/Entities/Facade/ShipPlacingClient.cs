using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Facade
{
    public class ShipPlacingClient
    {
        private Facade _facade;
        public ShipPlacingClient(Facade facade)
        {
            _facade = facade;
        }
        public void PlaceShip()
        {
            _facade.PlaceShip();
        }
    }
}
