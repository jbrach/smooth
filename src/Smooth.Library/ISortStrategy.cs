using System.IO;

namespace Smooth.Library
{
    public interface ISortStrategy
    {
        string BuildNewFilePath(FileInfo originalFile, string destinationRootDirectory);
    }


}