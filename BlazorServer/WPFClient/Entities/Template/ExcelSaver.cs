using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClient.Entities.Iterator;

namespace WPFClient.Entities.Template
{
    public class ExcelSaver : Saver
    {
        protected override bool CheckIfUserExists(string fileName, string name)
        {
            fileName += ".csv";
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (string line in lines)
                {
                    string[] nameAndScore = line.Split(';');

                    if (nameAndScore[0] == name)
                        return true;
                }
            }
            return false;
        }

        protected override void SaveScore(string fileName, List<string> toSave)
        {
            fileName += ".csv";
            List<string> modifiedEntries = new List<string>();
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                LeaderboardEntries entries = new LeaderboardEntries(lines.ToList<string>());
                entries.SetIterationOrder(LeaderboardEntries.IterationOrder.Reverse);
                foreach (var entry in entries)
                {
                    string[] nameAndScore = entry.Split(';');

                    if (nameAndScore[0] == toSave[0])
                    {
                        int score = Int32.Parse(nameAndScore[1]) + Int32.Parse(toSave[1]);
                        modifiedEntries.Add(nameAndScore[0] + ";" + score.ToString());
                    }
                    else
                    {
                        modifiedEntries.Add(entry);
                    }
                }
                File.WriteAllLines(fileName, modifiedEntries);
            }
        }

        protected override void SaveUser(string fileName, List<string> toSave)
        {
            fileName += ".csv";
            string usernameAndScore = toSave[0] + ";" + toSave[1] + Environment.NewLine;
            File.AppendAllText(fileName, usernameAndScore);
        }
    }
}
