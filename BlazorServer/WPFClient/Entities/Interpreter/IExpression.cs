using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Strategy;

namespace WPFClient.Entities.Interpreter
{
    public interface IExpression
    {
        void Interpret(PanelContext context);
    }
}
