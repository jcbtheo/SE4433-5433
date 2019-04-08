using System.Collections.Generic;
using System.IO;

namespace DataStore
{
    public static class DataLayer
    {
        // need to set path, likely in constructor 
        private static string basePath = @"C:\Users\Jacob\Documents\School\Software Architecture and Design\SE4433-5433\KWICWeb\test.txt";

        public static void StoreLines(List<string> lines)
        {
            using (StreamWriter dataBaseFile = new StreamWriter(basePath, false))
            {
                foreach (string line in lines)
                {
                    dataBaseFile.WriteLine(line);
                }
            }
        }

        public static List<string> RetrieveLines()
        {
            List<string> lines = new List<string>();
            foreach (string line in File.ReadLines(basePath))
            {
                lines.Add(line);
            }
            return lines;
        }
    }
}
