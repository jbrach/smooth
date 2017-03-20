using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.CommandLineUtils;
using Smooth.Library;

namespace Smooth
{
    public class SortCommand : ICommand
    {
        private readonly CommandLineApplication _app;
        private bool _showOnly = false;

        public object Console { get; private set; }

        public SortCommand(CommandLineApplication commandLineApp)
        {
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
        public IResult Run()
        {
            var result = new SmoothResult(Validate());
            //Validate sub ICommands if they ever get created
            if (!result.HasErrors)
            {
                var strategy = _app.Arguments.FirstOrDefault(x => x.Name == "sort");
                var sourceDirectory = _app.Arguments.FirstOrDefault(x => x.Name == "source");
                var rootDirectory = _app.Arguments.FirstOrDefault(x => x.Name == "destination");
                var sorter = new Sorter(sourceDirectory.Value, rootDirectory.Value);
                sorter.RaiseFileSortEvent += HandleSortEvent;
                sorter.Sort();
            }

            return result;
        }

        
        private void HandleSortEvent(object sender, SorterFile e)
        {
            var result = e.Stage(new YearSortStrategy());
            System.Console.WriteLine(string.Format("Staged File: {0}  To Location {1}", e.FileToSort.FullName,  e.StagedFilePath));
         
            if (!_showOnly)
            {
                result.Move();
                if (e.Moved)
                {
                    System.Console.WriteLine(string.Format("Moved File: {0} To Location {1}", e.FileToSort.Name,  e.StagedFilePath));
                }
            }
           
        }

        private List<string> Validate()
        {

            var errors = new List<string>();
            var strategy = _app.Arguments.FirstOrDefault(x => x.Name == "sort");
            var sourceDirectory = _app.Arguments.FirstOrDefault(x => x.Name == "source");
            var rootDirectory = _app.Arguments.FirstOrDefault(x => x.Name == "destination");
            DirectoryHelper root = null;
            DirectoryHelper source = null;

            if (!string.IsNullOrEmpty(strategy.Value) && strategy.Value != "year")
            {
                errors.Add(string.Format("Strategy {0} is not implemented.", strategy.Value));
            }

            try
            {
                 root = new DirectoryHelper(rootDirectory.Value);
            }
            catch (DirectoryNotFoundException)
            {
                errors.Add(string.Format("Root Directory {0} does not exist.", rootDirectory.Value));
            }

            try
            {
                 source = new DirectoryHelper(sourceDirectory.Value);
            }
            catch (DirectoryNotFoundException)
            {
                errors.Add(string.Format("Source Directory {0} does not exist.", sourceDirectory.Value));
            }

            var parentUri =  new Uri(source.GetFullPath());
            var childUri = new Uri(root.GetFullPath());
            if (parentUri == childUri || parentUri.IsBaseOf(childUri))
            {
                 errors.Add("Destination Directory should not be a child of directory being sorted. \nPlease modify the destination directory to another location.");
            
            }
            return errors;
        }
    }
}