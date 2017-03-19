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
                System.Console.WriteLine("**************EXECUTING SMOOTH********************");
                
                app.Arguments.ForEach(x=>System.Console.WriteLine(string.Concat("Name:", x.Name, " Value:", x.Value)));
                ICommand command = new SortCommand(app);
                var errors = command.Validate();

                if (errors.Count==0)
                {
                    command.Run();
                }
                else{
                    errors.ForEach(x=> System.Console.WriteLine(string.Concat("Error running command:", x)));
                    app.ShowHelp();
                    
                }
                System.Console.WriteLine("**************END EXECUTING SMOOTH********************");
           
                return 0;
            });
              
              System.Console.WriteLine("**************ARGUMENTS********************");
              args.ToList().ForEach(x=>System.Console.WriteLine(x));
              System.Console.WriteLine("**************END ARGUMENTS********************");
              
             app.Execute(args);
        }


    }

   

}
