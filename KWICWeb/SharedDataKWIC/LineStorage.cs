using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.SharedDataKWIC
{
    public class LineStorage
    {

        private List<string> words = new List<string>();
        private List<int> lineIndexes = new List<int>();
        int lastLineIndex = -1;

        public void SetWord(int lineIndex, string word)
        {
            if (lineIndex != lastLineIndex)
            {
                lineIndexes.Add(words.Count);
                lastLineIndex = lineIndex;
            }
            words.Add(word);
        }

        public string GetWord(int lineIndex, int wordIndex)
        {
            return words[lineIndexes[lineIndex] + wordIndex];
        }

        public int WordCountForLine(int line)
        {
            if (line > lineIndexes.Count -1 || line < 0)
            {
                return -1;
            }
            if (line == lineIndexes.Count - 1)
            {
                return words.Count - lineIndexes[line];
            }
            else
            {
                return lineIndexes[line + 1] - lineIndexes[line];
            }
        }

        public int GetNumberOfLines()
        {
            return lineIndexes.Count();
        }
    }
}
