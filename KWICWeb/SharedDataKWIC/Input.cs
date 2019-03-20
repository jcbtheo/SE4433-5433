using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.SharedDataKWIC
{
    public class Input
    {
        //public void Read(string input)
        //{

        //}

        public void Store(LineStorage storage, string input)
        {
            string[] lines = input.Split('\n');
            int lineIndex = 0;

            foreach (string line in lines)
            {
                string[] words = line.Trim().Split(' ');
                int wordIndex = 0;

                foreach (string word in words)
                {
                    storage.SetWord(lineIndex, wordIndex, word);
                    wordIndex++;
                }

                lineIndex++;
            }
        }
    }
}
