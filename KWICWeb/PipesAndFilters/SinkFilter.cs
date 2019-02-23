using System;
using System.IO;
using System.Collections.Generic;

namespace KWICWeb.PipesAndFilters
{
    public class SinkFilter : IFilter
    {
        private MemoryStream inputStream;
        public List<string> output;

        public void SetInput(MemoryStream stream)
        {
            inputStream = stream;
        }

        public void SetOutput(MemoryStream stream)
        {
            throw new InvalidOperationException("SourceFilter does not take an ouput stream");
        }

        public void Filter()
        {
            // Simply reads in every line from the input pipe and collects them in the output list.
            inputStream.Position = 0;
            output = new List<string>();
            using (StreamReader sr = new StreamReader(inputStream))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    output.Add(line);
                }
            }
        }
    }
}
