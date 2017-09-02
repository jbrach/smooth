using Xunit;
using Smooth.Library;
using Smooth.Library.Extensions;
using Smooth.Library.FileNaming;
using System.IO;
using System.Linq;

namespace Test.Extensions
{
    public class SmoothValidatorTests
    {
        [Fact]
        public void Should_AddError_When_DestinationDirectoryDoesNotExist()
        {
            var path = Path.GetTempPath();
            var doesNotExistPath = Path.Combine(path, System.Guid.NewGuid().ToString());

            var validator = new SmoothValidator("year", doesNotExistPath, doesNotExistPath);
            var result = validator.Validate();
            Assert.True(result.HasErrors);
            Assert.NotNull(result.Errors.FirstOrDefault(x => x.Contains("Destination Directory")));
        }

        public void Should_NotError_When_SourceAndDestinationDirectoryExist()
        {
            var destinationDirectory = Path.GetTempPath();
            var sourceDirectory = Path.Combine(Path.GetTempPath(), System.Guid.NewGuid().ToString());
            Directory.CreateDirectory(sourceDirectory);
            var validator = new SmoothValidator("year", sourceDirectory, destinationDirectory);
            var result = validator.Validate();
            Assert.Equal(false, result.HasErrors);

        }

        public void Should_Error_When_DesitinationDirectoryUnderSource()
        {
            var sourceDirectory = Path.GetTempPath();
            var destinationDirectory = Path.Combine(Path.GetTempPath(), System.Guid.NewGuid().ToString());
            Directory.CreateDirectory(destinationDirectory);
            var validator = new SmoothValidator("year", sourceDirectory, destinationDirectory);
            var result = validator.Validate();
            Assert.Equal(true, result.HasErrors);
            Assert.NotNull(result.Errors.FirstOrDefault(x => x.Contains("Destination Directory is not allowed to be a subdirectory")));
            Directory.Delete(destinationDirectory);
        }

        [Fact]
        public void Should_AddError_When_SourceDirectoryDoesNotExist()
        {
            var path = Path.GetTempPath();
            var doesNotExistPath = Path.Combine(path, System.Guid.NewGuid().ToString());

            var validator = new SmoothValidator("year", doesNotExistPath, doesNotExistPath);
            var result = validator.Validate();
            Assert.True(result.HasErrors);
            Assert.NotNull(result.Errors.FirstOrDefault(x => x.Contains("Source Directory")));
        }
    }
}