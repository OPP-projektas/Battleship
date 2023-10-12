using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        public void BuildBattleship()
        {
            logger.Log($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            _product.Add(TypesOfShips.Battleship);
        }

        public void BuildBoat()
        {
            logger.Log($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            _product.Add(TypesOfShips.Boat);
        }

        public void BuildCarrier()
        {
            logger.Log($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            _product.Add(TypesOfShips.Carrier);
        }

        public void BuildSubmarine()
        {
            logger.Log($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            _product.Add(TypesOfShips.Submarine);
        }
        public Product GetProduct()
        {
            return this._product;
        }
    }
}
