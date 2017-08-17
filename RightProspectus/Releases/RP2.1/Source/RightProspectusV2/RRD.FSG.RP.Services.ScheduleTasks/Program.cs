using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using RRD.FSG.RP.Scheduler.Interfaces;
using RRD.FSG.RP.Scheduler;
using RRD.FSG.RP.Utilities;

namespace RRD.FSG.RP.Services.ScheduleTasks
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
                //#if(!DEBUG)
             ServiceBase[] ServicesToRun;
               ServicesToRun = new ServiceBase[] 
               { 
                   new ScheduleTasksService() 
               };
               ServiceBase.Run(ServicesToRun);
            //#else
            /*    RPV2Resolver.LoadConfiguration();
                IReportScheduleEntry entry=null;
                TaskEngine t = new TaskEngine();
                t.Process(entry);

            
                ScheduleTasksService myServ = new ScheduleTasksService();
                myServ.Process();
                //#endif*/
        }
    }
}
