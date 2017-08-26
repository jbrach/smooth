using Xunit;
using Smooth.Library;
using Smooth.Library.Extensions;
using Smooth.Library.FileNaming;

namespace Test.Extensions
{
     public class CreateDateAndCustomTextStrategyTest
    {
         [Fact]
        public void FileNameContainsTodaysDate_WhenFileCreated_Today() 
        {
            string name = "MyFile01";
            var testSetup = Tests.TestingHelpers.SmoothFileHelper.GenerateTestFile(name);
            var strategy = new CreateDateAndCustomTextStrategy("name",testSetup.TestFile);
            var result = strategy.GenerateName();
            Assert.True(result.Contains(System.DateTime.Now.ToString("MMddyyyy")));

        }
    }
}