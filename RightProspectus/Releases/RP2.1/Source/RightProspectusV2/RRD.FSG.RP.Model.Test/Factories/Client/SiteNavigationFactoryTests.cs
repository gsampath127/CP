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
using System.Web.Script.Serialization;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for SiteNavigationFactory class
    /// </summary>
    [TestClass]
    public class SiteNavigationFactoryTests : BaseTestFactory<SiteNavigationObjectModel>
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

        #region GetAllEntities_Returns_Ienumerable_SiteNavigationObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_SiteNavigationObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_SiteNavigationObjectModel()
        {
            //Arrange
            SiteNavigationSortDetail objSortDtl = new SiteNavigationSortDetail();
            objSortDtl.Column = SiteNavigationSortColumn.NavigationKey;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteNavigationId", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("SITEID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("NoCol", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForSiteNavigationId", typeof(bool));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("NavigationXML", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteNavigationId"] = 132;
            dtrow["Version"] = 1;
            dtrow["SITEID"] = 1;
            dtrow["SiteName"] = "Forethought";
            dtrow["NavigationKey"] = "test200";
            dtrow["PageId"] = 1;
            dtrow["NoCol"] = "testml";
            dtrow["IsProofing"] = 0;
            dtrow["IsProofingAvailableForSiteNavigationId"] = 0;
            dtrow["LanguageCulture"] = DBNull.Value;
            dtrow["NavigationXML"] = "Test";
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
            SiteNavigationFactoryCache objSiteNavigationFactoryCache = new SiteNavigationFactoryCache(mockDataAccess.Object);
            objSiteNavigationFactoryCache.ClientName = "Forethought";
            objSiteNavigationFactoryCache.Mode = FactoryCacheMode.All;
            var result = objSiteNavigationFactoryCache.GetAllEntities(0, 0, objSortDtl);

            //Assert
            List<SiteNavigationObjectModel> lstExpected = new List<SiteNavigationObjectModel>();
            lstExpected.Add(new SiteNavigationObjectModel()
            {
                NavigationKey = "test200",
                NavigationXML = "Test",
                PageDescription = "Taxonomy Association Link",
                PageId = 1,
                PageName = "TAL",
                SiteId = 1,
                SiteNavigationId = 132,
                Version = 1
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<SiteNavigationKey>(lstExpected, result.ToList());

            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_SiteNavigationObjectModel_ZeroPageid
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_SiteNavigationObjectModel_ZeroPageid
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_SiteNavigationObjectModel_ZeroPageid()
        {
            //Arrange
            SiteNavigationSortDetail objSortDtl = new SiteNavigationSortDetail();
            objSortDtl.Column = SiteNavigationSortColumn.NavigationKey;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteNavigationId", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("SITEID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("NoCol", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForSiteNavigationId", typeof(bool));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("NavigationXML", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteNavigationId"] = 132;
            dtrow["Version"] = 1;
            dtrow["SITEID"] = 1;
            dtrow["SiteName"] = "Forethought";
            dtrow["NavigationKey"] = "test200";
            dtrow["PageId"] = 0;
            dtrow["NoCol"] = "testml";
            dtrow["IsProofing"] = 0;
            dtrow["IsProofingAvailableForSiteNavigationId"] = 0;
            dtrow["LanguageCulture"] = DBNull.Value;
            dtrow["NavigationXML"] = "Test";
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
            SiteNavigationFactoryCache objSiteNavigationFactoryCache = new SiteNavigationFactoryCache(mockDataAccess.Object);
            objSiteNavigationFactoryCache.ClientName = "Forethought";
            objSiteNavigationFactoryCache.Mode = FactoryCacheMode.All;
            var result = objSiteNavigationFactoryCache.GetAllEntities(0, 0, objSortDtl);

            //Assert
            List<SiteNavigationObjectModel> lstExpected = new List<SiteNavigationObjectModel>();
            lstExpected.Add(new SiteNavigationObjectModel()
            {
                NavigationKey = "test200",
                NavigationXML = "Test",
                PageDescription = string.Empty,
                PageId = 0,
                PageName = string.Empty,
                SiteId = 1,
                SiteNavigationId = 132,
                Version = 1
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<SiteNavigationKey>(lstExpected, result.ToList());

            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_SiteNavigationObjectModel_nullPageid
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_SiteNavigationObjectModel_ZeroPageid
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_SiteNavigationObjectModel_nullPageid()
        {
            //Arrange
            SiteNavigationSortDetail objSortDtl = new SiteNavigationSortDetail();
            objSortDtl.Column = SiteNavigationSortColumn.NavigationKey;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteNavigationId", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("SITEID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("NoCol", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForSiteNavigationId", typeof(bool));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("NavigationXML", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteNavigationId"] = 132;
            dtrow["Version"] = 1;
            dtrow["SITEID"] = 1;
            dtrow["SiteName"] = "Forethought";
            dtrow["NavigationKey"] = "test200";
            dtrow["PageId"] = DBNull.Value;
            dtrow["NoCol"] = "testml";
            dtrow["IsProofing"] = 0;
            dtrow["IsProofingAvailableForSiteNavigationId"] = 0;
            dtrow["LanguageCulture"] = DBNull.Value;
            dtrow["NavigationXML"] = "Test";
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
            dtrowTP["PageID"] = DBNull.Value;
            dtrowTP["PageName"] = "TAL";
            dtrowTP["PageDescription"] = "Taxonomy Association Link";
            dtTemltPage.Rows.Add(dtrowTP);

            ClientData();

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt)
              .Returns(dtTemltPage);

            //Act
            SiteNavigationFactoryCache objSiteNavigationFactoryCache = new SiteNavigationFactoryCache(mockDataAccess.Object);
            objSiteNavigationFactoryCache.ClientName = "Forethought";
            objSiteNavigationFactoryCache.Mode = FactoryCacheMode.All;
            var result = objSiteNavigationFactoryCache.GetAllEntities(0, 0, objSortDtl);

            //Assert
            List<SiteNavigationObjectModel> lstExpected = new List<SiteNavigationObjectModel>();
            lstExpected.Add(new SiteNavigationObjectModel()
            {
                NavigationKey = "test200",
                NavigationXML = "Test",
                PageDescription = string.Empty,
                PageId = null,
                PageName = string.Empty,
                SiteId = 1,
                SiteNavigationId = 132,
                Version = 1
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<SiteNavigationKey>(lstExpected, result.ToList());

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
            SiteNavigationSortDetail objSortDtl = new SiteNavigationSortDetail();
            objSortDtl.Column = SiteNavigationSortColumn.NavigationKey;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteNavigationId", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("SITEID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("NoCol", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForSiteNavigationId", typeof(bool));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("NavigationXML", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteNavigationId"] = 132;
            dtrow["Version"] = 1;
            dtrow["SITEID"] = 1;
            dtrow["SiteName"] = "Forethought";
            dtrow["NavigationKey"] = "test200";
            dtrow["PageId"] = 1;
            dtrow["NoCol"] = "testml";
            dtrow["IsProofing"] = 0;
            dtrow["IsProofingAvailableForSiteNavigationId"] = 0;
            dtrow["LanguageCulture"] = DBNull.Value;
            dtrow["NavigationXML"] = "Test";
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
            SiteNavigationFactory objSiteNavigationFactory = new SiteNavigationFactory(mockDataAccess.Object);
            var result = objSiteNavigationFactory.GetAllEntities(0, 0, objSortDtl);

            //Assert
            List<SiteNavigationObjectModel> lstExpected = new List<SiteNavigationObjectModel>();
            lstExpected.Add(new SiteNavigationObjectModel()
            {
                NavigationKey = "test200",
                NavigationXML = "Test",
                PageDescription = "Taxonomy Association Link",
                PageId = 1,
                PageName = "TAL",
                SiteId = 1,
                SiteNavigationId = 132,
                Version = 1
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<SiteNavigationKey>(lstExpected, result.ToList());

            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Calls_Factory_ZeroPageid
        /// <summary>
        /// GetAllEntities_Calls_Factory_ZeroPageid
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Calls_Factory_ZeroPageid()
        {
            //Arrange
            SiteNavigationSortDetail objSortDtl = new SiteNavigationSortDetail();
            objSortDtl.Column = SiteNavigationSortColumn.NavigationKey;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteNavigationId", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("SITEID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("NoCol", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForSiteNavigationId", typeof(bool));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("NavigationXML", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteNavigationId"] = 132;
            dtrow["Version"] = 1;
            dtrow["SITEID"] = 1;
            dtrow["SiteName"] = "Forethought";
            dtrow["NavigationKey"] = "test200";
            dtrow["PageId"] = 0;
            dtrow["NoCol"] = "testml";
            dtrow["IsProofing"] = 0;
            dtrow["IsProofingAvailableForSiteNavigationId"] = 0;
            dtrow["LanguageCulture"] = DBNull.Value;
            dtrow["NavigationXML"] = "Test";
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
            SiteNavigationFactory objSiteNavigationFactory = new SiteNavigationFactory(mockDataAccess.Object);
            var result = objSiteNavigationFactory.GetAllEntities(0, 0, objSortDtl);

            //Assert
            List<SiteNavigationObjectModel> lstExpected = new List<SiteNavigationObjectModel>();
            lstExpected.Add(new SiteNavigationObjectModel()
            {
                NavigationKey = "test200",
                NavigationXML = "Test",
                PageDescription = string.Empty,
                PageId = 0,
                PageName = string.Empty,
                SiteId = 1,
                SiteNavigationId = 132,
                Version = 1
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<SiteNavigationKey>(lstExpected, result.ToList());

            mockDataAccess.VerifyAll();
        }
        #endregion


        #region GetAllEntities_Calls_Factory_nullPageid
        /// <summary>
        /// GetAllEntities_Calls_Factory_ZeroPageid
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Calls_Factory_nullPageid()
        {
            //Arrange
            SiteNavigationSortDetail objSortDtl = new SiteNavigationSortDetail();
            objSortDtl.Column = SiteNavigationSortColumn.NavigationKey;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteNavigationId", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("SITEID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("NoCol", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForSiteNavigationId", typeof(bool));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("NavigationXML", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteNavigationId"] = 132;
            dtrow["Version"] = 1;
            dtrow["SITEID"] = 1;
            dtrow["SiteName"] = "Forethought";
            dtrow["NavigationKey"] = "test200";
            dtrow["PageId"] = DBNull.Value;
            dtrow["NoCol"] = "testml";
            dtrow["IsProofing"] = 0;
            dtrow["IsProofingAvailableForSiteNavigationId"] = 0;
            dtrow["LanguageCulture"] = DBNull.Value;
            dtrow["NavigationXML"] = "Test";
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
            dtrowTP["PageID"] = DBNull.Value;
            dtrowTP["PageName"] = "TAL";
            dtrowTP["PageDescription"] = "Taxonomy Association Link";
            dtTemltPage.Rows.Add(dtrowTP);

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt)
              .Returns(dtTemltPage);

            //Act
            SiteNavigationFactory objSiteNavigationFactory = new SiteNavigationFactory(mockDataAccess.Object);
            var result = objSiteNavigationFactory.GetAllEntities(0, 0, objSortDtl);

            //Assert
            List<SiteNavigationObjectModel> lstExpected = new List<SiteNavigationObjectModel>();
            lstExpected.Add(new SiteNavigationObjectModel()
            {
                NavigationKey = "test200",
                NavigationXML = "Test",
                PageDescription = string.Empty,
                PageId = null,
                PageName = string.Empty,
                SiteId = 1,
                SiteNavigationId = 132,
                Version = 1
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<SiteNavigationKey>(lstExpected, result.ToList());

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
            SiteNavigationFactory objFactory = new SiteNavigationFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<SiteNavigationObjectModel>(dr);

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
            SiteNavigationObjectModel objObjectModel = new SiteNavigationObjectModel();
            objObjectModel.SiteNavigationId = 133;
            objObjectModel.PageId = 1;
            objObjectModel.SiteId = 1;
            objObjectModel.NavigationKey = "Test_001";
            objObjectModel.NavigationXML = "test_001";
            objObjectModel.IsProofing = false;
            objObjectModel.Version = 1;

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="SiteNavigationId", Value=133 },
                new SqlParameter(){ ParameterName="PageId", Value=1 },
                new SqlParameter(){ ParameterName="SiteId", Value=1 },
                new SqlParameter(){ ParameterName="NavigationKey", Value="Test_001" },
                new SqlParameter(){ ParameterName="NavigationXML", Value="test_001" },
                new SqlParameter(){ ParameterName="IsProofing", Value=true },
                new SqlParameter(){ ParameterName="Version", Value=1 },
                
              
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
            SiteNavigationFactory objSiteNavigationFactory = new SiteNavigationFactory(mockDataAccess.Object);
            objSiteNavigationFactory.SaveEntity(objObjectModel, 1);

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
            SiteNavigationObjectModel entity = new SiteNavigationObjectModel();
            entity.SiteNavigationId = 53;
            entity.Version = 1;
            entity.IsProofing = false;

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                new SqlParameter(){ ParameterName="SiteNavigationId", Value=53 },
                new SqlParameter(){ ParameterName="Version", Value=1 },
                new SqlParameter(){ ParameterName="IsProofing", Value=false }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            SiteNavigationFactory objSiteNavigationFactory = new SiteNavigationFactory(mockDataAccess.Object);
            objSiteNavigationFactory.DeleteEntity(entity);

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
            SiteNavigationSearchDetail objSearchDtl = new SiteNavigationSearchDetail();
            objSearchDtl.SiteId = 1;
            objSearchDtl.PageId = 1;

            SiteNavigationSortDetail objSortDtl = new SiteNavigationSortDetail();
            objSortDtl.Column = SiteNavigationSortColumn.PageName;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteNavigationId", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("SITEID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("NoCol", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForSiteNavigationId", typeof(bool));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("NavigationXML", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteNavigationId"] = 132;
            dtrow["Version"] = 1;
            dtrow["SITEID"] = 1;
            dtrow["SiteName"] = "Forethought";
            dtrow["NavigationKey"] = "test200";
            dtrow["PageId"] = 1;
            dtrow["NoCol"] = "testml";
            dtrow["IsProofing"] = 0;
            dtrow["IsProofingAvailableForSiteNavigationId"] = 0;
            dtrow["LanguageCulture"] = DBNull.Value;
            dtrow["NavigationXML"] = "Test";
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
            SiteNavigationFactory objSiteNavigationFactory = new SiteNavigationFactory(mockDataAccess.Object);
            var result = objSiteNavigationFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, null);

            //Assert
            List<SiteNavigationObjectModel> lstExpected = new List<SiteNavigationObjectModel>();
            lstExpected.Add(new SiteNavigationObjectModel()
            {
                NavigationKey = "test200",
                NavigationXML = "Test",
                PageId = 1,
                SiteId = 1,
                SiteNavigationId = 132,
                Version = 1
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<SiteNavigationKey>(lstExpected, result.ToList());

            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region GetEntityByKey_Returns_SiteNavigationObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_SiteNavigationObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_SiteNavigationObjectModel()
        //{
        //    //Arrange
        //    SiteNavigationKey objSiteNavigationKey = new SiteNavigationKey(1,1);
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    SiteNavigationFactory objSiteNavigationFactory = new SiteNavigationFactory(mockDataAccess);
        //    try
        //    {
        //        objSiteNavigationFactory.GetEntityByKey(objSiteNavigationKey);
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
        //    SiteNavigationKey objSiteNavigationKey = new SiteNavigationKey(1,2);
        //    Exception exe = null;
        //    ClientData();

        //    Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //      .IgnoreArguments();

        //    //Act
        //    SiteNavigationFactoryCache objSiteNavigationFactoryCache = new SiteNavigationFactoryCache(mockDataAccess);
        //    objSiteNavigationFactoryCache.ClientName = "Forethought";
        //    objSiteNavigationFactoryCache.Mode = FactoryCacheMode.All;

        //    //Act
        //    try
        //    {
        //        objSiteNavigationFactoryCache.DeleteEntity(objSiteNavigationKey);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }
        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));

        //    //Assert
        //    //Mock.Assert(mockDataAccess);
        //}
        //#endregion
    }
}
