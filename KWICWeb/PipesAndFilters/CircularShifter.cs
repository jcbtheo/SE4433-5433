using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.PipesAndFilters
{
    public class CircularShifter : IFilter
    {
        private MemoryStream outputStream = new MemoryStream();
        private MemoryStream inputStream = new MemoryStream();

        public bool IsComplete { get; private set; }

        public CircularShifter()
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
                    string[] words = line.Split(" ");
                    StreamWriter sw = new StreamWriter(outputStream);

                    for (int i = 0; i < words.Length; i++)
                    {
                        sw.WriteLine(string.Join(" ", words));
                        sw.Flush();

                        string firstWord = words[0];
                        for (int j = 0; j < words.Length - 1; j++)
                        {
                            words[j] = words[j + 1];
                        }
                        words[words.Length - 1] = firstWord;
                    }
                }
            }
            IsComplete = true;
        }
    }
}
