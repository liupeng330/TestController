using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using TestTechnology.Controller.DTO;

namespace TestTechnology.TestClient.Utilities
{
    public static class SystemInfoHelper
    {
        public static ClientMachineInfo GetOperatingSystemInfo()
        {
            ClientMachineInfo machineInfo = new ClientMachineInfo();
            machineInfo.MachineName = System.Environment.MachineName;

            //Create an object of ManagementObjectSearcher class and pass query as parameter.
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    machineInfo.OS = managementObject["Caption"].ToString();
                }
                if (managementObject["OSArchitecture"] != null)
                {
                    machineInfo.SystemType = managementObject["OSArchitecture"].ToString();
                }
                if (managementObject["CSDVersion"] != null)
                {
                    machineInfo.OS += " " + managementObject["CSDVersion"].ToString();
                }
            }

            return machineInfo;
        }

        public static string GetProcessorInfo()
        {
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);   //This registry entry contains entry for processor info.

            if (processor_name != null)
            {
                if (processor_name.GetValue("ProcessorNameString") != null)
                {
                    return processor_name.GetValue("ProcessorNameString").ToString();   //Display processor ingo.
                }
            }

            return string.Empty;
        }
    }
}
