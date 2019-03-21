using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.SharedDataKWIC
{
    public struct IndexOffsets
    {
        public int line;
        public int offset;

        public IndexOffsets(int l, int o)
        {
            line = l;
            offset = o;
        }
    }

    public class CircularShifter
    {
        private LineStorage storage;
        private List<IndexOffsets> offsets = new List<IndexOffsets>();

        public CircularShifter(LineStorage lineStorage)
        {
            storage = lineStorage;

            int line = 0;
            int wordCount = lineStorage.WordCountForLine(line);
            while (wordCount != -1)
            {
                for (int wordIndex = 0; wordIndex < wordCount; wordIndex++)
                {
                    offsets.Add(new IndexOffsets(line, wordIndex));
                }
                line++;
                wordCount = lineStorage.WordCountForLine(line);
            }
           
        }

        // shift count is for a given line only
        public void CsSetWord(int shiftIndex, int wordIndex, string word)
        {

        }

        public string CsGetWord(int shiftIndex, int wordIndex)
        {
            int wordsInLine = CsWord(shiftIndex);
            int actualLine = offsets[shiftIndex].line;
            int offset = offsets[shiftIndex].offset;
            int actualPos = offset + wordIndex >= wordsInLine ? offset + wordIndex - wordsInLine : offset + wordIndex;
            return storage.GetWord(actualLine, actualPos);
        }

        public int CsWord(int line)
        {
            try
            {
                return storage.WordCountForLine(offsets[line].line);
            }
            catch
            {
                return -1;
            }
        }
    }
}
