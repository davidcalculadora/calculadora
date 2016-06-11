using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
namespace HospitalAutodialerService
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main(string[] args)
        {
            if
            (
              args != null &&
              args.Length == 1 &&
              args[0].Length > 1 &&
              (args[0][0] == '-' || args[0][0] == '/')
            )
            {
                switch (args[0].Substring(1).ToLower())
                {
                    case "install":
                    case "i":
                        SelfInstaller.InstallMe();
                        break;
                    case "uninstall":
                    case "u":
                        SelfInstaller.UninstallMe();
                        break;
                    case "start":
                    case "s":
                        SelfInstaller.StartMe("Service1");
                        break;
                    case "stop":
                    case "p":
                        SelfInstaller.StopMe("Service1");
                        break;
                    case "check":
                    case "c":
                        SelfInstaller.CheckMe("Service1");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // To run more than one service you have to add them here
                ServiceBase.Run(new ServiceBase[] { new HospitalAutodialerService() });
            }
        }
    }
}
