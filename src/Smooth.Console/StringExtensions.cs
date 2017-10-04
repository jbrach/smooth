using System;

namespace Smooth
{
    public static class StringExtensions {

            public static void Log(this string message, ConsoleColor color= ConsoleColor.DarkGreen)
            {
                  Console.WriteLine(message, color);
            }

        }
}