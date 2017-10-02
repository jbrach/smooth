     
     
 
Project setup --> I started by figuring out how to reference projects.
Need to go deeper on how "dotnet build" command uses the Project.json and global.json for references.
The Visual Code Documentation is not great but found a simple example <http://www.natemcmaster.com/blog/2016/03/29/project-json/> to get me started

Loving Visual Studio Code editor.   Feels similar to sublime.   Not as intrusive as Visual Studio for .NET. 

Bug or issue.  FileInfo.CreationTime continues to return 01 as the creationyear of the files.
Next need to simplify the codebase if feels to complicated for such a small project and I want to use .NET base as a guide
Making event driven to remove the ugly loops and allow for extensibility or differnt types of grouping.

Modified code to use observer pattern using .NET EventHandler.  

Next steps is to build console client how well we can get it to perform in .NET using parallels and async.  
Finally work on packaging including laying out what my CI/CD solution before moving onto next language -- Node
.NET core Packaging research.  <https://www.hanselman.com/blog/SelfcontainedNETCoreApplications.aspx>

Parsing Arguments .NET Core:  
<https://msdn.microsoft.com/en-us/magazine/mt763239.aspx>
<https://github.com/iamarcel/dotnet-core-neat-console-starter>

Finished adding expected Console behavior in C#
![alt tag](https://github.com/jbrach/smooth/blob/master/stuff/smoothconsole_core.png?raw=true) 
