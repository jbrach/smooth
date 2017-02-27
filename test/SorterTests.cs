using Xunit;
using SimpleFileSorter.Library;
using System.IO;
using System.Reflection;
using System.Resources;

namespace Tests
{
    public class SorterTests
    {
        
        [Fact]
        public void Test_Files_Load()
        {
            //Issues making files a resource in the test assembly
        
            var assembly = Assembly.GetEntryAssembly();
        
            ResourceManager rm = new ResourceManager("tests.images", assembly);
            ResourceManager rm1 = new ResourceManager("images", assembly);
            ResourceManager rm2 = new ResourceManager("test.images", assembly);
    
            var resourceStream = assembly.GetManifestResourceStream("test.images.2012_M.jpg");
            Assert.NotNull(resourceStream);
        }

        [Fact]
        public void Stages_File_Given_Created_This_Year() 
        {
            var newFilePath = System.IO.Path.GetTempFileName();
            FileInfo info = new FileInfo(newFilePath);
            info.Refresh();
        
            File.Move(newFilePath, newFilePath.Replace(info.Extension,".jpg"));
            var sorter = new Sorter(info.DirectoryName, info.DirectoryName);
            sorter.MoveFiles();
            var expectedPathAfterSorting = Path.Combine(info.DirectoryName, info.CreationTime.Year.ToString(),info.Name.Replace(info.Extension,".jpg"));
            Assert.True(File.Exists(expectedPathAfterSorting));
      
          
        }


    
        public void Stages_ManyFiles_Given_Created_This_Year() 
        {
           Assert.True(false);
           
        }
        

        public void Stages_Files_Given_Created_Last_Year() 
        {
           Assert.True(false);
           
        }
    }
}
