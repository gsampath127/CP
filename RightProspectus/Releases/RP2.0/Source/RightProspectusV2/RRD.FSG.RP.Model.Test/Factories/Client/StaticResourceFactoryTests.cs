using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for StaticResourceFactory class
    /// </summary>
    [TestClass]
    public class StaticResourceFactoryTests : BaseTestFactory<StaticResourceObjectModel>
    {
        private Mock<IDataAccess> mockDataAccess;

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

            mockDataAccess.Setup(X => X.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
            .Returns(dSet);
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_StaticResourceObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_StaticResourceObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_StaticResourceObjectModel()
        {
            //Arrange
            StaticResourceSortDetail objSortDtl = new StaticResourceSortDetail();
            objSortDtl.Column = StaticResourceSortColumn.FileName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("StaticResourceId", typeof(Int32));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("Size", typeof(Int32));
            dt.Columns.Add("MimeType", typeof(string));
            dt.Columns.Add("Data", typeof(byte[]));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            var dataBinary = new byte[] { 0x20 };
            DataRow dtrow = dt.NewRow();
            dtrow["StaticResourceId"] = 1;
            dtrow["FileName"] = "forethought_logo.jpg";
            dtrow["Size"] = 114453;
            dtrow["MimeType"] = "image/jpg";
            dtrow["Data"] = dataBinary;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;

            dt.Rows.Add(dtrow);

            ClientData();
            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
           .Returns(dt);

            //Act
            StaticResourceFactoryCache objStaticResourceFactoryCache = new StaticResourceFactoryCache(mockDataAccess.Object);
            objStaticResourceFactoryCache.ClientName = "Forethought";
            objStaticResourceFactoryCache.Mode = FactoryCacheMode.All;
            var result = objStaticResourceFactoryCache.GetAllEntities(0, 0, objSortDtl);

            //Assert
            mockDataAccess.VerifyAll();
            List<StaticResourceObjectModel> lstExpected = new List<StaticResourceObjectModel>();
            lstExpected.Add(new StaticResourceObjectModel()
            {
                StaticResourceId = 1,
                FileName = "forethought_logo.jpg",
                Size = 114453,
                MimeType = "image/jpg",
                Data = dataBinary
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
           
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
            StaticResourceSortDetail objSortDtl = new StaticResourceSortDetail();
            objSortDtl.Column = StaticResourceSortColumn.FileName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("StaticResourceId", typeof(Int32));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("Size", typeof(Int32));
            dt.Columns.Add("MimeType", typeof(string));
            dt.Columns.Add("Data", typeof(byte[]));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            var dataBinary = new byte[] { 0x20 };

            DataRow dtrow = dt.NewRow();
            dtrow["StaticResourceId"] = 1;
            dtrow["FileName"] = "forethought_logo.jpg";
            dtrow["Size"] = 114453;
            dtrow["MimeType"] = "image/jpg";
            dtrow["Data"] = dataBinary;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;

            dt.Rows.Add(dtrow);

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
             .Returns(dt);

            //Act
            StaticResourceFactory objStaticResourceFactory = new StaticResourceFactory(mockDataAccess.Object);
            var result = objStaticResourceFactory.GetAllEntities(0, 0, objSortDtl);

            //Assert
            mockDataAccess.VerifyAll();
            List<StaticResourceObjectModel> lstExpected = new List<StaticResourceObjectModel>();
            lstExpected.Add(new StaticResourceObjectModel()
            {
                StaticResourceId = 1,
                FileName = "forethought_logo.jpg",
                Size = 114453,
                MimeType = "image/jpg",
                Data = dataBinary
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
          
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
            StaticResourceFactory objFactory = new StaticResourceFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<StaticResourceObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
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
            string str = "test";
            StaticResourceObjectModel objObjectModel = new StaticResourceObjectModel();
            objObjectModel.StaticResourceId = 100;
            objObjectModel.FileName = "Test_001";
            objObjectModel.Size = 10;
            objObjectModel.MimeType = "image/jpg";
            objObjectModel.Data =  Encoding.UTF8.GetBytes(str);
            var dataBinary = Encoding.UTF8.GetBytes(str);

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="StaticResourceId", Value=100 },
                new SqlParameter(){ ParameterName="FileName", Value="Test_001" },
                new SqlParameter(){ ParameterName="Size", Value=10 },
                 new SqlParameter(){ ParameterName="MimeType", Value="image/jpg" },
                new SqlParameter(){ ParameterName="Data", Value=dataBinary }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4])

            //mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<SqlDbType>(), It.IsAny<object>()))
              .Returns(parameters[5]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            StaticResourceFactory objStaticResourceFactory = new StaticResourceFactory(mockDataAccess.Object);
            objStaticResourceFactory.SaveEntity(objObjectModel, 1);

            //Assert
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
            
            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                new SqlParameter(){ ParameterName="StaticResourceId", Value=1 }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            StaticResourceFactory objStaticResourceFactory = new StaticResourceFactory(mockDataAccess.Object);
            objStaticResourceFactory.DeleteEntity(1);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region DeleteEntity
        ///// <summary>
        ///// DeleteEntity
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity()
        //{
        //    //Arrange
        //    StaticResourceObjectModel entity = new StaticResourceObjectModel();
        //    Exception exe = null;
        //    ClientData();

        //    Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //        .IgnoreArguments()
        //        .MustBeCalled();

        //    StaticResourceFactoryCache objStaticResourceFactoryCache = new StaticResourceFactoryCache(mockDataAccess);
        //    objStaticResourceFactoryCache.ClientName = "Forethought";
        //    objStaticResourceFactoryCache.Mode = FactoryCacheMode.All;

        //    //Act
        //    try
        //    {
        //        objStaticResourceFactoryCache.DeleteEntity(entity);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion

        //#region DeleteEntity_WithDeletedBy
        ///// <summary>
        ///// DeleteEntity_WithDeletedBy
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_WithDeletedBy()
        //{
        //    //Arrange
        //    StaticResourceObjectModel entity = new StaticResourceObjectModel();
        //    Exception exe = null;
        //    ClientData();

        //    Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //        .IgnoreArguments()
        //        .MustBeCalled();

        //    StaticResourceFactoryCache objStaticResourceFactoryCache = new StaticResourceFactoryCache(mockDataAccess);
        //    objStaticResourceFactoryCache.ClientName = "Forethought";
        //    objStaticResourceFactoryCache.Mode = FactoryCacheMode.All;

        //    //Act
        //    try
        //    {
        //        objStaticResourceFactoryCache.DeleteEntity(entity,1);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion        

        //#region GetStaticResourceEntity_Returns_StaticResourceObjectModel
        ///// <summary>
        ///// GetStaticResourceEntity_Returns_StaticResourceObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetStaticResourceEntity_Returns_StaticResourceObjectModel()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    ClientData();

        //    //Act
        //    StaticResourceFactory objStaticResourceFactory = new StaticResourceFactory(mockDataAccess);
        //    try
        //    {
        //        objStaticResourceFactory.GetStaticResourceEntity(1);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion

        //#region GetEntitiesBySearch_Returns_IEnumerable_Entity
        ///// <summary>
        ///// GetEntitiesBySearch_Returns_IEnumerable_Entity
        ///// </summary>
        //[TestMethod]
        //public void GetEntitiesBySearch_Returns_IEnumerable_Entity()
        //{
        //    //Arrange  
        //    StaticResourceSearchDetail objSearchDtl = new StaticResourceSearchDetail();
        //    StaticResourceSortDetail objSortDtl = new StaticResourceSortDetail();

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    StaticResourceFactory objStaticResourceFactory = new StaticResourceFactory(mockDataAccess);
        //    try
        //    {
        //        objStaticResourceFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, null);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion

        //#region GetEntityByKey_Returns_StaticResourceObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_StaticResourceObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_StaticResourceObjectModel()
        //{
        //    //Arrange
        //    Exception exe = null;

        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("StaticResourceId", typeof(Int32));
        //    dt.Columns.Add("FileName", typeof(string));
        //    dt.Columns.Add("Size", typeof(Int32));
        //    dt.Columns.Add("MimeType", typeof(string));
        //    dt.Columns.Add("Data", typeof(SqlDbType));
        //    dt.Columns.Add("UtcLastModified", typeof(DateTime));
        //    dt.Columns.Add("ModifiedBy", typeof(Int32));

        //    DataRow dtrow = dt.NewRow();
        //    dtrow["StaticResourceId"] = 1;
        //    dtrow["FileName"] = "forethought_logo.jpg";
        //    dtrow["Size"] = 114453;
        //    dtrow["MimeType"] = "image/jpg";
        //    dtrow["Data"] = DBNull.Value;
        //    dtrow["UtcLastModified"] = DateTime.Now;
        //    dtrow["ModifiedBy"] = 1;

        //    dt.Rows.Add(dtrow);

        //    ClientData();

        //    Mock.Arrange(() => mockDataAccess.ExecuteDataTable(string.Empty, string.Empty, null))
        //   .IgnoreArguments()
        //   .Returns(dt)
        //   .MustBeCalled();

        //    //Act
        //    StaticResourceFactory objPageTextFactory = new StaticResourceFactory(mockDataAccess);
        //    try
        //    {
        //        objPageTextFactory.GetEntityByKey(1);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe,typeof(NotImplementedException));
        //}
        //#endregion
    }
}
