using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataStore;

namespace KWICWeb.KWICSearch
{
    public class KwicSearch
    {
        public List<string> Search(string searchInput)
        {
            List<string> lines = DataLayer.RetrieveLines();
            List<string> searchTerms = searchInput.Split(" ").ToList();

            List<string> test = new List<string>();

            foreach (string term in searchTerms)
            {
                // gets the first word in the stored line and compares it to the passed term
                test = lines.Where(x => x.Split(" ")[0].ToLower() == term.ToLower()).ToList();
            }


            return test;


        }
    }
}
