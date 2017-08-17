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

namespace RRD.FSG.RP.Scheduler.Test.Task
{ /// <summary>
    /// Test class for WeeklyErrorReportGenerator class
    /// </summary>
    [TestClass]
   public class ReportContentTest
   {
        Mock<IFactory<ReportContentObjectModel, int>> mockReportContentFactory;
       ///// <summary>
       ///// TestInitialze
       ///// </summary>

       [TestInitialize]
       public void TestInitialize()
       {
           mockReportContentFactory = new Mock<IFactory<ReportContentObjectModel, int>>();

       }
       #region SaveReportContent_Calls_Scheduler
       /// <summary>
       /// GenerateReport_Calls_Scheduler
       /// </summary>
       [TestMethod]
       public void SaveReportContent_Calls_Scheduler()
       {
           
           ReportContentObjectModel objReportContentObjectModel = new ReportContentObjectModel();
           objReportContentObjectModel.FileName = "Test";
           ReportContent objDocumentTypeExternalIdFactory = new ReportContent(mockReportContentFactory.Object, "");
           objDocumentTypeExternalIdFactory.SaveReportContent(objReportContentObjectModel);

       }

       #endregion
    }
}
