namespace KWICWeb.SharedDataKWIC
{
    public class Input
    {
        public void Store(LineStorage storage, string input)
        {
            string[] lines = input.Split('\n');
            int lineIndex = 0;

            foreach (string line in lines)
            {
                string[] words = line.Trim().Split(' ');

                foreach (string word in words)
                {
                    storage.SetWord(lineIndex, word);
                }

                lineIndex++;
            }
        }
    }
}
