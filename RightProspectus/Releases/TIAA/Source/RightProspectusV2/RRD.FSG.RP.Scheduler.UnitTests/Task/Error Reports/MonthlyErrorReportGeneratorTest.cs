using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RRD.DSA.Core.DAL;

namespace RRD.FSG.RP.Scheduler.UnitTests.Task.Error_Reports
{
    /// <summary>
    /// Test class for MonthlyErrorReportGeneratorTest class
    /// </summary>
    [TestClass]
    public class MonthlyErrorReportGeneratorTest
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

    }
}