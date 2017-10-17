namespace Smooth
{
    public static class Program
    {
        public static void Main(string[] args)
        {
           var ccommandLine = new SmoothCommandLineConfigurer();
           ccommandLine.Run(args);
         
        }
    }
}
