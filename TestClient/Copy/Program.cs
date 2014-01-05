using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using TestTechnology.TestClient.Utilities;

namespace TestTechnology.TestClient.Plugin
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Environment.Exit(0);
            }
            Console.WriteLine("Starting to copy from {0} to {1}", args[0], args[1]);
            try
            {
                File.Copy(args[0], args[1]);
                CommandLineHelper.Pass(string.Format("Finish to copy from {0} to {1}", args[0], args[1]));
            }
            catch (Exception ex)
            {
                CommandLineHelper.Fail(string.Format("Fail to copy from {0} to {1}", args[0], args[1]));
                CommandLineHelper.Fail(ex.ToString());
                Environment.Exit(0);
            }

            Environment.Exit(1);
        }
    }
}
