using Xunit;
using Smooth.Library;
using Tests.TestingHelpers;

namespace Tests
{
    public class DirectoryHelperTests
    {
        [Fact]
        public void DirectoryHelper_Throws_DirectoryNotFoundException_When_Directory_Does_Not_Exist() 
        {
            Assert.Throws(typeof(System.IO.DirectoryNotFoundException), 
            ()=> new DirectoryHelper("/blah/blah/foo/"));
        }

         [Fact]
        public void DirectoryHelper_Does_Not_Throw_DirectoryNotFoundException_When_Directory_Does_Not_Exist() 
        {
            Assert.NotNull(new DirectoryHelper(ImageHelper.GetAssemblyDirectory()).GetFullPath());
        }
        
    }
}
