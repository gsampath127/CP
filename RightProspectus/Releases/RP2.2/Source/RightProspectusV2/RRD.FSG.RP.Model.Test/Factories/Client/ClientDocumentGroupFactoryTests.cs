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
using System.Reflection;
using RRD.FSG.RP.Model.Factories;
using System.Web.Script.Serialization;
using Moq;


namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for ClientDocumentGroupFactory class
    /// </summary>
    [TestClass]
    public class ClientDocumentGroupFactoryTests : BaseTestFactory<ClientDocumentGroupObjectModel>
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

        #region GetAllEntities_Returns_Ienumerable_ClientDocumentGroupObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_ClientDocumentGroupObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_ClientDocumentGroupObjectModel()
        {
            //Arrange

            ClientDocumentGroupSortDetail objSortDtl = new ClientDocumentGroupSortDetail();
            objSortDtl.Column = ClientDocumentGroupSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("ClientDocumentGroupId", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("ParentClientDocumentGroupId", typeof(Int32));
            dt.Columns.Add("CssClass", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("ClientDocumentId", typeof(Int32));
            dt.Columns.Add("Order", typeof(Int32));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("MimeType", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["ClientDocumentGroupId"] = 3;
            dtrow["Name"] = "N";
            dtrow["Description"] = "TEST";
            dtrow["ParentClientDocumentGroupId"] = 1;
            dtrow["CssClass"] = "C";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["ClientDocumentId"] = 3;
            dtrow["Order"] = 3;
            dtrow["FileName"] = "lESSS.txt";
            dtrow["MimeType"] = "text/plain";
            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            ClientDocumentGroupFactory objPageFeatureFactoryCache = new ClientDocumentGroupFactory(mockDataAccess.Object);
            var result = objPageFeatureFactoryCache.GetAllEntities(0, 0, objSortDtl);

            //Assert
            List<ClientDocumentGroupObjectModel> lstExpected = new List<ClientDocumentGroupObjectModel>();
            ClientDocumentGroupObjectModel obj = new ClientDocumentGroupObjectModel()
            {
                ParentClientDocumentGroupId = 1,
                CssClass = "C",
                ClientDocuments = null,
                ClientDocumentGroupId = 3,
                Description = "TEST",
                Name = "N",
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

        #region GetAllEntities_Returns_Ienumerable
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable()
        {
            //Arrange
            ClientDocumentGroupSortDetail objSortDtl = new ClientDocumentGroupSortDetail();
            objSortDtl.Column = ClientDocumentGroupSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("ClientDocumentGroupId", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("ParentClientDocumentGroupId", typeof(Int32));
            dt.Columns.Add("CssClass", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("ClientDocumentId", typeof(Int32));
            dt.Columns.Add("Order", typeof(Int32));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("MimeType", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["ClientDocumentGroupId"] = 3;
            dtrow["Name"] = "N";
            dtrow["Description"] = "TEST";
            dtrow["ParentClientDocumentGroupId"] = 1;
            dtrow["CssClass"] = "C";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["ClientDocumentId"] = 3;
            dtrow["Order"] = 3;
            dtrow["FileName"] = "lESSS.txt";
            dtrow["MimeType"] = "text/plain";
            dt.Rows.Add(dtrow);

            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);
            //Act
            ClientDocumentGroupFactoryCache objPageFeatureFactoryCache = new ClientDocumentGroupFactoryCache(mockDataAccess.Object);
            objPageFeatureFactoryCache.ClientName = "Forethought";
            objPageFeatureFactoryCache.Mode = FactoryCacheMode.All;
            var result = objPageFeatureFactoryCache.GetAllEntities(0, 0, objSortDtl);

            //Assert

            List<ClientDocumentObjectModel> lstExpectedClient = new List<ClientDocumentObjectModel>();
            ClientDocumentObjectModel objClientDocumentObjectModel = new ClientDocumentObjectModel()
            {
                ClientDocumentId = 3,
                ClientDocumentTypeId = 0,
                ClientDocumentTypeName = null,
                ContentUri = null,
                FileData = null,
                FileName = "lESSS.txt",
                IsPrivate = false,
                MimeType = "text/plain",
                Order = 0
            };
            List<string> lstExcludeClient = new List<string>
            {
                "ModifiedBy",
                "LastModified",
                "Key"
            };
            lstExpectedClient.Add(objClientDocumentObjectModel);

            List<ClientDocumentGroupObjectModel> lstExpected = new List<ClientDocumentGroupObjectModel>();
            ClientDocumentGroupObjectModel obj = new ClientDocumentGroupObjectModel()
            {
                ParentClientDocumentGroupId = 1,
                CssClass = "C",
                ClientDocumentGroupId = 3,
                Description = "Test",
                Name = "N",
                ClientDocuments = lstExpectedClient

            };
            List<string> lstExclude = new List<string>
            {
                "ModifiedBy",
                "LastModified",
                "Key"
            };
            lstExpected.Add(obj);
            ValidateObjectModelData(result, lstExpected);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_NUll
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_NUll
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_NUll()
        {
            //Arrange
            ClientDocumentGroupSortDetail objSortDtl = new ClientDocumentGroupSortDetail();
            objSortDtl.Column = ClientDocumentGroupSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();

            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);
            //Act
            ClientDocumentGroupFactoryCache objPageFeatureFactoryCache = new ClientDocumentGroupFactoryCache(mockDataAccess.Object);
            objPageFeatureFactoryCache.ClientName = "Forethought";
            objPageFeatureFactoryCache.Mode = FactoryCacheMode.All;
            var result = objPageFeatureFactoryCache.GetAllEntities(0, 0, objSortDtl);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_ClientDocumentID_Null
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_ClientDocumentID_Null
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_ClientDocumentID_Null()
        {
            //Arrange

            ClientDocumentGroupSortDetail objSortDtl = new ClientDocumentGroupSortDetail();
            objSortDtl.Column = ClientDocumentGroupSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("ClientDocumentGroupId", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("ParentClientDocumentGroupId", typeof(Int32));
            dt.Columns.Add("CssClass", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("ClientDocumentId", typeof(Int32));
            dt.Columns.Add("Order", typeof(Int32));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("MimeType", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["ClientDocumentGroupId"] = 3;
            dtrow["Name"] = "N";
            dtrow["Description"] = "TEST";
            dtrow["ParentClientDocumentGroupId"] = 1;
            dtrow["CssClass"] = "C";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["ClientDocumentId"] = DBNull.Value;
            dtrow["Order"] = 3;
            dtrow["FileName"] = "lESSS.txt";
            dtrow["MimeType"] = "text/plain";
            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            ClientDocumentGroupFactory objPageFeatureFactoryCache = new ClientDocumentGroupFactory(mockDataAccess.Object);
            var result = objPageFeatureFactoryCache.GetAllEntities<ClientDocumentGroupObjectModel>();

            //Assert
            List<ClientDocumentObjectModel> lstExpectedClient = new List<ClientDocumentObjectModel>();
            ClientDocumentObjectModel objClientDocumentObjectModel = new ClientDocumentObjectModel();
            lstExpectedClient.Add(objClientDocumentObjectModel);
           
            ClientDocumentGroupObjectModel lstExpected = new ClientDocumentGroupObjectModel()
            {
                ParentClientDocumentGroupId = 1,
                CssClass = "C",
                ClientDocuments = lstExpectedClient,
                ClientDocumentGroupId = 3,
                Description = "TEST",
                Name = "N",
            };
            ValidateObjectModelData(result.ToList()[0], lstExpected);

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
            List<ClientDocumentObjectModel> lst = new List<ClientDocumentObjectModel>();
            ClientDocumentObjectModel obj = new ClientDocumentObjectModel();
            obj.ClientDocumentId = 1;
            lst.Add(obj);
            ClientDocumentGroupObjectModel objObjectModel = new ClientDocumentGroupObjectModel()
            {
                ClientDocumentGroupId = 2,
                Name = "Test_001",
                Description = "Test",
                ParentClientDocumentGroupId = 1,
                CssClass = "TEST_001",
                ClientDocuments = lst

            };

            var parameters = new[]
              { 
                new SqlParameter(){ ParameterName="ClientDocumentGroupId", Value=2 },
                new SqlParameter(){ ParameterName="Name", Value="TEST_001" },
                new SqlParameter(){ ParameterName="Description", Value= "TEST" },
                new SqlParameter(){ ParameterName="ParentClientDocumentGroupId", Value=1 },
                 new SqlParameter(){ ParameterName="CssClass", Value="TEST_001" },
                 new SqlParameter(){ParameterName="ModifiedBy", Value=12},
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4])
            .Returns(parameters[5]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ClientDocumentGroupFactory objClientDocumentGroupFactoryCache = new ClientDocumentGroupFactory(mockDataAccess.Object);
            objClientDocumentGroupFactoryCache.SaveEntity(objObjectModel, 1);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region SaveEntity_Null
        /// <summary>
        /// SaveEntity_Null
        /// </summary>
        [TestMethod]
        public void SaveEntity_Null()
        {
            //Arrange
            List<ClientDocumentObjectModel> lst = new List<ClientDocumentObjectModel>();
            ClientDocumentObjectModel obj = new ClientDocumentObjectModel();
            lst.Add(obj);
            ClientDocumentGroupObjectModel objObjectModel = new ClientDocumentGroupObjectModel()
            {
                ClientDocumentGroupId = 2,
                Name = "Test_001",
                Description = "Test",
                ParentClientDocumentGroupId = 1,
                CssClass = "TEST_001",

            };

            var parameters = new[]
              { 
                new SqlParameter(){ ParameterName="ClientDocumentGroupId", Value=2 },
                new SqlParameter(){ ParameterName="Name", Value="TEST_001" },
                new SqlParameter(){ ParameterName="Description", Value= "TEST" },
                new SqlParameter(){ ParameterName="ParentClientDocumentGroupId", Value=1 },
                 new SqlParameter(){ ParameterName="CssClass", Value="TEST_001" }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ClientDocumentGroupFactory objClientDocumentGroupFactoryCache = new ClientDocumentGroupFactory(mockDataAccess.Object);
            objClientDocumentGroupFactoryCache.SaveEntity(objObjectModel, 1);

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
            ClientDocumentGroupObjectModel obj = new ClientDocumentGroupObjectModel()
            {
                ClientDocumentGroupId = 1
            };
            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ClientDocumentGroupId", Value=1 },
                new SqlParameter(){ ParameterName="DeletedBy", Value=8},

            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);
            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ClientDocumentGroupFactory objClientDocumentGroupFactory = new ClientDocumentGroupFactory(mockDataAccess.Object);
            objClientDocumentGroupFactory.DeleteEntity(1);

            //Assert
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region GetEntityByKey_Returns_ClientDocumentGroupObjectModel
        /// <summary>
        /// GetEntityByKey_Returns_ClientDocumentGroupObjectModel
        /// </summary>
        [TestMethod]
        public void GetEntityByKey_Returns_ClientDocumentGroupObjectModel()
        {
            //Arrange
            ClientData();
            Exception exe = null; ;

            //Act
            ClientDocumentGroupFactory objClientDocumentGroupFactory = new ClientDocumentGroupFactory(mockDataAccess.Object);
            try
            {
                objClientDocumentGroupFactory.GetEntityByKey(1);
            }
            catch (Exception e)
            {
                if (e is NotImplementedException)
                    exe = e;
            }

            //Assert
            Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        }
        #endregion

        #region GetEntitiesBySearch_Returns_IEnumerable_Entity
        /// <summary>
        /// GetEntitiesBySearch_Returns_IEnumerable_Entity
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_IEnumerable_Entity()
        {
            //Arrange  
            ClientDocumentGroupSearchDetail objSearchDtl = new ClientDocumentGroupSearchDetail();
            ClientDocumentGroupSortDetail objSortDtl = new ClientDocumentGroupSortDetail();

            ClientData();
            Exception exe = null; ;

            //Act
            ClientDocumentGroupFactory objClientDocumentGroupFactory = new ClientDocumentGroupFactory(mockDataAccess.Object);
            try
            {
                objClientDocumentGroupFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, null);
            }
            catch (Exception e)
            {
                if (e is NotImplementedException)
                    exe = e;
            }

            //Assert
            Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
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
            ClientDocumentGroupFactory objClientDocumentGroupFactory = new ClientDocumentGroupFactory(mockDataAccess.Object);
            var result = objClientDocumentGroupFactory.CreateEntity<ClientDocumentGroupObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

    }
}
