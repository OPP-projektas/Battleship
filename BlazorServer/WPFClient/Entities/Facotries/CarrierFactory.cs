using WPFClient.Entities.Facotries;
using WPFClient.Entities.ShipTypes;

namespace WPFClient.Entities
{
    class CarrierFactory : ShipFactory
    {
        public override Ship CreateShip(string username)
        {        
            Logger logger = Logger.GetInstance();
            logger.Log($"Class = {GetType().Name}, method = GetShip");
            return new Carrier(username);
          
        }
    }
}
