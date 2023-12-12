using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFClient.Entities.Prototype;

namespace WPFClient.Entities.Mediator
{
    public class ReadyButtonComponent : BaseComponent
    {
        private Button button;

        public ReadyButtonComponent(Button Button)
        {
            this.button = Button;
        }

        public void Operation()
        {
            Logger logger = Logger.GetInstance();
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message, 1);
            this._mediator.Notify(this, "ButtonComponent");


        }

    }
}
