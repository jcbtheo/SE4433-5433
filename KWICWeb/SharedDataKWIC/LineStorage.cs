using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.SharedDataKWIC
{
    public class LineStorage
    {

        private List<string> words = new List<string>();
        private List<int> newLineIndexes = new List<int>();
        private List<int> lineWordCount = new List<int>();
        private int lastLineIndex = -1;

        public void SetWord(int lineIndex, string word)
        {
            // is new line
            if (lineIndex != lastLineIndex)
            {
                newLineIndexes.Add(words.Count);
                lineWordCount.Add(1);
                lastLineIndex = lineIndex;
            }
            else
            {
                lineWordCount[lineIndex]++;
            }
            words.Add(word);
        }

        public string GetWord(int lineIndex, int wordIndex)
        {
            return words[newLineIndexes[lineIndex] + wordIndex];
        }

        public int WordCountForLine(int line)
        {
            try
            {
                return lineWordCount[line];
            }
            catch
            {
                return -1;
            }
        }
    }
}
