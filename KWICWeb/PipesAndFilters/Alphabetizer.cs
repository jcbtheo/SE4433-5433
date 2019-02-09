using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.PipesAndFilters
{
    public class Alphabetizer : IFilter<string>
    {
        public IEnumerable<string> Filter(IEnumerable<string> input)
        {
            List<string> sortedList = input.ToList();
            sortedList.Sort();
            return sortedList;
        }

        //private sortLine
    }
}
