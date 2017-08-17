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
    /// Test class for SiteFactory class
    /// </summary>
    [TestClass]
    public class SiteFactoryTests : BaseTestFactory<SiteObjectModel>
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

        #region GetAllEntities_Returns_Ienumerable_SiteObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_SiteObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_SiteObjectModel()
        {
            //Arrange
            SiteSortDetail objSortDtl = new SiteSortDetail();
            objSortDtl.Column = SiteSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("NAME", typeof(string));
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("DefaultPageID", typeof(Int32));
            dt.Columns.Add("ParentSiteId", typeof(Int32));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("IsDefaultSite", typeof(bool));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteId"] = 1;
            dtrow["NAME"] = "Forethought";
            dtrow["TemplateId"] = 1;
            dtrow["DefaultPageID"] = 1;
            dtrow["ParentSiteId"] = DBNull.Value;
            dtrow["Description"] = "Forethought Site";
            dtrow["IsDefaultSite"] = false;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dt.Rows.Add(dtrow);

            DataTable dtTemltPage = new DataTable();
            dtTemltPage.Columns.Add("TemplateId", typeof(Int32));
            dtTemltPage.Columns.Add("TemplateName", typeof(string));
            dtTemltPage.Columns.Add("PageID", typeof(Int32));
            dtTemltPage.Columns.Add("PageName", typeof(string));
            dtTemltPage.Columns.Add("PageDescription", typeof(string));

            DataRow dtrowTP = dtTemltPage.NewRow();
            dtrowTP["TemplateId"] = 1;
            dtrowTP["TemplateName"] = "Default";
            dtrowTP["PageID"] = 1;
            dtrowTP["PageName"] = "TAL";
            dtrowTP["PageDescription"] = "Taxonomy Association Link";
            dtTemltPage.Rows.Add(dtrowTP);

            ClientData();

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt)
                .Returns(dtTemltPage);

            //Act
            SiteFactoryCache objSiteFactoryCache = new SiteFactoryCache(mockDataAccess.Object);
            objSiteFactoryCache.ClientName = "Forethought";
            objSiteFactoryCache.Mode = FactoryCacheMode.All;
            var result = objSiteFactoryCache.GetAllEntities(0, 0, objSortDtl);

            List<SiteObjectModel> lstExpected = new List<SiteObjectModel>();
            lstExpected.Add(new SiteObjectModel()
            {
                DefaultPageId = 1,
                DefaultPageName = "TAL",
                PageDescription = "Taxonomy Association Link",
                SiteID = 1,
                TemplateId = 1,
                TemplateName = "Default",
                Name = "Forethought",
                Description = "Forethought Site"
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
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
            SiteFactory objFactory = new SiteFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<SiteObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
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
            SiteSortDetail objSortDtl = new SiteSortDetail();
            objSortDtl.Column = SiteSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("NAME", typeof(string));
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("DefaultPageID", typeof(Int32));
            dt.Columns.Add("ParentSiteId", typeof(Int32));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("IsDefaultSite", typeof(bool));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteId"] = 1;
            dtrow["NAME"] = "Forethought";
            dtrow["TemplateId"] = 1;
            dtrow["DefaultPageID"] = 1;
            dtrow["ParentSiteId"] = DBNull.Value;
            dtrow["Description"] = "Forethought Site";
            dtrow["IsDefaultSite"] = false;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dt.Rows.Add(dtrow);

            DataTable dtTemltPage = new DataTable();
            dtTemltPage.Columns.Add("TemplateId", typeof(Int32));
            dtTemltPage.Columns.Add("TemplateName", typeof(string));
            dtTemltPage.Columns.Add("PageID", typeof(Int32));
            dtTemltPage.Columns.Add("PageName", typeof(string));
            dtTemltPage.Columns.Add("PageDescription", typeof(string));

            DataRow dtrowTP = dtTemltPage.NewRow();
            dtrowTP["TemplateId"] = 1;
            dtrowTP["TemplateName"] = "Default";
            dtrowTP["PageID"] = 1;
            dtrowTP["PageName"] = "TAL";
            dtrowTP["PageDescription"] = "Taxonomy Association Link";
            dtTemltPage.Rows.Add(dtrowTP);

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                 .Returns(dt)
                 .Returns(dtTemltPage);

            //Act
            SiteFactory objSiteFactory = new SiteFactory(mockDataAccess.Object);
            var result = objSiteFactory.GetAllEntities(0, 0, objSortDtl);

            List<SiteObjectModel> lstExpected = new List<SiteObjectModel>();
            lstExpected.Add(new SiteObjectModel()
            {
                DefaultPageId = 1,
                DefaultPageName = "TAL",
                PageDescription = "Taxonomy Association Link",
                SiteID = 1,
                TemplateId = 1,
                TemplateName = "Default",
                Name = "Forethought",
                Description = "Forethought Site"
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
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
            SiteObjectModel objObjectModel = new SiteObjectModel();
            objObjectModel.SiteID = 2;
            objObjectModel.Name = "Test_001";
            objObjectModel.DefaultPageId = 1;
            objObjectModel.ParentSiteId = null;
            objObjectModel.Description = "TEST_001";

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="SiteId", Value=2 },
                new SqlParameter(){ ParameterName="Name", Value="Test_001" },
                new SqlParameter(){ ParameterName="TemplateId", Value=1 },
                new SqlParameter(){ ParameterName="DefaultPageId", Value=1 },
                new SqlParameter(){ ParameterName="ParentSiteId", Value=1 },
                new SqlParameter(){ ParameterName="Description", Value="TEST_001" },
                new SqlParameter(){ ParameterName="ClientId", Value=1 },
                new SqlParameter(){ ParameterName="IsDefaultSite", Value=false },
                new SqlParameter(){ ParameterName="TemplateTextData", Value=null },
                new SqlParameter(){ ParameterName="TemplatePageTextData", Value=null },
                new SqlParameter(){ ParameterName="TemplateNavigationData", Value=null },
                new SqlParameter(){ ParameterName="TemplatePageNavigationData", Value=null }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4])
                .Returns(parameters[5])
                .Returns(parameters[6])
                .Returns(parameters[7])
                .Returns(parameters[8]);

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<SqlDbType>(), It.IsAny<object>()))
                .Returns(parameters[9])
                .Returns(parameters[10])
                .Returns(parameters[11])
                .Returns(parameters[12]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            SiteFactory objSiteFactory = new SiteFactory(mockDataAccess.Object);
            objSiteFactory.SaveEntity(objObjectModel, 1);

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
        //    SiteObjectModel entity = new SiteObjectModel();
        //    entity.SiteID = 2;
        //    Exception exe = null;
        //    ClientData();

        //    Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //        .IgnoreArguments()
        //        .MustBeCalled();

        //    SiteFactoryCache objSiteFactoryCache = new SiteFactoryCache(mockDataAccess);
        //    objSiteFactoryCache.ClientName = "Forethought";
        //    objSiteFactoryCache.Mode = FactoryCacheMode.All;

        //    //Act
        //    try
        //    {
        //        objSiteFactoryCache.DeleteEntity(entity);
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
                new SqlParameter(){ ParameterName="SiteId", Value=1 }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            SiteFactory objSiteFactory = new SiteFactory(mockDataAccess.Object);
            objSiteFactory.DeleteEntity(1);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity
        ///// <summary>
        ///// GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity
        ///// </summary>
        //[TestMethod]
        //public void GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity()
        //{
        //    //Arrange  
        //    SiteSearchDetail objSearchDtl = new SiteSearchDetail();

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    SiteFactory objSiteFactory = new SiteFactory(mockDataAccess);
        //    try
        //    {
        //        objSiteFactory.GetEntitiesBySearch(0, 0, objSearchDtl, 1);
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
        //    SiteSearchDetail objSearchDtl = new SiteSearchDetail();          
        //    SiteSortDetail objSortDtl = new SiteSortDetail();
        //    Exception exe = null; 
        //    ClientData();

        //    //Act
        //    SiteFactory objSiteFactory = new SiteFactory(mockDataAccess);
        //    try
        //    {
        //        objSiteFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, null);
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

        //#region GetSiteEntity_Returns_ObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_ObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetSiteEntity_Returns_ObjectModel()
        //{
        //    //Arrange  
        //    Exception exe = null;
        //    ClientData();

        //    //Act
        //    SiteFactory objSiteFactory = new SiteFactory(mockDataAccess);
        //    try
        //    {
        //        objSiteFactory.GetSiteEntity(1);
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

        //#region GetEntityByKey_Returns_SiteObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_SiteObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_SiteObjectModel()
        //{
        //    //Arrange
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    SiteFactory objSiteFactory = new SiteFactory(mockDataAccess);
        //    try
        //    {
        //        objSiteFactory.GetEntityByKey(1);
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
