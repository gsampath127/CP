using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Scheduler.Interfaces;

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
            ReportContentObjectModel obj = new ReportContentObjectModel()
            {
                ReportToDate = DateTime.Parse("01/02/2015"),
                Name = "Test",
                ReportScheduleId = 1,
                FrequencyType="test",
            };

        
        }

        #endregion
    }
}