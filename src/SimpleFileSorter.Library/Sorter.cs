using System.IO;
using System.Collections.Generic;
using System;

namespace SimpleFileSorter.Library
{
    public class Sorter
    {
        private readonly string _directory;

        private IList<SorterFile> _files = new List<SorterFile>();
        private readonly string _moveDirectory;

    /// <summary>
    /// Creates a Sorter object
    /// This object will drive the sorting of files into the respective directories based on createdate.
    ///   
    /// Support Year Organization on day 1
    /// </summary>
    /// <param name="sortingDirectory"></param>
    /// <param name="moveDirectory"></param>
        public Sorter(string sortingDirectory, string moveDirectory)
        {
            _directory = sortingDirectory;
            _moveDirectory = moveDirectory;
        }

        private IList<SorterFile> Sort()
        {
            DirectorySearch(new DirectoryInfo(_directory));
            return _files;
        }

        public IList<SorterFile> MoveFiles()
        {
            Sort();
            foreach(SorterFile file in _files)
            {
                file.Stage(_moveDirectory).Move();
            }
            return _files;

        }

         public IList<SorterFile> StageFiles()
         {
            Sort();
            foreach(SorterFile file in _files)
            {
                file.Stage(_moveDirectory);
            }
            return _files;

        }

        private void DirectorySearch(DirectoryInfo root)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;
            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles();
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                //log.Add(e.Message);
                System.Diagnostics.Debug.WriteLine(e.Message);

            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                 System.Diagnostics.Debug.WriteLine(e.Message);
            }
            
            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    //Console.WriteLine(fi.FullName);
                    if  (IsSortableFile(fi))
                    {
                        _files.Add(new SorterFile(fi));
                    }
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    DirectorySearch(dirInfo);
                }
            }            
        }

        private bool IsSortableFile(FileInfo fi)
        {
            var sortable = false;
            switch(fi.Extension)
            {
                case ".png":
                case ".jpg":
                case ".mod":
                    sortable = true;
                    break;

            }
            return sortable;
        }
    }
}