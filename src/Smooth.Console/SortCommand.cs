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

        public SortCommand(CommandLineApplication commandLineApp)
        {
            _app = commandLineApp;
        }
        public bool Run()
        {
            var errors = Validate();
            //Validate sub ICommands if they ever get created
            if (errors.Count == 0)
            {

                var strategy = _app.Arguments.FirstOrDefault(x => x.Name == "sort");
                var sourceDirectory = _app.Arguments.FirstOrDefault(x => x.Name == "source");
                var rootDirectory = _app.Arguments.FirstOrDefault(x => x.Name == "destination");
                var sorter = new Sorter(sourceDirectory.Value, rootDirectory.Value);
                sorter.RaiseFileSortEvent += HandleSortEvent;
                sorter.Sort();
            }

            return true;
        }

        
        private void HandleSortEvent(object sender, SorterFile e)
        {
            System.Console.WriteLine(string.Concat("Staging File: ", e.StagedFilePath));
            var result = e.Stage(new YearSortStrategy());
            System.Console.WriteLine(string.Concat("Done Staging File: ", e.StagedFilePath));
            result.Move();
            
           
        }

        public List<string> Validate()
        {

            var errors = new List<string>();
            var strategy = _app.Arguments.FirstOrDefault(x => x.Name == "sort");
            var sourceDirectory = _app.Arguments.FirstOrDefault(x => x.Name == "source");
            var rootDirectory = _app.Arguments.FirstOrDefault(x => x.Name == "destination");

            if (!string.IsNullOrEmpty(strategy.Value) && strategy.Value != "year")
            {
                errors.Add(string.Format("Strategy {0} is not implemented.", strategy.Value));
            }

            try
            {
                var root = new DirectoryHelper(rootDirectory.Value);
            }
            catch (DirectoryNotFoundException ex)
            {
                errors.Add(string.Format("Root Directory {0} does not exist. Message: {1}", rootDirectory.Value, ex.Message));

            }

            try
            {
                var root = new DirectoryHelper(sourceDirectory.Value);
            }
            catch (DirectoryNotFoundException ex)
            {
                errors.Add(string.Format("Source Directory {0} does not exist. Message: {1}", sourceDirectory.Value, ex.Message));

            }




            return errors;
        }
    }
}