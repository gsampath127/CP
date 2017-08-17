using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
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
    /// Test class for PageNavigationFactory class
    /// </summary>
    [TestClass]
    public class PageNavigationFactoryTests : BaseTestFactory<PageNavigationObjectModel>
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
            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dSet);


            //Mock.Arrange(() => mockDataAccess.ExecuteDataSet(string.Empty, string.Empty, null))
            // .IgnoreArguments()
            // .Returns(dSet)
            // .MustBeCalled();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_PageNavigationObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_PageNavigationObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_PageNavigationObjectModel()
        {
            //Arrange

            PageNavigationSortDetail objSortDtl = new PageNavigationSortDetail();
            objSortDtl.Column = PageNavigationSortColumn.NavigationKey;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("PageNavigationId", typeof(Int32));
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("CurrentVersion", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("NavigationXML", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForPageNavigationID", typeof(bool));
            dt.Columns.Add("TemplateID", typeof(Int32));
            dt.Columns.Add("TemplateName", typeof(string));
            dt.Columns.Add("PageName", typeof(string));
            dt.Columns.Add("PageDescription", typeof(string));
            DataRow dtrow = dt.NewRow();
            dtrow["PageNavigationId"] = 1;
            dtrow["SiteId"] = 1;
            dtrow["PageId"] = 1;
            dtrow["NavigationKey"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["LanguageCulture"] = 1033;
            dtrow["CurrentVersion"] = 1;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["Version"] = 1;
            dtrow["NavigationXML"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["IsProofing"] = false;
            dtrow["IsProofingAvailableForPageNavigationID"] = false;
            dtrow["TemplateID"] = 3;
            dtrow["TemplateName"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["PageName"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["PageDescription"] = "TaxonomySpecificDocumentFrame_DocumentType";

            dt.Rows.Add(dtrow);
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TemplateId", typeof(Int32));
            dt2.Columns.Add("TemplateName", typeof(string));
            dt2.Columns.Add("PageID", typeof(Int32));
            dt2.Columns.Add("PageName", typeof(string));
            dt2.Columns.Add("PageDescription", typeof(string));

            DataRow dtrow2 = dt2.NewRow();
            dtrow2["TemplateId"] = 1;
            dtrow2["TemplateName"] = "Default";
            dtrow2["PageID"] = 1;
            dtrow2["PageName"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow2["PageDescription"] = "TaxonomySpecificDocumentFrame_DocumentType";

            dt2.Rows.Add(dtrow2);


            ClientData();
            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt)
                .Returns(dt2);




            //Act
            PageNavigationFactoryCache objPageNavigationFactoryCache = new PageNavigationFactoryCache(mockDataAccess.Object);
            objPageNavigationFactoryCache.ClientName = "Forethought";
            objPageNavigationFactoryCache.Mode = FactoryCacheMode.All;
            var result = objPageNavigationFactoryCache.GetAllEntities(0, 0, objSortDtl);
            List<PageNavigationObjectModel> lstExpected = new List<PageNavigationObjectModel>();
            lstExpected.Add(new PageNavigationObjectModel()
            {
                PageNavigationId = 1,
                SiteId = 1,
                PageId = 1,
                NavigationKey = "TaxonomySpecificDocumentFrame_DocumentType",
                Version = 1,
                NavigationXML = "TaxonomySpecificDocumentFrame_DocumentType",
                IsProofing = false,
                IsProofingAvailableForPageNavigationID = false,
                PageName = "TaxonomySpecificDocumentFrame_DocumentType",
                PageDescription = "TaxonomySpecificDocumentFrame_DocumentType"
            });
            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_PageNavigationObjectModel_nullorempty
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_PageNavigationObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_PageNavigationObjectModel_nullorempty()
        {
            //Arrange
            PageNavigationSortDetail objSortDtl = new PageNavigationSortDetail();
            objSortDtl.Column = PageNavigationSortColumn.NavigationKey;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("PageNavigationId", typeof(Int32));
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("CurrentVersion", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("NavigationXML", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForPageNavigationID", typeof(bool));
            dt.Columns.Add("TemplateID", typeof(Int32));
            dt.Columns.Add("TemplateName", typeof(string));
            dt.Columns.Add("PageName", typeof(string));
            dt.Columns.Add("PageDescription", typeof(string));
            DataRow dtrow = dt.NewRow();
            dtrow["PageNavigationId"] = 1;
            dtrow["SiteId"] = 1;
            dtrow["PageId"] = 0;
            dtrow["NavigationKey"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["LanguageCulture"] = 1033;
            dtrow["CurrentVersion"] = 1;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["Version"] = 1;
            dtrow["NavigationXML"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["IsProofing"] = false;
            dtrow["IsProofingAvailableForPageNavigationID"] = false;
            dtrow["TemplateID"] = 3;
            dtrow["TemplateName"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["PageName"] = "";
            dtrow["PageDescription"] = "";


            dt.Rows.Add(dtrow);
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TemplateId", typeof(Int32));
            dt2.Columns.Add("TemplateName", typeof(string));
            dt2.Columns.Add("PageID", typeof(Int32));
            dt2.Columns.Add("PageName", typeof(string));
            dt2.Columns.Add("PageDescription", typeof(string));

            DataRow dtrow2 = dt2.NewRow();
            dtrow2["TemplateId"] = 1;
            dtrow2["TemplateName"] = "Default";
            dtrow2["PageID"] = 0;
            dtrow2["PageName"] = "";
            dtrow2["PageDescription"] = "TaxonomySpecificDocumentFrame_DocumentType";


            ClientData();
            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt).Returns(dt2); ;

            //Act
            PageNavigationFactoryCache objPageNavigationFactoryCache = new PageNavigationFactoryCache(mockDataAccess.Object);
            objPageNavigationFactoryCache.ClientName = "Forethought";
            objPageNavigationFactoryCache.Mode = FactoryCacheMode.All;
            var result = objPageNavigationFactoryCache.GetAllEntities(0, 0, objSortDtl);
            List<PageNavigationObjectModel> lstExpected = new List<PageNavigationObjectModel>();
            lstExpected.Add(new PageNavigationObjectModel()
            {
                PageNavigationId = 1,
                SiteId = 1,
                PageId = 0,
                NavigationKey = "TaxonomySpecificDocumentFrame_DocumentType",
                // LanguageCulture = 1033,
                //  CurrentVersion = 1,
                // UtcLastModified = DateTime.Now,
                //  ModifiedBy = 1,
                Version = 1,
                NavigationXML = "TaxonomySpecificDocumentFrame_DocumentType",
                IsProofing = false,
                IsProofingAvailableForPageNavigationID = false,
                //  TemplateID= 3,
                // TemplateName = "TaxonomySpecificDocumentFrame_DocumentType",
                PageName = "",
                PageDescription = ""
            });
            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            //lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);


            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region GetEntityByKey_Returns_PageNavigationObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_PageNavigationObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_PageNavigationObjectModel()
        //{
        //    //Arrange
        //    PageNavigationKey objPageNavigationKey = new PageNavigationKey(1, 1);


        //    Exception exe = null;

        //    //Act

        //    PageNavigationFactory objPageNavigationFactory = new PageNavigationFactory(mockDataAccess.Object);
        //    try
        //    {
        //        var result = objPageNavigationFactory.GetEntityByKey(objPageNavigationKey);
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

        #region GetEntitiesBySearch_Returns_IEnumerable_Entity
        /// <summary>
        /// GetEntitiesBySearch_Returns_IEnumerable_Entity
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_IEnumerable_Entity()
        {
            //Arrange  
            PageNavigationSearchDetail objSearchDtl = new PageNavigationSearchDetail();
            PageNavigationSortDetail objSortDtl = new PageNavigationSortDetail();

            DataTable dt = new DataTable();
            dt.Columns.Add("PageNavigationId", typeof(Int32));
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("CurrentVersion", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("NavigationXML", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForPageNavigationID", typeof(bool));
            dt.Columns.Add("TemplateID", typeof(Int32));
            dt.Columns.Add("TemplateName", typeof(string));
            dt.Columns.Add("PageName", typeof(string));
            dt.Columns.Add("PageDescription", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["PageNavigationId"] = 1;
            dtrow["SiteId"] = 1;
            dtrow["PageId"] = 3;
            dtrow["NavigationKey"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["LanguageCulture"] = 1033;
            dtrow["CurrentVersion"] = 1;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["Version"] = 1;
            dtrow["NavigationXML"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["IsProofing"] = false;
            dtrow["IsProofingAvailableForPageNavigationID"] = false;
            dtrow["TemplateID"] = 3;
            dtrow["TemplateName"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["PageName"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["PageDescription"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dt.Rows.Add(dtrow);
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TemplateId", typeof(Int32));
            dt2.Columns.Add("TemplateName", typeof(string));
            dt2.Columns.Add("PageID", typeof(Int32));
            dt2.Columns.Add("PageName", typeof(string));
            dt2.Columns.Add("PageDescription", typeof(string));

            DataRow dtrow2 = dt2.NewRow();
            dtrow2["TemplateId"] = 1;
            dtrow2["TemplateName"] = "Default";
            dtrow2["PageID"] = 3;
            dtrow2["PageName"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow2["PageDescription"] = "TaxonomySpecificDocumentFrame_DocumentType";

            dt2.Rows.Add(dtrow2);

            Exception exe = null;
            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt)
              .Returns(dt2);


            //Act
            PageNavigationFactory objPageNavigationFactory = new PageNavigationFactory(mockDataAccess.Object);
            var result = objPageNavigationFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, null);
            List<PageNavigationObjectModel> lstExpected = new List<PageNavigationObjectModel>();
            lstExpected.Add(new PageNavigationObjectModel()
            {
                PageNavigationId = 1,
                SiteId = 1,
                PageId = 3,
                NavigationKey = "TaxonomySpecificDocumentFrame_DocumentType",
                Version = 1,
                NavigationXML = "TaxonomySpecificDocumentFrame_DocumentType",
                IsProofing = false,
                IsProofingAvailableForPageNavigationID = false,
                //  TemplateID= 3,
                // TemplateName = "TaxonomySpecificDocumentFrame_DocumentType",
                PageName = "TaxonomySpecificDocumentFrame_DocumentType",
                PageDescription = "TaxonomySpecificDocumentFrame_DocumentType"
            });
            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            //lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            //Assert
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region GetEntitiesBySearch_Returns_IEnumerable_Entity_pageidzero
        /// <summary>
        /// GetEntitiesBySearch_Returns_IEnumerable_Entity_pageidzero
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_IEnumerable_Entity_pageidzero()
        {
            //Arrange  
            PageNavigationSearchDetail objSearchDtl = new PageNavigationSearchDetail();
            PageNavigationSortDetail objSortDtl = new PageNavigationSortDetail();

            DataTable dt = new DataTable();
            dt.Columns.Add("PageNavigationId", typeof(Int32));
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("LanguageCulture", typeof(Int32));
            dt.Columns.Add("CurrentVersion", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("Version", typeof(Int32));
            dt.Columns.Add("NavigationXML", typeof(string));
            dt.Columns.Add("IsProofing", typeof(bool));
            dt.Columns.Add("IsProofingAvailableForPageNavigationID", typeof(bool));
            dt.Columns.Add("TemplateID", typeof(Int32));
            dt.Columns.Add("TemplateName", typeof(string));
            dt.Columns.Add("PageName", typeof(string));
            dt.Columns.Add("PageDescription", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["PageNavigationId"] = 1;
            dtrow["SiteId"] = 1;
            dtrow["PageId"] = 0;
            dtrow["NavigationKey"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["LanguageCulture"] = 1033;
            dtrow["CurrentVersion"] = 1;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["Version"] = 1;
            dtrow["NavigationXML"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["IsProofing"] = false;
            dtrow["IsProofingAvailableForPageNavigationID"] = false;
            dtrow["TemplateID"] = 3;
            dtrow["TemplateName"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow["PageName"] = "";
            dtrow["PageDescription"] = "";

            dt.Rows.Add(dtrow);
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TemplateId", typeof(Int32));
            dt2.Columns.Add("TemplateName", typeof(string));
            dt2.Columns.Add("PageID", typeof(Int32));
            dt2.Columns.Add("PageName", typeof(string));
            dt2.Columns.Add("PageDescription", typeof(string));

            DataRow dtrow2 = dt2.NewRow();
            dtrow2["TemplateId"] = 1;
            dtrow2["TemplateName"] = "Default";
            dtrow2["PageID"] = 0;
            dtrow2["PageName"] = "";
            dtrow2["PageDescription"] = "TaxonomySpecificDocumentFrame_DocumentType";

            dt2.Rows.Add(dtrow2);

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt)
              .Returns(dt2);

            //Act
            PageNavigationFactory objPageNavigationFactory = new PageNavigationFactory(mockDataAccess.Object);
            var result = objPageNavigationFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, null);
            List<PageNavigationObjectModel> lstExpected = new List<PageNavigationObjectModel>();
            lstExpected.Add(new PageNavigationObjectModel()
            {
                PageNavigationId = 1,
                SiteId = 1,
                PageId = 0,
                NavigationKey = "TaxonomySpecificDocumentFrame_DocumentType",
                // LanguageCulture = 1033,
                //  CurrentVersion = 1,
                // UtcLastModified = DateTime.Now,
                //  ModifiedBy = 1,
                Version = 1,
                NavigationXML = "TaxonomySpecificDocumentFrame_DocumentType",
                IsProofing = false,
                IsProofingAvailableForPageNavigationID = false,
                //  TemplateID= 3,
                // TemplateName = "TaxonomySpecificDocumentFrame_DocumentType",
                PageName = "",
                PageDescription = ""
            });
            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
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
        //    PageNavigationSearchDetail objSearchDtl = new PageNavigationSearchDetail();
        //    PageNavigationKey objPageNavigationKey = new PageNavigationKey(1, 1);

        //  //  ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    PageNavigationFactory objPageNavigationFactory = new PageNavigationFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objPageNavigationFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objPageNavigationKey);
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

        #region SaveEntity_WithModifiedBy
        /// <summary>
        /// SaveEntity_WithModifiedBy
        /// </summary>
        [TestMethod]
        public void SaveEntity_WithModifiedBy()
        {
            //Arrange
            PageNavigationObjectModel objObjectModel = new PageNavigationObjectModel();
            objObjectModel.PageNavigationId = 1;
            objObjectModel.PageId = 1;
            objObjectModel.NavigationKey = "Test";
            objObjectModel.IsProofing = false;
            objObjectModel.ModifiedBy = 1;
            objObjectModel.NavigationXML = "Test";
            objObjectModel.Version = 1;
            objObjectModel.Text = "Test";
            objObjectModel.PageName = "Test";
            objObjectModel.PageDescription = "Test";
            objObjectModel.IsProofingAvailableForPageNavigationID = false;

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="PageNavigationId", Value=1 },
                new SqlParameter(){ ParameterName="PageId", Value=1 },
                new SqlParameter(){ ParameterName="NavigationKey", Value= "TEST" },
                 new SqlParameter(){ ParameterName="NavigationXML", Value="TEST"},
                new SqlParameter(){ ParameterName="Version", Value=1 },
                new SqlParameter(){ ParameterName="Text", Value="Test" },
                 new SqlParameter(){ ParameterName="PageName", Value="Test" },
                  new SqlParameter(){ ParameterName="Text", Value="Test" },
                new SqlParameter(){ ParameterName="PageDescription", Value=false},
                  new SqlParameter(){ ParameterName="IsProofingAvailableForPageNavigationID", Value=false},
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
                .Returns(parameters[8])
                .Returns(parameters[9])
                .Returns(parameters[10]);
            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            PageNavigationFactory objPageNavigationFactory = new PageNavigationFactory(mockDataAccess.Object);
            objPageNavigationFactory.SaveEntity(objObjectModel, 1);

            //Assert
            mockDataAccess.VerifyAll();
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
        //    ClientData();
        //    Exception exe = null;
        //    PageNavigationKey objPageNavigationKey = new PageNavigationKey(1, 1);
        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));

        //    //Act
        //    PageNavigationFactoryCache objPageNavigationFactoryCache = new PageNavigationFactoryCache(mockDataAccess.Object);
        //    objPageNavigationFactoryCache.ClientName = "Forethought";
        //    objPageNavigationFactoryCache.Mode = FactoryCacheMode.All;
        //    try
        //    {
        //        objPageNavigationFactoryCache.DeleteEntity(objPageNavigationKey);
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

        #region DeleteEntity
        /// <summary>
        /// DeleteEntity
        /// </summary>
        [TestMethod]
        public void DeleteEntity()
        {
            //Arrange
            PageNavigationObjectModel objObjectModel = new PageNavigationObjectModel();
            objObjectModel.PageNavigationId = 1;
            objObjectModel.Version = 1;
            objObjectModel.IsProofing = false;
            var parameters = new[]
            { new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                new SqlParameter(){ ParameterName="PageNavigationId", Value=1 },
                new SqlParameter(){ ParameterName="Version", Value=1 },
                new SqlParameter(){ ParameterName="IsProofing", Value=false},
               
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3]);



            PageNavigationFactory objPageNavigationFactory = new PageNavigationFactory(mockDataAccess.Object);
            objPageNavigationFactory.DeleteEntity(objObjectModel);
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
            PageNavigationFactory objFactory = new PageNavigationFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<PageNavigationObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion
    }


}
