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
    /// Test class for SiteTextFactory class
    /// </summary>
    [TestClass]
    public class SiteTextFactoryTests : BaseTestFactory<SiteTextObjectModel>
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

        #region GetAllEntities_Returns_Ienumerable_SiteTextObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_SiteTextObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_SiteTextObjectModel()
        {
            //Arrange
            SiteTextSortDetail objSortDtl = new SiteTextSortDetail();
            objSortDtl.Column = SiteTextSortColumn.SiteName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteTextId", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("SITEID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("ResourceKey", typeof(string));
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForSiteTextID", typeof(bool));
            dt.Columns.Add("LanguageCulture", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteTextId"] = 91;
            dtrow["Version"] = 1;
            dtrow["SITEID"] = 1;
            dtrow["SiteName"] = "Forethought";
            dtrow["ResourceKey"] = "cssFile";
            dtrow["Text"] = "test";
            dtrow["IsProofing"] = false;
            dtrow["IsProofingAvailableForSiteTextID"] = false;
            dtrow["LanguageCulture"] = DBNull.Value;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dt.Rows.Add(dtrow);

            ClientData();
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
             .Returns(dt);

            //Act
            SiteTextFactoryCache objSiteTextFactoryCache = new SiteTextFactoryCache(mockDataAccess.Object);
            objSiteTextFactoryCache.ClientName = "Forethought";
            objSiteTextFactoryCache.Mode = FactoryCacheMode.All;
            var result = objSiteTextFactoryCache.GetAllEntities(0, 0, objSortDtl);

            //Assert
            mockDataAccess.VerifyAll();
            List<SiteTextObjectModel> lstExpected = new List<SiteTextObjectModel>();
            lstExpected.Add(new SiteTextObjectModel()
            {
                SiteTextID = 91,
                Version = 1,
                SiteID = 1,
                SiteName = "Forethought",
                ResourceKey = "cssFile",
                Text = "test",
                IsProofing = false,
                IsProofingAvailableForSiteTextId = false
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "LastModified" };
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
            SiteTextSortDetail objSortDtl = new SiteTextSortDetail();
            objSortDtl.Column = SiteTextSortColumn.SiteName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteTextId", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("SITEID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("ResourceKey", typeof(string));
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForSiteTextID", typeof(bool));
            dt.Columns.Add("LanguageCulture", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteTextId"] = 91;
            dtrow["Version"] = 1;
            dtrow["SITEID"] = 1;
            dtrow["SiteName"] = "Forethought";
            dtrow["ResourceKey"] = "cssFile";
            dtrow["Text"] = "test";
            dtrow["IsProofing"] = false;
            dtrow["IsProofingAvailableForSiteTextID"] = false;
            dtrow["LanguageCulture"] = DBNull.Value;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
            .Returns(dt);

            //Act
            SiteTextFactory objSiteTextFactory = new SiteTextFactory(mockDataAccess.Object);
            var result = objSiteTextFactory.GetAllEntities(0, 0, objSortDtl);

            //Assert
            mockDataAccess.VerifyAll();
            List<SiteTextObjectModel> lstExpected = new List<SiteTextObjectModel>();
            lstExpected.Add(new SiteTextObjectModel()
            {
                SiteTextID = 91,
                Version = 1,
                SiteID = 1,
                SiteName = "Forethought",
                ResourceKey = "cssFile",
                Text = "test",
                IsProofing = false,
                IsProofingAvailableForSiteTextId = false
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "LastModified" };
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
            SiteTextFactory objFactory = new SiteTextFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<SiteTextObjectModel>(dr);

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
            SiteTextObjectModel objObjectModel = new SiteTextObjectModel();
            objObjectModel.SiteTextID = 91;
            objObjectModel.Version = 2;
            objObjectModel.SiteID = 1;
            objObjectModel.ResourceKey = "XBRL";
            objObjectModel.Text = "Test";
            objObjectModel.IsProofing = false;

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="SiteTextID", Value=91 },
                new SqlParameter(){ ParameterName="Version", Value=2 },
                new SqlParameter(){ ParameterName="SiteID", Value=1 },
                 new SqlParameter(){ ParameterName="ResourceKey", Value="XBRL" },
                new SqlParameter(){ ParameterName="Text", Value="Test" },
                new SqlParameter(){ ParameterName="IsProofing", Value=false }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4])
                .Returns(parameters[5])
                .Returns(parameters[6]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            SiteTextFactory objSiteTextFactory = new SiteTextFactory(mockDataAccess.Object);
            objSiteTextFactory.SaveEntity(objObjectModel, 1);

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
            SiteTextObjectModel entity = new SiteTextObjectModel();
            entity.SiteTextID = 1;
            entity.Version = 1;
            entity.IsProofing = false;

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                new SqlParameter(){ ParameterName="SiteTextID", Value=91 },
                new SqlParameter(){ ParameterName="Version", Value=2 },
                new SqlParameter(){ ParameterName="IsProofing", Value=false }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            SiteTextFactory objSiteTextFactory = new SiteTextFactory(mockDataAccess.Object);
            objSiteTextFactory.DeleteEntity(entity);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region DeleteEntity_With_ModifiedBy
        ///// <summary>
        ///// DeleteEntity_With_ModifiedBy
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_ModifiedBy()
        //{
        //    //Arrange
        //    SiteTextKey objSiteTextKey = new SiteTextKey(1,1);
        //    Exception exe = null;
        //    ClientData();

        //    Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //      .IgnoreArguments();

        //    //Act
        //    SiteTextFactoryCache objSiteTextFactoryCache = new SiteTextFactoryCache(mockDataAccess);
        //    objSiteTextFactoryCache.ClientName = "Forethought";
        //    objSiteTextFactoryCache.Mode = FactoryCacheMode.All;


        //    //Act
        //    try
        //    {
        //        objSiteTextFactoryCache.DeleteEntity(objSiteTextKey,1);
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

        //#region GetSiteTextEntity_Returns_SiteTextObjectModel
        ///// <summary>
        ///// GetSiteTextEntity_Returns_SiteTextObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetSiteTextEntity_Returns_SiteTextObjectModel()
        //{
        //    //Arrange
        //    SiteTextKey objSiteTextKey = new SiteTextKey(1,1);
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    SiteTextFactory objSiteTextFactory = new SiteTextFactory(mockDataAccess);
        //    try
        //    {
        //        objSiteTextFactory.GetSiteTextEntity(objSiteTextKey);
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

        //#region GetEntityByKey_Returns_SiteTextObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_SiteTextObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_SiteTextObjectModel()
        //{
        //    //Arrange
        //    SiteTextKey objSiteTextKey = new SiteTextKey(1,1);
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    SiteTextFactory objSiteTextFactory = new SiteTextFactory(mockDataAccess);
        //    try
        //    {
        //        objSiteTextFactory.GetEntityByKey(objSiteTextKey);
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
        //    SiteTextSearchDetail objSearchDtl = new SiteTextSearchDetail();
        //    SiteTextSortDetail objSortDtl = new SiteTextSortDetail();

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    SiteTextFactory objSiteTextFactory = new SiteTextFactory(mockDataAccess);
        //    try
        //    {
        //        objSiteTextFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, null);
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

        //#region GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity
        ///// <summary>
        ///// GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity
        ///// </summary>
        //[TestMethod]
        //public void GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity()
        //{
        //    //Arrange  
        //    SiteTextSearchDetail objSearchDtl = new SiteTextSearchDetail();
        //    SiteTextKey objSiteTextKey = new SiteTextKey(1,1);

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    SiteTextFactory objSiteTextFactory = new SiteTextFactory(mockDataAccess);
        //    try
        //    {
        //        objSiteTextFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSiteTextKey);
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
