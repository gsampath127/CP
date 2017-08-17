using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Scheduler.Interfaces;
using RRD.FSG.RP.Scheduler.Task;

namespace RRD.FSG.RP.Scheduler.UnitTests.Task.Error_Reports
{  /// <summary>
    /// Test class for MonthlyErrorReportGeneratorTest class
    /// </summary>
    [TestClass]
    public class ErrorReportsTaskTest
    {
        private Mock<IDataAccess> mockDataAccess;
        /// <summary>
        /// TestInitialze
        /// </summary>
        [TestInitialize]
        public void TestInitialze()
        {
            mockDataAccess = new Mock<IDataAccess>();
        }
        #region Process_IReportScheduleEntry
        ///<summary>
        ///Process_IReportScheduleEntry
        ///</summary>
        [TestMethod]
        public void Process_IReportScheduleEntry()
        {
            //Assign
            ReportScheduleEntry objReportScheduleEntry = new ReportScheduleEntry(1, "Test", 1, "Test", 2, 1, 1, DateTime.Now, "Test", 1, "xyz@rrd.com", "Test", "D:", "Test_001", "test");
            //Act
            ErrorReportsTask objErrorReportsTask = new ErrorReportsTask();
            objErrorReportsTask.Process(objReportScheduleEntry);
            //Assert
            mockDataAccess.VerifyAll();
        }

        #endregion
    }
}