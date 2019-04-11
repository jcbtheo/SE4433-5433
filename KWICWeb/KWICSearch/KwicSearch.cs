using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DataStore;

namespace KWICWeb.KWICSearch
{
    public class KwicSearch
    {
        public List<string> Search(string searchInput)
        {
            List<string> shiftedLines = DataLayer.RetrieveLines("output.txt");
            List<string> originalLines = DataLayer.RetrieveLines("input.txt");
            List<string> searchTerms = searchInput.Trim().Split(" ").ToList();
            List<string> firstWordsFromShifts = shiftedLines.Select(x => x.Split(" ")[0].ToLower()).ToList();
            List<int> firstWordMatchIndices = new List<int>();
            List<string> originalLineMatches = new List<string>();

            if (searchTerms.Count > 0)
            {
                string lower = searchTerms[0].ToLower();
                // find the first binary search index of the first search term. 
                int firstMatchIndex = firstWordsFromShifts.BinarySearch(lower);
                // if it is found, check the lines before and after for matches on that term. If there are no matches on a the first term then there can be no matches at all. 
                if (firstMatchIndex >= 0)
                {
                    //int tempIndex = firstMatchIndex;
                    CheckFollowingIndexes(firstMatchIndex, lower, firstWordsFromShifts, firstWordMatchIndices);
                    CheckPreviousIndexes(--firstMatchIndex, lower, firstWordsFromShifts, firstWordMatchIndices);
                    originalLineMatches = GetOriginalLines(firstWordMatchIndices, shiftedLines, originalLines);

                    // remove all original line matches that do not contain the subsequent search terms.
                    foreach (string term in searchTerms)
                    {
                        originalLineMatches = originalLineMatches.Where(x => x.ToLower().Contains(term.ToLower())).ToList();
                    }
                }
            }
            return originalLineMatches;
        }

        private List<string> GetOriginalLines(List<int> firstWordIndices, List<string> shiftedLines, List<string> originalLines)
        {
            var regex = new Regex(@"https?:\/\/(?:www\.)[a-zA-Z0-9]+\.(?:edu|com|org|net)", RegexOptions.Compiled);
            Dictionary<string, int> usedUrls = new Dictionary<string, int>();
            List<string> outputMatches = new List<string>();

            foreach (int index in firstWordIndices)
            {
                string url = regex.Match(shiftedLines[index]).ToString();
                // if the url hasn't already been used get it's original line
                if (usedUrls.TryAdd(url, 1))
                {
                    outputMatches.AddRange(originalLines.Where(x => x.Contains(url)).ToList());
                }
            }

            return outputMatches;
        }

        private void CheckPreviousIndexes(int index, string term, List<string> searchList, List<int> existingIndices)
        {
            while (index > 0)
            {
                if (searchList[index] == term.ToLower())
                {
                    existingIndices.Add(index);
                }
                else
                {
                    break;
                }
                index--;
            }
        }

        private void CheckFollowingIndexes(int index, string term, List<string> searchList, List<int> existingIndices)
        {
            while (index < searchList.Count)
            {
                if (searchList[index] == term.ToLower())
                {
                    existingIndices.Add(index);
                }
                else
                {
                    break;
                }
                index++;
            }
        }
    }
}
