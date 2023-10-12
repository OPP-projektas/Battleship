using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Builder
{
    public interface IBuilder
    {
        void BuildBoat();
        void BuildBattleship();
        void BuildSubmarine();
        void BuildCarrier();
    }
}
