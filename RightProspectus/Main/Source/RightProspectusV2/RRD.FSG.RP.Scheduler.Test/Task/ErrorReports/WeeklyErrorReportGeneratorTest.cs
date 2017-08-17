using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Scheduler.Task.Reports;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using System.Data;
using System.Data.Common;
using RRD.FSG.RP.Model.SearchEntities.System;

namespace RRD.FSG.RP.Scheduler.Test.Task.ErrorReports
{
    /// <summary>
    /// Test class for WeeklyErrorReportGenerator class
    /// </summary>
    [TestClass]
    public class WeeklyErrorReportGeneratorTest
    {


        Mock<IFactory<SiteActivityObjectModel, int>> mockbadRequestFactory;
        ///// <summary>
        ///// TestInitialze
        ///// </summary>
      
        [TestInitialize]
        public void TestInitialize()
        {
            mockbadRequestFactory = new Mock<IFactory<SiteActivityObjectModel, int>>();
           
        }

        #region GenerateReport_Calls_Scheduler
        /// <summary>
        /// GenerateReport_Calls_Scheduler
        /// </summary>
        [TestMethod]
        public void GenerateReport_Calls_Scheduler()
        {
            List<SiteActivityObjectModel> lst = new List<SiteActivityObjectModel>();
            SiteActivityObjectModel obj = new SiteActivityObjectModel();
            ReportContentObjectModel objReportContentObjectModel = new ReportContentObjectModel();
            objReportContentObjectModel.FileName = "0";
            WeeklyErrorReportGenerator objDocumentTypeExternalIdFactory = new WeeklyErrorReportGenerator(mockbadRequestFactory.Object, "");
            var result = objDocumentTypeExternalIdFactory.GenerateReport(objReportContentObjectModel);

        }

        #endregion

        #region GenerateReport_Calls_Scheduler
        /// <summary>
        /// GenerateReport_Calls_Scheduler
        /// </summary>
        [TestMethod]
        public void GenerateReport_Calls_Scheduler2()
        {
            
        IEnumerable<SiteActivityObjectModel> ObjIenumerable1 = Enumerable.Empty<SiteActivityObjectModel>();
        List<SiteActivityObjectModel> lstSiteActivityObjectModel = new List<SiteActivityObjectModel>();
        SiteActivityObjectModel objSiteActivityObjectModel = new SiteActivityObjectModel();
        objSiteActivityObjectModel.UserId = 1;
        lstSiteActivityObjectModel.Add(objSiteActivityObjectModel);
        ObjIenumerable1 = lstSiteActivityObjectModel;
           
            ReportContentObjectModel objReportContentObjectModel = new ReportContentObjectModel();
            objReportContentObjectModel.FileName = "0";
           
            WeeklyErrorReportGenerator objDocumentTypeExternalIdFactory = new WeeklyErrorReportGenerator(mockbadRequestFactory.Object, "");
            mockbadRequestFactory.Setup(x => x.GetEntitiesBySearch(It.IsAny<SiteActivitySearchDetail>())).Returns(ObjIenumerable1);
           
            var result = objDocumentTypeExternalIdFactory.GenerateReport(objReportContentObjectModel);


        }

        #endregion

    
         
    }
}