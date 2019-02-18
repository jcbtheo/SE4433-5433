using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KWICWeb.Comparers;

namespace KWICWeb.PipesAndFilters
{
    public class Alphabetizer : IFilter
    {
        private MemoryStream outputStream;
        private MemoryStream inputStream;
        private List<string> sortedList;

        public bool IsComplete { get; private set; }

        public Alphabetizer()
        {
            IsComplete = false;
        }

        public void SetInput(MemoryStream stream)
        {
            inputStream = stream;
        }

        public void SetOutput(MemoryStream stream)
        {
            outputStream = stream;
        }

        public void Filter()
        {
            using (StreamReader sr = new StreamReader(inputStream))
            {
                inputStream.Position = 0;
                sortedList = new List<string>();
                LowercasePrecedence lcSorter = new LowercasePrecedence();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (sortedList.Count == 0)
                    {
                        sortedList.Add(line);
                    }
                    else
                    {
                        int index = sortedList.BinarySearch(line, lcSorter);
                        if (index < 0)
                        {
                            sortedList.Insert(~index, line);
                        }
                        //int index = 0;
                        //while (index < sortedList.Count && !HasLowerCasePrecedence(line, sortedList[index]))
                        //{
                        //    index++;
                        //}
                        //sortedList.Insert(index, line);
                    }
                }
                foreach (string sortedLine in sortedList)
                {
                    StreamWriter sw = new StreamWriter(outputStream);
                    sw.WriteLine(sortedLine);
                    sw.Flush();
                }
            }
            IsComplete = true;
        }

        private bool HasLowerCasePrecedence(string newString, string sortedString)
        {
            int newStringLength = newString.Length;
            int sortedStringLength = sortedString.Length;

            bool newIsShorter = newStringLength <= sortedStringLength;
            string shorterString = newIsShorter ? newString : sortedString;
            string longerString = newIsShorter ? sortedString : newString;

            bool shorterStringLowercase = false;
            bool longerStringLowercase = false;
            bool shorterGoesFirst = true;

            for (int i = 0; i < shorterString.Length; i++)
            {
                shorterStringLowercase = char.IsLower(shorterString[i]);
                longerStringLowercase = char.IsLower(longerString[i]);

                int letterCompare = char.ToUpperInvariant(shorterString[i]).CompareTo(char.ToUpperInvariant(longerString[i]));

                if (letterCompare == 0)
                {
                    // Letters are the same but new string is lowercase it should be sorted before the existing string
                    if (shorterStringLowercase && !longerStringLowercase)
                    {
                        shorterGoesFirst = true;
                        break;
                    }
                    // new word letter is upper case and sorted word letter is lower case - new should be sorted after
                    else if (!shorterStringLowercase && longerStringLowercase)
                    {
                        shorterGoesFirst = false;
                        break;
                    }
                    // else letters are same with same case, need to continue checking the rest of the string
                }
                else
                {
                    shorterGoesFirst = letterCompare < 0;
                    break;
                }
            }
            // have hit all letters and they are equal. new is shorter or same length so place before
            if (shorterGoesFirst && newIsShorter)
            {
                return true;
            }
            if (!shorterGoesFirst && !newIsShorter)
            {
                return true;
            }
            return false;
        }
    }
}
