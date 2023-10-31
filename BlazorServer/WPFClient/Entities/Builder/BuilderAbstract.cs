using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Prototype;

namespace WPFClient.Entities.Builder
{
    public abstract class BuilderAbstract
    {
        protected Product product = new Product();

        public abstract BuilderAbstract BuildShip();
        public  BuilderAbstract BuildMessage()
        {
            Logger logger = Logger.GetInstance();
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);
            return (this);
        }

        public  Product GetProduct()
        {
            return product;
        }
    }
}
