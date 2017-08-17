using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Keys;
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
    /// Test class for SiteFeatureFeatureFactory class
    /// </summary>
    [TestClass]
    public class SiteFeatureFactoryTests : BaseTestFactory<SiteFeatureObjectModel>
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

            mockDataAccess.Setup(X => X.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_SiteFeatureObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_SiteFeatureObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_SiteFeatureObjectModel()
        {
            //Arrange
            SiteFeatureSortDetail objSortDtl = new SiteFeatureSortDetail();
            objSortDtl.Column = SiteFeatureSortColumn.FeatureMode;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("SiteKey", typeof(string));
            dt.Columns.Add("FeatureMode", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteId"] = 1;
            dtrow["SiteKey"] = "RequestMaterial";
            dtrow["FeatureMode"] = 1;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dt.Rows.Add(dtrow);

            ClientData();
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dt);

            //Act
            SiteFeatureFactoryCache objSiteFeatureFactoryCache = new SiteFeatureFactoryCache(mockDataAccess.Object);
            objSiteFeatureFactoryCache.ClientName = "Forethought";
            objSiteFeatureFactoryCache.Mode = FactoryCacheMode.All;
            var result = objSiteFeatureFactoryCache.GetAllEntities(0, 0, objSortDtl);

            List<SiteFeatureObjectModel> lstExpected = new List<SiteFeatureObjectModel>();
            lstExpected.Add(new SiteFeatureObjectModel()
            {
                SiteId = 1,
                SiteKey = "RequestMaterial",
                FeatureMode = 1
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "LastModified" };
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
            SiteFeatureSortDetail objSortDtl = new SiteFeatureSortDetail();
            objSortDtl.Column = SiteFeatureSortColumn.FeatureMode;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("SiteKey", typeof(string));
            dt.Columns.Add("FeatureMode", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteId"] = 1;
            dtrow["SiteKey"] = "RequestMaterial";
            dtrow["FeatureMode"] = 1;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dt);

            //Act
            SiteFeatureFactory objSiteFeatureFactory = new SiteFeatureFactory(mockDataAccess.Object);
            var result = objSiteFeatureFactory.GetAllEntities(0, 0, objSortDtl);

            List<SiteFeatureObjectModel> lstExpected = new List<SiteFeatureObjectModel>();
            lstExpected.Add(new SiteFeatureObjectModel()
            {
                SiteId = 1,
                SiteKey = "RequestMaterial",
                FeatureMode = 1
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);

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
            SiteFeatureFactory objFactory = new SiteFeatureFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<SiteFeatureObjectModel>(dr);

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
            SiteFeatureObjectModel objObjectModel = new SiteFeatureObjectModel();
            objObjectModel.SiteId = 1;
            objObjectModel.SiteKey = "Test";
            objObjectModel.FeatureMode = 22;

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="SiteId", Value=1 },
                new SqlParameter(){ ParameterName="SiteKey", Value="Test" },
                new SqlParameter(){ ParameterName="FeatureMode", Value=22 }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            SiteFeatureFactory objSiteFeatureFactory = new SiteFeatureFactory(mockDataAccess.Object);
            objSiteFeatureFactory.SaveEntity(objObjectModel, 1);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region DeleteEntity_WithDeletedBy
        /// <summary>
        /// DeleteEntity_WithDeletedBy
        /// </summary>
        [TestMethod]
        public void DeleteEntity_WithDeletedBy()
        {
            //Arrange
            SiteFeatureObjectModel objObjectModel = new SiteFeatureObjectModel();
            objObjectModel.SiteId = 1;
            objObjectModel.SiteKey = "Test";
            objObjectModel.FeatureMode = 22;

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                new SqlParameter(){ ParameterName="SiteId", Value=1 },
                new SqlParameter(){ ParameterName="SiteKey", Value="Test" }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            SiteFeatureFactory objSiteFactory = new SiteFeatureFactory(mockDataAccess.Object);
            objSiteFactory.DeleteEntity(objObjectModel, 1);

            //Assert
            mockDataAccess.VerifyAll();
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
            SiteFeatureSearchDetail objSearchDtl = new SiteFeatureSearchDetail();
            objSearchDtl.SiteId = 1;
            objSearchDtl.SiteKey = "RequestMaterial";

            SiteFeatureSortDetail objSortDtl = new SiteFeatureSortDetail();
            objSortDtl.Column = SiteFeatureSortColumn.SiteKey;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("SiteKey", typeof(string));
            dt.Columns.Add("FeatureMode", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteId"] = 1;
            dtrow["SiteKey"] = "RequestMaterial";
            dtrow["FeatureMode"] = 1;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dt);

            SiteFeatureFactory objSiteFeatureFactory = new SiteFeatureFactory(mockDataAccess.Object);
            var result = objSiteFeatureFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, null);

            List<SiteFeatureObjectModel> lstExpected = new List<SiteFeatureObjectModel>();
            lstExpected.Add(new SiteFeatureObjectModel()
            {
                SiteId = 1,
                SiteKey = "RequestMaterial",
                FeatureMode = 1
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);

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
        //    SiteFeatureObjectModel entity = new SiteFeatureObjectModel();
        //    Exception exe = null;
        //    ClientData();

        //    SiteFeatureFactory objSiteFeatureFactory = new SiteFeatureFactory(mockDataAccess);
        //    try
        //    {
        //        objSiteFeatureFactory.DeleteEntity(entity);
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

        //#region DeleteEntity_With_Key
        ///// <summary>
        ///// DeleteEntity_With_Key
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Key()
        //{
        //    //Arrange
        //    SiteFeatureKey objSiteFeatureKey = new SiteFeatureKey(1, "XBRL");
        //    Exception exe = null;
        //    ClientData();

        //    Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //      .IgnoreArguments();

        //    //Act
        //    SiteFeatureFactoryCache objSiteFeatureFactoryCache = new SiteFeatureFactoryCache(mockDataAccess);
        //    objSiteFeatureFactoryCache.ClientName = "Forethought";
        //    objSiteFeatureFactoryCache.Mode = FactoryCacheMode.All;

        //    //Act
        //    try
        //    {
        //        objSiteFeatureFactoryCache.DeleteEntity(objSiteFeatureKey);
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

        //#region GetEntityByKey_Returns_SiteFeatureObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_SiteFeatureObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_SiteFeatureObjectModel()
        //{
        //    //Arrange
        //    SiteFeatureKey objSiteFeatureKey = new SiteFeatureKey(1, "RequestMaterial");
        //    ClientData();
        //    Exception exe = null;

        //    //Act
        //    SiteFeatureFactory objSiteFeatureFactory = new SiteFeatureFactory(mockDataAccess);
        //    try
        //    {
        //        objSiteFeatureFactory.GetEntityByKey(objSiteFeatureKey);
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
    }
}
