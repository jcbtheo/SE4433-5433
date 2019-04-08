using System.Text.RegularExpressions;

namespace KWICWeb.SharedDataKWIC
{
    public class Input
    {
        public void Store(LineStorage storage, string input)
        {
            var regex = new Regex(@"https?:\/\/[a-zA-Z0-9]+\.(?:edu|com|org|net)", RegexOptions.Compiled);
            string[] lines = input.Split('\n');
            int lineIndex = 0;

            foreach (string line in lines)
            {
                string[] words = line.Trim().Split(' ');

                foreach (string word in words)
                {
                    // check for URL regex
                    if (regex.IsMatch(word))
                    {
                        storage.SetUrl(word);
                    }
                    else
                    {
                        storage.SetWord(lineIndex, word);
                    }
                }

                lineIndex++;
            }
        }
    }
}
