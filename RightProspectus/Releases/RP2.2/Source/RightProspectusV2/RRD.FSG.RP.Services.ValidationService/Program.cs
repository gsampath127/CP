using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Services.ValidationService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //new ValidationService().CheckCUSIPMergerLiqudation();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ValidationService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
