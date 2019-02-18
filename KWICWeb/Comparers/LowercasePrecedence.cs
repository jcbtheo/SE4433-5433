using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.Comparers
{
    public class LowercasePrecedence : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            bool xIsShorter = x.Length <= y.Length;
            string shorterString = xIsShorter ? x : y;
            string longerString = xIsShorter ? y : x;

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
            if (shorterGoesFirst && xIsShorter)
            {
                return -1;
            }
            if (!shorterGoesFirst && !xIsShorter)
            {
                return -1;
            }
            return 1;
        }
    }
}
