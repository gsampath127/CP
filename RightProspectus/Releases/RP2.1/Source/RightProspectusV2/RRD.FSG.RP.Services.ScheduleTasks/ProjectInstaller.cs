using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Services.ScheduleTasks
{
    /// <summary>
    /// Partial class setup that runs the service to schedule the tasks
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            this.ScheduledTasksServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.User;
            this.ScheduledTasksServiceProcessInstaller.Username = "test-svc-pcentral@ecomad.int";
            this.ScheduledTasksServiceProcessInstaller.Password = "$0cNL0wSryLm";

            this.ScheduledTasksServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Manual;
            this.ScheduledTasksServiceInstaller.ServiceName = "RRD.FSG.RP.Services.ScheduleTasks";
        }
    }
}
