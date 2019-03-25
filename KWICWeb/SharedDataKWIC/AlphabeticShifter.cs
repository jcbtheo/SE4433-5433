using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using KWICWeb.Comparers;

namespace KWICWeb.SharedDataKWIC
{
    public class AlphabeticShifter
    {
        private CircularShifter shifter;
        private List<int> sortedCsIndices = new List<int>();

        public AlphabeticShifter(CircularShifter cs)
        {
            shifter = cs;
        }

        public string GetString(int shiftIndex)
        {
            // create the string
            string sb = "";
            int wordCount = shifter.CsWordCountForLine(shiftIndex);
            for (int i = 0; i < wordCount; i++)
            {
                sb += shifter.CsGetWord(shiftIndex, i);
                if (i != wordCount - 1)
                {
                    sb += " ";
                }
            }
            return sb;
        }

        public void Alph()
        {
            List<string> tempSortedList = new List<string>();
            LowercasePrecedence lcSorter = new LowercasePrecedence();

            int shiftIndex = 0;
            while (shifter.CsWordCountForLine(shiftIndex) != -1)
            {
                string shiftedString = GetString(shiftIndex);

                if (tempSortedList.Count == 0)
                {
                    tempSortedList.Add(shiftedString);
                    sortedCsIndices.Add(shiftIndex);
                }
                else
                {
                    int index = tempSortedList.BinarySearch(shiftedString, lcSorter);
                    if (index < 0)
                    {
                        // ~ is the xor operator. Returns the bitwise complement of the negative number returned by the comparer which is the index where it should
                        // be inserted into the list.
                        tempSortedList.Insert(~index, shiftedString);
                        sortedCsIndices.Insert(~index, shiftIndex);
                    }
                }
                shiftIndex++;
            }
        }

        public int IndexOfShift(int alphIndex)
        {
            // Returns the index of the circular 
            // shift which comes i-th in the ordering
            // need to test this, might just need a try catch
            try
            {
                return sortedCsIndices[alphIndex];
            }
            catch
            {
                return -1;
            }
        }
    }
}
