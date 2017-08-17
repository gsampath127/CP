using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.SearchEntities.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRD.FSG.RP.Model.Factories;
using System.Web.Script.Serialization;
using System.Data.Common;
using System.Data.SqlClient;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for SiteActivityFactory class
    /// </summary>
    [TestClass]
    public class SiteActivityFactoryTests : BaseTestFactory<SiteActivityObjectModel>
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

        #region GetAllEntities_Returns_Ienumerable_SiteActivityObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_SiteActivityObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_SiteActivityObjectModel()
        {
            //Arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("SiteActivityId", typeof(Int32));
            dt.Columns.Add("ClientIPAddress", typeof(string));
            dt.Columns.Add("UserAgentString", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteActivityId"] = 53;
            dtrow["ClientIPAddress"] = "Test";
            dtrow["UserAgentString"] = "Test";
            dtrow["Name"] = 2;
            dtrow["Description"] = 1;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dt.Rows.Add(dtrow);


            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);

            //Act
            SiteActivityFactory objSiteActivityFactory = new SiteActivityFactory(mockDataAccess.Object);
            var result = objSiteActivityFactory.GetAllEntities<SiteActivityObjectModel>(0, 0);

            List<SiteActivityObjectModel> lstExpected = new List<SiteActivityObjectModel>();
            lstExpected.Add(new SiteActivityObjectModel()
            {
                BadRequestIssue = 0,
                BadRequestIssueDescription = null,
                BadRequestParameterName = null,
                BadRequestParameterValue = null,
                Click = 0,
                ClientDocumentGroupId = null,
                ClientDocumentId = null,
                ClientIPAddress = "Test",
                DocumentType = null,
                DocumentTypeExternalID = null,
                DocumentTypeId = null,
                DocumentTypeMarketId = null,
                HTTPMethod = null,
                InitDoc = false,
                Level = null,
                PageId = null,
                ParsedRequestUriString = null,
                ReferrerUriString = null,
                RequestUriString = null,
                ServerName = null,
                SiteActivityId = 53,
                SiteId = 0,
                SiteName = null,
                TaxonomyAssociationGroupId = null,
                TaxonomyAssociationId = null,
                TaxonomyExternalId = null,
                UserAgentString = "Test",
                UserId = null,
                XBRLDocumentName = null,
                XBRLItemType = 0,
                Name = null,
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllSiteAcitivityForReportbyDate_Returns_IEnumerable_SiteActivityObjectModel
        /// <summary>
        /// GetAllSiteAcitivityForReportbyDate_Returns_IEnumerable_SiteActivityObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllSiteAcitivityForReportbyDate_Returns_IEnumerable_SiteActivityObjectModel()
        {
            //Arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("SiteActivityId", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("Click", typeof(Int32));
            dt.Columns.Add("RequestBatchId", typeof(Guid));
            dt.Columns.Add("UriString", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("RequestUtcDate", typeof(DateTime));
            dt.Columns.Add("ClientIPAddress", typeof(string));
            dt.Columns.Add("TaxonomyAssociationGroupId", typeof(Int32));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("HeaderText", typeof(string));
            dt.Columns.Add("ClientDocumentId", typeof(Int32));
            dt.Columns.Add("MarketId", typeof(Int32));
            dt.Columns.Add("InitDoc", typeof(Int32));
            dt.Columns.Add("ClientDocumentGroupId", typeof(Int32));


            DataRow dtrow = dt.NewRow();
            dtrow["SiteActivityId"] = 1;
            dtrow["SiteName"] = "Test";
            dtrow["SiteId"] = 1;
            dtrow["Click"] = 1;
            dtrow["MarketId"] = 1;
            dtrow["RequestBatchId"] = Guid.Empty;
            dtrow["UriString"] = "TAL";
            dtrow["TaxonomyAssociationId"] = 1;
            dtrow["NameOverride"] = "Default";
            dtrow["RequestUtcDate"] = DateTime.MinValue;
            dtrow["ClientIPAddress"] = "TAL";
            dtrow["TaxonomyAssociationGroupId"] = 1;
            dtrow["DocumentTypeId"] = 1;
            dtrow["HeaderText"] = "TAL";
            dtrow["ClientDocumentId"] = 1;
            dtrow["InitDoc"] = 1;
            dtrow["ClientDocumentGroupId"] = 1;
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
            SiteActivityFactory objSiteActivityFactory = new SiteActivityFactory(mockDataAccess.Object);
            objSiteActivityFactory.ClientName = "Forethought";
            var result = objSiteActivityFactory.GetAllSiteAcitivityForReportbyDate(DateTime.Now, DateTime.Now.AddDays(1));

            List<SiteActivityObjectModel> lstExpected = new List<SiteActivityObjectModel>();
            lstExpected.Add(new SiteActivityObjectModel()
            {
                BadRequestIssue = 0,
                BadRequestIssueDescription = null,
                BadRequestParameterName = null,
                BadRequestParameterValue = null,
                Click = 1,
                ClientDocumentGroupId = null,
                ClientDocumentId = null,
                ClientIPAddress = "TAL",
                DocumentType = "TAL",
                DocumentTypeExternalID = null,
                DocumentTypeId = 1,
                DocumentTypeMarketId = "1",
                HTTPMethod = null,
                InitDoc = true,
                Level = null,
                PageId = null,
                ParsedRequestUriString = null,
                ReferrerUriString = "TAL",
                RequestUriString = null,
                RequestUtcDate = DateTime.MinValue,
                RequestBatchId = Guid.Empty,
                ServerName = null,
                SiteActivityId = 0,
                SiteId = 1,
                SiteName = "Test",
                TaxonomyAssociationGroupId = 1,
                TaxonomyAssociationId = 1,
                TaxonomyExternalId = null,
                UserAgentString = null,
                UserId = null,
                XBRLDocumentName = null,
                XBRLItemType = 0,
                Name = "Default",
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllSiteAcitivityForReportbyDate_WithEmptyDatatable_Returns_IEnumerable_SiteActivityObjectModel
        /// <summary>
        /// GetAllSiteAcitivityForReportbyDate_Returns_IEnumerable_SiteActivityObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllSiteAcitivityForReportbyDate_WithEmptyDatatable_Returns_IEnumerable_SiteActivityObjectModel()
        {
            //Arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("SiteActivityId", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("Click", typeof(Int32));
            dt.Columns.Add("RequestBatchId", typeof(Guid));
            dt.Columns.Add("UriString", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("RequestUtcDate", typeof(DateTime));
            dt.Columns.Add("ClientIPAddress", typeof(string));
            dt.Columns.Add("TaxonomyAssociationGroupId", typeof(Int32));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("HeaderText", typeof(string));
            dt.Columns.Add("ClientDocumentId", typeof(Int32));
            dt.Columns.Add("MarketId", typeof(Int32));
            dt.Columns.Add("InitDoc", typeof(Int32));
            dt.Columns.Add("ClientDocumentGroupId", typeof(Int32));


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
            SiteActivityFactory objSiteActivityFactory = new SiteActivityFactory(mockDataAccess.Object);
            objSiteActivityFactory.ClientName = "Forethought";
            var result = objSiteActivityFactory.GetAllSiteAcitivityForReportbyDate(DateTime.Now, DateTime.Now.AddDays(1));

            ValidateEmptyData(result);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region SaveEntity
        /// <summary>
        /// SaveEntity
        /// </summary>
        [TestMethod]
        public void SaveEntity()
        {
            //Arrange
            SiteActivityObjectModel objObjectModel = new SiteActivityObjectModel();
            objObjectModel.SiteName = "Test";
            objObjectModel.ClientIPAddress = "Test";
            objObjectModel.UserAgentString = "Test";
            objObjectModel.HTTPMethod = "Test";
            objObjectModel.RequestUriString = "Test";
            objObjectModel.ParsedRequestUriString = "Test";
            objObjectModel.ServerName = "Test";
            objObjectModel.ReferrerUriString = "Test";
            objObjectModel.InitDoc = false;
            objObjectModel.RequestBatchId = Guid.NewGuid();
            objObjectModel.UserId = 1;
            objObjectModel.PageId = 1;
            objObjectModel.Level = 1;
            objObjectModel.DocumentTypeExternalID = "Test";
            objObjectModel.TaxonomyExternalId = "Test";
            objObjectModel.TaxonomyAssociationGroupId = 1;
            objObjectModel.TaxonomyAssociationId = 1;
            objObjectModel.DocumentTypeId = 1;
            objObjectModel.SiteActivityId = 1;
            objObjectModel.ClientDocumentId = 1;
            objObjectModel.XBRLDocumentName = "Test";
            objObjectModel.XBRLItemType = 1;
            objObjectModel.BadRequestIssue = 1;
            objObjectModel.BadRequestParameterName = "Test";
            objObjectModel.BadRequestParameterValue = "Test";
            objObjectModel.DocumentTypeMarketId = "1";

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="SiteName", Value="Test" },
                new SqlParameter(){ ParameterName="ClientIPAddress", Value="Test" },
                new SqlParameter(){ ParameterName="UserAgentString", Value="Test" },
                new SqlParameter(){ ParameterName="HTTPMethod", Value="Test" },
                new SqlParameter(){ ParameterName="RequestUriString", Value="Test" },
                new SqlParameter(){ ParameterName="ParsedRequestUriString", Value="TEST" },
                new SqlParameter(){ ParameterName="ServerName", Value="Test" },
                new SqlParameter(){ ParameterName="ReferrerUriString", Value="Test" },
                new SqlParameter(){ ParameterName="UserId", Value=false },
                new SqlParameter(){ ParameterName="PageId", Value=1 },
                new SqlParameter(){ ParameterName="Level", Value=1 },
                new SqlParameter(){ ParameterName="DocumentTypeExternalId", Value="Test" },
                new SqlParameter(){ ParameterName="TaxonomyExternalId", Value="TEST" },
                new SqlParameter(){ ParameterName="TaxonomyAssociationGroupId", Value=1 },
                new SqlParameter(){ ParameterName="TaxonomyAssociationId", Value=1 },
                new SqlParameter(){ ParameterName="DocumentTypeId", Value=1 },
                new SqlParameter(){ ParameterName="ClientDocumentGroupId", Value=1 },
                new SqlParameter(){ ParameterName="ClientDocumentId", Value=1 },
                new SqlParameter(){ ParameterName="RequestBatchId", Value= Guid.Empty },
                new SqlParameter(){ ParameterName="InitDoc", Value=false },
                new SqlParameter(){ ParameterName="XBRLDocumentName", Value="Test" },
                new SqlParameter(){ ParameterName="XBRLItemType", Value=1 },
                new SqlParameter(){ ParameterName="BadRequestIssue", Value=1 },
                new SqlParameter(){ ParameterName="BadRequestParameterName", Value="Test" },
                new SqlParameter(){ ParameterName="BadRequestParameterValue", Value="Test" },
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
                .Returns(parameters[10])
                .Returns(parameters[11])
                .Returns(parameters[12])
                .Returns(parameters[13])
                .Returns(parameters[14])
                .Returns(parameters[15])
                .Returns(parameters[16])
                .Returns(parameters[17])
                .Returns(parameters[18])
                .Returns(parameters[19])
                .Returns(parameters[20])
                .Returns(parameters[21])
                .Returns(parameters[22])
                .Returns(parameters[23])
                .Returns(parameters[24]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            SiteActivityFactory objSiteActivityFactory = new SiteActivityFactory(mockDataAccess.Object);
            objSiteActivityFactory.SaveEntity(objObjectModel);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region GetEntityByKey_Returns_SiteActivityObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_SiteActivityObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_SiteActivityObjectModel()
        //{
        //    //Arrange
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    SiteActivityFactory objSiteActivityFactory = new SiteActivityFactory(mockDataAccess);
        //    try
        //    {
        //        objSiteActivityFactory.GetEntityByKey(1);
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

        //#region SaveEntity_With_ModifiedBy
        ///// <summary>
        ///// SaveEntity_With_ModifiedBy
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_With_ModifiedBy()
        //{
        //    //Arrange
        //    SiteActivityObjectModel objObjectModel = new SiteActivityObjectModel();
        //    Exception exe = null;
        //    ClientData();

        //    Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //      .IgnoreArguments()
        //      .MustBeCalled();

        //    //Act
        //    SiteActivityFactory objSiteActivityFactory = new SiteActivityFactory(mockDataAccess);          
        //    try
        //    {
        //        objSiteActivityFactory.SaveEntity(objObjectModel, 1);
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
        //    Exception exe = null;
        //    ClientData();

        //    SiteActivityFactory objSiteActivityFactory = new SiteActivityFactory(mockDataAccess);

        //    //Act
        //    try
        //    {
        //        objSiteActivityFactory.DeleteEntity(1);
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

        //#region DeleteEntity_WithKey_ModifiedBy
        ///// <summary>
        ///// DeleteEntity_WithKey_ModifiedBy
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_WithKey_ModifiedBy()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    ClientData();

        //    SiteActivityFactory objSiteActivityFactory = new SiteActivityFactory(mockDataAccess);

        //    //Act
        //    try
        //    {
        //        objSiteActivityFactory.DeleteEntity(1, 1);
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
        //    Exception exe = null;
        //    ClientData();
        //    SiteActivityObjectModel objObjectModel = new SiteActivityObjectModel();
        //    SiteActivityFactory objSiteActivityFactory = new SiteActivityFactory(mockDataAccess);

        //    //Act
        //    try
        //    {
        //        objSiteActivityFactory.DeleteEntity(objObjectModel);
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

        //#region DeleteEntity_With_Entity_ModifiedBy
        ///// <summary>
        ///// DeleteEntity_With_Entity_ModifiedBy
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Entity_ModifiedBy()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    ClientData();
        //    SiteActivityObjectModel objObjectModel = new SiteActivityObjectModel();
        //    SiteActivityFactory objSiteActivityFactory = new SiteActivityFactory(mockDataAccess);

        //    //Act
        //    try
        //    {
        //        objSiteActivityFactory.DeleteEntity(objObjectModel, 1);
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
            SiteActivitySearchDetail objSearchDtl = new SiteActivitySearchDetail();

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteActivityId", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("RequestBatchId", typeof(Guid));
            dt.Columns.Add("UriString", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("RequestUtcDate", typeof(DateTime));
            dt.Columns.Add("ClientIPAddress", typeof(string));
            dt.Columns.Add("TaxonomyAssociationGroupId", typeof(Int32));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("HeaderText", typeof(string));
            dt.Columns.Add("ClientDocumentId", typeof(Int32));
            dt.Columns.Add("InitDoc", typeof(Int32));
            dt.Columns.Add("ClientDocumentGroupId", typeof(Int32));
            dt.Columns.Add("Click", typeof(Int32));
            dt.Columns.Add("MarketId", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteActivityId"] = 1;
            dtrow["SiteName"] = "Test";
            dtrow["SiteId"] = 1;
            dtrow["Click"] = 1;
            dtrow["MarketId"] = 1;
            dtrow["RequestBatchId"] = Guid.Empty;
            dtrow["UriString"] = "TAL";
            dtrow["TaxonomyAssociationId"] = 1;
            dtrow["NameOverride"] = "Default";
            dtrow["RequestUtcDate"] = DateTime.MinValue;
            dtrow["ClientIPAddress"] = "TAL";
            dtrow["TaxonomyAssociationGroupId"] = 1;
            dtrow["DocumentTypeId"] = 1;
            dtrow["HeaderText"] = "TAL";
            dtrow["ClientDocumentId"] = 1;
            dtrow["InitDoc"] = 1;
            dtrow["ClientDocumentGroupId"] = 1;
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
            SiteActivityFactory objSiteActivityFactory = new SiteActivityFactory(mockDataAccess.Object);
            objSiteActivityFactory.ClientName = "Forethought";
            var result = objSiteActivityFactory.GetEntitiesBySearch(0, 0, objSearchDtl, null, null);

            List<SiteActivityObjectModel> lstExpected = new List<SiteActivityObjectModel>();
            lstExpected.Add(new SiteActivityObjectModel()
            {
                BadRequestIssue = 0,
                BadRequestIssueDescription = null,
                BadRequestParameterName = null,
                BadRequestParameterValue = null,
                Click = 1,
                ClientDocumentGroupId = null,
                ClientDocumentId = null,
                ClientIPAddress = "TAL",
                DocumentType = "TAL",
                DocumentTypeExternalID = null,
                DocumentTypeId = 1,
                DocumentTypeMarketId = "1",
                HTTPMethod = null,
                InitDoc = true,
                Level = null,
                PageId = null,
                ParsedRequestUriString = null,
                ReferrerUriString = "TAL",
                RequestUriString = null,
                RequestUtcDate = DateTime.MinValue,
                RequestBatchId = Guid.Empty,
                ServerName = null,
                SiteActivityId = 0,
                SiteId = 1,
                SiteName = "Test",
                TaxonomyAssociationGroupId = 1,
                TaxonomyAssociationId = 1,
                TaxonomyExternalId = null,
                UserAgentString = null,
                UserId = null,
                XBRLDocumentName = null,
                XBRLItemType = 0,
                Name = "Default",
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
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
            SiteActivityFactory objFactory = new SiteActivityFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<SiteActivityObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion
    }
}
