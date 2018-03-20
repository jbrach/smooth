using System.IO;
using System;

namespace Smooth.Library
{

    public class Sorter
    {
        private readonly string _fileSourceRootDirectory;

        private readonly string _fileDestinationRootDirectory;

        private readonly int _filesSortedCounter;

       
        public delegate void SortHandler(StageFileCommand sort);

      
        public event EventHandler<StageFileCommand> RaiseFileSortEvent;


    /// <summary>
    /// Creates a Sorter object
    /// This object will drive the sorting of files into the respective directories based on createdate.
    ///   
    /// Support Year Organization on day 1
    /// </summary>
    /// <param name="sortingDirectory">Root directory contains the files to sort</param>
    /// <param name="sortMoveToRootDirectory">Destination directory.  Root directory the files will end up with the strategy applied</param>
        public Sorter(string sortingDirectory, string sortMoveToRootDirectory)
        {
            _fileSourceRootDirectory = sortingDirectory;
            _fileDestinationRootDirectory = sortMoveToRootDirectory;
            _filesSortedCounter = 0;
        }

        public int Sort()
        {
            DirectorySearch(new DirectoryInfo(_fileSourceRootDirectory));
            return _filesSortedCounter;
        }

        private void DirectorySearch(DirectoryInfo root)
        {
            System.IO.FileInfo[] files = null;
            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles();
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                 System.Diagnostics.Debug.WriteLine(e.Message);
            }
            
            if (files != null)
            {
                //Find the files that need to be sorted
                SortFiles(files);

                // Now find all the subdirectories under this directory.
                SearchDirectories(root);
            }
        }

        private void SearchDirectories(DirectoryInfo root)
        {
            foreach (System.IO.DirectoryInfo dirInfo in root.GetDirectories())
            {
                // Resursive call for each subdirectory.
                DirectorySearch(dirInfo);
            }

          
        }

        private void SortFiles(FileInfo[] files)
        {
            foreach (System.IO.FileInfo fi in files)
            {
               
                if (IsSortableFile(fi))
                {
                    EventHandler<StageFileCommand> handler = RaiseFileSortEvent;
                    if (handler!=null)
                    {
                         handler(this, e: new StageFileCommand(fi,_fileDestinationRootDirectory));
                    }

                }
            }
        }

        protected virtual bool IsSortableFile(FileInfo fi)
        {
            var sortable = false;
            switch(fi.Extension.ToLowerInvariant())
            {
                case ".png":
                case ".jpg":
                case ".mod":
                    sortable = true;
                    break;
                default:
                    sortable = false;
                    break;

            }
            return sortable;
        }
    }
}