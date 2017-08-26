using System;
using System.IO;

namespace Smooth.Library.FileNaming
{
    public class CreateDateAndUploadDateStrategy : IFileNameStrategy
    {
            
        private readonly FileInfo _fileInfo;

        public CreateDateAndUploadDateStrategy( FileInfo fileInfo)
        {
             _fileInfo = fileInfo;
        }

        public string GenerateName()
        {
            var uploadText = string.Concat("Uploaded", DateTime.Now.ToString("MMyyyy"));
            var baseGenerator = new CreateDateAndCustomTextStrategy(uploadText, _fileInfo);
            return baseGenerator.GenerateName();
        }
    }
}