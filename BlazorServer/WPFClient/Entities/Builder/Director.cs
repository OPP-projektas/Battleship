using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Builder
{
    public class Director
    {
        private IBuilder _builder;

        public IBuilder Builder 
        { 
            set { _builder = value; } 
        }
        public void BuildBoat()
        {
            this._builder.BuildBoat();
        }
        public void BuildBattleship()
        {
            this._builder.BuildBattleship();
        }
        public void BuildSubmarine()
        {
            this._builder.BuildSubmarine();
        }
        public void BuildCarrier()
        {
            this._builder.BuildCarrier();
        }
    }
}
