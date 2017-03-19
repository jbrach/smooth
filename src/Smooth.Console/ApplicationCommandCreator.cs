using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Smooth
{
    public class SmoothCommandLineConfigurer
    {
        private readonly CommandLineApplication _app;

        public SmoothCommandLineConfigurer()
        {
            _app = new CommandLineApplication(throwOnUnexpectedArg: false)
            {
                Name = "Smooth",
                Description = "Sorts files with different strategies",
                FullName = "Smooth File Sorter",
                ShowInHelpText = true,
                ExtendedHelpText = "Smooth"

            };
        }

        public CommandLineApplication Create()
        {

            _app.HelpOption("-? | -h | --help");
            _app.Argument("source", "Source directory: location of  files that will be sorted", false);
            _app.Argument("destination", "Root destination directory:  Root location where the files will be sorted.  The files will end up in different sub-directory based on the strategy used", false);
             _app.Argument("sort", "Strategy to be used for sorting {year}", false);
            var showOnlyOption = _app.Option(
            "-$|-s |--show <show>",
            "Option will only show the results to the screen using the strategy selected ",
            CommandOptionType.SingleValue);
          
            return _app;

        }

      
    }
}