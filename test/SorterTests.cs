using Xunit;
using Smooth.Library;
using System.IO;
using System;
using System.Linq;
using Tests.TestingHelpers;
using Smooth.Library.FileNaming;
using System.Collections.Generic;

namespace Tests
{
    public class SorterTests : IDisposable
    {

        private readonly string _rootPath;
        private readonly DirectoryInfo _sortingDirectory;
        private readonly DirectoryInfo _destinationDirectory;

        public SorterTests()
        {
            Random r = new Random(DateTime.Now.Millisecond);

            _rootPath = Path.GetTempPath();
            _sortingDirectory = Directory.CreateDirectory(Path.Combine(_rootPath, "testing_sorting_" + r.Next(9999).ToString()));
            _destinationDirectory = Directory.CreateDirectory(Path.Combine(_rootPath, "testing_destination_" + r.Next(99999).ToString()));

        }

        [Fact]
        public void ImageHelper_Finds_Images()
        {

            Assert.True(File.Exists(Path.Combine(ImageHelper.GetAssemblyDirectory(), "images/2012_M.jpg")));

        }

        [Fact]
        public void File_Moved_Current_Year_Folder_Given_Created_This_Year()
        {

            ImageHelper helper = new ImageHelper();
            string imageFileName = helper.GetRandomImageFileName();
            var fileInfo = helper.CopyLarge(_sortingDirectory.FullName, DateTime.Now, imageFileName);
            var destinationName = (new CreateDateAndUploadDateStrategy(fileInfo)).GenerateName();

            var sorter = new Sorter(_sortingDirectory.FullName, _destinationDirectory.FullName);
            sorter.RaiseFileSortEvent += HandleSortEvent;

            sorter.Sort();


            Assert.True(File.Exists(Path.Combine(_destinationDirectory.FullName, DateTime.Now.Year.ToString(), destinationName)));
        }

        private void HandleSortEvent(object sender, StageFileCommand e)
        {
            e.Stage(new YearSortStrategy()).Move(false);
        }


        [Fact]
        public void Move_Multipile_Files_Creates_MultipleDirectories_Given_Files_Created_Multiple_Years()
        {

            ImageHelper helper = new ImageHelper();
            helper.CopyLarge(_sortingDirectory.FullName, new DateTime(2014, 3, 7));
            helper.CopyMedium(_sortingDirectory.FullName, new DateTime(2015, 3, 7));
            helper.CopySmall(_sortingDirectory.FullName, new DateTime(2016, 3, 7));
            var sorter = new Sorter(_sortingDirectory.FullName, _destinationDirectory.FullName);
            sorter.RaiseFileSortEvent += HandleSortEvent;

            sorter.Sort();

            _destinationDirectory.Refresh();
            Assert.True(_destinationDirectory.EnumerateDirectories().Any(x => x.Name == "2014"));
            Assert.True(_destinationDirectory.EnumerateDirectories().Any(x => x.Name == "2015"));
            Assert.True(_destinationDirectory.EnumerateDirectories().Any(x => x.Name == "2016"));
        }



        [Fact]

        public void Move_Multipile_Files_Creates_MultipleDirectories_Given_LargerNumber_Files()
        {

            //TODO Figure out how to test performance.  Single Threaded verse async, parallal  Client of sort library will decide. 


            int i = 0;
            while (i < 100)
            {
                ImageHelper helper = new ImageHelper();
                helper.CopyLarge(_sortingDirectory.FullName, helper.GetRandomDate(), i.ToString() + "_Large_" + helper.GetRandomImageFileName());
                helper.CopyMedium(_sortingDirectory.FullName, helper.GetRandomDate(), i.ToString() + "_Medium_" + helper.GetRandomImageFileName());
                helper.CopySmall(_sortingDirectory.FullName, helper.GetRandomDate(), i.ToString() + "_Small_" + helper.GetRandomImageFileName());
                i++;
            }
            var sorter = new Sorter(_sortingDirectory.FullName, _destinationDirectory.FullName);
            sorter.RaiseFileSortEvent += HandleSortEvent;

            sorter.Sort();
            _destinationDirectory.Refresh();
            Assert.True(_destinationDirectory.EnumerateDirectories().Count() > 1);
        }

        [Fact]
        public void Sorter_Run_Multiple_Times_Contains_Both_Images_Current_Year()
        {

            ImageHelper helper = new ImageHelper();
            string imageFileName1 = helper.GetRandomImageFileName();
            var fileInfo1 = helper.CopyLarge(_sortingDirectory.FullName, DateTime.Now, imageFileName1);

            var generatedName1 = new CreateDateAndUploadDateStrategy(fileInfo1);
            var name1 = generatedName1.GenerateName();

            var sorter = new Sorter(_sortingDirectory.FullName, _destinationDirectory.FullName);
            sorter.RaiseFileSortEvent += HandleSortEvent;
            sorter.Sort();

            string imageFileName2 = helper.GetRandomImageFileName();
            var fileInfo2 = helper.CopyLarge(_sortingDirectory.FullName, DateTime.Now, imageFileName2);

            var generatedName2 = new CreateDateAndUploadDateStrategy(fileInfo2);
            var name2 = generatedName2.GenerateName();

            var sorter1 = new Sorter(_sortingDirectory.FullName, _destinationDirectory.FullName);
            sorter1.RaiseFileSortEvent += HandleSortEvent;
            sorter1.Sort();


            Assert.True(File.Exists(Path.Combine(_destinationDirectory.FullName, DateTime.Now.Year.ToString(), name1)));
            Assert.True(File.Exists(Path.Combine(_destinationDirectory.FullName, DateTime.Now.Year.ToString(), name2)));
        }



        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _sortingDirectory.Delete(true);
                    _destinationDirectory.Delete(true);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SorterTests() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}
