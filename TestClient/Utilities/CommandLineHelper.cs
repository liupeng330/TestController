using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTechnology.TestClient.Utilities
{
    public static class CommandLineHelper
    {
        public static void Pass(string message)
        {
            Output(message, ConsoleColor.Green);
        }

        public static void Fail(string message)
        {
            Output(message, ConsoleColor.Red);
        }

        private static void Output(string message, ConsoleColor color)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
        }
    }
}
