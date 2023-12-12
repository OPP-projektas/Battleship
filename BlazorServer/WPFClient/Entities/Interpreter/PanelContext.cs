using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Interpreter
{
    public class PanelContext
    {
        public string Name { get; set; }

        public PanelContext(string name)
        {
            Name = name;
        }
    }
}
