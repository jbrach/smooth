using System;
using System.Linq;

namespace Smooth
{
    public class Program
    {
        public static void Main(string[] args)
        {

      
           var creator = new SmoothCommandLineConfigurer();
           var app  = creator.Create();

       
            app.OnExecute(() =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                  
                Console.WriteLine("**************************************************************");
                Console.WriteLine("Running smooth with the following Arguments");
                Console.WriteLine("___________________________________________");
               
                app.Arguments.ForEach(x=>System.Console.WriteLine(string.Concat( x.Name, " \t", x.Value)));
                Console.WriteLine("");
                Console.ResetColor();
                ICommand command = new SortCommand(app);
                
                var commandResult = command.Run();

                if (commandResult.HasErrors)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Smooth detected error:  Smooth -h for help on Smooth ");
                    commandResult.Errors.ForEach(x=> System.Console.WriteLine(string.Concat("Error: ", x)));
                }
                 Console.ForegroundColor = ConsoleColor.DarkBlue;
                 
                 Console.WriteLine("\nBye Bye -- Pew Pew Pew\n");
                 Console.ResetColor();
              
                return 0;
               
            });
             app.Execute(args);
        }


    }

   

}
