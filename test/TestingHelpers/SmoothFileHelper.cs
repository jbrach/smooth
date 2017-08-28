using System.IO;
using System;

namespace Tests.TestingHelpers
{
    public class SmoothFileHelper
    {
        public SmoothFileHelper(string fileName)
        {
           Random r = new Random(DateTime.Now.Millisecond);
           var rootPath = Path.GetTempPath();
           SortingDirectory = Directory.CreateDirectory(Path.Combine(rootPath, "testing_sorting_" + r.Next(9999).ToString()));
           DestinationDirectory = Directory.CreateDirectory(Path.Combine(rootPath, "testing_destination_" + r.Next(99999).ToString()));
           ImageHelper helper = new ImageHelper();
           TestFile = helper.CopyLarge(SortingDirectory.FullName, DateTime.Now);
           
        }

        public DirectoryInfo SortingDirectory { get; }
        public DirectoryInfo DestinationDirectory { get; }
        public FileInfo TestFile { get; private set; }

        public static SmoothFileHelper GenerateTestFile(string fileName)
        {
            return new SmoothFileHelper(fileName);
        }
    }
}
