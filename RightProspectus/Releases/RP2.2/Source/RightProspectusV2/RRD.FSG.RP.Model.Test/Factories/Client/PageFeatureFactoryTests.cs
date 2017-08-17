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
using Moq;


namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for PageFeatureFactory class
    /// </summary>
    [TestClass]
    public class PageFeatureFactoryTests : BaseTestFactory<PageFeatureObjectModel>
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

        #region GetAllEntities_Returns_IEnumerable_Entity
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_Entity
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_Entity()
        {
            //Arrange
            PageFeatureSortDetail objSortDtl = new PageFeatureSortDetail();
            objSortDtl.Column = PageFeatureSortColumn.SiteId;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dtPageFeature = new DataTable();
            dtPageFeature.Columns.Add("SiteId", typeof(Int32));
            dtPageFeature.Columns.Add("PageId", typeof(Int32));
            dtPageFeature.Columns.Add("PageKey", typeof(string));
            dtPageFeature.Columns.Add("FeatureMode", typeof(Int32));
            dtPageFeature.Columns.Add("UtcLastModified", typeof(DateTime));
            dtPageFeature.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrowPageFeature = dtPageFeature.NewRow();
            dtrowPageFeature["SiteId"] = 1;
            dtrowPageFeature["PageId"] = 1;
            dtrowPageFeature["PageKey"] = "XBRL";
            dtrowPageFeature["FeatureMode"] = 24;
            dtrowPageFeature["UtcLastModified"] = DateTime.Now;
            dtrowPageFeature["ModifiedBy"] = 1;
            dtPageFeature.Rows.Add(dtrowPageFeature);

            DataTable dtTemplatePage = new DataTable();
            dtTemplatePage.Columns.Add("TemplateId", typeof(Int32));
            dtTemplatePage.Columns.Add("PageID", typeof(Int32));
            dtTemplatePage.Columns.Add("TemplateName", typeof(string));
            dtTemplatePage.Columns.Add("PageName", typeof(string));
            dtTemplatePage.Columns.Add("PageDescription", typeof(string));

            DataRow dtrowTemplatePage = dtTemplatePage.NewRow();
            dtrowTemplatePage["TemplateId"] = 1;
            dtrowTemplatePage["PageId"] = 1;
            dtrowTemplatePage["TemplateName"] = "Text";
            dtrowTemplatePage["PageName"] = "TAL";
            dtrowTemplatePage["PageDescription"] = "Taxonomy Association Link";
            dtTemplatePage.Rows.Add(dtrowTemplatePage);

            ClientData();

            mockDataAccess.SetupSequence(
                x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dtPageFeature)
                .Returns(dtTemplatePage);

            //Act
            PageFeatureFactoryCache objPageFeatureFactoryCache = new PageFeatureFactoryCache(mockDataAccess.Object)
            {
                ClientName = "Forethought",
                Mode = FactoryCacheMode.All
            };
            var result = objPageFeatureFactoryCache.GetAllEntities(0, 0, objSortDtl);
            List<PageFeatureObjectModel> lstExpected = new List<PageFeatureObjectModel>
            {
                new PageFeatureObjectModel()
                {
                    SiteId = 1,
                    PageId = 1,
                    PageKey = "XBRL",
                    PageName = "TAL",
                    PageDescription = "Taxonomy Association Link",
                    FeatureMode = 24
                }
            };

            List<string> lstExclude = new List<string> { "ModifiedBy", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Factory
        /// <summary>
        /// GetAllEntities_Factory
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Factory()
        {
            //Arrange
            PageFeatureSortDetail objSortDtl = new PageFeatureSortDetail();
            objSortDtl.Column = PageFeatureSortColumn.SiteId;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dtPageFeature = new DataTable();
            dtPageFeature.Columns.Add("SiteId", typeof(Int32));
            dtPageFeature.Columns.Add("PageId", typeof(Int32));
            dtPageFeature.Columns.Add("PageKey", typeof(string));
            dtPageFeature.Columns.Add("FeatureMode", typeof(Int32));
            dtPageFeature.Columns.Add("UtcLastModified", typeof(DateTime));
            dtPageFeature.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrowPageFeature = dtPageFeature.NewRow();
            dtrowPageFeature["SiteId"] = 1;
            dtrowPageFeature["PageId"] = 1;
            dtrowPageFeature["PageKey"] = "XBRL";
            dtrowPageFeature["FeatureMode"] = 24;
            dtrowPageFeature["UtcLastModified"] = DateTime.Now;
            dtrowPageFeature["ModifiedBy"] = 1;
            dtPageFeature.Rows.Add(dtrowPageFeature);

            DataTable dtTemplatePage = new DataTable();
            dtTemplatePage.Columns.Add("TemplateId", typeof(Int32));
            dtTemplatePage.Columns.Add("PageID", typeof(Int32));
            dtTemplatePage.Columns.Add("TemplateName", typeof(string));
            dtTemplatePage.Columns.Add("PageName", typeof(string));
            dtTemplatePage.Columns.Add("PageDescription", typeof(string));

            DataRow dtrowTemplatePage = dtTemplatePage.NewRow();
            dtrowTemplatePage["TemplateId"] = 1;
            dtrowTemplatePage["PageId"] = 1;
            dtrowTemplatePage["TemplateName"] = "Text";
            dtrowTemplatePage["PageName"] = "TAL";
            dtrowTemplatePage["PageDescription"] = "Taxonomy Association Link";
            dtTemplatePage.Rows.Add(dtrowTemplatePage);

            mockDataAccess.SetupSequence(
                x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dtPageFeature)
                .Returns(dtTemplatePage);

            //Act
            PageFeatureFactory objPageFeatureFactory = new PageFeatureFactory(mockDataAccess.Object);
            var result = objPageFeatureFactory.GetAllEntities(0, 0);
            List<PageFeatureObjectModel> lstExpected = new List<PageFeatureObjectModel>
            {
                new PageFeatureObjectModel()
                {
                    SiteId = 1,
                    PageId = 1,
                    PageKey = "XBRL",
                    PageName = "TAL",
                    PageDescription = "Taxonomy Association Link",
                    FeatureMode = 24
                }
            };

            List<string> lstExclude = new List<string> { "ModifiedBy", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);

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
            PageFeatureSortDetail objSortDtl = new PageFeatureSortDetail
            {
                Column = PageFeatureSortColumn.SiteId,
                Order = SortOrder.Ascending
            };

            PageFeatureSearchDetail objSearchDtl = new PageFeatureSearchDetail { PageId = 1 };

            DataTable dtPageFeature = new DataTable();
            dtPageFeature.Columns.Add("SiteId", typeof(Int32));
            dtPageFeature.Columns.Add("PageId", typeof(Int32));
            dtPageFeature.Columns.Add("PageKey", typeof(string));
            dtPageFeature.Columns.Add("FeatureMode", typeof(Int32));
            dtPageFeature.Columns.Add("UtcLastModified", typeof(DateTime));
            dtPageFeature.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrowPageFeature = dtPageFeature.NewRow();
            dtrowPageFeature["SiteId"] = 1;
            dtrowPageFeature["PageId"] = 1;
            dtrowPageFeature["PageKey"] = "XBRL";
            dtrowPageFeature["FeatureMode"] = 24;
            dtrowPageFeature["UtcLastModified"] = DateTime.Now;
            dtrowPageFeature["ModifiedBy"] = 1;
            dtPageFeature.Rows.Add(dtrowPageFeature);

            DataTable dtTemplatePage = new DataTable();
            dtTemplatePage.Columns.Add("TemplateId", typeof(Int32));
            dtTemplatePage.Columns.Add("PageID", typeof(Int32));
            dtTemplatePage.Columns.Add("TemplateName", typeof(string));
            dtTemplatePage.Columns.Add("PageName", typeof(string));
            dtTemplatePage.Columns.Add("PageDescription", typeof(string));

            DataRow dtrowTemplatePage = dtTemplatePage.NewRow();
            dtrowTemplatePage["TemplateId"] = 1;
            dtrowTemplatePage["PageId"] = 1;
            dtrowTemplatePage["TemplateName"] = "Text";
            dtrowTemplatePage["PageName"] = "TAL";
            dtrowTemplatePage["PageDescription"] = "Taxonomy Association Link";
            dtTemplatePage.Rows.Add(dtrowTemplatePage);

            mockDataAccess.SetupSequence(
                x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dtPageFeature)
                .Returns(dtTemplatePage);


            //Act
            PageFeatureFactoryCache objPageFeatureFactoryCache = new PageFeatureFactoryCache(mockDataAccess.Object);
            var result = objPageFeatureFactoryCache.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl);
            List<PageFeatureObjectModel> lstExpected = new List<PageFeatureObjectModel>
            {
                new PageFeatureObjectModel()
                {
                    SiteId = 1,
                    PageId = 1,
                    PageKey = "XBRL",
                    PageName = "TAL",
                    PageDescription = "Taxonomy Association Link",
                    FeatureMode = 24
                }
            };

            List<string> lstExclude = new List<string> { "ModifiedBy", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region SaveEntity_With_ModifiedBy
        /// <summary>
        /// SaveEntity_With_ModifiedBy
        /// </summary>
        [TestMethod]
        public void SaveEntity_With_ModifiedBy()
        {
            //Arrange
            PageFeatureObjectModel objObjectModel = new PageFeatureObjectModel
            {
                SiteId = 1,
                PageId = 1,
                PageKey = "XBRL",
                FeatureMode = 1
            };
            var parameters = new[]
            {
                new SqlParameter {ParameterName = "ModifiedBy", Value = 1},
                new SqlParameter {ParameterName = "SiteId", Value = 1},
                new SqlParameter {ParameterName = "PageId", Value = 1},
                new SqlParameter {ParameterName = "PageKey", Value = "XBRL"},
                new SqlParameter {ParameterName = "FeatureMode", Value = 1}
            };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4]);
            mockDataAccess.Setup(
                x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            PageFeatureFactory objPageFeatureFactory = new PageFeatureFactory(mockDataAccess.Object);
            objPageFeatureFactory.SaveEntity(objObjectModel, 1);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region DeleteEntity_With_Entity
        /// <summary>
        /// DeleteEntity_With_Entity
        /// </summary>
        [TestMethod]
        public void DeleteEntity_With_Entity()
        {
            //Arrange
            PageFeatureObjectModel entity = new PageFeatureObjectModel
            {
               
                PageId = 1,
                PageKey = "XBRL",
                SiteId = 1,
            };

            var parameters = new[]
            {
                new SqlParameter {ParameterName = "DeletedBy", Value = 1},
                new SqlParameter {ParameterName = "PageId", Value = 1},
                new SqlParameter {ParameterName = "PageKey", Value = "XBRL"},
                new SqlParameter {ParameterName = "SiteId", Value = 1}
            };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3]);
            mockDataAccess.Setup(
                x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            PageFeatureFactory objPageFeatureFactory = new PageFeatureFactory(mockDataAccess.Object);
            objPageFeatureFactory.DeleteEntity(entity);

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
            PageFeatureFactory objPageFeatureFactory = new PageFeatureFactory(mockDataAccess.Object);
            var result = objPageFeatureFactory.CreateEntity<PageFeatureObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

    }
}