using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Services
{
    public class ScoreBoardService
    {
        private StreamWriter sw;
        private StreamReader sr;

        public ScoreBoardService()
        {

        }
        public void SaveNewScore(int score, string playername)
        {

            if (!File.Exists(Directory.GetCurrentDirectory() + "/save.txt"))
            {
                sw = new StreamWriter(Directory.GetCurrentDirectory() + "/save.txt", true);
            }
            else
            {
                sw = File.AppendText(Directory.GetCurrentDirectory() + "/save.txt");
            }
            sw.WriteLine($"{ DateTime.Today.ToShortDateString()}%{playername}%{score}");
            sw.Close();
        }
        public int GetHighScore()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/save.txt"))
            {
                sr = new StreamReader(Directory.GetCurrentDirectory() + "/save.txt");
                List<int> scores = new List<int>();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    scores.Add(int.Parse(line.Split('%')[2]));
                }
                sr.Close();
                return scores.Max();
            }
            else return 0;
        }
    }
}
