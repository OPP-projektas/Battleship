using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Facotries;
using WPFClient.Entities.ShipTypes;

namespace WPFClient.Entities
{
    class BattleshipFactory : ShipFactory
    {
        public override Ship CreateShip(string username)
        {
            Logger logger = Logger.GetInstance();
            logger.Log($"Class = {GetType().Name}, method = GetShip");

            return new Battleship(username);
        }
    }
}
