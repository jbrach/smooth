using System;
using System.IO;
namespace Smooth.Library.Extensions
{
public static class FileInfoExtensions
{

    public static System.DateTime GetCreateDate(this System.IO.FileInfo file)
    {
        var createDate = File.GetCreationTime(file.FullName);
        var modifiedDate = File.GetLastWriteTime(file.FullName);
        return (createDate<=modifiedDate)? createDate: modifiedDate;

    }    
}
}