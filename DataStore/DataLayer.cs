using System.Collections.Generic;
using System.IO;

namespace DataStore
{
    public static class DataLayer
    {
        private static string basePath = @"C:\Users\Jacob\Documents\School\Software Architecture and Design\SE4433-5433\KWICWeb\";

        public static void StoreLines(string filename, string lines)
        {
            using (StreamWriter dataBaseFile = new StreamWriter(basePath + filename, false))
            {
                dataBaseFile.WriteLine(lines);
            }
        }

        public static List<string> RetrieveLines(string filename)
        {
            List<string> lines = new List<string>();
            foreach (string line in File.ReadLines(basePath + filename))
            {
                lines.Add(line);
            }
            return lines;
        }
    }
}
