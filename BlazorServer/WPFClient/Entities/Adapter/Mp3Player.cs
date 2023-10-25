using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WPFClient.Entities.Prototype;

namespace WPFClient.Entities.Adapter
{
    class Mp3Player : IPlayer
    {
        private System.Windows.Media.MediaPlayer mediaPlayer = new System.Windows.Media.MediaPlayer();
        public void PlaySound(string fileName, int volume)
        {
            Logger logger = Logger.GetInstance();
            Message message = new Message();
            message.SetMessage($"Class = {GetType().Name}, method = {MethodBase.GetCurrentMethod().Name}");
            logger.Log(message);

            mediaPlayer.Open(new Uri(fileName));
            SetVolume(volume);
            mediaPlayer.Play();
        }
        public void SetVolume(int volume)
        {
            mediaPlayer.Volume = volume / 100.0f;
        }
    }
}
