using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFClient.Entities.Template
{
    public abstract class Saver
    {
        public void Save(string fileName, int score)
        {
            List<string> toSave = Format(score);
            if (CheckIfUserExists(fileName, Player.Username))
                SaveScore(fileName, toSave);
            else
                SaveUser(fileName, toSave);
            //Sort();

        }
        protected List<string> Format(int score)
        {
            List<string> result = new List<string>();
            result.Add(Player.Username);
            result.Add(score.ToString());
            return result;
        }
        protected abstract bool CheckIfUserExists(string fileName, string name);
        protected abstract void SaveUser(string fileName, List<string> toSave);
        protected abstract void SaveScore(string fileName, List<string> toSave);


    }
}
