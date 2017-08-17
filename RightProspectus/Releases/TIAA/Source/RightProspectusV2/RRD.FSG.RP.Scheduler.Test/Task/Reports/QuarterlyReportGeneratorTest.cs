using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using RRD.DSA.Core;
using RRD.FSG.RP.Model.Interfaces.HostedPages;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Scheduler.Task.Reports;
using RRD.FSG.RP.Scheduler.Task;
using System.Data;
using System.Xml;

namespace RRD.FSG.RP.Scheduler.Test.Task.Reports
{
    [TestClass]
    public class QuarterlyReportGeneratorTest
    {
        Mock<IFactory<SiteActivityObjectModel, int>> mocksiteActivityFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            mocksiteActivityFactory = new Mock<IFactory<SiteActivityObjectModel, int>>();
        }

        #region GetQuarterlyReportbyDate_Returns_IEnumerable
        /// <summary>
        /// GetQuarterlyReportbyDate_Returns_IEnumerable
        /// </summary>
        [TestMethod]
        public void GetQuarterlyReportbyDate_Returns_IEnumerable()
        {
            //Act
            QuarterlyReportGenerator objQuarterlyReportGenerator = new QuarterlyReportGenerator(mocksiteActivityFactory.Object);
            var result = objQuarterlyReportGenerator.GetQuarterlyReportbyDate(It.IsAny<DateTime>(), It.IsAny<DateTime>());
            //Assert  
            Assert.AreNotEqual(result, null);
            mocksiteActivityFactory.VerifyAll();
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
            QuarterlyReportGenerator objQuarterlyReportGenerator = new QuarterlyReportGenerator(mocksiteActivityFactory.Object);
            var resultActual = objQuarterlyReportGenerator.GenerateReport(objReportContentObjectModel);
            //Assert    
            Assert.AreNotEqual(resultActual,null);
            mocksiteActivityFactory.VerifyAll();
        }
        #endregion
        #region PrepareSummaryQuarterlyDataset_returns_Datatable
        /// <summary>
        /// PrepareSummaryQuarterlyDataset_returns_Datatable
        /// </summary>
        [TestMethod]
        public void PrepareSummaryQuarterlyDataset_returns_Datatable()
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
            QuarterlyReportGenerator objQuarterlyReportGenerator = new QuarterlyReportGenerator(mocksiteActivityFactory.Object);
            var result = objQuarterlyReportGenerator.PrepareSummaryQuarterlyDataset(IenumSiteActivityObjectModel, DateTime.Now, DateTime.Now);
            //Assert
            Assert.AreEqual(result.DataSet, null);
            mocksiteActivityFactory.VerifyAll();
        }
        #endregion
        #region PrepareSummaryQuarterlyDataset_returns_Datatable_docTypes_As_Test_MID
        /// <summary>
        /// PrepareSummaryQuarterlyDataset_returns_Datatable_docTypes_As_Test_MID
        /// </summary>
        [TestMethod]
        public void PrepareSummaryQuarterlyDataset_returns_Datatable_docTypes_As_Test_MID()
        {
            //Arrange
            IEnumerable<SiteActivityObjectModel> IenumSiteActivityObjectModel = Enumerable.Empty<SiteActivityObjectModel>();
            List<SiteActivityObjectModel> lstSiteActivityObjectModel = new List<SiteActivityObjectModel>();
            SiteActivityObjectModel objSiteActivityObjectModel = new SiteActivityObjectModel();
            objSiteActivityObjectModel.SiteId = 1;
            objSiteActivityObjectModel.SiteName = "test";
            objSiteActivityObjectModel.DocumentTypeMarketId = "Test_MID";
            lstSiteActivityObjectModel.Add(objSiteActivityObjectModel);
            IenumSiteActivityObjectModel = lstSiteActivityObjectModel;

            //Act
            QuarterlyReportGenerator objQuarterlyReportGenerator = new QuarterlyReportGenerator(mocksiteActivityFactory.Object);
            var result = objQuarterlyReportGenerator.PrepareSummaryQuarterlyDataset(IenumSiteActivityObjectModel, DateTime.Now, DateTime.Now);
            //Assert
            Assert.AreEqual(result.DataSet, null);
            mocksiteActivityFactory.VerifyAll();
        }
        #endregion
        #region PrepareQuarterlyDataset_returns_Datatable
        /// <summary>
        /// PrepareQuarterlyDataset_returns_Datatable
        /// </summary>
        [TestMethod]
        public void PrepareQuarterlyDataset_returns_Datatable()
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
            QuarterlyReportGenerator objQuarterlyReportGenerator = new QuarterlyReportGenerator(mocksiteActivityFactory.Object);
            var result = objQuarterlyReportGenerator.PrepareQuarterlyDataset(IenumSiteActivityObjectModel);
            Assert.AreEqual(result.DataSet, null);
            mocksiteActivityFactory.VerifyAll();
        }
        #endregion
    }
}
