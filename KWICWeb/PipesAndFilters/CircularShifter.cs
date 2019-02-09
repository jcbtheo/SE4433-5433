using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KWICWeb.PipesAndFilters
{
    public class CircularShifter : IFilter<string>
    {
        public IEnumerable<string> Filter(IEnumerable<string> input)
        {
            List<string> shiftedLines = new List<string>();

            foreach(string line in input)
            {
                string[] words = line.Split(" ");
                
                for (int i = 0; i < words.Length; i++)
                {
                    shiftedLines.Add(string.Join(" ", words));
                    string firstWord = words[0];

                    for (int j = 0; j < words.Length - 1; j++)
                    {
                        words[j] = words[j + 1];
                    }
                    words[words.Length - 1] = firstWord;
                }
            }

            return shiftedLines;
        }
    }
}
