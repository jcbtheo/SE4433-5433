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
    }

    public class CircularShifter
    {
        private LineStorage storage;
        private Dictionary<int, Tuple<int, int>> shiftIndices = new Dictionary<int, Tuple<int, int>>();

        public CircularShifter(LineStorage lineStorage)
        {
            // ??? needs to be set up with a line storage object after input is complete? does the shift, 42:38 in vid. Each "filter" will need this type of function
            storage = lineStorage;

            int lineIndex = 0;
            int shiftIndex = 0;
            int wordCount = storage.Word(lineIndex);
            while (wordCount != 0)
            {
                // shift and store shift
                for (int i = 0; i < wordCount; i++)
                {
                    shiftIndices.Add(shiftIndex, new Tuple<int, int>(lineIndex, i));
                    shiftIndex++;
                }
                lineIndex++;
                wordCount = storage.Word(lineIndex);
            }
        }

        // shift count is for a given line only
        public void CsSetWord(int shiftIndex, int wordIndex, string word)
        {

        }

        public string CsGetWord(int shiftIndex, int wordIndex)
        {
            // makes a call to linestorage - all 'get' calls will be chained back this way so that only line storage has lines, everything else just has indexes
            int lsLineIndex = shiftIndices[shiftIndex].Item1;
            int offset = shiftIndices[shiftIndex].Item2;
            int lsWordCount = storage.Word(lsLineIndex);
            int actualPos = offset + wordIndex >= lsWordCount ? offset + wordIndex - lsWordCount : offset + wordIndex;
            string word = storage.GetWord(lsLineIndex, actualPos);
            return word;
        }

        public int CsWord(int line)
        {
            if (shiftIndices.TryGetValue(line, out Tuple<int, int> value))
            {
                return storage.Word(shiftIndices[line].Item1);
            }
            return 0;
        }
    }
}
