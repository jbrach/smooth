using Xunit;
using Smooth.Library;
using Smooth.Library.Extensions;
using Smooth.Library.FileNaming;
using Tests.TestingHelpers;

namespace Test.Extensions
{
    public class CreateDateAndCustomTextStrategyTest
    {
        [Fact]
        public void FileNameContains_TodaysDate_WhenFileCreated_Today()
        {
            string name = "MyFile01";
            var testSetup = Tests.TestingHelpers.SmoothFileHelper.GenerateTestFile(name);
            var strategy = new CreateDateAndCustomTextStrategy("name", testSetup.TestFile);
            var result = strategy.GenerateName();
            Assert.True(result.Contains(System.DateTime.Now.ToString("MMddyyyy")));

        }

        [Fact]
        public void FileNameContains_CustomText_WhenFileCreated_Today()
        {
            string name = "MyFile01";
            string customText = "SmoothUpload1";
            var testSetup = Tests.TestingHelpers.SmoothFileHelper.GenerateTestFile(name);

            var strategy = new CreateDateAndCustomTextStrategy(customText, testSetup.TestFile);
            var result = strategy.GenerateName();
            Assert.True(result.Contains(customText));

        }


        [Fact]
        public void FileNameContains_FileName_WhenFileCreated_Today()
        {
            string name = new ImageHelper().GetRandomImageFileName();
            string customText = "SmoothUpload1";
            var testSetup = Tests.TestingHelpers.SmoothFileHelper.GenerateTestFile(name);

            var strategy = new CreateDateAndCustomTextStrategy(customText, testSetup.TestFile);
            var result = strategy.GenerateName();
            Assert.True(result.Contains(name));

        }
    }
}