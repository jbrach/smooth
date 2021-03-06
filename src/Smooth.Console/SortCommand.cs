using System.Linq;
using Microsoft.Extensions.CommandLineUtils;
using Smooth.Library;
using System;
using System.Diagnostics;

namespace Smooth
{
    public class SortCommand : ICommand
    {
        private readonly CommandLineApplication _app;
        private readonly bool _showOnly;

        private readonly bool _deleteSourceFile;
        private readonly Stopwatch _watch;

        public SortCommand(CommandLineApplication commandLineApp)
        {

            _watch = new System.Diagnostics.Stopwatch();
             
            _app = commandLineApp;
            var showOption =  commandLineApp.Options.FirstOrDefault(x=>x.LongName=="show");
           
             if (showOption!=null)
            {
                var value = showOption.Values.FirstOrDefault();
                if (!string.IsNullOrEmpty(value))
                {
                    _showOnly = (value == "on");
                }
            } 

              var deleteSourceOption =  commandLineApp.Options.FirstOrDefault(x=>x.LongName=="move");

            if (deleteSourceOption!=null)
            {
                var value = deleteSourceOption.Values.FirstOrDefault();
                if (!string.IsNullOrEmpty(value))
                {
                    _deleteSourceFile = (value == "on");
                }
            } 
          
        }
        public IValidationResult Run()
        {
            var strategy = _app.Arguments.FirstOrDefault(x => x.Name == "sort");
            var sourceDirectory = _app.Arguments.FirstOrDefault(x => x.Name == "source");
            var rootDirectory = _app.Arguments.FirstOrDefault(x => x.Name == "destination");

            var validator = new SmoothValidator(strategy.Value, sourceDirectory.Value,rootDirectory.Value);
            var result = validator.Validate();
        
           if (!result.HasErrors)
            {
              
                var sorter = new Sorter(sourceDirectory.Value, rootDirectory.Value);
                sorter.RaiseFileSortEvent += HandleSortEvent;
               _watch.Start();
                sorter.Sort();
               _watch.Stop();
                string.Format("Total Time For Sorting {0} seconds", _watch.Elapsed.Seconds).Log(ConsoleColor.Yellow);
            }

            return result;
        }

        
        private void HandleSortEvent(object sender, StageFileCommand e)
        {
            var stagingResults = e.Stage(new YearSortStrategy());
            string.Format("Staged File: {0}  To Location {1}", e.FileToSort.FullName,  e.StagedFilePath).Log(ConsoleColor.Yellow);
         
            if (!_showOnly)
            {
                var newFile = stagingResults.Move(_deleteSourceFile);
                String.Concat("New File Name:", newFile).Log();
                if (stagingResults.Moved)
                {
                     string.Format("Moved File: {0} To Location {1}", e.FileToSort.Name,  e.StagedFilePath).Log();
                }
            }
           
        }

       
    }
}