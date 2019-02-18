using System;
using System.IO;

namespace KWICWeb.PipesAndFilters
{
    public class SourceFilter : IFilter
    {
        private MemoryStream outputStream;
        private string InputString {get; set;}

        public SourceFilter(string input)
        {
            InputString = input;
        }

        public void SetInput(MemoryStream stream)
        {
            throw new InvalidOperationException("SourceFilter does not take an input stream");
        }

        public void SetOutput(MemoryStream stream)
        {
            outputStream = stream;
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
        }
    }
}
