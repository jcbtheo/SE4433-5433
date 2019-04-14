using System.IO;
using System.Collections.Generic;
using System;

namespace KWICWeb.SharedDataKWIC
{
    public class CircularShifter
    {
        private Dictionary<string, int> filterWords = new Dictionary<string, int>();
        private LineStorage storage;
        private List<ValueTuple<int, int>> offsets = new List<ValueTuple<int, int>>();

        public CircularShifter(LineStorage lineStorage)
        {
            storage = lineStorage;
        }

        public void SetFilterWords(string filePath)
        {
            foreach (string line in File.ReadLines(filePath))
            {
                filterWords.Add(line, 1);
            }
        }

        public void Shift()
        {
            int line = 0;
            int wordCount = storage.WordCountForLine(line);
            while (wordCount != -1)
            {
                for (int wordIndex = 0; wordIndex < wordCount; wordIndex++)
                {
                    if (!filterWords.ContainsKey(storage.GetWord(line, wordIndex).ToLower()))
                    {
                        // store original line number and the word offset of the shiftfilter
                        offsets.Add(new ValueTuple<int, int>(line, wordIndex));
                    }
                }
                line++;
                wordCount = storage.WordCountForLine(line);
            }
        }

        public int GetOriginalLineIndex(int shiftIndex)
        {
            return offsets[shiftIndex].Item1;
        }

        public string CsGetWord(int shiftIndex, int wordIndex)
        {
            int wordsInLine = CsWordCountForLine(shiftIndex);
            int offset = offsets[shiftIndex].Item2;
            int actualPos = offset + wordIndex >= wordsInLine ? offset + wordIndex - wordsInLine : offset + wordIndex;
            return storage.GetWord(offsets[shiftIndex].Item1, actualPos);
        }

        public string CsGetUrl(int shiftIndex)
        {
            return storage.GetUrl(offsets[shiftIndex].Item1);
        }

        public int CsWordCountForLine(int line)
        {
            try
            {
                return storage.WordCountForLine(offsets[line].Item1);
            }
            catch
            {
                return -1;
            }
        }
    }
}

