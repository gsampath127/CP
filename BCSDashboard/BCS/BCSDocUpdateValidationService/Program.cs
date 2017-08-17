using BCS.ObjectModel.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace BCSDocUpdateValidationService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service1() 
            };
            ServiceBase.Run(ServicesToRun);

            //ServiceFactory serviceFactory = new ServiceFactory();
            //serviceFactory.CheckBCSDocUpdateValidations();
            //serviceFactory.AddNewCUSIPsForExistingFunds();
            //serviceFactory.SendBCSDocUpdateValidationReport();
        }
    }
}
