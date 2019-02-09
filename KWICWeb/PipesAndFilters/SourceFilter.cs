using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.PipesAndFilters
{
    public class SourceFilter : IFilter<string>
    {
        private string InputString {get; set;}

        public SourceFilter(string input)
        {
            InputString = input;
        }

        public IEnumerable<string> Filter(IEnumerable<string> input)
        {
            string[] inputLines = InputString.Split("\n");
            List<string> lines = new List<string>();

            foreach(string line in inputLines)
            {
                lines.Add(line);
            }
            return inputLines;
        }
    }
}
