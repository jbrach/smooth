using System.IO;

namespace Smooth.Library
{

    public class SorterFile
    {
        public FileInfo FileToSort { get; private set; }
        public  string SortRootDirectory { get; private set; }
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