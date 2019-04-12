using System.Collections.Generic;
using System.Linq;
using System;
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
            List<string> shiftedOriginalIndices = DataLayer.RetrieveLines("shiftedOriginalLines.txt");
            List<string> searchTerms = searchInput.Trim().Split(" ").ToList();
            List<string> firstWordsFromShifts = shiftedLines.Select(x => x.Split(" ")[0].ToLower()).ToList();
            List<string> matchedOriginalLines = new List<string>();
            List<int> originalIndexMatches = new List<int>();

            foreach (string term in searchTerms)
            {
                List<int> firstWordMatchIndices = BinarySearchIndices(term.ToLower(), firstWordsFromShifts);
                if (originalIndexMatches.Count == 0)
                {
                    foreach (int index in firstWordMatchIndices)
                    {
                        originalIndexMatches.Add(Int32.Parse(shiftedOriginalIndices[index]));
                    }
                }
                else
                {
                    List<int> tempLineMatches = new List<int>();
                    foreach (int index in firstWordMatchIndices)
                    {
                        tempLineMatches.Add(Int32.Parse(shiftedOriginalIndices[index]));
                    }
                    // remove non-duplicate indices
                    originalIndexMatches = originalIndexMatches.Intersect(tempLineMatches).ToList();
                }

            }
            originalIndexMatches = originalIndexMatches.Distinct().ToList();
            foreach (int matchIndex in originalIndexMatches)
            {
                matchedOriginalLines.Add(originalLines[matchIndex]);
            }
            return matchedOriginalLines;
        }

        private List<int> BinarySearchIndices(string term, List<string> wordsToMatch)
        {
            string lower = term;
            // find the first binary search index of the first search term. 
            int firstMatchIndex = wordsToMatch.BinarySearch(lower);
            // if it is found, check the lines before and after for matches on that term. If there are no matches on a the first term then there can be no matches at all. 
            List<int> firstMatchIndices = new List<int>();

            if (firstMatchIndex >= 0)
            {
                //int tempIndex = firstMatchIndex;
                firstMatchIndices.AddRange(CheckFollowingIndexes(firstMatchIndex, lower, wordsToMatch));
                firstMatchIndices.AddRange(CheckPreviousIndexes(--firstMatchIndex, lower, wordsToMatch));
            }
            return firstMatchIndices;
        }

        private List<int> CheckPreviousIndexes(int index, string term, List<string> searchList)
        {
            List<int> existingIndices = new List<int>();

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
            return existingIndices;
        }

        private List<int> CheckFollowingIndexes(int index, string term, List<string> searchList)
        {
            List<int> existingIndices = new List<int>();
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
            return existingIndices;
        }
    }
}
