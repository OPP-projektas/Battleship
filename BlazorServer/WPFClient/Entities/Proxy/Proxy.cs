using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using WPFClient.Entities.Adapter;
using WPFClient.Entities.Prototype;

namespace WPFClient.Entities.Proxy
{
    public class Proxy
    {
        private IProxyInterface _realSubject;

        public Proxy(IProxyInterface realSubject)
        {
            this._realSubject = realSubject;
        }

        public void Request(Page page, Board? board)
        {
            this._realSubject.Request(page, board);

            this.PlaySound();
        }


        public void PlaySound()
        {
            Adapter.MediaPlayer player = new MediaAdapter(new Mp3Player());
            player.PlayFullVolume("yeah_boy.mp3");
        }
    }
}
