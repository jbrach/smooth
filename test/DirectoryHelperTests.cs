using Xunit;
using SimpleFileSorter.Library;

namespace Tests
{
    public class DirectoryHelperTests
    {
        [Fact]
        public void Test1() 
        {
            Assert.Throws(typeof(System.IO.DirectoryNotFoundException), 
            ()=> new DirectoryHelper("/blah/blah/foo/"));
        }
        
    }
}
