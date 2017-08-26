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
        private bool _showOnly = false;
        private Stopwatch _watch;

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
        }
        public IValidationResult Run()
        {
            var strategy = _app.Arguments.FirstOrDefault(x => x.Name == "sort");
            var sourceDirectory = _app.Arguments.FirstOrDefault(x => x.Name == "source");
            var rootDirectory = _app.Arguments.FirstOrDefault(x => x.Name == "destination");

            var validator = new SmoothValidator(strategy.Value, sourceDirectory.Value,rootDirectory.Value);
            var result = validator.Validate();
            //Validate sub ICommands if they ever get created
            if (!result.HasErrors)
            {
              
                var sorter = new Sorter(sourceDirectory.Value, rootDirectory.Value);
                sorter.RaiseFileSortEvent += HandleSortEvent;
               _watch.Start();
                sorter.Sort();
               _watch.Stop();
                Console.ForegroundColor =ConsoleColor.Yellow;
                Console.WriteLine(string.Format("Total Time For Sorting {0} seconds", _watch.Elapsed.Seconds));
            }

            return result;
        }

        
        private void HandleSortEvent(object sender, SorterFile e)
        {
            var result = e.Stage(new YearSortStrategy());
            Console.ForegroundColor =ConsoleColor.Yellow;
            Console.WriteLine(string.Format("Staged File: {0}  To Location {1}", e.FileToSort.FullName,  e.StagedFilePath));
         
            if (!_showOnly)
            {
                var newFile = result.Move();
                Console.WriteLine("New File Name:", newFile);
                if (e.Moved)
                {
                    Console.ForegroundColor =ConsoleColor.Red;
                    Console.WriteLine(string.Format("Moved File: {0} To Location {1}", e.FileToSort.Name,  e.StagedFilePath));
                }
            }
           
        }

       
    }
}