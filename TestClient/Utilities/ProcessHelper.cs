using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTechnology.TestClient.Utilities
{
    public static class ProcessHelper
    {
        public static int StartProcess(string appFullPath, string args)
        {
            Process process = new Process();
            process.StartInfo.FileName = appFullPath;
            process.StartInfo.Arguments = args;

            try
            {
                process.Start();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                //TODO: Log exception here
                return -1;
            }
            return process.ExitCode;
        }
    }
}
