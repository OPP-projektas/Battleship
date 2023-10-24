using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Prototype;

namespace WPFClient.Entities.Builder
{
    public class ShipBuilder : IBuilder
    {
        private Product _product;
        Logger logger = Logger.GetInstance();
        public ShipBuilder()
        {
            this._product = new Product();
        }
        public void Reset()
        {
            this._product = new Product();
        }
        public IBuilder BuildBattleship()
        {
            _product.Add(TypesOfShips.Battleship);
            return (this);
        }

        public IBuilder BuildBoat()
        {
            _product.Add(TypesOfShips.Boat);
            return (this);
        }

        public IBuilder BuildCarrier()
        {
            _product.Add(TypesOfShips.Carrier);
            return (this);
        }

        public IBuilder BuildSubmarine()
        {
            _product.Add(TypesOfShips.Submarine);
            return (this);
        }
        public IBuilder BuildMessage()
        {
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);
            return (this);
        }
        public Product GetProduct()
        {
            return this._product;
        }

    }
}
