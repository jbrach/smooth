using System.IO;

namespace Smooth.Library
{
    public class YearSortStrategy : ISortStrategy
    {
        
        public string BuildNewFilePath(FileInfo originalFile, string destinationRootDirectory)
        {
            DirectoryHelper rootDirectoryHelper = new DirectoryHelper(destinationRootDirectory);
            //For Sorting files correctly we will look at the oldest between create date and the modified date
            //Copied files the create date is set to the datetime it was copied
            var createDate = File.GetCreationTime(originalFile.FullName);
            var modifiedDate = File.GetLastWriteTime(originalFile.FullName);
            var sortDate = (createDate<=modifiedDate)? createDate: modifiedDate;

            string newPath = Path.Combine(rootDirectoryHelper.GetFullPath(), sortDate.Year.ToString());
            rootDirectoryHelper.BuildSubDirectory(newPath);
            return newPath;
        }


    }


}