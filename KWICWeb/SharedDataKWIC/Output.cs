using System.Collections.Generic;


namespace KWICWeb.SharedDataKWIC
{
    public class Output
    {
        public List<string> GetOuputAsString(AlphabeticShifter alphShifter)
        {
            List<string> tempList = new List<string>();

            int alphIndex = 0;
            while (alphShifter.IndexOfShift(alphIndex) != -1)
            {
                tempList.Add(alphShifter.GetString(alphShifter.IndexOfShift(alphIndex)));
                alphIndex++;
            }

            // here we would store the output into the 'db'
            return tempList; //string.Join('\n', tempList);
        }


    }
}
