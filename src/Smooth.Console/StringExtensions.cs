using System;

namespace Smooth
{
    public static class StringExtensions {

            public static void Log(this string message)
            {
                 Log(message, ConsoleColor.DarkGreen);
            }

            public static void Log(this string message, ConsoleColor color)
            {
                  Console.WriteLine(message, color);
            }


        }
}