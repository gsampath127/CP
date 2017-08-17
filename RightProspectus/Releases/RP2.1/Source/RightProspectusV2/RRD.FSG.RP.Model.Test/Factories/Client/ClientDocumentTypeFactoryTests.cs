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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Common;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for ClientDocumentGroupFactory class
    /// </summary>
    [TestClass]
    public class ClientDocumentTypeFactoryTests : BaseTestFactory<ClientDocumentTypeObjectModel>
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


        #region GetAllEntities_Returns_Ienumerable_ClientDocumentTypeObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_ClientDocumentTypeObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_ClientDocumentTypeObjectModel()
        {
            //Arrange
            ClientDocumentTypeSortDetail objSortDtl = new ClientDocumentTypeSortDetail();
            objSortDtl.Column = ClientDocumentTypeSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("ClientDocumentTypeId", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["ClientDocumentTypeId"] = 0;
            dtrow["Name"] = "2";
            dtrow["Description"] = "1";
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

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);


            //Act


            ClientDocumentTypeFactoryCache objFactoryCache = new ClientDocumentTypeFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;
            var result = objFactoryCache.GetAllEntities(0, 0, objSortDtl);



            //Verify 

            List<ClientDocumentTypeObjectModel> lstExpected = new List<ClientDocumentTypeObjectModel>();
            lstExpected.Add(new ClientDocumentTypeObjectModel()
            {
                ClientDocumentTypeId = 0,
                Name = "2",
                Description = "1",

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
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
            ClientDocumentTypeSortDetail objSortDtl = new ClientDocumentTypeSortDetail();
            objSortDtl.Column = ClientDocumentTypeSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("ClientDocumentTypeId", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["ClientDocumentTypeId"] = 0;
            dtrow["Name"] = "2";
            dtrow["Description"] = "1";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                 .Returns(dt);

            //Act
            ClientDocumentTypeFactory objClientDocumentTypeFactory = new ClientDocumentTypeFactory(mockDataAccess.Object);
            var result = objClientDocumentTypeFactory.GetAllEntities(0, 0, objSortDtl);


            //Verify 

            List<ClientDocumentTypeObjectModel> lstExpected = new List<ClientDocumentTypeObjectModel>();
            lstExpected.Add(new ClientDocumentTypeObjectModel()
            {
                ClientDocumentTypeId = 0,
                Name = "2",
                Description = "1",

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);


            //Assert
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
            ClientDocumentTypeObjectModel objObjectModel = new ClientDocumentTypeObjectModel();
            objObjectModel.ClientDocumentTypeId = 1;
            objObjectModel.Name = "Test";
            objObjectModel.Description = "Test";

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="ClientDocumentTypeId", Value=1 },
                new SqlParameter(){ ParameterName="Name", Value="TEST_001" },
                new SqlParameter(){ ParameterName="Description", Value="TEST_002" }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
               .Returns(parameters[0])
               .Returns(parameters[1])
               .Returns(parameters[2])
               .Returns(parameters[3]);
            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));
            //Act
            ClientDocumentTypeFactory objFactory = new ClientDocumentTypeFactory(mockDataAccess.Object);
            objFactory.SaveEntity(objObjectModel, 1);

            //Verify And Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region DeleteEntity_With_Key
        /// <summary>
        /// DeleteEntity_With_Key
        /// </summary>
        [TestMethod]
        public void DeleteEntity_With_Key()
        {
            //Arrange
            ClientDocumentTypeObjectModel obj = new ClientDocumentTypeObjectModel()
            {
                ClientDocumentTypeId = 53
            };

            var parameters = new[]
            { 
                
                new SqlParameter(){ ParameterName="ClientDocumentTypeId", Value=53 },
                new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
              
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);


            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));


            //Act
            ClientDocumentTypeFactoryCache objFactoryCache = new ClientDocumentTypeFactoryCache(mockDataAccess.Object);
            objFactoryCache.DeleteEntity(1, 1);

            //Verify 
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
            ClientDocumentTypeFactory objFactory = new ClientDocumentTypeFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<ClientDocumentTypeObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion









    }
}
