using System.IO;

namespace Smooth.Library
{

    public class StageFileCommand
    {
        public FileInfo FileToSort { get; private set; }
        public  string SortRootDirectory { get; private set; }
       
        public string StagedFilePath { get; private set; }


        public StageFileCommand(FileInfo fileToSort, string sortRootDirectory)
        {

            FileToSort = fileToSort;
            SortRootDirectory = sortRootDirectory;
        }


        public StagedFile Stage( ISortStrategy strategy )
        {
       
            StagedFilePath = strategy.BuildNewFilePath(FileToSort, SortRootDirectory);
            return new StagedFile(this.FileToSort, this.StagedFilePath);
            
        }
    }
}