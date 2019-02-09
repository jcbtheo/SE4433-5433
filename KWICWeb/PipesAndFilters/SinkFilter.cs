using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.PipesAndFilters
{
    public class SinkFilter : IFilter
    {
        MemoryStream inputStream = new MemoryStream();
        public bool IsComplete { get; private set; }

        public List<string> Output = new List<string>();

        public SinkFilter()
        {
            IsComplete = false;
        }

        public void SetInput(MemoryStream stream)
        {
            inputStream = stream;
        }

        public void Connect(IFilter nextFilter)
        {
            throw new InvalidOperationException("SourceFilter does not take an ouput stream");
        }

        public void Filter()
        {
            inputStream.Position = 0;
            using (StreamReader sr = new StreamReader(inputStream))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Output.Add(line);
                }
            }
            IsComplete = true;
        }
    }
}
