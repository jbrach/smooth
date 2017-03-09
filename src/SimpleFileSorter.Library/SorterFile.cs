using System.IO;

namespace SimpleFileSorter.Library
{
    public interface ISortStrategy
    {
        string BuildNewFilePath(FileInfo originalFile, string destinationRootDirectory);
    }

    public class YearSortStrategy : ISortStrategy
    {
        public string BuildNewFilePath(FileInfo originalFile, string destinationRootDirectory)
        {
            DirectoryHelper rootDirectoryHelper = new DirectoryHelper(destinationRootDirectory);
            var createDate = File.GetCreationTime(originalFile.FullName);
            string newPath = Path.Combine(rootDirectoryHelper.GetFullPath(), originalFile.CreationTime.Year.ToString());
            rootDirectoryHelper.BuildSubDirectory(newPath);
            return newPath;
        }
    }

    public class SorterFile
    {
        private readonly FileInfo FileToSort;
        private readonly string SortRootDirectory;
        public bool Moved { get; private set; }
        public string StagedFilePath { get; private set; }


        public SorterFile(FileInfo fileToSort, string sortRootDirectory)
        {

            FileToSort = fileToSort;
            SortRootDirectory = sortRootDirectory;
            Moved = false;
        }


        public SorterFileMover Stage( ISortStrategy strategy )
        {
       
            StagedFilePath = strategy.BuildNewFilePath(FileToSort, SortRootDirectory);
            var moverFactory = new SorterFileMover.Factory();
            return moverFactory.Create(this);
        }


        public class SorterFileMover
        {
            private SorterFile _file;

            private SorterFileMover(SorterFile file)
            {
                _file = file;
            }

            public void Move()
            {
                System.Diagnostics.Debug.WriteLine("Moving " + _file.FileToSort.FullName + " TO: " + _file.StagedFilePath);
                var newFile = Path.Combine(_file.StagedFilePath, _file.FileToSort.Name);

                if (!File.Exists(newFile))
                {
                    File.Move(_file.FileToSort.FullName, Path.Combine(_file.StagedFilePath, _file.FileToSort.Name));
                    _file.Moved = true;
                }

            }
            public class Factory
            {
                public SorterFileMover Create(SorterFile file)
                {
                    return new SorterFileMover(file);
                }
            }
        }
    }


}