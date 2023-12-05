using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPFClient.Entities.Proxy
{
    public interface IProxyInterface
    {
        void Request(Page page, Board? board);
    }
}
