using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.AbstractFactory.VerticalFactory
{
    public class HorizontalCarrier : Ship
    {
        public HorizontalCarrier(string username) : base(username)
        {
            Logger logger = Logger.GetInstance();
            logger.Log($"Class = {GetType().Name}, method = GetShip");

            // Initialize HorizontalCarrier-specific properties or logic if needed
        }
    }
}
