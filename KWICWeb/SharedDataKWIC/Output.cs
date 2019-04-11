using System.Collections.Generic;

namespace KWICWeb.SharedDataKWIC
{
    public class Output
    {
        public string GetOuputAsString(AlphabeticShifter alphShifter)
        {
            List<string> tempList = new List<string>();

            int alphIndex = 0;
            while (alphShifter.IndexOfShift(alphIndex) != -1)
            {
                tempList.Add(alphShifter.GetString(alphShifter.IndexOfShift(alphIndex)));
                alphIndex++;
            }

            return string.Join('\n', tempList);
        }

    }
}
