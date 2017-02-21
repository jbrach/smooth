using System.IO;


namespace SimpleFileSorter.Library
{

/// <summary>
///Object to manage validating and ensuring the directory is valid
/// </summary>
    public class DirectoryHelper
    {
        private string _directory;

        public DirectoryHelper(string directory)
        {
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException(directory);
            }
            _directory = Path.GetFullPath(directory);
        }
        
        public string GetCleanDirectory()
        {
            return _directory;
        }
    }

}
