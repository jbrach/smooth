using System.IO;
using Smooth.Library.Extensions;

namespace Smooth.Library
{
    public class YearSortStrategy : ISortStrategy
    {
        
        public string BuildNewFilePath(FileInfo originalFile, string destinationRootDirectory)
        {
            DirectoryHelper rootDirectoryHelper = new DirectoryHelper(destinationRootDirectory);
            var sortDate = originalFile.GetCreateDate();
            string newPath = Path.Combine(rootDirectoryHelper.GetFullPath(), sortDate.Year.ToString());
            rootDirectoryHelper.BuildSubDirectory(newPath);
            return newPath;
        }


    }


}