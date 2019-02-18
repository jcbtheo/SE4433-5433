using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.PipesAndFilters
{
    public class SinkFilter : IFilter
    {
        private MemoryStream inputStream;

        public bool IsComplete { get; private set; }

        public List<string> output;

        public SinkFilter()
        {
            IsComplete = false;
        }

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
            IsComplete = true;
        }
    }
}
