using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRD.FSG.RP.Model.Factories;
using System.Web.Script.Serialization;
using Moq;
using System.Data.Common;
namespace RRD.FSG.RP.Model.Test.Factories.System
{
    /// <summary>
    /// Test class for VeticalMarketFactory class
    /// </summary>
    [TestClass]
    public class VerticalMarketFactoryTest : BaseTestFactory<VerticalMarketsObjectModel>
    {
        private Mock<IDataAccess> mockDataAccess;

        [TestInitialize]
        public void TestInitialize()
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

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dSet);
        }
        #endregion

        #region GetAllEntities_Returns_IEnumerable_VerticalMarketsObjectModel
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_VerticalMarketsObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_VerticalMarketsObjectModel()
        {
            //Arrange

            DataTable dt = new DataTable();
            dt.Columns.Add("VerticalMarketId", typeof(Int32));
            dt.Columns.Add("MarketName", typeof(string));
            dt.Columns.Add("ConnectionStringName", typeof(string));
            dt.Columns.Add("DatabaseName", typeof(string));
            dt.Columns.Add("MarketDescription", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("UtcModifiedDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));

            DataRow dtrow = dt.NewRow();
            dtrow["VerticalMarketId"] = 2;
            dtrow["MarketName"] = "US";
            dtrow["ConnectionStringName"] = "USVerticalMarketDBInstance";
            dtrow["DatabaseName"] = "RPV2USDB";
            dtrow["MarketDescription"] = "U.S.A";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["UtcModifiedDate"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;

            dt.Rows.Add(dtrow);

            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);
            //Act
            VerticalMarketsFactoryCache objVerticalMarketsFactoryCache = new VerticalMarketsFactoryCache(mockDataAccess.Object);
            objVerticalMarketsFactoryCache.ClientName = "Forethought";
            objVerticalMarketsFactoryCache.Mode = FactoryCacheMode.All;
            var result = objVerticalMarketsFactoryCache.GetAllEntities(0, 0);


            List<VerticalMarketsObjectModel> lstExpected = new List<VerticalMarketsObjectModel>();
            lstExpected.Add(new VerticalMarketsObjectModel()
            {
                ConnectionStringName = null,
                DatabaseName = null,
                MarketDescription = "U.S.A",
                MarketName = "US",
                VerticalMarketId = 2,
                Name = null,
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            lstExclude.Add("Key");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_IEnumerable_VerticalMarketsObjectModel_WithSortData
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_VerticalMarketsObjectModel_WithSortData
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_VerticalMarketsObjectModel_WithSortData()
        {
            //Arrange

            DataTable dt = new DataTable();
            dt.Columns.Add("VerticalMarketId", typeof(Int32));
            dt.Columns.Add("MarketName", typeof(string));
            dt.Columns.Add("ConnectionStringName", typeof(string));
            dt.Columns.Add("DatabaseName", typeof(string));
            dt.Columns.Add("MarketDescription", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));

            DataRow dtrow = dt.NewRow();
            dtrow["VerticalMarketId"] = 2;
            dtrow["MarketName"] = "US";
            dtrow["ConnectionStringName"] = "USVerticalMarketDBInstance";
            dtrow["DatabaseName"] = "RPV2USDB";
            dtrow["MarketDescription"] = "U.S.A";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;

            dt.Rows.Add(dtrow);

            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);
            //Act
            VerticalMarketsFactory objFactory = new VerticalMarketsFactory(mockDataAccess.Object);
            objFactory.ClientName = "Forethought";
            var result = objFactory.GetAllEntities(0, 0, null);

            List<VerticalMarketsObjectModel> lstExpected = new List<VerticalMarketsObjectModel>();
            lstExpected.Add(new VerticalMarketsObjectModel()
            {
                ConnectionStringName = null,
                DatabaseName = null,
                MarketDescription = "U.S.A",
                MarketName = "US",
                VerticalMarketId = 2,
                Name = null,
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            lstExclude.Add("Key");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region GetAllEntities_Returns_IEnumerable_VerticalMarketsObjectModel_WithSortData_WithNullEntity
        ///// <summary>
        ///// GetAllEntities_Returns_IEnumerable_VerticalMarketsObjectModel_WithSortData
        ///// </summary>
        //[TestMethod]
        //public void GetAllEntities_Returns_IEnumerable_VerticalMarketsObjectModel_WithSortData_WithNullEntity()
        //{
        //    //Arrange

        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("VerticalMarketId", typeof(Int32));
        //    dt.Columns.Add("MarketName", typeof(string));
        //    dt.Columns.Add("ConnectionStringName", typeof(string));
        //    dt.Columns.Add("DatabaseName", typeof(string));
        //    dt.Columns.Add("MarketDescription", typeof(string));
        //    dt.Columns.Add("UtcLastModified", typeof(DateTime));
        //    dt.Columns.Add("ModifiedBy", typeof(int));

        //    DataRow dtrow = dt.NewRow();
        //    //dtrow["VerticalMarketId"] = 2;
        //    //dtrow["MarketName"] = "US";
        //    //dtrow["ConnectionStringName"] = "USVerticalMarketDBInstance";
        //    //dtrow["DatabaseName"] = "RPV2USDB";
        //    //dtrow["MarketDescription"] = "U.S.A";
        //    //dtrow["UtcLastModified"] = DateTime.Now;
        //    //dtrow["ModifiedBy"] = 1;

        //    //dt.Rows.Add(dtrow);

        //    ClientData();

        //    Mock.Arrange(() => mockDataAccess.ExecuteDataTable(string.Empty, string.Empty, null))
        //    .IgnoreArguments()
        //    .Returns(dt)
        //    .MustBeCalled();

        //    //Act
        //    VerticalMarketsFactory objFactory = new VerticalMarketsFactory(mockDataAccess);
        //    objFactory.ClientName = "Forethought";
        //    var result = objFactory.GetAllEntities(0, 0, null);

        //    List<VerticalMarketsObjectModel> lstExpected = new List<VerticalMarketsObjectModel>();
        //    lstExpected.Add(new VerticalMarketsObjectModel()
        //    {
        //        ConnectionStringName = null,
        //        DatabaseName = null,
        //        MarketDescription = "U.S.A",
        //        MarketName = "US",
        //        VerticalMarketId = 2,
        //        Name = null,
        //        Description = null

        //    });

        //    List<string> lstExclude = new List<string>();
        //    lstExclude.Add("ModifiedBy");
        //    lstExclude.Add("ModifiedDate");
        //    lstExclude.Add("LastModified");
        //    lstExclude.Add("Key");
        //    //ValidateListData(lstExpected, result.ToList(), lstExclude);
        //    Assert.AreEqual(result, 0);

        //    //Assert
        //    Mock.Assert(mockDataAccess);
        //}
        //#endregion

        //#region SaveEntity_With_Entity_VerticalMarketsFactory
        ///// <summary>
        ///// SaveEntity_With_Entity_VerticalMarketsFactory
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_With_Entity_VerticalMarketsFactory()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    ClientData();

        //    //Act
        //    VerticalMarketsFactory objVerticalMarketsFactory = new VerticalMarketsFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objVerticalMarketsFactory.SaveEntity(null);
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

        //#region DeleteEntity_With_Entity_VerticalMarketsFactory
        ///// <summary>
        ///// SaveEntity_With_Entity_VerticalMarketsFactory
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Entity_VerticalMarketsFactory()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    ClientData();

        //    //Act
        //    VerticalMarketsFactory objVerticalMarketsFactory = new VerticalMarketsFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objVerticalMarketsFactory.DeleteEntity(0, 0);
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
        //#region DeleteEntity_With_Entity_VerticalMarketsFactory_Oneparameter
        ///// <summary>
        ///// SaveEntity_With_Entity_VerticalMarketsFactory
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Entity_VerticalMarketsFactory_Oneparameter()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    ClientData();
        //    VerticalMarketsObjectModel objVerticalMarketsObjectModel = new VerticalMarketsObjectModel();
        //    //Act
        //    VerticalMarketsFactory objVerticalMarketsFactory = new VerticalMarketsFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objVerticalMarketsFactory.DeleteEntity(objVerticalMarketsObjectModel);
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
        //#region DeleteEntity_With_Entity_VerticalMarketsFactory_twoparameter
        ///// <summary>
        ///// SaveEntity_With_Entity_VerticalMarketsFactory
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Entity_VerticalMarketsFactory_twoparameter()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    ClientData();
        //    VerticalMarketsObjectModel objVerticalMarketsObjectModel = new VerticalMarketsObjectModel();
        //    //Act
        //    VerticalMarketsFactory objVerticalMarketsFactory = new VerticalMarketsFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objVerticalMarketsFactory.DeleteEntity(objVerticalMarketsObjectModel, 0);
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

        //#region SaveEntity_With_EntityModBy_VerticalMarketsFactory
        ///// <summary>
        ///// SaveEntity_With_EntityModBy_VerticalMarketsFactory
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_With_EntityModBy_VerticalMarketsFactory()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    ClientData();

        //    //Act
        //    VerticalMarketsFactory objVerticalMarketsFactory = new VerticalMarketsFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objVerticalMarketsFactory.SaveEntity(null, 1);
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

        //#region DeleteEntity_With_Key_VerticalMarketsFactory
        ///// <summary>
        ///// DeleteEntity_With_Key_VerticalMarketsFactory
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Key_VerticalMarketsFactory()
        //{
        //    //Arrange
        //    ClientData();
        //    Exception exe = null;
        //    int VerticalMarketId = 1;

        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));



        //    //Act
        //    VerticalMarketsFactoryCache objVerticalMarketsFactoryCache = new VerticalMarketsFactoryCache(mockDataAccess.Object);
        //    objVerticalMarketsFactoryCache.ClientName = "Forethought";
        //    objVerticalMarketsFactoryCache.Mode = FactoryCacheMode.All;
        //    try
        //    {
        //        objVerticalMarketsFactoryCache.DeleteEntity(VerticalMarketId);
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

        //#region GetEntityByKey_Returns_VerticalMarketsObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_VerticalMarketsObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_VerticalMarketsObjectModel()
        //{
        //    //Arrange
        //    ClientData();
        //    Exception exe = null;
        //    int key = 1;

        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));

        //    //Act
        //    VerticalMarketsFactory objFactory = new VerticalMarketsFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.GetEntityByKey(key);
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


        //#region GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity_VerticalMarketsFactory
        ///// <summary>
        ///// GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity_VerticalMarketsFactory
        ///// </summary>
        //[TestMethod]
        //public void GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity_VerticalMarketsFactory()
        //{
        //    //Arrange  
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    VerticalMarketsFactory objVerticalMarketsFactory = new VerticalMarketsFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objVerticalMarketsFactory.GetEntitiesBySearch(0, 0, null, null, null);
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
            VerticalMarketsFactory objFactory = new VerticalMarketsFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<VerticalMarketsObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion
    }
}
