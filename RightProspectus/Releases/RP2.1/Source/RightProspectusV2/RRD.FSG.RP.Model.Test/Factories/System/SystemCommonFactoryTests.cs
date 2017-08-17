using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RRD.FSG.RP.Model.Test.Factories.System
{
    [TestClass]
    public class SystemCommonFactoryTests : BaseTestFactory<ClientDataFromSystem>
    {
        Mock<IDataAccess> mockDataAccess;

        #region TestIntialize
        /// <summary>
        /// TestInitialze
        /// </summary>
        [TestInitialize]
        public void TestInitialze()
        {
            mockDataAccess = new Mock<IDataAccess>();
        }
        #endregion

        #region ClientData
        /// <summary>
        /// ClientData
        /// </summary>
        public void ClientData()
        {
            DataSet dSet = new DataSet();
            //Table 0
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("ClientID", typeof(Int32));
            dt1.Columns.Add("ClientName", typeof(string));
            dt1.Columns.Add("ClientDNS", typeof(string));
            dt1.Columns.Add("ClientConnectionStringName", typeof(string));
            dt1.Columns.Add("ClientDatabaseName", typeof(string));
            dt1.Columns.Add("VerticalMarketConnectionStringName", typeof(string));
            dt1.Columns.Add("VerticalMarketsDatabaseName", typeof(string));

            DataRow dtrow1 = dt1.NewRow();
            dtrow1["ClientID"] = 2;
            dtrow1["ClientName"] = "Forethought";
            dtrow1["ClientDNS"] = null;
            dtrow1["ClientConnectionStringName"] = "ClientDBInstance1";
            dtrow1["ClientDatabaseName"] = "RPV2ClientDb1";
            dtrow1["VerticalMarketConnectionStringName"] = "USVerticalMarketDBInstance";
            dtrow1["VerticalMarketsDatabaseName"] = "RPV2USDB";
            dt1.Rows.Add(dtrow1);
            dSet.Tables.Add(dt1);

            //Table 1
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TemplateId", typeof(Int32));
            dt2.Columns.Add("TemplateName", typeof(string));
            dt2.Columns.Add("PageID", typeof(Int32));
            dt2.Columns.Add("PageName", typeof(string));

            DataRow dtrow2 = dt2.NewRow();
            dtrow2["TemplateId"] = 1;
            dtrow2["TemplateName"] = "Default";
            dtrow2["PageID"] = 1;
            dtrow2["PageName"] = "TAL";

            dt2.Rows.Add(dtrow2);
            dSet.Tables.Add(dt2);

            //Table 2
            DataTable dt3 = new DataTable();
            dt3.Columns.Add("TemplateId", typeof(Int32));
            dt3.Columns.Add("DefaultNavigationXml", typeof(string));
            dt3.Columns.Add("NavigationKey", typeof(string));
            dt3.Columns.Add("XslTransform", typeof(string));

            dSet.Tables.Add(dt3);
            DataRow dtrow3 = dt3.NewRow();
            dtrow3["TemplateId"] = 1;
            dtrow3["DefaultNavigationXml"] = DBNull.Value; ;
            dtrow3["NavigationKey"] = "TaxonomySpecificDocumentFrame_DocumentType"; ;
            dtrow3["XslTransform"] = DBNull.Value; ;

            dt3.Rows.Add(dtrow3);
            //dSet.Tables.Add(dt3);

            //Table 3
            DataTable dt4 = new DataTable();
            dt4.Columns.Add("TemplateId", typeof(Int32));
            dt4.Columns.Add("PageId", typeof(Int32));
            dt4.Columns.Add("NavigationKey", typeof(string));
            dt4.Columns.Add("XslTransform", typeof(XmlReadMode));
            dt4.Columns.Add("DefaultNavigationXml", typeof(XmlReadMode));

            DataRow dtrow4 = dt4.NewRow();
            dtrow4["TemplateId"] = 1;
            dtrow4["PageId"] = 3;
            dtrow4["NavigationKey"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow4["XslTransform"] = DBNull.Value;
            dtrow4["DefaultNavigationXml"] = DBNull.Value;
            dt4.Rows.Add(dtrow4);
            dSet.Tables.Add(dt4);
            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>())).Returns(dSet);

        }
        #endregion

        #region GetClientsDataFromCache_Returns_ClientDataFromSystem
        /// <summary>
        /// GetClientsDataFromCache_Returns_ClientDataFromSystem
        /// </summary>
        [TestMethod]
        public void GetClientsDataFromCache_Returns_ClientDataFromSystem()
        {
            //Arrange
            ClientData();
            //Act
            SystemCommonFactory objSystemCommonFactory = new SystemCommonFactory(mockDataAccess.Object);
            var result = objSystemCommonFactory.GetClientsDataFromCache();
            var resultActual = result as ClientDataFromSystem;
            ClientDataFromSystem objClientDataFromSystem = new ClientDataFromSystem();

            List<ClientDbConnection> lstExpected = new List<ClientDbConnection>();
            lstExpected.Add(new ClientDbConnection()
         {
             ClientID = 2,
             ClientName = "Forethought",
             ClientDNS = "",
             ClientConnectionStringName = "ClientDBInstance1",
             ClientDatabaseName = "RPV2ClientDb1",
             VerticalMarketConnectionStringName = "USVerticalMarketDBInstance",
             VerticalMarketsDatabaseName = "RPV2USDB"

         });

            List<HostedTemplatePage> lstExpected1 = new List<HostedTemplatePage>();
            lstExpected1.Add(new HostedTemplatePage()
           {
               TemplateID = 1,
               TemplateName = "Default",
               PageID = 1,
               PageName = "TAL"
           });

            List<HostedTemplateNavigation> lstExpected2 = new List<HostedTemplateNavigation>();
            lstExpected2.Add(new HostedTemplateNavigation()
            {
                TemplateID = 1,
                DefaultNavigationXml = "",
                NavigationKey = "TaxonomySpecificDocumentFrame_DocumentType",
                XslTransform = ""
            });

            List<HostedTemplatePageNavigation> lstExpected3 = new List<HostedTemplatePageNavigation>();
            lstExpected3.Add(new HostedTemplatePageNavigation()
            {
                TemplateID=1,
                PageID = 3,
                NavigationKey = "TaxonomySpecificDocumentFrame_DocumentType",
                XslTransform = "",
                DefaultNavigationXml = ""
            });
            objClientDataFromSystem.ClientDbConnections = lstExpected;
            objClientDataFromSystem.TemplatePages = lstExpected1;
            objClientDataFromSystem.HostedTemplateNavigations = lstExpected2;
            objClientDataFromSystem.HostedTemplatePageNavigations = lstExpected3;

            ValidateObjectModelData<ClientDataFromSystem>(resultActual, objClientDataFromSystem);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetDBConnections_Returns_List_ClientDbConnection
        /// <summary>
        /// GetDBConnections_Returns_List_ClientDbConnection
        /// </summary>
        [TestMethod]
        public void GetDBConnections_Returns_List_ClientDbConnection()
        {
            //Arrange
            ClientData();

            //Act
            SystemCommonFactory objSystemCommonFactory = new SystemCommonFactory(mockDataAccess.Object);
            var result = objSystemCommonFactory.GetDBConnections();
            List<ClientDbConnection> lstExpected = new List<ClientDbConnection>();
            lstExpected.Add(new ClientDbConnection()
         {
             ClientID = 2,
             ClientName = "Forethought",
             ClientDNS = "",
             ClientConnectionStringName = "ClientDBInstance1",
             ClientDatabaseName = "RPV2ClientDb1",
             VerticalMarketConnectionStringName = "USVerticalMarketDBInstance",
             VerticalMarketsDatabaseName = "RPV2USDB"

         });
           

            ValidateListData(lstExpected, result.ToList());
            //Assert
            mockDataAccess.VerifyAll();

        }
        #endregion
    }
}

