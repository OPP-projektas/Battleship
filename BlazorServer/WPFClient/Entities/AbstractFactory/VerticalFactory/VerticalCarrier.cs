using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.AbstractFactory.VerticalFactory
{
    public class VerticalCarrier : Ship
    {
        public VerticalCarrier(string username) : base(username)
        {
            Logger logger = Logger.GetInstance();
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);
            Length = 4;
        }
    }
}
