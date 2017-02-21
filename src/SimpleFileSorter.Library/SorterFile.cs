using System;
using System.IO;

namespace SimpleFileSorter.Library
{
    public class SorterFile
    {
        public SorterFile(FileInfo fileInfo)
        {
            OriginalFilePathAndName = fileInfo.FullName;
            FileNameOnly = fileInfo.Name;
            CreateDate = fileInfo.CreationTime;
            Moved = false;
            Staged = false;
        }

        public string NewFilePathAndName { get; private set; }
        public string FileNameOnly{ get; private set; }
        public DateTime CreateDate { get; private set; }
        public string OriginalFilePathAndName{ get; private set; }
        public bool Moved { get; private set; }
        public bool Staged { get; private set; }

        public SorterFileMover Stage(string newDirectory)
        {
            //Check directories
            DirectoryHelper helper = new DirectoryHelper(newDirectory);
            
            NewFilePathAndName = Path.Combine(helper.GetCleanDirectory(), FileNameOnly);
            //Check file name and other error conditions

            //Store this variabile for clients.  Might need to let them know why this file could not be sent as well
            Staged = true;

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
                //TODO Use the year and create the directory if it does not exist
                //Combine the path with the year
                //Ensure the new file does not exist
                //
                //File.Move(_file.OriginalFilePathAndName, _file.NewFilePathAndName);
                _file.Moved = true;

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