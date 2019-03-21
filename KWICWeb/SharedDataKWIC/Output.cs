﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.SharedDataKWIC
{
    public class Output
    {
        private AlphabeticShifter alphShifter;

        public Output(AlphabeticShifter shifter)
        {
            alphShifter = shifter;
        }

        public string GetOuputAsString()
        {
            List<string> tempList = new List<string>();

            int alphIndex = 0;
            while (!string.IsNullOrEmpty(alphShifter.IndexOfShift(alphIndex)))
            {
                tempList.Add(alphShifter.IndexOfShift(alphIndex));
                alphIndex++;
            }

            return string.Join('\n', tempList);
        }


    }
}