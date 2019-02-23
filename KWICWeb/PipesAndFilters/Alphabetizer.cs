using System.IO;
using System.Collections.Generic;
using KWICWeb.Comparers;

namespace KWICWeb.PipesAndFilters
{
    public class Alphabetizer : IFilter
    {
        private MemoryStream outputStream;
        private MemoryStream inputStream;
        private List<string> sortedList;

        public void SetInput(MemoryStream stream)
        {
            inputStream = stream;
        }

        public void SetOutput(MemoryStream stream)
        {
            outputStream = stream;
        }

        public void Filter()
        {
            // Read each item from the stream and add it to the sorted list one line at a time. 
            using (StreamReader sr = new StreamReader(inputStream))
            {
                inputStream.Position = 0;
                sortedList = new List<string>();
                LowercasePrecedence lcSorter = new LowercasePrecedence();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (sortedList.Count == 0)
                    {
                        sortedList.Add(line);
                    }
                    else
                    {
                        int index = sortedList.BinarySearch(line, lcSorter);
                        if (index < 0)
                        {
                            // ~ is the xor operator. Returns the bitwise complement of the negative number returned by the comparer which is the index where it should
                            // be inserted into the list.
                            sortedList.Insert(~index, line);
                        }
                    }
                }
                // Write the entire sorted list back out. 
                foreach (string sortedLine in sortedList)
                {
                    StreamWriter sw = new StreamWriter(outputStream);
                    sw.WriteLine(sortedLine);
                    sw.Flush();
                }
            }
        }
    }
}
