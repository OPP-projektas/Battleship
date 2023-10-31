using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Builder
{
    public class Director
    {
        private BuilderAbstract absBuilder;
        public BuilderAbstract AbstractBuilder
        {
            set { absBuilder = value; }
        }
        public void BuildShipWithMessage()
        {
            this.absBuilder.BuildShip().BuildMessage();
        }
    }
}
