using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Interpreter
{
    public class ExpressionHandler : IExpression
    {
        public IExpression Expression1 { get; set; }

        public IExpression Expression2 { get; set; }
        public void Interpret(PanelContext context)
        {
            Expression1.Interpret(context);
            Expression2.Interpret(context);
        }
    }
}
