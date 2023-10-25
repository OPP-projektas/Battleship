using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Adapter
{
    interface MediaPlayer
    {
        void PlayFullVolume(string fileName);
        void PlayHalfVolume(string fileName);
        void PlayQuarterVolume(string fileName);
    }
}
