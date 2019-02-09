﻿using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.PipesAndFilters
{
    public class Alphabetizer : IFilter
    {
        MemoryStream outputStream = new MemoryStream();
        MemoryStream inputStream = new MemoryStream();
        public bool IsComplete { get; private set; }

        public Alphabetizer()
        {
            IsComplete = false;
        }

        public void SetInput(MemoryStream stream)
        {
            inputStream = stream;
        }

        public void Connect(IFilter nextFilter)
        {
            nextFilter.SetInput(outputStream);
        }

        public void Filter()
        {

            using (StreamReader sr = new StreamReader(inputStream))
            {
                inputStream.Position = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    StreamWriter sw = new StreamWriter(outputStream);
                    // need to do some alphebetizer stuff here?
                    sw.WriteLine(line + "!");
                    sw.Flush();
                }
            }
            IsComplete = true;
        }
    }
}
