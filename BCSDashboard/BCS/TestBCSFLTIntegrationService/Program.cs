using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using BCS.ObjectModel;
using BCS.ObjectModel.Factories;
using System.Timers;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using BCSFLTIntegrationService;



namespace TestBCSFLTIntegrationService
{
    class Program
    {
        public static void Main(string[] args)
        {
            BCSFLTIntegrationService.Service1 s1 = new Service1();
            s1.PickUpAndProcessFLTFileForToday();

        }
    }
}
