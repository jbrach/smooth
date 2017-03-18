using System;
using System.Linq;
using Microsoft.Extensions.CommandLineUtils;

namespace Smooth
{
    public class Program
    {
        public static int Main(string[] args)
        {

            // Program.exe <-g|--greeting|-$ <greeting>> [name <fullname>]
            // [-?|-h|--help] [-u|--uppercase]
            CommandLineApplication app =
              new CommandLineApplication(throwOnUnexpectedArg: false)
              {
                  Name = "Smooth",
                  Description = "Sorts files with different strategies",
                  FullName = "Smooth File Sorter",
                  ShowInHelpText = true,
                  ExtendedHelpText = "Smooth"

              };

         

        
            app.Command("year", c =>
            {
                c.Description = "Command that Sorts files using the year strateg:  Files moved to [destination]/CreateYear/";
                c.FullName = "";
                c.ExtendedHelpText = "";



                c.OnExecute(() =>
                {
                    c.ShowHelp();
                    return 0;

                });
            });
         
       
          var showOnlyOption = app.Option(
                "-$|-s |--show <show>",
                "Option will only show the results to the screen using the strategy selected ",
                CommandOptionType.SingleValue);
           app.HelpOption("-? | -h | --help");

            app.Argument("source", "Source directory: location of  files that will be sorted", false);
            app.Argument("destination", "Root destination directory:  Root location where the files will be sorted.  The files will end up in different sub-directory based on the strategy used", false);


            /*  CommandArgument names = null;
              app.Command("name",
                (target) =>
                  names = target.Argument(
                    "fullname",
                    "Enter the full name of the person to be greeted.",
                    multipleValues: true));
              var greeting = app.Option(
                "-$|-g |--greeting <greeting>",
                "The greeting to display. The greeting supports"
                + " a format string where {fullname} will be "
                + "substituted with the full name.",
                CommandOptionType.SingleValue);
              var uppercase = app.Option(
                "-u | --uppercase", "Display the greeting in uppercase.",
                CommandOptionType.NoValue);
              app.HelpOption("-? | -h | --help");
              */
            app.OnExecute(() =>
            {

                
                app.ShowHelp();
                //app.Commands.ToList().ForEach(x => x.ShowHelp());
                return 0;
            });
            return app.Execute(args);
        }



    }

}
