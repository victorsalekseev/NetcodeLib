using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Collections;
using System.ServiceProcess;
using System.IO;
using System.Reflection;

namespace WindowsService1
{
    [RunInstallerAttribute(true)]
    public class MyProjectInstaller : Installer
    {
        private ServiceInstaller serviceInstaller1;
        ////private ServiceInstaller serviceInstaller2;
        private ServiceProcessInstaller processInstaller;

        System.Configuration.Install.InstallContext ic = new InstallContext("install.log", null);
        
        public MyProjectInstaller(string ServiceName, string ServicePath)
        {
            ic.Parameters.Add("assemblyPath", ServicePath);
            // Instantiate installers for process and services.
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller1 = new ServiceInstaller();
            ////serviceInstaller2 = new ServiceInstaller();

            // The services run under the system account.
            processInstaller.Account = ServiceAccount.LocalSystem;
            //processInstaller.Account = System.ServiceProcess.ServiceAccount.User;
            //processInstaller.Username = "123";
            //processInstaller.Password = "123";

            // The services are started manually.
            serviceInstaller1.StartType = ServiceStartMode.Automatic;
            serviceInstaller1.Context = ic;
            ////serviceInstaller2.StartType = ServiceStartMode.Manual;

            // ServiceName must equal those on ServiceBase derived classes.            
            serviceInstaller1.ServiceName = ServiceName;
            ////serviceInstaller2.ServiceName = "Hello-World Service 2";

            // Add installers to collection. Order is not important.
            Installers.Add(serviceInstaller1);
            ////Installers.Add(serviceInstaller2);
            Installers.Add(processInstaller);
            IDictionary mySavedState = new Hashtable();

            serviceInstaller1.Install(mySavedState);
        }
    }

}