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
    public class UndoButtonComponent : BaseComponent
    {
        private Button button;
        private bool flag = false;

        public UndoButtonComponent(Button Button, bool flag)
        {
            this.button = Button;
            this.flag = flag;
        }

        public void Operation()
        {

            Logger logger = Logger.GetInstance();
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message, 1);

            if (flag)
            {
                this._mediator.Notify(this, "UndoButtonComponent");
                flag = false;
            }

        }


        public void setFlag(bool f)
        {
            this.flag = f;
        }

    }
}
