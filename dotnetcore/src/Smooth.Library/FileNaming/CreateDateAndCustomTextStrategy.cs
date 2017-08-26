
using System;
using System.IO;
using Smooth.Library.Extensions;

namespace Smooth.Library.FileNaming
{

public class CreateDateAndCustomTextStrategy : IFileNameStrategy
{
    private readonly string _customText;
    private readonly FileInfo _fileInfo;
    public CreateDateAndCustomTextStrategy(string customText,FileInfo fileInfo ) 
    {
        _customText = customText;
        _fileInfo = fileInfo;
    }


        public string GenerateName()
    {
       return string.Format("{0}_{1}_{2}",
            _fileInfo.GetCreateDate().ToString("MMddyyyy"),
            _customText, 
            _fileInfo.Name);
    }
}
}