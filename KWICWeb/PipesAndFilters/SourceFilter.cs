using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.PipesAndFilters
{
    public class SourceFilter : IFilter
    {
        private MemoryStream outputStream = new MemoryStream();
        private string InputString {get; set;}

        public bool IsComplete { get; private set; }

        public SourceFilter(string input)
        {
            InputString = input;
            IsComplete = false;
        }

        public void SetInput(MemoryStream stream)
        {
            throw new InvalidOperationException("SourceFilter does not take an input stream");
        }

        public void Connect(IFilter nextFilter)
        {
            nextFilter.SetInput(outputStream);
        }

        public void Filter()
        {
            string[] inputLines = InputString.Split("\n");

            StreamWriter sw = new StreamWriter(outputStream);
            foreach (string line in inputLines)
            {
                sw.WriteLine(line);
            }
            sw.Flush();
            IsComplete = true;
        }
    }
}
