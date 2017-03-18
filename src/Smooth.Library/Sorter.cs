using System.IO;
using System;

namespace Smooth.Library
{

    public class Sorter
    {
        private readonly string _directory;

        private readonly string _sortMoveToRootDirectory;

        private int _sortedFileCount;

        //This delegate can be used to point to methods
        //which return void and take a string.
        public delegate void SortHandler(SorterFile sort);

        //This event can cause any method which conforms
        //to MyEventHandler to be called.
        public event EventHandler<SorterFile> RaiseFileSortEvent;


    /// <summary>
    /// Creates a Sorter object
    /// This object will drive the sorting of files into the respective directories based on createdate.
    ///   
    /// Support Year Organization on day 1
    /// </summary>
    /// <param name="sortingDirectory"></param>
    /// <param name="sortMoveToRootDirectory"></param>
        public Sorter(string sortingDirectory, string sortMoveToRootDirectory)
        {
            _directory = sortingDirectory;
            _sortMoveToRootDirectory = sortMoveToRootDirectory;
            _sortedFileCount = 0;
        }

        public int Sort()
        {
            DirectorySearch(new DirectoryInfo(_directory));
            return _sortedFileCount;
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
                    EventHandler<SorterFile> handler = RaiseFileSortEvent;
                    if (handler!=null)
                    {
                         handler(this, new SorterFile(fi,_sortMoveToRootDirectory));
                    }

                }
            }
        }

        protected virtual bool IsSortableFile(FileInfo fi)
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