using System;
using Microsoft.Extensions.CommandLineUtils;

namespace Smooth
{
    public class SmoothCommandLineConfigurer
    {
       
        public CommandOption ShowOnlyOption { get; private set; }
        public CommandOption DeleteSourceOption { get; private set; }

        public SmoothCommandLineConfigurer()
        {}
        public CommandLineApplication CreateCommandLineApplication()
        {
            CommandLineApplication   _app = new CommandLineApplication(throwOnUnexpectedArg: false)
            {
                Name = "Smooth",
                Description = "Sorts files with different strategies",
                FullName = "Smooth File Sorter",
                ShowInHelpText = true,
                ExtendedHelpText = "Smooth"

            };
            _app.HelpOption("-? | -h | --help");
            _app.Argument("source", "Root directory of files that will be sorted.", false);
            _app.Argument("destination", "Root directory where the files will be sorted into."  +  
                           @"  Note the files will end up in different sub-directory based on the strategy used" + 
                           @"  The destination directory can not be a subdirectory of the source", false);
             _app.Argument("sort", "Strategy to be used for sorting {year}", false);
             DeleteSourceOption = _app.Option(
            "-d|--delete <delete>",
            "Option will delete the files from the source location.  By default smooth will copy the files and the source location will be kept." +   
            "  Use this option when you want the source location to be cleaned.",
            CommandOptionType.NoValue);
          
            ShowOnlyOption = _app.Option(
            "-$|-s|--show <show>",
            "Option will only show the results to the screen using the strategy selected ",
            CommandOptionType.NoValue);
          
            return _app;

        }

        public void Run(string[] args)
        {
            var app  = CreateCommandLineApplication();
            app.OnExecute(() =>
            {
                return RunCommand(app);
            });
             app.Execute(args);
        }

        private int RunCommand(CommandLineApplication app)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("___________________________________________");
            Console.WriteLine("Running smooth with the following Arguments");
            Console.WriteLine("___________________________________________");
            Console.WriteLine("");
          
            app.Arguments.ForEach(x => System.Console.WriteLine(string.Concat(x.Name, " \t", x.Value)));
            Console.WriteLine("");
            Console.ResetColor();
            ICommand command = new SortCommand(app);

            var commandResult = command.Run();

            if (commandResult.HasErrors)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Smooth detected error:  Smooth -h for help on Smooth ");
                commandResult.Errors.ForEach(x => System.Console.WriteLine(string.Concat("Error: ", x)));
            }
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\nBye Bye -- Pew Pew Pew\n");
            Console.ResetColor();

            return 0;
        }

    }
}