using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RRD.FSG.RP.Scheduler;
using RRD.FSG.RP.Scheduler.Task;
using RRD.FSG.RP.Scheduler.Task.PrintRequest;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Interfaces.HostedPages;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Scheduler.Task;
using RRD.FSG.RP.Scheduler.Task.PrintRequest;
using RRD.FSG.RP.Model.Entities.HostedPages;

namespace RRD.FSG.RP.Scheduler.UnitTests.Task.Print_Request
{
    /// <summary>
    /// TestClass for PrintRequestGenerator class
    /// </summary>
    [TestClass]
    public class PrintRequestGeneratorTest
    {
        Mock<IHostedClientRequestMaterialFactory> mockhostedClientRequestMaterialFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            mockhostedClientRequestMaterialFactory = new Mock<IHostedClientRequestMaterialFactory>();
        }
        #region GeneratePrintRequestXML
        ///<summary>
        ///GeneratePrintRequestXML
        ///</summary>
        [TestMethod]
        public void GeneratePrintRequestXML()
        {
            //Arrange

            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType()
            {
                ContentURI = "Test",
                SKUName = "TestHostedDocumentType",
                DocumentTypeLinkText = "TestLinkText",
            };
            lstHostedDocumentType.Add(objHostedDocumentType);
            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData()
           {
               TaxonomyName = "Test_001",
               DocumentTypes = lstHostedDocumentType
           };
            List<RequestMaterialPrintHistory> lst = new List<RequestMaterialPrintHistory>();
            RequestMaterialPrintHistory objRequestMaterialPrintHistory = new RequestMaterialPrintHistory();
            objRequestMaterialPrintHistory.RequestMaterialPrintHistoryID = 1;
            objRequestMaterialPrintHistory.ClientCompanyName = "Test";
            objRequestMaterialPrintHistory.ClientFirstName = "";
            objRequestMaterialPrintHistory.ClientMiddleName = "";
            objRequestMaterialPrintHistory.ClientLastName = "";
            objRequestMaterialPrintHistory.ClientFullName = "";
            objRequestMaterialPrintHistory.TaxonomyAssociationData = objTaxonomyAssociationData;

            lst.Add(objRequestMaterialPrintHistory);
            ReportContentObjectModel obj = new ReportContentObjectModel();

            obj.ReportFromDate = DateTime.Parse("01/02/2015");
            obj.ReportToDate = DateTime.Parse("01/02/2015");
            mockhostedClientRequestMaterialFactory.Setup(x => x.GetRequestMaterialPrintRequests(It.IsAny<string>(), obj.ReportToDate, obj.ReportFromDate))
                .Returns(lst);
            //Act
            PrintRequestGenerator objPrintRequestGenerator = new Scheduler.Task.PrintRequest.PrintRequestGenerator(mockhostedClientRequestMaterialFactory.Object);
            objPrintRequestGenerator.GeneratePrintRequestXML("Test", obj);

            //Assert
            mockhostedClientRequestMaterialFactory.VerifyAll();

        }
        #endregion

        #region GeneratePrintRequestXML_Null
        ///<summary>
        ///GeneratePrintRequestXML_Null
        ///</summary>
        [TestMethod]
        public void GeneratePrintRequestXML_Null()
        {
            //Arrange

            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType()
            {
                ContentURI = "Test",
                SKUName = null,
                DocumentTypeLinkText = "TestLinkText",
            };
            lstHostedDocumentType.Add(objHostedDocumentType);
            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData()
            {
                TaxonomyName = "Test_001",
                DocumentTypes = lstHostedDocumentType
            };
            List<RequestMaterialPrintHistory> lst = new List<RequestMaterialPrintHistory>();
            RequestMaterialPrintHistory objRequestMaterialPrintHistory = new RequestMaterialPrintHistory();
            objRequestMaterialPrintHistory.TaxonomyAssociationData = objTaxonomyAssociationData;

            lst.Add(objRequestMaterialPrintHistory);
            ReportContentObjectModel obj = new ReportContentObjectModel();

            obj.ReportFromDate = DateTime.Parse("01/02/2015");
            obj.ReportToDate = DateTime.Parse("01/02/2015");
            mockhostedClientRequestMaterialFactory.Setup(x => x.GetRequestMaterialPrintRequests(It.IsAny<string>(), obj.ReportToDate, obj.ReportFromDate))
                .Returns(lst);
            //Act
            PrintRequestGenerator objPrintRequestGenerator = new Scheduler.Task.PrintRequest.PrintRequestGenerator(mockhostedClientRequestMaterialFactory.Object);
            objPrintRequestGenerator.GeneratePrintRequestXML("Test", obj);

            //Assert
            mockhostedClientRequestMaterialFactory.VerifyAll();

        }
        #endregion
    }
}