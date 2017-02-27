using System.IO;

namespace SimpleFileSorter.Library
{
    public class SorterFile
    {
        private readonly FileInfo OriginalFileInfo;
        public bool Moved { get; private set; }
        public string StagedFilePath { get; private set; }


        public SorterFile(FileInfo fileInfo)
        {
            
           /* OriginalFilePathAndName = fileInfo.FullName;
            FileNameOnly = fileInfo.Name;
            CreateDate = fileInfo.CreationTime;
            */
            OriginalFileInfo = fileInfo;
            
            Moved = false;
            }


        public SorterFileMover Stage(string moveToDirectory)
        { 
            DirectoryHelper rootDirectoryHelper = new DirectoryHelper(moveToDirectory);
            var createDate = File.GetCreationTime(OriginalFileInfo.FullName);

            StagedFilePath = Path.Combine(rootDirectoryHelper.GetFullPath(),OriginalFileInfo.CreationTime.Year.ToString());
            rootDirectoryHelper.BuildSubDirectory(StagedFilePath);

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
                 System.Diagnostics.Debug.WriteLine("Moving " + _file.OriginalFileInfo.FullName + " TO: " +_file.StagedFilePath);
                 var newFile =  Path.Combine(_file.StagedFilePath,_file.OriginalFileInfo.Name);

                 if (!File.Exists(newFile))
                 {
                    File.Move(_file.OriginalFileInfo.FullName, Path.Combine(_file.StagedFilePath,_file.OriginalFileInfo.Name));
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