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
        private Dictionary<string, int> filterWords = new Dictionary<string, int>()
        {
            {"a", 1},
            {"an", 1},
            {"the", 1},
            {"and", 1},
            {"or", 1},
            {"of", 1},
            {"to", 1},
            {"be", 1},
            {"is", 1},
            {"in", 1},
            {"out", 1},
            {"by", 1},
            {"as", 1},
            {"at", 1},
            {"off", 1},
        };

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
                    if (!filterWords.ContainsKey(storage.GetWord(line, wordIndex).ToLower()))
                    {
                        offsets.Add(new IndexOffsets(line, wordIndex));
                    }
                }
                line++;
                wordCount = lineStorage.WordCountForLine(line);
            }
        }

        public string CsGetWord(int shiftIndex, int wordIndex)
        {
            int wordsInLine = CsWordCountForLine(shiftIndex);
            int offset = offsets[shiftIndex].offset;
            int actualPos = offset + wordIndex >= wordsInLine ? offset + wordIndex - wordsInLine : offset + wordIndex;
            return storage.GetWord(offsets[shiftIndex].line, actualPos);
        }

        public int CsWordCountForLine(int line)
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

