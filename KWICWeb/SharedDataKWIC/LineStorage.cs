using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.SharedDataKWIC
{
    public class LineStorage
    {
        // implement a storage mechanism for the all the char sets
        // can easily do a 2d array I think
        private List<List<string>> words = new List<List<string>>();

        public void SetWord(int lineIndex, int wordIndex, string word)
        {
            // I hate this because the lineIndex can be set at anything??
            if (words.ElementAtOrDefault(lineIndex) == null)
            {
                words.Add(new List<string>());
                words.Last().Insert(wordIndex, word);
            }
            else
            {
                words[lineIndex].Insert(wordIndex, word);
            }
        }

        public string GetWord(int lineIndex, int wordIndex)
        {
            if (words.ElementAtOrDefault(lineIndex) != null)
            {
                if (words[lineIndex].ElementAtOrDefault(wordIndex) != null)
                {
                    return words[lineIndex][wordIndex];
                }
            }
            return null;
        }

        public int Word(int line)
        {
            if (words.ElementAtOrDefault(line) != null)
            {
                return words[line].Count;
            }
            return 0;
        }
    }
}
