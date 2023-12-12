using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Prototype;
using WPFClient.Entities.Strategy;

namespace WPFClient.Entities.Interpreter
{
    public class ClearConsoleExpression : IExpression
    {
        public void Interpret(PanelContext context)
        {
            if (context.Name.ToLower() != "clear")
                return;
            Logger logger = Logger.GetInstance();
            logger.ClearLogger();
        }
    }
}
