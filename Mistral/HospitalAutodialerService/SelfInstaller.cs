using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Configuration.Install;
using System.ServiceProcess;
namespace HospitalAutodialerService
{
    // Esta clase implementa dos utilidades para instalar y desinstalar un servicio.
    // Referencia: http://www.codeproject.com/KB/dotnet/WinSvcSelfInstaller.aspx?msg=2335929

    public static class SelfInstaller
    {
        private static readonly string _exePath = Assembly.GetExecutingAssembly().Location;
        
        public static bool InstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { _exePath });
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool UninstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { "/u", _exePath });
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool StartMe(string sServiceName)
        {
            // Esta parte de código no es de la web referida

            bool rc = false;
            ServiceController Service = new ServiceController(sServiceName);
            try
            {
                Service.Refresh();
                if (Service.Status == ServiceControllerStatus.Stopped)
                {
                    Service.Start();
                    rc = true;
                }
            }
            catch
            {
                rc = false;
            }
            return rc;
        }

        public static bool StopMe(string sServiceName)
        {
            // Esta parte de código no es de la web referida

            bool rc = false;
            ServiceController Service = new ServiceController(sServiceName);
            try
            {
                Service.Refresh();
                if (Service.Status == ServiceControllerStatus.Running)
                {
                    Service.Stop();
                    rc = true;
                }
            }
            catch
            {
                rc = false;
            }
            return rc;
        }

        public static void CheckMe(string sServiceName)
        {
            // Esta parte de código no es de la web referida

            ServiceController Service = new ServiceController(sServiceName);
            try
            {
                Service.Refresh();
                System.Windows.Forms.MessageBox.Show(Service.Status.ToString());
            }
            catch (Exception E)
            {
                System.Windows.Forms.MessageBox.Show(E.Message);
            }
        }

    }
}