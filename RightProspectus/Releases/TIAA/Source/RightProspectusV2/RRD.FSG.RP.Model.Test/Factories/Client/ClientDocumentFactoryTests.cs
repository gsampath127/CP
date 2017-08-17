using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRD.FSG.RP.Model.Factories;
using System.Web.Script.Serialization;
using Moq;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for ClientDocumentFactory class
    /// </summary>
    [TestClass]
    public class ClientDocumentFactoryTests : BaseTestFactory<ClientDocumentObjectModel>
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

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dSet);

        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_ClientDocumentObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_ClientDocumentObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_ClientDocumentObjectModel()
        {
            //Arrange
            ClientDocumentSortDetail objSortDtl = new ClientDocumentSortDetail();
            objSortDtl.Column = ClientDocumentSortColumn.FileName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("ClientDocumentId", typeof(Int32));
            dt.Columns.Add("ClientDocumentTypeId", typeof(Int32));
            dt.Columns.Add("ClientDocumentTypeName", typeof(string));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("MimeType", typeof(string));
            dt.Columns.Add("IsPrivate", typeof(bool));
            dt.Columns.Add("ContentUri", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["ClientDocumentId"] = 3;
            dtrow["ClientDocumentTypeId"] = 5;
            dtrow["ClientDocumentTypeName"] = "xyz name";
            dtrow["FileName"] = "lESSS.txt";
            dtrow["MimeType"] = "text/plain";
            dtrow["IsPrivate"] = 0;
            dtrow["ContentUri"] = null;
            dtrow["Name"] = "NU";
            dtrow["Description"] = "DU";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
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

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dSet);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);


            //Act
            ClientDocumentFactoryCache objClientDocumentFactoryCache = new ClientDocumentFactoryCache(mockDataAccess.Object);
            objClientDocumentFactoryCache.ClientName = "Forethought";
            objClientDocumentFactoryCache.Mode = FactoryCacheMode.All;
            var result = objClientDocumentFactoryCache.GetAllEntities<ClientDocumentObjectModel>(0, 0, objSortDtl);

            //Assert

            List<ClientDocumentObjectModel> lstExpected = new List<ClientDocumentObjectModel>();
            ClientDocumentObjectModel obj = new ClientDocumentObjectModel()
               {
                   ClientDocumentId = 3,
                   ClientDocumentTypeId = 5,
                   ClientDocumentTypeName = "xyz name",
                   IsPrivate = false,
                   ContentUri = null,
                   FileData = null,
                   FileName = "lESSS.txt",
                   MimeType = "text/plain",
                   Order = 0,
                   Description = "DU",
                   Name = "NU"


               };
            lstExpected.Add(obj);

            List<string> lstExclude = new List<string>
            {
                "ModifiedBy",
                "LastModified",
                "Key"
            };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Calls_Factory
        /// <summary>
        /// GetAllEntities_Calls_Factory
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Calls_Factory()
        {
            //Arrange
            ClientDocumentSortDetail objSortDtl = new ClientDocumentSortDetail();
            objSortDtl.Column = ClientDocumentSortColumn.FileName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("ClientDocumentId", typeof(Int32));
            dt.Columns.Add("ClientDocumentTypeId", typeof(Int32));
            dt.Columns.Add("ClientDocumentTypeName", typeof(string));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("MimeType", typeof(string));
            dt.Columns.Add("IsPrivate", typeof(bool));
            dt.Columns.Add("ContentUri", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["ClientDocumentId"] = 3;
            dtrow["ClientDocumentTypeId"] = 5;
            dtrow["ClientDocumentTypeName"] = "xyz name";
            dtrow["FileName"] = "lESSS.txt";
            dtrow["MimeType"] = "text/plain";
            dtrow["IsPrivate"] = 0;
            dtrow["ContentUri"] = null;
            dtrow["Name"] = "NU";
            dtrow["Description"] = "DU";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);

            //Act
            ClientDocumentFactory objClientDocumentFactory = new ClientDocumentFactory(mockDataAccess.Object);
            var result = objClientDocumentFactory.GetAllEntities(0, 0, objSortDtl);

            //Assert
            List<ClientDocumentObjectModel> lstExpected = new List<ClientDocumentObjectModel>();
            ClientDocumentObjectModel obj = new ClientDocumentObjectModel()
            {
                ClientDocumentId = 3,
                ClientDocumentTypeId = 5,
                ClientDocumentTypeName = "xyz name",
                IsPrivate = false,
                ContentUri = null,
                FileData = null,
                FileName = "lESSS.txt",
                MimeType = "text/plain",
                Order = 0,
                Description = "DU",
                Name = "NU"


            };
            lstExpected.Add(obj);

            List<string> lstExclude = new List<string>
            {
                "ModifiedBy",
                "LastModified",
                "Key"
            };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region SaveEntity_WithModifiedBy
        /// <summary>
        /// SaveEntity_WithModifiedBy
        /// </summary>
        [TestMethod]
        public void SaveEntity_WithModifiedBy()
        {
            //Arrange
            ClientDocumentObjectModel objObjectModel = new ClientDocumentObjectModel()
            {
                ClientDocumentId = 2,
                Name = "Test_001",
                Description = "TEST",
                IsPrivate = false,
                ContentUri = "TEST_001",
                ClientDocumentTypeId = 1,
                MimeType = "Test",
                FileName = "Test"
            };

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="ClientDocumentId", Value=1 },
                new SqlParameter(){ ParameterName="Name", Value="TEST_001" },
                new SqlParameter(){ ParameterName="Description", Value= "TEST" },
                 new SqlParameter(){ ParameterName="IsPrivate", Value=false },
                new SqlParameter(){ ParameterName="ContentUri", Value="TEST_001" },
                new SqlParameter(){ ParameterName="MimeType", Value="Test" },
                 new SqlParameter(){ ParameterName="FileName", Value="Test" }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4])
                .Returns(parameters[5])
                .Returns(parameters[6])
                .Returns(parameters[7]);


            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ClientDocumentFactory objClientDocumentFactory = new ClientDocumentFactory(mockDataAccess.Object);
            objClientDocumentFactory.SaveEntity(objObjectModel, 1);

            //Assert and Validate 

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
            ClientDocumentObjectModel obj = new ClientDocumentObjectModel()
            {
                ClientDocumentId = 1,
              
            };
            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ClientDocumentId", Value=1 },
                new SqlParameter(){ ParameterName="DeletedBy", Value=34 },
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);


            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ClientDocumentFactory objClientDocumentFactory = new ClientDocumentFactory(mockDataAccess.Object);
            objClientDocumentFactory.DeleteEntity(1);

            //Assert  
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region CreateEntities_NullValue
        /// <summary>
        /// CreateEntities_NullValue
        /// </summary>
        [TestMethod]
        public void CreateEntities_NullValue()
        {
            //Arrange
            DataRow dr = null;

            //Act
            ClientDocumentFactory objFactory = new ClientDocumentFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<ClientDocumentObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion
    }
}
