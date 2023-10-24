using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Builder
{
    public interface IBuilder
    {
        IBuilder BuildBoat();
        IBuilder BuildBattleship();
        IBuilder BuildSubmarine();
        IBuilder BuildCarrier();
        IBuilder BuildMessage();
    }
}
