using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Scheduler;
using RRD.FSG.RP.Scheduler.Task;
using RRD.FSG.RP.Model.Interfaces.HostedPages;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Scheduler.Task.PrintRequest;

namespace RRD.FSG.RP.Scheduler.Test.Task.PrintRequest
{
    [TestClass]
    public class PrintRequestsTaskTest
    {
        //Mock<IHostedClientRequestMaterialFactory> mockhostedClientRequestMaterialFactory;
        ///// <summary>
        ///// TestInitialze
        ///// </summary>
        //[TestInitialize]
        //public void TestInitialze()
        //{
        //    mockhostedClientRequestMaterialFactory = new Mock<IHostedClientRequestMaterialFactory>();
        //}
        #region Process_With_FrequencyType_0
        /// <summary>
        /// Process_With_FrequencyType_0
        /// </summary>
        [TestMethod]
        public void Process_With_FrequencyType_0()
        {
            //Act
            PrintRequestsTask objPrintRequestsTask = new PrintRequestsTask();
            ReportScheduleEntry ie = new ReportScheduleEntry(1, "defaultmonthlyerrorreport", 1, "forethought", 1, 0, 1, DateTime.Now.AddDays(-200), "test", 0, "pathanmuzafar.alikhan@rrd.com", "test1", "test2", "test3", "test4");
            objPrintRequestsTask.Process(ie);
        }
        #endregion
        #region Process_With_FrequencyType_1
        /// <summary>
        /// Process_With_FrequencyType_1
        /// </summary>
        [TestMethod]
        public void Process_With_FrequencyType_1()
        {
            //Act
            PrintRequestsTask objPrintRequestsTask = new PrintRequestsTask();
            ReportScheduleEntry ie = new ReportScheduleEntry(1, "defaultmonthlyerrorreport", 1, "forethought", 1, 1, 1, DateTime.Now.AddDays(-200), "test", 0, "pathanmuzafar.alikhan@rrd.com", "test1", "test2", "test3", "test4");
            objPrintRequestsTask.Process(ie);

        }
        #endregion
        #region Process_With_FrequencyType_2
        /// <summary>
        /// Process_With_FrequencyType_2
        /// </summary>
        [TestMethod]
        public void Process_With_FrequencyType_2()
        {
            //Act
            PrintRequestsTask objPrintRequestsTask = new PrintRequestsTask();
            ReportScheduleEntry ie = new ReportScheduleEntry(1, "defaultmonthlyerrorreport", 1, "forethought", 1, 2, 1, DateTime.Now.AddDays(-200), "test", 0, "pathanmuzafar.alikhan@rrd.com", "test1", "test2", "test3", "test4");
            objPrintRequestsTask.Process(ie);
        }
        #endregion
        #region Process_With_FrequencyType_3
        /// <summary>
        /// Process_With_FrequencyType_3
        /// </summary>
        [TestMethod]
        public void Process_With_FrequencyType_()
        {
            //Act
            PrintRequestsTask objPrintRequestsTask = new PrintRequestsTask();
            ReportScheduleEntry ie = new ReportScheduleEntry(1, "defaultmonthlyerrorreport", 1, "forethought", 1, 3, 1, DateTime.Now.AddDays(-200), "test", 0, "pathanmuzafar.alikhan@rrd.com", "test1", "test2", "test3", "test4");
            objPrintRequestsTask.Process(ie);
        }
        #endregion
        #region Process_With_FrequencyType_4
        /// <summary>
        /// Process_With_FrequencyType_4
        /// </summary>
        [TestMethod]
        public void Process_With_FrequencyType_4()
        {
            //Act
            PrintRequestsTask objPrintRequestsTask = new PrintRequestsTask();
            ReportScheduleEntry ie = new ReportScheduleEntry(1, "defaultmonthlyerrorreport", 1, "forethought", 1, 4, 1, DateTime.Now.AddDays(-200), "test", 0, "pathanmuzafar.alikhan@rrd.com", "test1", "test2", "test3", "test4");
            objPrintRequestsTask.Process(ie);
        }
        #endregion
        #region Process_With_FrequencyType_5
        /// <summary>
        /// Process_With_FrequencyType_5
        /// </summary>
        [TestMethod]
        public void Process_With_FrequencyType_5()
        {
            PrintRequestsTask objPrintRequestsTask = new PrintRequestsTask();
            ReportScheduleEntry ie = new ReportScheduleEntry(1, "defaultmonthlyerrorreport", 1, "forethought", 1, 5, 1, DateTime.Now.AddDays(-200), "test", 0, "pathanmuzafar.alikhan@rrd.com", "test1", "test2", "test3", "test4");
            objPrintRequestsTask.Process(ie);
        }
        #endregion
        #region Process_With_FrequencyType_6
        /// <summary>
        /// Process_With_FrequencyType_6
        /// </summary>
        [TestMethod]
        public void Process_With_FrequencyType_6()
        {
            //Act
            PrintRequestsTask objPrintRequestsTask = new PrintRequestsTask();
            ReportScheduleEntry ie = new ReportScheduleEntry(1, "defaultmonthlyerrorreport", 1, "forethought", 1, 6, 1, DateTime.Now.AddDays(-200), "test", 0, "pathanmuzafar.alikhan@rrd.com", "test1", "test2", "test3", "test4");
            objPrintRequestsTask.Process(ie);
        }
        #endregion
        #region Process_With_FrequencyType_7
        /// <summary>
        /// Process_With_FrequencyType_7
        /// </summary>
        [TestMethod]
        public void Process_With_FrequencyType_7()
        {
            //Act
            PrintRequestsTask objPrintRequestsTask = new PrintRequestsTask();
            ReportScheduleEntry ie = new ReportScheduleEntry(1, "defaultmonthlyerrorreport", 1, "forethought", 1, 7, 1, DateTime.Now.AddDays(-200), "test", 0, "pathanmuzafar.alikhan@rrd.com", "test1", "test2", "test3", "test4");
            objPrintRequestsTask.Process(ie);
        }
        #endregion       
    }
}
