using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Prototype;

namespace WPFClient.Entities.Builder
{
    public class ConcreteBuilder : IBuilder
    {
        private Product _product;
        Logger logger = Logger.GetInstance();
        public ConcreteBuilder()
        {
            this._product = new Product();
        }
        public void Reset()
        {
            this._product = new Product();
        }
        //return builder
        public IBuilder BuildBattleship()
        {
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);
            _product.Add(TypesOfShips.Battleship);
            return (this);
        }

        public IBuilder BuildBoat()
        {
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);
            _product.Add(TypesOfShips.Boat);
            return (this);
        }

        public IBuilder BuildCarrier()
        {
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);
            _product.Add(TypesOfShips.Carrier);
            return (this);
        }

        public IBuilder BuildSubmarine()
        {
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);
            _product.Add(TypesOfShips.Submarine);
            return (this);
        }
        public Product GetProduct()
        {
            return this._product;
        }
    }
}
