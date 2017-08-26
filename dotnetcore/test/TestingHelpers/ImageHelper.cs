using System.IO;
using System.Reflection;
using System;

namespace Tests.TestingHelpers
{
    public class ImageHelper
    {

        public const string File_2012_M = "2012_M.jpg";

        public const string File_2012_S = "2012_S.jpg";

        public const string File_2013_M = "2013_M.jpg";

        public const string File_2014_L = "2014_L.jpg";
        private Random _random;

        public string Medium2012ImagePath { get { return Path.Combine(ImageHelper.GetAssemblyDirectory(), "images/", File_2012_M); } }
        public string Small2012ImagePath { get { return Path.Combine(ImageHelper.GetAssemblyDirectory(), "images/", File_2012_S); } }
        public string Medium2013ImagePath { get { return Path.Combine(ImageHelper.GetAssemblyDirectory(), "images/", File_2013_M); } }
        public string Large2014ImagePath { get { return Path.Combine(ImageHelper.GetAssemblyDirectory(), "images/", File_2014_L); } }

        public ImageHelper()
        {
            _random = new Random();
        }
        public static string GetAssemblyDirectory()
        {

            string codeBase = typeof(ImageHelper).GetTypeInfo().Assembly.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);

        }

        public FileInfo CopyMedium(string directory, DateTime createDate, string fileName = File_2012_M)
        {
            return CopyFile(Medium2012ImagePath, Path.Combine(directory, fileName), createDate);
        }

        public FileInfo CopySmall(string directory, DateTime createDate, string fileName = File_2012_S)
        {
            return CopyFile(Small2012ImagePath, Path.Combine(directory, fileName), createDate);
        }
        public FileInfo CopyLarge(string directory, DateTime createDate, string fileName = File_2014_L)
        {
           return CopyFile(Large2014ImagePath, Path.Combine(directory, fileName), createDate);

        }


        private FileInfo CopyFile(string source, string destination, DateTime createDate)
        {
            if (File.Exists(destination))
            {
                throw new Exception("File Exists");
            }
            else
            {
                File.Copy(source, destination);
                File.SetCreationTime(destination, createDate);
                File.SetLastWriteTime(destination, createDate);
            }

            return new FileInfo(destination);
        }

        public DateTime GetRandomDate()
        {
            return new DateTime(_random.Next(1950, DateTime.Now.Year), _random.Next(1, 12), _random.Next(1, 25));
        }

        public string GetRandomImageFileName()
        {
            return _random.Next().ToString() + _random.Next(9999).ToString() + ".jpg";
        }

    }
}
