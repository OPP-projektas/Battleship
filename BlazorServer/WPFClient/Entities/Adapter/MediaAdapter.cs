using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WPFClient.Sounds;

namespace WPFClient.Entities.Adapter
{
    public class MediaAdapter : MediaPlayer
    {
        public string path = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName, "Sounds");
        private IPlayer _player;
        public MediaAdapter(IPlayer player)
        {
            _player = player;
        }
        public void PlayFullVolume(string fileName)
        {
            string fullPath = Path.Combine(path, fileName);
            if (File.Exists(fullPath))
                _player.PlaySound(fullPath, 100);
        }
        public void PlayHalfVolume(string fileName)
        {
            string fullPath = Path.Combine(path, fileName);
            if (File.Exists(fullPath))
                _player.PlaySound(fullPath, 50);
        }
        public void PlayQuarterVolume(string fileName)
        {
            string fullPath = Path.Combine(path, fileName);
            if (File.Exists(fullPath))
                _player.PlaySound(fullPath, 25);
        }
    }
}
