using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRD.DSA.Core;
using RRD.DSA.Core.DAL;
using System.Data;
using System.Data.Common;
using Moq;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Scheduler.Task;
using System.Xml;

namespace RRD.FSG.RP.Scheduler.Test.Task.Reports
{
    /// <summary>
    /// TestClass for MonthlyReportGenerator class
    /// </summary>
    [TestClass]
    public class MonthlyReportGeneratorTest
    {
        Mock<IFactory<SiteActivityObjectModel, int>> mocksiteActivityFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            mocksiteActivityFactory = new Mock<IFactory<SiteActivityObjectModel, int>>();
        }

        #region GetMonthlyReportbyDate
        ///<summary>
        ///GetMonthlyReportbyDate
        ///</summary>
        [TestMethod]
        public void GetMonthlyReportbyDate()
        {
            //Arrange

            //Act
            MonthlyReportGenerator obj = new MonthlyReportGenerator(mocksiteActivityFactory.Object);
            var result = obj.GetMonthlyReportbyDate(DateTime.Now, DateTime.Now);
            //Assert
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
            ReportContentObjectModel objReportContentObjectModel = new ReportContentObjectModel()
            {
                ReportFromDate = DateTime.Parse("01/02/2015"),
                ReportToDate = DateTime.Now,
                FrequencyType = "Test",
                FileName = "Test_001"
            };

            //Act
            MonthlyReportGenerator obj = new MonthlyReportGenerator(mocksiteActivityFactory.Object);
            var result = obj.GenerateReport(objReportContentObjectModel);

            //Assert
            mocksiteActivityFactory.Verify();
        }
        #endregion

        #region PrepareMonthlyDataset
        ///<summary>
        ///PrepareMonthlyDataset
        ///</summary>
        [TestMethod]
        public void PrepareMonthlyDataset()
        {
            IEnumerable<SiteActivityObjectModel> Ienum = Enumerable.Empty<SiteActivityObjectModel>();
            List<SiteActivityObjectModel> lst = new List<SiteActivityObjectModel>();
            SiteActivityObjectModel objSiteActivityObjectModel = new SiteActivityObjectModel()
            {
                DocumentTypeMarketId = "Test_Doc",
                DocumentType = "Test"
            };
            lst.Add(objSiteActivityObjectModel);
            Ienum = lst;

            //Act
            MonthlyReportGenerator obj = new MonthlyReportGenerator(mocksiteActivityFactory.Object);
            var result = obj.PrepareMonthlyDataset(Ienum, "Test_001", DateTime.Now, DateTime.Now);
            //Assert
            mocksiteActivityFactory.VerifyAll();
        }
        #endregion

        #region PrepareMonthlyDataset_Doctype
        ///<summary>
        ///PrepareMonthlyDataset
        ///</summary>
        [TestMethod]
        public void PrepareMonthlyDataset_Doctype()
        {
            //Assign
            IEnumerable<SiteActivityObjectModel> Ienum = Enumerable.Empty<SiteActivityObjectModel>();
            List<SiteActivityObjectModel> lst = new List<SiteActivityObjectModel>();
            SiteActivityObjectModel objSiteActivityObjectModel = new SiteActivityObjectModel()
            {
                DocumentTypeMarketId = "XBRL",
                DocumentType = "Test",

            };
            lst.Add(objSiteActivityObjectModel);
            Ienum = lst;

            //Act
            MonthlyReportGenerator obj = new MonthlyReportGenerator(mocksiteActivityFactory.Object);
            var result = obj.PrepareMonthlyDataset(Ienum, "Test_001", DateTime.Now, DateTime.Now);
            //Assert
            mocksiteActivityFactory.VerifyAll();
        }
        #endregion
    }
}
