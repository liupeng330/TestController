using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTechnology.TestClient.Utilities;

namespace TestTechnology.TestClient.Plugin
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Environment.Exit(0);
            }
            Console.WriteLine("Starting to delete {0}", args[0]);
            try
            {
                File.Delete(args[0]);
                CommandLineHelper.Pass(string.Format("Finish to delete {0}", args[0]));
            }
            catch (Exception ex)
            {
                CommandLineHelper.Fail(string.Format("Fail to delete {0}", args[0]));
                CommandLineHelper.Fail(ex.ToString());
                Environment.Exit(0);
            }

            Environment.Exit(1);
        }
    }
}
