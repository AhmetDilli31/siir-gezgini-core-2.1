using System.Collections.Generic;
using System.IO;

namespace SiirGezgini.Shared
{
    public static class FileHelper
    {
        public static void DeleteExistsFiles(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static IList<string> GetFilesTitles(string path)
        {
            return Directory.GetFiles(path);
        }
    }
}
