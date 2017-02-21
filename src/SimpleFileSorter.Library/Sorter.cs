using System.IO;
using System.Collections.Generic;

namespace SimpleFileSorter.Library
{
    public class Sorter
    {
        private int _year;
        private string _directory;

        private IList<SorterFile> _files = new List<SorterFile>();

        public Sorter(int year, string directory)
        {
            _year = year;
            _directory = directory;
        }

        public IList<SorterFile> Sort()
        {
            DirectorySearch(_directory);
            return _files;
        }

        public IList<SorterFile> MoveFiles(string moveToRootDirectory)
        {
            //TODO What type of threading.. makes sense.
            foreach(SorterFile file in _files)
            {
                file.Stage(moveToRootDirectory).Move();
            }
            return _files;

        }

          public IList<SorterFile> StageFiles(string stageToRootDirectory)
        {
            //TODO What type of threading.. makes sense.
            foreach(SorterFile file in _files)
            {
                file.Stage(stageToRootDirectory);
            }
            return _files;

        }

        private void DirectorySearch(string currentDirectory)
        {

            foreach (string nextDirectory in Directory.GetDirectories(currentDirectory))
            {
                foreach (string file in Directory.GetFiles(currentDirectory))
                {
                    _files.Add(new SorterFile(new FileInfo(file)));
                }
                DirectorySearch(nextDirectory);
            }
        }
    }
}