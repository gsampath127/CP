using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Model.SortDetail.System;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for ClientFactory class
    /// </summary>
    [TestClass]
    public class ClientTests : BaseTestFactory<ClientObjectModel>
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


        #region GetAllEntities_Returns_IEnumerable
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable()
        {
            //Arrange
            ClientSortDetail objSortDtl = new ClientSortDetail();
            objSortDtl.Column = ClientSortColumn.ClientConnectionStringName;
            objSortDtl.Order = SortOrder.Ascending;

            ClientDNSObjectModel objClientDNSObjectModel = new ClientDNSObjectModel();
            objClientDNSObjectModel.ClientDnsId = 1;
            objClientDNSObjectModel.Dns = "10.20.0.11";
            List<int> objlist = new List<int>();

            DataTable dt = new DataTable();
            dt.Columns.Add("ClientId", typeof(Int32));
            dt.Columns.Add("ClientName", typeof(string));
            dt.Columns.Add("ClientConnectionStringName", typeof(string));
            dt.Columns.Add("ClientDatabaseName", typeof(string));
            dt.Columns.Add("VerticalMarketId", typeof(Int32));
            dt.Columns.Add("ClientDescription", typeof(string));
            dt.Columns.Add("UtcModifiedDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("VerticalMarketName", typeof(string));
            dt.Columns.Add("VerticalMarketDatabaseName", typeof(string));
            dt.Columns.Add("VerticalMarketConnectionStringName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ClientDnsList", typeof(List<ClientDNSObjectModel>));
            dt.Columns.Add("ClientDnsId", typeof(Int32));
            dt.Columns.Add("Dns", typeof(string));
            dt.Columns.Add("Users", typeof(List<int>));
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("ClientDnsSiteId", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["ClientId"] = 1;
            dtrow["ClientName"] = "Forethought";
            dtrow["ClientConnectionStringName"] = 1;
            dtrow["ClientDatabaseName"] = 1;
            dtrow["VerticalMarketId"] = 1;
            dtrow["ClientDescription"] = "Forethought Site";
            dtrow["UtcModifiedDate"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["VerticalMarketName"] = "Test2";
            dtrow["VerticalMarketDatabaseName"] = "DB_Test";
            dtrow["VerticalMarketConnectionStringName"] = "DB_Test";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["UtcLastModified"] = DateTime.Today;
            dtrow["ClientDnsList"] = null;// new ClientDNSObjectModel { ClientDnsId = 1 };
            dtrow["ClientDnsId"] = 1;
            dtrow["Dns"] = "1";
            dtrow["Users"] = objlist;
            dtrow["UserId"] = 1;
            dtrow["ClientDnsSiteId"] = 12;

            dt.Rows.Add(dtrow);


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

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(dt);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            //Act
            ClientFactory objFactoryCache = new ClientFactory(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            var result = objFactoryCache.GetAllEntities(0, 0, objSortDtl);

            List<ClientDNSObjectModel> cdnlist = new List<ClientDNSObjectModel>();
            ClientDNSObjectModel obj = new ClientDNSObjectModel();
            obj.ClientDnsId = 1;
            obj.ClientDnsSiteId = 12;
            obj.Dns = "1";
            cdnlist.Add(obj);

            List<int> userslist = new List<int>();
            userslist.Add(1);

            ClientObjectModel objExpected = new ClientObjectModel
            {

                ClientConnectionStringName = "1",
                ClientDatabaseName = "1",
                ClientDescription = "Forethought Site",
                ClientDnsList = cdnlist,
                ClientID = 1,
                ClientName = "Forethought",
                Users = userslist,
                VerticalMarketConnectionStringName = "DB_Test",
                VerticalMarketDatabaseName = "DB_Test",
                VerticalMarketId = 1,
                VerticalMarketName = "Test2"

            };
            ValidateObjectModelData<ClientObjectModel>(result.ToList()[0], objExpected);
            //Assert

            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetAllEntities_Returns_IEnumerable_DNSId_DBCUserId_Null
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_DNSId_DBCUserId_Null
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_DNSId_DBCUserId_Null()
        {
            //Arrange
            ClientSortDetail objSortDtl = new ClientSortDetail();
            objSortDtl.Column = ClientSortColumn.ClientConnectionStringName;
            objSortDtl.Order = SortOrder.Ascending;

            ClientDNSObjectModel objClientDNSObjectModel = new ClientDNSObjectModel();
            objClientDNSObjectModel.ClientDnsId = 1;
            objClientDNSObjectModel.Dns = "10.20.0.11";
            List<int> objlist = new List<int>();

            DataTable dt = new DataTable();
            dt.Columns.Add("ClientId", typeof(Int32));
            dt.Columns.Add("ClientName", typeof(string));
            dt.Columns.Add("ClientConnectionStringName", typeof(string));
            dt.Columns.Add("ClientDatabaseName", typeof(string));
            dt.Columns.Add("VerticalMarketId", typeof(Int32));
            dt.Columns.Add("ClientDescription", typeof(string));
            dt.Columns.Add("UtcModifiedDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("VerticalMarketName", typeof(string));
            dt.Columns.Add("VerticalMarketDatabaseName", typeof(string));
            dt.Columns.Add("VerticalMarketConnectionStringName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ClientDnsList", typeof(List<ClientDNSObjectModel>));
            dt.Columns.Add("ClientDnsId", typeof(Int32));
            dt.Columns.Add("Dns", typeof(string));
            dt.Columns.Add("Users", typeof(List<int>));
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("ClientDnsSiteId", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["ClientId"] = 1;
            dtrow["ClientName"] = "Forethought";
            dtrow["ClientConnectionStringName"] = 1;
            dtrow["ClientDatabaseName"] = 1;
            dtrow["VerticalMarketId"] = 1;
            dtrow["ClientDescription"] = "Forethought Site";
            dtrow["UtcModifiedDate"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["VerticalMarketName"] = "Test2";
            dtrow["VerticalMarketDatabaseName"] = "DB_Test";
            dtrow["VerticalMarketConnectionStringName"] = "DB_Test";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["UtcLastModified"] = DateTime.Today;
            dtrow["ClientDnsList"] = null;// new ClientDNSObjectModel { ClientDnsId = 1 };
            dtrow["ClientDnsId"] = DBNull.Value;
            dtrow["Dns"] = "1";
            dtrow["Users"] = objlist;
            dtrow["UserId"] = DBNull.Value;
            dtrow["ClientDnsSiteId"] = 12;

            dt.Rows.Add(dtrow);


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

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(dt);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            //Act
            ClientFactory objFactoryCache = new ClientFactory(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            var result = objFactoryCache.GetAllEntities(0, 0, objSortDtl);
               
            //Assert
            Assert.AreNotEqual(result,"");
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetAllEntities_Returns_IEnumerable_With_Two_Rows
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_With_Two_Rows
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_With_Two_Rows()
        {
            //Arrange
            ClientSortDetail objSortDtl = new ClientSortDetail();
            objSortDtl.Column = ClientSortColumn.ClientConnectionStringName;
            objSortDtl.Order = SortOrder.Ascending;

            ClientDNSObjectModel objClientDNSObjectModel = new ClientDNSObjectModel();
            objClientDNSObjectModel.ClientDnsId = 1;
            objClientDNSObjectModel.Dns = "10.20.0.11";
            List<int> objlist = new List<int>();

            DataTable dt = new DataTable();
            dt.Columns.Add("clientId", typeof(int));               
            dt.Columns.Add("ClientName", typeof(string));
            dt.Columns.Add("ClientConnectionStringName", typeof(string));
            dt.Columns.Add("ClientDatabaseName", typeof(string));
            dt.Columns.Add("VerticalMarketId", typeof(Int32));
            dt.Columns.Add("ClientDescription", typeof(string));
            dt.Columns.Add("UtcModifiedDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("VerticalMarketName", typeof(string));
            dt.Columns.Add("VerticalMarketDatabaseName", typeof(string));
            dt.Columns.Add("VerticalMarketConnectionStringName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ClientDnsList", typeof(List<ClientDNSObjectModel>));
            dt.Columns.Add("ClientDnsId", typeof(int));
            dt.Columns.Add("Dns", typeof(string));
            dt.Columns.Add("Users", typeof(List<int>));
            dt.Columns.Add("UserId", typeof(Int32));
            dt.Columns.Add("ClientDnsSiteId", typeof(Int32));

            DataRow dtrow =  dt.NewRow();
            dtrow["clientId"] = 1;
            dtrow["ClientName"] = "Forethought";
            dtrow["ClientConnectionStringName"] = 1;
            dtrow["ClientDatabaseName"] = 1;
            dtrow["VerticalMarketId"] = 1;
            dtrow["ClientDescription"] = "Forethought Site";
            dtrow["UtcModifiedDate"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["VerticalMarketName"] = "Test2";
            dtrow["VerticalMarketDatabaseName"] = "DB_Test";
            dtrow["VerticalMarketConnectionStringName"] = "DB_Test";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["UtcLastModified"] = DateTime.Today;
            dtrow["ClientDnsList"] = null;// new ClientDNSObjectModel { ClientDnsId = 1 };
            dtrow["ClientDnsId"] = 1;
            dtrow["Dns"] = "1";
            dtrow["Users"] = objlist;
            dtrow["UserId"] = 1;
            dtrow["ClientDnsSiteId"] = 12;
            dt.Rows.Add(dtrow);

            DataRow dtrow_2 = dt.NewRow();
            dtrow_2["clientId"] = 2;
            dtrow_2["ClientName"] = "Forethought1";
            dtrow_2["ClientConnectionStringName"] = 15;
            dtrow_2["ClientDatabaseName"] = 1;
            dtrow_2["VerticalMarketId"] = 1;
            dtrow_2["ClientDescription"] = "Forethought Site";
            dtrow_2["UtcModifiedDate"] = DateTime.Now;
            dtrow_2["ModifiedBy"] = 1;
            dtrow_2["VerticalMarketName"] = "Test2";
            dtrow_2["VerticalMarketDatabaseName"] = "DB_Test";
            dtrow_2["VerticalMarketConnectionStringName"] = "DB_Test";
            dtrow_2["LastModified"] = DateTime.Today;
            dtrow_2["UtcLastModified"] = DateTime.Today;
            dtrow_2["ClientDnsList"] = null;// new ClientDNSObjectModel { ClientDnsId = 1 };
            dtrow_2["ClientDnsId"] = 3;
            dtrow_2["Dns"] = "1";
            dtrow_2["Users"] = objlist;
            dtrow_2["UserId"] = 1;
            dtrow_2["ClientDnsSiteId"] = 12;
            dt.Rows.Add(dtrow_2);


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

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(dt);          
            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            //Act
            ClientFactory objFactoryCache = new ClientFactory(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            var result = objFactoryCache.GetAllEntities(0, 0, objSortDtl);

            List<ClientDNSObjectModel> cdnlist = new List<ClientDNSObjectModel>();
            ClientDNSObjectModel obj = new ClientDNSObjectModel();
            obj.ClientDnsId = 1;
            obj.ClientDnsSiteId = 12;
            obj.Dns = "1";
            cdnlist.Add(obj);

            List<int> userslist = new List<int>();
            userslist.Add(1);

            ClientObjectModel objExpected = new ClientObjectModel
            {

                ClientConnectionStringName = "1",
                ClientDatabaseName = "1",
                ClientDescription = "Forethought Site",
                ClientDnsList = cdnlist,
                ClientID = 1,
                ClientName = "Forethought",
                Users = userslist,
                VerticalMarketConnectionStringName = "DB_Test",
                VerticalMarketDatabaseName = "DB_Test",
                VerticalMarketId = 1,
                VerticalMarketName = "Test2"

            };
            ValidateObjectModelData<ClientObjectModel>(result.ToList()[0], objExpected);
            //Assert

            mockDataAccess.VerifyAll();
        }
        #endregion
        #region SaveEntity_ClientObjectModel_Users_Value_Zero
        /// <summary>
        /// SaveEntity_ClientObjectModel_Users_Value_Zero
        /// </summary>
        [TestMethod]
        public void SaveEntity_ClientObjectModel_Users_Value_Zero()
        {
            //Arrange           
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


            ClientObjectModel objObjectModel = new ClientObjectModel();
            objObjectModel.ClientID = 2;
            objObjectModel.Name = "Test_001";
            objObjectModel.VerticalMarketId = 1;
            objObjectModel.ClientName = "Client_1";
            objObjectModel.Description = "TEST_001";
            objObjectModel.ClientDatabaseName = "DBS_001";
            objObjectModel.VerticalMarketDatabaseName = "VDBS_002";

            List<int> objUsers = new List<int>();
            objUsers.Add(0);
            objObjectModel.Users = objUsers;

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ClientID", Value=1 },
                new SqlParameter(){ ParameterName="ClientName", Value="Test_client" },
                new SqlParameter(){ ParameterName="ClientConnectionStringName", Value="Con_001" },
                new SqlParameter(){ ParameterName="ClientDatabaseName", Value="Test_dbname" }  ,
                new SqlParameter(){ ParameterName="VerticalMarketId", Value=3 }   ,  
                    new SqlParameter(){ ParameterName="ClientDescription", Value="Test_desc" },     
                        new SqlParameter(){ ParameterName="clientUsers", Value="Test1" } 
                             
            };

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4])
                .Returns(parameters[5])
                .Returns(parameters[6]);

            mockDataAccess.Setup(
                x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ClientFactoryCache objFactoryCache = new ClientFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;
            objFactoryCache.SaveEntity(objObjectModel);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region SaveEntity_ClientObjectModel_modifiedBy
        /// <summary>
        /// SaveEntity_ClientObjectModel_modifiedBy
        /// </summary>
        [TestMethod]
        public void SaveEntity_ClientObjectModel_modifiedBy()
        {
            //Arrange           
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


            ClientObjectModel objObjectModel = new ClientObjectModel();
            objObjectModel.ClientID = 2;
            objObjectModel.Name = "Test_001";
            objObjectModel.VerticalMarketId = 1;
            objObjectModel.ClientName = "Client_1";
            objObjectModel.Description = "TEST_001";
            objObjectModel.ClientDatabaseName = "DBS_001";
            objObjectModel.VerticalMarketDatabaseName = "VDBS_002";

            var parameters = new[]
                { 
                    new SqlParameter(){ ParameterName="ClientID", Value=1 },
                    new SqlParameter(){ ParameterName="ClientName", Value="Test_client" },
                    new SqlParameter(){ ParameterName="ClientConnectionStringName", Value="Con_001" },
                    new SqlParameter(){ ParameterName="ClientDatabaseName", Value="Test_dbname" }  ,
                    new SqlParameter(){ ParameterName="VerticalMarketId", Value=3 }   ,  
                        new SqlParameter(){ ParameterName="ClientDescription", Value="Test_desc" },     
                            new SqlParameter(){ ParameterName="clientUsers", Value="Test1" } ,
                            new SqlParameter(){ ParameterName="ModifiedBy", Value=32 } 

                };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4])
                .Returns(parameters[5])
                .Returns(parameters[6])
                .Returns(parameters[7]);


            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);
            mockDataAccess.Setup(
                x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ClientFactoryCache objFactoryCache = new ClientFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;
            objFactoryCache.SaveEntity(objObjectModel, 32);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region SaveEntity_ClientObjectModel_UsersNull
        /// <summary>
        /// SaveEntity_ClientObjectModel_UsersNull
        /// </summary>
        [TestMethod]
        public void SaveEntity_ClientObjectModel_UsersNull()
        {
            //Arrange           
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


            ClientObjectModel objObjectModel = new ClientObjectModel();
            objObjectModel.ClientID = 2;
            objObjectModel.Name = "Test_001";
            objObjectModel.VerticalMarketId = 1;
            objObjectModel.ClientName = "Client_1";
            objObjectModel.Description = "TEST_001";
            objObjectModel.ClientDatabaseName = "DBS_001";
            objObjectModel.VerticalMarketDatabaseName = "VDBS_002";

            List<int> objUsers = new List<int>();
            objUsers.Add(1);
            objObjectModel.Users = objUsers;

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ClientID", Value=1 },
                new SqlParameter(){ ParameterName="ClientName", Value="Test_client" },
                new SqlParameter(){ ParameterName="ClientConnectionStringName", Value="Con_001" },
                new SqlParameter(){ ParameterName="ClientDatabaseName", Value="Test_dbname" }  ,
                new SqlParameter(){ ParameterName="VerticalMarketId", Value=3 }   ,  
                    new SqlParameter(){ ParameterName="ClientDescription", Value="Test_desc" },     
                        new SqlParameter(){ ParameterName="clientUsers", Value="Test1" } 
                             
            };

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4])
                .Returns(parameters[5])
                .Returns(parameters[6]);

            mockDataAccess.Setup(
                x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ClientFactoryCache objFactoryCache = new ClientFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;
            objFactoryCache.SaveEntity(objObjectModel);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region DeleteEntity

        /// <summary>
        /// DeleteEntity
        /// </summary>
        [TestMethod]
        public void DeleteEntity()
        {
            //Arrange
            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="deletedBy", Value=1 },
                new SqlParameter(){ ParameterName="DBCClientId", Value=1 },           
            };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);


            mockDataAccess.Setup(
                x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ClientFactoryCache objDocumentTypeAssociationFactory = new ClientFactoryCache(mockDataAccess.Object);
            objDocumentTypeAssociationFactory.DeleteEntity(3);
            //Assert
            mockDataAccess.VerifyAll();
        }

        #endregion
        #region GetEntityByKey_With_EmptyTable
        /// <summary>
        /// GetEntityByKey_With_EmptyTable
        /// </summary>
        [TestMethod]
        public void GetEntityByKey_With_EmptyTable()
        {
            //Arrange          
            List<int> objlist = new List<int>();

            DataTable dt = new DataTable();
            dt.Columns.Add("ClientId", typeof(Int32));
            dt.Columns.Add("ClientName", typeof(string));
            dt.Columns.Add("ClientConnectionStringName", typeof(string));
            dt.Columns.Add("ClientDatabaseName", typeof(string));
            dt.Columns.Add("VerticalMarketId", typeof(Int32));
            dt.Columns.Add("ClientDescription", typeof(string));
            dt.Columns.Add("UtcModifiedDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("VerticalMarketName", typeof(string));
            dt.Columns.Add("VerticalMarketDatabaseName", typeof(string));
            dt.Columns.Add("VerticalMarketConnectionStringName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ClientDnsList", typeof(List<ClientDNSObjectModel>));
            dt.Columns.Add("ClientDnsId", typeof(Int32));
            dt.Columns.Add("Dns", typeof(string));
            dt.Columns.Add("Users", typeof(List<int>));
            dt.Columns.Add("UserId", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["ClientId"] = 1;
            dtrow["ClientName"] = "Forethought";
            dtrow["ClientConnectionStringName"] = 1;
            dtrow["ClientDatabaseName"] = 1;
            dtrow["VerticalMarketId"] = 1;
            dtrow["ClientDescription"] = "Forethought Site";
            dtrow["UtcModifiedDate"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["VerticalMarketName"] = "Test2";
            dtrow["VerticalMarketDatabaseName"] = "DB_Test";
            dtrow["VerticalMarketConnectionStringName"] = "DB_Test";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["UtcLastModified"] = DateTime.Today;
            dtrow["ClientDnsList"] = null;// new ClientDNSObjectModel { ClientDnsId = 1 };
            dtrow["ClientDnsId"] = 1;
            dtrow["Dns"] = "1";
            dtrow["Users"] = objlist;
            dtrow["UserId"] = 1;
            dt.Rows.Add(dtrow);

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

            DataTable EmptyTable = new DataTable();


            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(EmptyTable);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            //Act
            ClientFactoryCache objclientFactoryCache = new ClientFactoryCache(mockDataAccess.Object);
            objclientFactoryCache.ClientName = "Forethought";

            var result = objclientFactoryCache.GetEntityByKey<ClientObjectModel>(1);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetEntityByKey_Returns_ClientObjectModel
        /// <summary>
        /// GetEntityByKey_Returns_ClientObjectModel
        /// </summary>
        [TestMethod]
        public void GetEntityByKey_Returns_ClientObjectModel()
        {
            //Arrange          
            List<int> objlist = new List<int>();

            DataTable dt = new DataTable();
            dt.Columns.Add("ClientId", typeof(Int32));
            dt.Columns.Add("ClientName", typeof(string));
            dt.Columns.Add("ClientConnectionStringName", typeof(string));
            dt.Columns.Add("ClientDatabaseName", typeof(string));
            dt.Columns.Add("VerticalMarketId", typeof(Int32));
            dt.Columns.Add("ClientDescription", typeof(string));
            dt.Columns.Add("UtcModifiedDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("VerticalMarketName", typeof(string));
            dt.Columns.Add("VerticalMarketDatabaseName", typeof(string));
            dt.Columns.Add("VerticalMarketConnectionStringName", typeof(string));
            dt.Columns.Add("LastModified", typeof(DateTime));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ClientDnsList", typeof(List<ClientDNSObjectModel>));
            dt.Columns.Add("ClientDnsId", typeof(Int32));
            dt.Columns.Add("Dns", typeof(string));
            dt.Columns.Add("Users", typeof(List<int>));
            dt.Columns.Add("UserId", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["ClientId"] = 1;
            dtrow["ClientName"] = "Forethought";
            dtrow["ClientConnectionStringName"] = 1;
            dtrow["ClientDatabaseName"] = 1;
            dtrow["VerticalMarketId"] = 1;
            dtrow["ClientDescription"] = "Forethought Site";
            dtrow["UtcModifiedDate"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["VerticalMarketName"] = "Test2";
            dtrow["VerticalMarketDatabaseName"] = "DB_Test";
            dtrow["VerticalMarketConnectionStringName"] = "DB_Test";
            dtrow["LastModified"] = DateTime.Today;
            dtrow["UtcLastModified"] = DateTime.Today;
            dtrow["ClientDnsList"] = null;// new ClientDNSObjectModel { ClientDnsId = 1 };
            dtrow["ClientDnsId"] = 1;
            dtrow["Dns"] = "1";
            dtrow["Users"] = objlist;
            dtrow["UserId"] = 1;
            dt.Rows.Add(dtrow);

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

            DataTable EmptyTable = new DataTable();


            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(EmptyTable);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            //Act
            ClientFactoryCache objclientFactoryCache = new ClientFactoryCache(mockDataAccess.Object);
            objclientFactoryCache.ClientName = "Forethought";
            var result = objclientFactoryCache.GetEntityByKey(1);

            //Assert

            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();
        }
        #endregion
    }
}
