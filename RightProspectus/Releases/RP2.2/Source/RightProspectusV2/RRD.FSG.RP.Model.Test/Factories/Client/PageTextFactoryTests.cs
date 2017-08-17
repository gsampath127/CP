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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRD.FSG.RP.Model.Factories;
using System.Web.Script.Serialization;
using Moq;
using System.Data.Common;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Entities.System;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for PageTextFactory class
    /// </summary>
    [TestClass]
    public class PageTextFactoryTests : BaseTestFactory<PageTextObjectModel>
    {

        Mock<IDataAccess> mockDataAccess;

        /// <summary>
        /// TestInitialze
        /// </summary>
        [TestInitialize]
        public void TestInitialze()
        {
            mockDataAccess = new Mock<IDataAccess>();
        }

        #region GetAllEntities_Returns_Ienumerable_PageTextObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_PageTextObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_PageTextObjectModel()
        {
            //Arrange
            PageTextSortDetail objSortDtl = new PageTextSortDetail();
            objSortDtl.Column = PageTextSortColumn.ResourceKey;
            objSortDtl.Order = SortOrder.Ascending;

            IEnumerable<TemplatePageObjectModel> templatePageDetails = Enumerable.Empty<TemplatePageObjectModel>();

            DataTable dt = new DataTable();
            dt.Columns.Add("PageTextId", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("SITEID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("ResourceKey", typeof(string));
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForPageTextID", typeof(bool));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["PageTextId"] = 53;
            dtrow["Version"] = 2;
            dtrow["PageId"] = 1;
            dtrow["TemplateId"] = 1;
            dtrow["SITEID"] = 1;
            dtrow["SiteName"] = "Forethought";
            dtrow["ResourceKey"] = "TAHD_ProductHeaderText";
            dtrow["Text"] = "Product Documents";
            dtrow["IsProofing"] = 0;
            dtrow["IsProofingAvailableForPageTextID"] = 1;
            dtrow["LanguageCulture"] = DBNull.Value;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dt.Rows.Add(dtrow);

            DataTable dtTemplatePage = new DataTable();
            dtTemplatePage.Columns.Add("PageId", typeof(Int32));
            dtTemplatePage.Columns.Add("TemplateId", typeof(Int32));
            dtTemplatePage.Columns.Add("TemplateName", typeof(String));
            dtTemplatePage.Columns.Add("PageName", typeof(String));
            dtTemplatePage.Columns.Add("PageDescription", typeof(String));

            DataRow dtrowTemplatePage = dtTemplatePage.NewRow();
            dtrowTemplatePage["PageId"] = 1;
            dtrowTemplatePage["TemplateId"] = 1;
            dtrowTemplatePage["TemplateName"] = "Test";
            dtrowTemplatePage["PageDescription"] = "Taxonomy Association Link";
            dtrowTemplatePage["PageName"] = "TAL";
            dtTemplatePage.Rows.Add(dtrowTemplatePage);

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

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dt)
               .Returns(dtTemplatePage);

            //Act
            PageTextFactoryCache objPageTextFactoryCache = new PageTextFactoryCache(mockDataAccess.Object);
            objPageTextFactoryCache.ClientName = "Forethought";
            objPageTextFactoryCache.Mode = FactoryCacheMode.All;
            var result = objPageTextFactoryCache.GetAllEntities(0, 0, objSortDtl);

            List<PageTextObjectModel> lstExpected = new List<PageTextObjectModel>();
            lstExpected.Add(new PageTextObjectModel()
            {
                IsProofing = false,
                IsProofingAvailableForPageTextId = true,
                PageDescription = "Taxonomy Association Link",
                PageID = 1,
                PageName = "TAL",
                PageTextID = 53,
                ResourceKey = "TAHD_ProductHeaderText",
                SiteID = 1,
                SiteName = "Forethought",
                TemplateID = 1,
                Text = "Product Documents",
                Version = 2,
                Name = null,
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            lstExclude.Add("Key");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<PageTextKey>(lstExpected, result.ToList());

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
            PageTextSortDetail objSortDtl = new PageTextSortDetail();
            objSortDtl.Column = PageTextSortColumn.ResourceKey;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("PageTextId", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("TemplateName", typeof(String));
            dt.Columns.Add("PageName", typeof(String));
            dt.Columns.Add("PageDescription", typeof(String));
            dt.Columns.Add("SITEID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("ResourceKey", typeof(string));
            dt.Columns.Add("Text", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForPageTextID", typeof(bool));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["PageTextId"] = 53;
            dtrow["Version"] = 2;
            dtrow["PageId"] = 1;
            dtrow["TemplateId"] = 1;
            dtrow["SITEID"] = 1;
            dtrow["SiteName"] = "Forethought";
            dtrow["TemplateName"] = "Test";
            dtrow["PageDescription"] = "Taxonomy Association Link";
            dtrow["PageName"] = "TAL";
            dtrow["ResourceKey"] = "TAHD_ProductHeaderText";
            dtrow["Text"] = "Product Documents";
            dtrow["IsProofing"] = 0;
            dtrow["IsProofingAvailableForPageTextID"] = 1;
            dtrow["LanguageCulture"] = DBNull.Value;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dt.Rows.Add(dtrow);

            DataTable dtTemplatePage = new DataTable();
            dtTemplatePage.Columns.Add("PageId", typeof(Int32));
            dtTemplatePage.Columns.Add("TemplateId", typeof(Int32));
            dtTemplatePage.Columns.Add("TemplateName", typeof(String));
            dtTemplatePage.Columns.Add("PageName", typeof(String));
            dtTemplatePage.Columns.Add("PageDescription", typeof(String));

            DataRow dtrowTemplatePage = dtTemplatePage.NewRow();
            dtrowTemplatePage["PageId"] = 1;
            dtrowTemplatePage["TemplateId"] = 1;
            dtrowTemplatePage["TemplateName"] = "Test";
            dtrowTemplatePage["PageDescription"] = "Taxonomy Association Link";
            dtrowTemplatePage["PageName"] = "TAL";
            dtTemplatePage.Rows.Add(dtrowTemplatePage);

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
             .Returns(dt)
             .Returns(dtTemplatePage);
            //Act
            PageTextFactory objPageTextFactory = new PageTextFactory(mockDataAccess.Object);
            var result = objPageTextFactory.GetAllEntities(0, 0, objSortDtl);

            List<PageTextObjectModel> lstExpected = new List<PageTextObjectModel>();
            lstExpected.Add(new PageTextObjectModel()
            {
                IsProofing = false,
                IsProofingAvailableForPageTextId = true,
                PageDescription = "Taxonomy Association Link",
                PageID = 1,
                PageName = "TAL",
                PageTextID = 53,
                ResourceKey = "TAHD_ProductHeaderText",
                SiteID = 1,
                SiteName = "Forethought",
                TemplateID = 1,
                Text = "Product Documents",
                Version = 2,
                Name = null,
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            lstExclude.Add("Key");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<PageTextKey>(lstExpected, result.ToList());

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
            PageTextObjectModel objObjectModel = new PageTextObjectModel();
            objObjectModel.PageTextID = 53;
            objObjectModel.Version = 3;
            objObjectModel.PageID = 2;
            objObjectModel.SiteID = 1;
            objObjectModel.ResourceKey = "TEST";
            objObjectModel.Text = "Test Test";
            objObjectModel.IsProofing = false;

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="Version", Value=1 },
                new SqlParameter(){ ParameterName="PageTextID", Value=53 },
                new SqlParameter(){ ParameterName="PageID", Value=2 },
                new SqlParameter(){ ParameterName="SiteID", Value=1 },
                new SqlParameter(){ ParameterName="ResourceKey", Value="TEST" },
                new SqlParameter(){ ParameterName="Text", Value="Test Test" },
                new SqlParameter(){ ParameterName="IsProofing", Value=false },
              
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
            PageTextFactory objPageTextFactory = new PageTextFactory(mockDataAccess.Object);
            objPageTextFactory.SaveEntity(objObjectModel, 1);

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
            PageTextObjectModel entity = new PageTextObjectModel();
            entity.PageTextID = 53;
            entity.Version = 1;
            entity.IsProofing = false;

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                new SqlParameter(){ ParameterName="PageTextID", Value=53 },
                 new SqlParameter(){ ParameterName="Version", Value=1 },
                new SqlParameter(){ ParameterName="IsProofing", Value=false },
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                 .Returns(parameters[3]);


            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));


            //Act
            PageTextFactory objPageTextFactory = new PageTextFactory(mockDataAccess.Object);
            objPageTextFactory.DeleteEntity(entity);
        }
        #endregion

        //#region DeleteEntity_With_DeletedBy
        ///// <summary>
        ///// DeleteEntity_With_Key
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_DeletedBy()
        //{
        //    //Arrange
        //    PageTextObjectModel entity = new PageTextObjectModel();
        //    entity.PageTextID = 53;
        //    entity.Version = 1;
        //    entity.IsProofing = false;

        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

        //    //Act
        //    PageTextFactoryCache objPageTextFactoryCache = new PageTextFactoryCache(mockDataAccess.Object);
        //    objPageTextFactoryCache.DeleteEntity(entity, 3);


        //    //Assert
        //    mockDataAccess.VerifyAll();
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
            PageTextFactory objFactory = new PageTextFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<PageTextObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

        //#region DeleteEntity_With_Key
        ///// <summary>
        ///// DeleteEntity_With_Key
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Key()
        //{
        //    //Arrange
        //    PageTextKey objPageTextKey = new PageTextKey(98, 1);
        //    Exception exe = null;
        //    //ClientData();

        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));

        //    //Act
        //    PageTextFactoryCache objPageTextFactoryCache = new PageTextFactoryCache(mockDataAccess.Object);

        //    //Act
        //    try
        //    {
        //        objPageTextFactoryCache.DeleteEntity(objPageTextKey);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }
        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));

        //    //Assert
        //    mockDataAccess.VerifyAll();
        //}
        //#endregion

        //#region GetEntityByKey_Returns_PageTextObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_PageTextObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_PageTextObjectModel()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    PageTextKey objPageTextKey = new PageTextKey(53, 2);

        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("PageTextId", typeof(Int32));
        //    dt.Columns.Add("Version", typeof(Int32));
        //    dt.Columns.Add("PageId", typeof(Int32));
        //    dt.Columns.Add("TemplateId", typeof(Int32));
        //    dt.Columns.Add("SITEID", typeof(Int32));
        //    dt.Columns.Add("SiteName", typeof(string));
        //    dt.Columns.Add("ResourceKey", typeof(string));
        //    dt.Columns.Add("Text", typeof(string));
        //    dt.Columns.Add("IsProofing", typeof(bool));
        //    dt.Columns.Add("IsProofingAvailableForPageTextID", typeof(bool));
        //    dt.Columns.Add("LanguageCulture", typeof(Int32));
        //    dt.Columns.Add("UtcLastModified", typeof(DateTime));
        //    dt.Columns.Add("ModifiedBy", typeof(Int32));

        //    DataRow dtrow = dt.NewRow();
        //    dtrow["PageTextId"] = 53;
        //    dtrow["Version"] = 2;
        //    dtrow["PageId"] = 1;
        //    dtrow["TemplateId"] = 1;
        //    dtrow["SITEID"] = 1;
        //    dtrow["SiteName"] = "Forethought";
        //    dtrow["ResourceKey"] = "TAHD_ProductHeaderText";
        //    dtrow["Text"] = "Product Documents";
        //    dtrow["IsProofing"] = 0;
        //    dtrow["IsProofingAvailableForPageTextID"] = 1;
        //    dtrow["LanguageCulture"] = DBNull.Value;
        //    dtrow["UtcLastModified"] = DateTime.Now;
        //    dtrow["ModifiedBy"] = 1;
        //    dt.Rows.Add(dtrow);

        //    ClientData();

        //    Mock.Arrange(() => mockDataAccess.ExecuteDataTable(string.Empty, string.Empty, null))
        //   .IgnoreArguments()
        //   .Returns(dt)
        //   .MustBeCalled();

        //    //Act
        //    PageTextFactory objPageTextFactory = new PageTextFactory(mockDataAccess);
        //    try
        //    {
        //        objPageTextFactory.GetEntityByKey(objPageTextKey);
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

        //#region GetPageTextEntity_Returns_IEnumerable_Entity
        ///// <summary>
        ///// GetPageTextEntity_Returns_IEnumerable_Entity
        ///// </summary>
        //[TestMethod]
        //public void GetPageTextEntity_Returns_IEnumerable_Entity()
        //{
        //    //Arrange  
        //    PageTextKey objPageTextKey = new PageTextKey(53, 2);
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    PageTextFactory objPageTextFactory = new PageTextFactory(mockDataAccess);
        //    try
        //    {
        //        objPageTextFactory.GetPageTextEntity(objPageTextKey);
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
        //    PageTextSearchDetail objSearchDtl = new PageTextSearchDetail();

        //    PageTextKey objPageTextKey = new PageTextKey(53,2);

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    PageTextFactory objPageTextFactory = new PageTextFactory(mockDataAccess);
        //    try
        //    {
        //        objPageTextFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objPageTextKey);
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
        //    PageTextSearchDetail objSearchDtl = new PageTextSearchDetail();

        //    PageTextSortDetail objSortDtl = new PageTextSortDetail();

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    PageTextFactory objPageTextFactory = new PageTextFactory(mockDataAccess);
        //    try
        //    {
        //        objPageTextFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, null);
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
