using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace KWICWeb.SharedDataKWIC
{
    public class Input
    {
        public void Store(LineStorage storage, string input)
        {
            var regex = new Regex(@"https?:\/\/(?:www\.)[a-zA-Z0-9]+\.(?:edu|com|org|net)", RegexOptions.Compiled);
            List<string> lines = new List<string>(input.Split('\n'));
            int lineIndex = 0;

            foreach (string line in lines)
            {
                bool hasRegexMatch = false;
                string[] words = Regex.Split(line.Trim(), @"\s+");

                foreach (string word in words)
                {
                    // check for URL regex
                    if (regex.IsMatch(word))
                    {
                        hasRegexMatch = true;
                        storage.SetUrl(word);
                    }
                    else
                    {
                        storage.SetWord(lineIndex, word);
                    }
                }
                if (!hasRegexMatch)
                {
                    storage.SetUrl("");
                }
                lineIndex++;
            }
        }
    }
}
