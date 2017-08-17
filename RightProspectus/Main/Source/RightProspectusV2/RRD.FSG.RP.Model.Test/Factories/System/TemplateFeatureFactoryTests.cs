using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Keys;
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
using System.Data.SqlClient;

namespace RRD.FSG.RP.Model.Test.Factories.System
{
    /// <summary>
    /// Test class for TemplateFeatureFactory class
    /// </summary>
    [TestClass]
    public class TemplateFeatureFactoryTests :BaseTestFactory<TemplateFeatureObjectModel>
    {
        Mock<IDataAccess> mockDataAccess { get; set; }
       

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


        #region GetAllEntities_Returns_IEnumerable_TemplateFeatureObjectModel
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_TemplateFeatureObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_TemplateFeatureObjectModel()
        {
            //Arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("FeatureKey", typeof(string));
            dt.Columns.Add("FeatureDescription", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["TemplateId"] = 1;
            dtrow["FeatureKey"] = "RequestMaterial";
            dtrow["FeatureDescription"] = "DescRequestMaterial";

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
            TemplateFeatureFactoryCache objFactoryCache = new TemplateFeatureFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;
            var result = objFactoryCache.GetAllEntities(0, 0);

            List<TemplateFeatureObjectModel> lstExpected = new List<TemplateFeatureObjectModel>();
            lstExpected.Add(new TemplateFeatureObjectModel()
            {
                TemplateId = 1,
                FeatureDescription = "DescRequestMaterial",
                FeatureKey = "RequestMaterial",
                Name = null,
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            lstExclude.Add("Key");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<TemplateFeatureKey>(lstExpected, result.ToList());
           
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_IEnumerable_WithSort_TemplateFeatureObjectModel
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_TemplateFeatureObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_WithSort_TemplateFeatureObjectModel()
        {
            //Arrange
            TemplateFeatureSortDetail objSortDtl = new TemplateFeatureSortDetail();
            objSortDtl.Column = TemplateFeatureSortColumn.TemplateId;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("FeatureKey", typeof(string));
            dt.Columns.Add("FeatureDescription", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["TemplateId"] = 1;
            dtrow["FeatureKey"] = "RequestMaterial";
            dtrow["FeatureDescription"] = "DescRequestMaterial";

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
            TemplateFeatureFactory objFactoryCache = new TemplateFeatureFactory(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            //objFactoryCache.Mode = FactoryCacheMode.All;
            var result = objFactoryCache.GetAllEntities<TemplateFeatureObjectModel>(0, 0, objSortDtl);

            List<TemplateFeatureObjectModel> lstExpected = new List<TemplateFeatureObjectModel>();
            lstExpected.Add(new TemplateFeatureObjectModel()
            {
                TemplateId = 1,
                FeatureDescription = "DescRequestMaterial",
                FeatureKey = "RequestMaterial",
                Name = null,
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            lstExclude.Add("Key");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<TemplateFeatureKey>(lstExpected, result.ToList());

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
            TemplateFeatureFactory objFactory = new TemplateFeatureFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<TemplateFeatureObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

        //#region SaveEntity_With_ModifiedBy
        ///// <summary>
        ///// SaveEntity_With_ModifiedBy
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_With_ModifiedBy()
        //{
        //    //Arrange
        //    TemplateFeatureObjectModel objObjectModel = new TemplateFeatureObjectModel();
        //    objObjectModel.TemplateId = 1;
        //    objObjectModel.FeatureKey = "RequestMaterial";
        //    objObjectModel.FeatureDescription = "DescRequestMaterial";
        //    Exception exe = null;
        //    ClientData();

        //    //Act
        //    TemplateFeatureFactory objFactory = new TemplateFeatureFactory(mockDataAccess);
        //    try
        //    {
        //        objFactory.SaveEntity(objObjectModel, 1);
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
        //    TemplateFeatureKey objKey = new TemplateFeatureKey(1, "Test_Key");
        //    Exception exe = null;
        //    ClientData();
            
        //    //Act
        //    TemplateFeatureFactoryCache objFactoryCache = new TemplateFeatureFactoryCache(mockDataAccess);
        //    try
        //    {
        //        objFactoryCache.DeleteEntity(objKey);
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

        //#region DeleteEntity_With_Entity
        ///// <summary>
        ///// DeleteEntity_With_Entity
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Entity()
        //{
        //    //Arrange
        //    TemplateFeatureObjectModel entity = new TemplateFeatureObjectModel();
        //    entity.TemplateId = 1;
        //    entity.FeatureKey = "RequestMaterial";
        //    entity.FeatureDescription = "DescRequestMaterial";
        //    ClientData();
        //    Exception exe = null;

        //    //Act
        //    TemplateFeatureFactory objFactory = new TemplateFeatureFactory(mockDataAccess);
        //    try
        //    {
        //        objFactory.DeleteEntity(entity);
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

        //#region GetEntityByKey_Returns_TemplateFeatureObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_TemplateFeatureObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_TemplateFeatureObjectModel()
        //{
        //    //Arrange
        //    TemplateFeatureKey key = new TemplateFeatureKey(1, "Test_Key");
        //    Exception exe = null;
        //    ClientData();

        //    //Act
        //    TemplateFeatureFactory objFactory = new TemplateFeatureFactory(mockDataAccess);
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

        //#region GetEntitiesBySearch_Returns_IEnumerable_Entity
        ///// <summary>
        ///// GetEntitiesBySearch_Returns_IEnumerable_Entity
        ///// </summary>
        //[TestMethod]
        //public void GetEntitiesBySearch_Returns_IEnumerable_Entity()
        //{
        //    //Arrange  
        //    TemplateFeatureSearchDetail objSearchDtl = new TemplateFeatureSearchDetail();
        //    TemplateFeatureSortDetail objSortDtl = new TemplateFeatureSortDetail();
        //    TemplateFeatureKey objKey = new TemplateFeatureKey(1, "Test_Key");

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    TemplateFeatureFactory objFactory = new TemplateFeatureFactory(mockDataAccess);
        //    try
        //    {
        //        objFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl);
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
