using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Prototype;

namespace WPFClient.Entities.Builder
{
    public class BattleshipBuilder : BuilderAbstract
    {
        public override BuilderAbstract BuildShip()
        {
            product.Add(TypesOfShips.Battleship);
            return (this);
        }     
    }
}
