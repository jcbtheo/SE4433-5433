using System.Collections.Generic;

namespace KWICWeb.SharedDataKWIC
{
    public class LineStorage
    {
        private List<string> words = new List<string>();
        private List<int> lineIndices = new List<int>();
        private List<string> urls = new List<string>();
        int lastLineIndex = -1;

        public void SetWord(int lineIndex, string word)
        {
            if (lineIndex != lastLineIndex)
            {
                lineIndices.Add(words.Count);
                lastLineIndex = lineIndex;
            }
            words.Add(word);
        }

        public void SetUrl(string url)
        {
            urls.Add(url);
        }

        public string GetWord(int lineIndex, int wordIndex)
        {
            return words[lineIndices[lineIndex] + wordIndex];
        }

        public string GetUrl(int lineIndex)
        {
            return urls[lineIndex];
        }

        public int WordCountForLine(int line)
        {
            if (line > lineIndices.Count -1 || line < 0)
            {
                return -1;
            }
            if (line == lineIndices.Count - 1)
            {
                return words.Count - lineIndices[line];
            }
            else
            {
                return lineIndices[line + 1] - lineIndices[line];
            }
        }
    }
}
