using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Prototype;
using WPFClient.Entities.Strategy;

namespace WPFClient.Entities.Interpreter
{
    public class PlayerNameExpression : IExpression
    {
        public void Interpret(PanelContext context)
        {
            if (context.Name.ToLower() != "player")
                return;
            Logger logger = Logger.GetInstance();
            Message message = new Message();
            message.SetMessage("Player name = " + Player.Username);
            logger.Log(message, 1);
        }
    }
}
