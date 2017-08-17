using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Scheduler.Task.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Scheduler.Test.Task.ErrorReports
{
    [TestClass]
    public class MonthlyErrorReportGeneratorTest
    {
        Mock<IFactory<SiteActivityObjectModel, int>> mockbadRequestFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            mockbadRequestFactory = new Mock<IFactory<SiteActivityObjectModel, int>>();
        }
        #region GetMonthlyErrorReportbyDate_Returns_IEnumerable
        /// <summary>
        /// GetMonthlyErrorReportbyDate_Returns_IEnumerable
        /// </summary>
        [TestMethod]
        public void GetMonthlyErrorReportbyDate_Returns_IEnumerable()
        {
            //Act
            MonthlyErrorReportGenerator objMonthlyErrorReportGenerator = new MonthlyErrorReportGenerator(mockbadRequestFactory.Object);
            var result = objMonthlyErrorReportGenerator.GetMonthlyErrorReportbyDate(It.IsAny<DateTime>(), It.IsAny<DateTime>());
            //Assert    
            Assert.AreNotEqual(result, null);   
            mockbadRequestFactory.VerifyAll();
        }
        #endregion
        #region GenerateReport
        /// <summary>
        /// GenerateReport
        /// </summary>
        [TestMethod]
        public void GenerateReport()
        {
            //Arrange
            ReportContentObjectModel objReportContentObjectModel = new ReportContentObjectModel();
            objReportContentObjectModel.ReportContentId = 1;
            objReportContentObjectModel.ReportScheduleId = 2;
            objReportContentObjectModel.FileName = "Test_file";
            //Act
            MonthlyErrorReportGenerator objMonthlyErrorReportGenerator = new MonthlyErrorReportGenerator(mockbadRequestFactory.Object);
            var resultActual = objMonthlyErrorReportGenerator.GenerateReport(objReportContentObjectModel);
            //Assert         
            Assert.AreNotEqual(resultActual,null);
            mockbadRequestFactory.VerifyAll();
        }
        #endregion
        #region PrepareMonthlyErrorDataset_returns_DataTable
        /// <summary>
        /// PrepareMonthlyErrorDataset_returns_DataTable
        /// </summary>
        [TestMethod]
        public void PrepareMonthlyErrorDataset_returns_DataTable()
        {
            //Arrange
            IEnumerable<SiteActivityObjectModel> IenumSiteActivityObjectModel = Enumerable.Empty<SiteActivityObjectModel>();
            List<SiteActivityObjectModel> lstSiteActivityObjectModel = new List<SiteActivityObjectModel>();
            SiteActivityObjectModel objSiteActivityObjectModel = new SiteActivityObjectModel();
            objSiteActivityObjectModel.SiteId = 1;
            objSiteActivityObjectModel.SiteName = "test";
            objSiteActivityObjectModel.DocumentTypeMarketId = "SP";
            lstSiteActivityObjectModel.Add(objSiteActivityObjectModel);
            IenumSiteActivityObjectModel = lstSiteActivityObjectModel;
            //Act
            MonthlyErrorReportGenerator objMonthlyErrorReportGenerator = new MonthlyErrorReportGenerator(mockbadRequestFactory.Object);
            var resultActual = objMonthlyErrorReportGenerator.PrepareMonthlyErrorDataset(IenumSiteActivityObjectModel,"test_type",DateTime.Now,DateTime.Now);
            //Assert    
            Assert.AreEqual(resultActual.DataSet,null);
            mockbadRequestFactory.VerifyAll();
        }
        #endregion

    }
}
