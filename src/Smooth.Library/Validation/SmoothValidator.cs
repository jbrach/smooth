using System;
using System.Collections.Generic;
using System.IO;

namespace Smooth.Library
{
    public class SmoothValidator
    {
        private readonly string _strategy;
        private readonly string _sourceDirectory;
        private readonly string _rootDirectory;

        public SmoothValidator(string strategy, string sourceDirectory, string destinationDirectory)
        {
                _strategy = strategy;
                _sourceDirectory = sourceDirectory;
                _rootDirectory = destinationDirectory;
        }

         public IValidationResult Validate()
        {

            var errors = new List<string>();
            var dirException = false;
            DirectoryHelper root = null;
            DirectoryHelper source = null;

            if (!string.IsNullOrEmpty(_strategy) && _strategy != "year")
            {
                errors.Add(string.Format("Strategy {0} is not implemented.", _strategy));
            }

            try
            {
                 root = new DirectoryHelper(_rootDirectory);
            }
            catch (DirectoryNotFoundException)
            {
                dirException = true;
                errors.Add(string.Format("Destination Directory {0} does not exist.", _rootDirectory));
            }

            try
            {
                 source = new DirectoryHelper(_sourceDirectory);
            }
            catch (DirectoryNotFoundException)
            {
                dirException = true;
                errors.Add(string.Format("Source Directory {0} does not exist.", _sourceDirectory));
            }

            if (!dirException)
            {
                if (source.IsSubDirectory(root.GetFullPath()))
                {
                    errors.Add(
                    "Destination Directory is not allowed to be a subdirectory of the directory being sorted." +  
                    @"\nPlease modify the destination directory to another location outside of the path directory being sorted." +
                    @"");
                }
            }
            return new SmoothValidationResult(errors);
        }
    }

}
