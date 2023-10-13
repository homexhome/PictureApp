using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureApp.Helpers
{
    public static class FileHelper
    {
        private static readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "data.txt");

        public static void SaveData(string data) {
            File.WriteAllText(FilePath, data);
        }

        public static string LoadData() {
            if (File.Exists(FilePath)) {
                return File.ReadAllText(FilePath);
            }
            return null;
        }
    }
}
