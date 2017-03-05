using Xunit;
using SimpleFileSorter.Library;
using System.IO;
using System.Resources;
using System;
using System.Linq;

namespace Tests
{
    public class SorterTests
    {

        [Fact]
        public void ImageHelper_Finds_Images()
        {

            Assert.True(File.Exists(Path.Combine(ImageHelper.GetAssemblyDirectory(), "images/2012_M.jpg")));

        }

        [Fact]
        public void File_Moved_Current_Year_Folder_Given_Created_This_Year()
        {
            var newFilePath = System.IO.Path.GetTempFileName();
            FileInfo info = new FileInfo(newFilePath);
            File.Move(newFilePath, newFilePath.Replace(info.Extension, ".jpg"));
            var sorter = new Sorter(info.DirectoryName, info.DirectoryName);
            sorter.MoveFiles();
            var expectedPathAfterSorting = Path.Combine(info.DirectoryName, DateTime.Now.Year.ToString(), info.Name.Replace(info.Extension, ".jpg"));
            Assert.True(File.Exists(expectedPathAfterSorting));
        }





        [Fact]
    
        public void Move_Multipile_Files_Creates_MultipleDirectories_Given_Files_Created_Multiple_Years()
        {

            Random r = new Random(DateTime.Now.Millisecond);

            var rootPath = Path.GetTempPath();
            var sortingDirectory = Directory.CreateDirectory(Path.Combine(rootPath, "testing_sorting_" + r.Next(9999).ToString()));
            var destinationDirectory = Directory.CreateDirectory(Path.Combine(rootPath, "testing_destination_" + r.Next(99999).ToString()));

            try
            {
                ImageHelper helper = new ImageHelper();
                helper.CopyLarge(sortingDirectory.FullName, new DateTime(2014,3,7));
                helper.CopyMedium(sortingDirectory.FullName, new DateTime(2015,3,7));
                helper.CopySmall(sortingDirectory.FullName, new DateTime(2016,3,7));
                var sorter = new Sorter(sortingDirectory.FullName, destinationDirectory.FullName);
                sorter.MoveFiles();

                destinationDirectory.Refresh();
                Assert.True(destinationDirectory.EnumerateDirectories().Any(x => x.Name == "2014"));
                Assert.True(destinationDirectory.EnumerateDirectories().Any(x => x.Name == "2015"));
                Assert.True(destinationDirectory.EnumerateDirectories().Any(x => x.Name == "2016"));
            }
            finally
            {
                sortingDirectory.Delete(true);
                destinationDirectory.Delete(true);
            }



        }


            [Fact]
            public void LargeNumbers()
        {

            Random r = new Random(DateTime.Now.Millisecond);

            var rootPath = Path.GetTempPath();
            var sortingDirectory = Directory.CreateDirectory(Path.Combine(rootPath, "testing_sorting_" + r.Next(9999).ToString()));
            var destinationDirectory = Directory.CreateDirectory(Path.Combine(rootPath, "testing_destination_" + r.Next(99999).ToString()));

            try
            {
                int i = 0;
                while ( i < 100)
                {
                    ImageHelper helper = new ImageHelper();
                    helper.CopyLarge(sortingDirectory.FullName, helper.GetRandomDate(), i.ToString() + "_Large_"+ helper.GetRandomImageFile() );
                    helper.CopyMedium(sortingDirectory.FullName, helper.GetRandomDate(), i.ToString() + "_Medium_" + helper.GetRandomImageFile() );
                    helper.CopySmall(sortingDirectory.FullName, helper.GetRandomDate(), i.ToString() + "_Small_" + helper.GetRandomImageFile() );
                    i++;
                }
                var sorter = new Sorter(sortingDirectory.FullName, destinationDirectory.FullName);
                sorter.MoveFiles();

                destinationDirectory.Refresh();
                Assert.True(destinationDirectory.EnumerateDirectories().Count()>1 );
           
            }
            finally
            {
                sortingDirectory.Delete(true);
                destinationDirectory.Delete(true);
            }



        }
    }
}
