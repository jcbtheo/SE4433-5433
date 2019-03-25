using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KWICWeb.Comparers;

namespace KWICWeb.SharedDataKWIC
{
    public class FullShareData
    {
        private char[] characters;
        private int[] lineIndex;
        private int[][] circularShifts;
        private int[][] alphaShifts;

        public List<string> output = new List<string>();

        public void Input(string input)
        {
            List<int> lineIndexes = new List<int>();

            //characters = input.ToCharArray();

            //lineIndexes.Add(0);

            //for (int i = 0; i < characters.Length; i++)
            //{
            //    if (characters[i] == '\n')
            //    {
            //        lineIndexes.Add(i + 1);
            //    }
            //}

            //lineIndex = lineIndexes.ToArray();


            string fullup = "";
            string[] lines = input.Split('\n');
            int lineIndexCount = 0;
            
            foreach (string line in lines)
            {
                lineIndexes.Add(lineIndexCount);
                fullup += line;
                lineIndexCount = line.Length;
            }
            characters = fullup.ToCharArray();
            lineIndex = lineIndexes.ToArray();
        }

        public void CircularShift()
        {
            List<int> wordIndexes = new List<int>();
            List<int> lineIndexes = new List<int>();

            // i is the actual line number
            // lineIndex[i] is where the line starts
            for (int i = 0; i < lineIndex.Length; i++)
            {
                wordIndexes.Add(lineIndex[i]);
                lineIndexes.Add(i);

                // last index is where the line ends
                int lastIndex = 0;
                if (i != lineIndex.Length - 1)
                {
                    lastIndex = lineIndex[i + 1];
                }
                else
                {
                    lastIndex = characters.Length;
                }

                for (int j = lineIndex[i]; j < lastIndex; j++)
                {
                    if (characters[j] == ' ')
                    {
                        // + 1 accounts for the space after each word
                        wordIndexes.Add(j + 1);
                        lineIndexes.Add(i);
                    }
                }
            }
            circularShifts = new int[2][];
            for (int i = 0; i < 2; i++)
            {
                circularShifts[i] = new int[wordIndexes.Count];
            }
            for (int i = 0; i < wordIndexes.Count; i++)
            {
                circularShifts[0][i] = lineIndexes[i];
                circularShifts[1][i] = wordIndexes[i];
            }
        }

        public void Alpabetize()
        {
            alphaShifts = new int[2][];
            for (int i = 0; i < 2; i++)
            {
                alphaShifts[i] = new int[circularShifts[0].Length];
            }
            LowercasePrecedence lc = new LowercasePrecedence();
            int alphaCount = 0;
            int low = 0;
            int high = 0;
            int mid = 0;

            for (int i = 0; i < alphaShifts[0].Length; i++)
            {
                int lineCount = circularShifts[0][i];
                int shiftBegin = circularShifts[1][i];
                int lineBegin = lineIndex[lineCount];
                int lineEnd = 0;
                if (lineCount == lineIndex.Length - 1)
                {
                    lineEnd = characters.Length;
                }
                else
                {
                    lineEnd = lineIndex[lineCount + 1];
                }

                string shift = "";
                if (lineBegin != shiftBegin)
                {
                    shift += new string(characters, shiftBegin, lineEnd - shiftBegin);
                    shift += " ";
                    shift += new string(characters, lineBegin, shiftBegin - lineBegin - 1);
                }
                else
                {
                    shift += new string(characters, lineBegin, lineEnd - lineBegin);
                }

                high = alphaCount - 1;
                while (low <= high)
                {
                    mid = (low + high) / 2;
                    int midLineCount = alphaShifts[0][mid];
                    int midShiftBegin = alphaShifts[1][mid];
                    int midLineBegin = lineIndex[midLineCount];
                    int midLineEnd = 0;
                    if (midLineCount == lineIndex.Length - 1)
                    {
                        midLineEnd = characters.Length;
                    }
                    else
                    {
                        midLineEnd = lineIndex[midLineCount + 1];
                    }

                    String midLine = "";
                    if (midLineBegin != midShiftBegin)
                    {
                        midLine += new String(characters, midShiftBegin, midLineEnd - midShiftBegin);
                        midLine += " ";
                        midLine += new String(characters, midLineBegin, midShiftBegin - midLineBegin - 1);
                    }
                    else
                        midLine += new String(characters, midLineBegin, midLineEnd - midLineBegin);

                    int compared = lc.Compare(shift, midLine);
                    if (compared > 0)
                        low = mid + 1;
                    else if (compared < 0)
                        high = mid - 1;
                    else
                    {
                        low = mid;
                        high = mid - 1;
                    }
                }

                Array.Copy(alphaShifts[0], low, alphaShifts[0], low + 1, alphaCount - low);
                Array.Copy(alphaShifts[1], low, alphaShifts[1], low + 1, alphaCount - low);
                alphaShifts[0][low] = lineCount;
                alphaShifts[1][low] = shiftBegin;
                alphaCount++;

            }
        }

        public void Output()
        {
            for (int i = 0; i < alphaShifts[0].Length; i++)
            {
                int lineCount = alphaShifts[0][i];
                int shiftBegin = alphaShifts[1][i];
                int lineBegin = lineIndex[lineCount];
                int lineEnd = 0;
                if (lineCount == lineIndex.Length - 1)
                {
                    lineEnd = characters.Length;
                }
                else
                {
                    lineEnd = lineIndex[lineCount + 1];
                }

                if (lineBegin != shiftBegin)
                {
                    output.Add(new string(characters, shiftBegin, lineEnd - shiftBegin));
                    output.Add(" ");
                    output.Add(new string(characters, lineBegin, shiftBegin - lineBegin - 1));
                }
                else
                {
                    output.Add(new string(characters, lineBegin, lineEnd - lineBegin));
                }
            }
        }
    }
}
