using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

namespace DemoWindowsService
{
    static class Program
    {
    static void Main(string [] args)
    {
        /* If you'd like specific behavior based on whether the user launches the executable you can use
        if(Environment.UserInteractive) { 
         
        }
        */
        /* If you'd like specific behavior based on whether the IDE Debugger launches or attached to the executable you can use
        if(Debugger.IsAttached) { 
         
        }
        */
        var service = new DemoWindowsService();
        var arguments = string.Concat(args);
        switch(arguments)
        {
            case "--console":
                RunInteractive(service,args);
                break;
            case "--install":
                ManagedInstallerClass.InstallHelper(new [] { Assembly.GetExecutingAssembly().Location });
                break;
            case "--uninstall":
                ManagedInstallerClass.InstallHelper(new [] { "/u", Assembly.GetExecutingAssembly().Location });
                break;
            default:
                ServiceBase.Run(service);
                break;
        }
    }

        private static void RunInteractive(DemoWindowsService service, string [] args)
        {
            service.InteractiveStart(args);
            Console.WriteLine("Press any key to stop!");
            Console.Read();
            service.InteractiveStop();
        }
    }
}
