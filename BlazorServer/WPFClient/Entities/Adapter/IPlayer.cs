using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Adapter
{
    public interface IPlayer
    {
        void PlaySound(string fileName, int volume);
    }
}
