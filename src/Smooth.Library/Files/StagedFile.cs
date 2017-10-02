using System.IO;
using Smooth.Library.FileNaming;

namespace Smooth.Library
{
    public class StagedFile
    {

        public StagedFile(FileInfo fileToSort, string destinationPath)
        {
            FileToSort = fileToSort;
            DestinationPath = destinationPath;
            Moved = false;
        }

        public FileInfo FileToSort { get; }
        public string DestinationPath { get; }
        public bool Moved { get; private set; }
        public string Move(bool deleteSourceFile )
        {
            var strategy = new CreateDateAndUploadDateStrategy(FileToSort);
            var newFile = Path.Combine(DestinationPath,strategy.GenerateName());
            if (!File.Exists(newFile))
            {
                 if (deleteSourceFile)  {
                      File.Move(FileToSort.FullName, newFile); 
                 }  
                 else {
                     File.Copy(FileToSort.FullName, newFile);
                }
                Moved = true;
            }
            return newFile;

        }
    }
}