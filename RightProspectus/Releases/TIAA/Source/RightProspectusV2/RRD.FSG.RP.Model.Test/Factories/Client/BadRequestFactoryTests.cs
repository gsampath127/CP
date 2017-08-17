using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Model.SortDetail.System;
using RRD.FSG.RP.Model.SearchEntities.VerticalMarkets;
using System.Data.Common;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    [TestClass]
    public class BadRequestFactoryTests : BaseTestFactory<SiteActivityObjectModel>
    {
        Mock<IDataAccess> mockDataAccess;

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

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>())).Returns(dSet);

        }
        #endregion

        #region GetAllBadRequestsForReportbyDate_Returns_Ienumerable_SiteActivityObjectModel
        /// <summary>
        /// GetAllBadRequestsForReportbyDate_Returns_Ienumerable_SiteActivityObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllBadRequestsForReportbyDate_Returns_Ienumerable_SiteActivityObjectModel()
        {
            //Arrange
            SiteActivityObjectModel objSiteactivityobjectmodel = new SiteActivityObjectModel();
            objSiteactivityobjectmodel.InitDoc = true;
            objSiteactivityobjectmodel.UserId = 3;
            objSiteactivityobjectmodel.BadRequestIssue = 42;
            objSiteactivityobjectmodel.BadRequestParameterName = "Param1";
            objSiteactivityobjectmodel.BadRequestIssueDescription = "TestingForBadrequest";

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteActivityId", typeof(Int32));
            dt.Columns.Add("BadRequestParameterName", typeof(string));
            dt.Columns.Add("BadRequestParameterValue", typeof(string));
            dt.Columns.Add("RequestUtcDate", typeof(DateTime));
            dt.Columns.Add("ClientIPAddress", typeof(string));
            dt.Columns.Add("UserAgentString", typeof(string));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("BadRequestIssue", typeof(int));
            dt.Columns.Add("UriString", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteActivityId"] = 1;
            dtrow["BadRequestParameterName"] = "Test_1";
            dtrow["BadRequestParameterValue"] = "34628P193";
            dtrow["RequestUtcDate"] = DateTime.Today;
            dtrow["ClientIPAddress"] = "10.20.300.30";
            dtrow["UserAgentString"] = "S_test2";
            dtrow["SiteName"] = "Site_Test";
            dtrow["BadRequestIssue"] = 3;
            dtrow["UriString"] = "Test_URI";

            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            ClientData();
            //Act
            BadRequestFactory objBadRequestFactory = new BadRequestFactory(mockDataAccess.Object);
            objBadRequestFactory.ClientName = "Forethought";

            var result = objBadRequestFactory.GetAllBadRequestsForReportbyDate(DateTime.Now, DateTime.Now);
            List<SiteActivityObjectModel> lstExpected = new List<SiteActivityObjectModel>();
            lstExpected.Add(new SiteActivityObjectModel()
            {
                SiteActivityId = 1,

                BadRequestParameterName = "Test_1",
                BadRequestIssueDescription = "InvalidXBRLTaxonomyLevelExternalID",
                BadRequestParameterValue = "34628P193",
                ReferrerUriString = "Test_URI",
                RequestUtcDate = DateTime.Today,
                ClientIPAddress = "10.20.300.30",
                UserAgentString = "S_test2",
                SiteName = "Site_Test",
                BadRequestIssue = 3,

            });
            ValidateListData(lstExpected, result.ToList());
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_SiteActivityObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_SiteActivityObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable()
        {
            // Arrange

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteActivityId", typeof(Int32));
            dt.Columns.Add("RequestIssue", typeof(Int32));
            dt.Columns.Add("BadRequestParameterName", typeof(string));
            dt.Columns.Add("BadRequestIssueDescription", typeof(string));
            dt.Columns.Add("BadRequestParameterValue", typeof(string));
            dt.Columns.Add("ReferrerUriString", typeof(string));
            dt.Columns.Add("RequestUtcDate", typeof(DateTime));
            dt.Columns.Add("ClientIPAddress", typeof(string));
            dt.Columns.Add("UserAgentString", typeof(string));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("BadRequestIssue", typeof(int));
            dt.Columns.Add("UriString", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteActivityId"] = 1;
            dtrow["RequestIssue"] = 3;
            dtrow["BadRequestParameterName"] = "Test_1";
            dtrow["BadRequestIssueDescription"] = "InvalidXBRLTaxonomyLevelExternalID";
            dtrow["BadRequestParameterValue"] = "34628P193";
            dtrow["ReferrerUriString"] = "34628P193";
            dtrow["RequestUtcDate"] = DateTime.MinValue;
            dtrow["ClientIPAddress"] = "10.20.300.30";
            dtrow["UserAgentString"] = "S_test2";
            dtrow["SiteName"] = null;
            dtrow["BadRequestIssue"] = 3;
            dtrow["UriString"] = null;

            dt.Rows.Add(dtrow);

            //Act
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            BadRequestFactory objBadRequestFactory = new BadRequestFactory(mockDataAccess.Object);
            var result = objBadRequestFactory.GetAllEntities(0, 0);
            //  Assert and Verify
            List<SiteActivityObjectModel> lstExpected = new List<SiteActivityObjectModel>();
            lstExpected.Add(new SiteActivityObjectModel()
            {
                SiteActivityId = 1,

                BadRequestParameterName = "Test_1",
                BadRequestIssueDescription = null,
                BadRequestParameterValue = "34628P193",
                ReferrerUriString = null,
                RequestUtcDate = DateTime.MinValue,
                ClientIPAddress = "10.20.300.30",
                UserAgentString = "S_test2",
                SiteName = null,
                BadRequestIssue = 3,

            });
            ValidateListData(lstExpected, result.ToList());
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetEntitiesBySearch_with_ModifyBy
        ///   <summary>
        ///   SaveEntity_with_ModifyBy
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_with_ModifyBy()
        {
            //  Arrange  
            SiteActivityObjectModel objSiteactivitysearchtmodel = new SiteActivityObjectModel();
            objSiteactivitysearchtmodel.UserId = 1;
            objSiteactivitysearchtmodel.SiteName = "Test1";
            SiteActivitySearchDetail objsearchdetails = new SiteActivitySearchDetail();
            DataTable dt = new DataTable();
            dt.Columns.Add("SiteActivityId", typeof(Int32));
            dt.Columns.Add("BadRequestParameterName", typeof(string));
            dt.Columns.Add("BadRequestParameterValue", typeof(string));
            dt.Columns.Add("RequestUtcDate", typeof(DateTime));
            dt.Columns.Add("ClientIPAddress", typeof(string));
            dt.Columns.Add("UserAgentString", typeof(string));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("BadRequestIssue", typeof(int));
            dt.Columns.Add("UriString", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["SiteActivityId"] = 1;
            dtrow["BadRequestParameterName"] = "Test_1";
            dtrow["BadRequestParameterValue"] = "34628P193";
            dtrow["RequestUtcDate"] = DateTime.Today;
            dtrow["ClientIPAddress"] = "10.20.300.30";
            dtrow["UserAgentString"] = "S_test2";
            dtrow["SiteName"] = "Site_Test";
            dtrow["BadRequestIssue"] = 3;
            dtrow["UriString"] = "Test_URI";

            dt.Rows.Add(dtrow);
            ClientData();


            //   Act
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            BadRequestFactory objBadRequestFactory = new BadRequestFactory(mockDataAccess.Object);
            objBadRequestFactory.ClientName = "Forethought";
            var result = objBadRequestFactory.GetEntitiesBySearch(0, 0, objsearchdetails);
            List<SiteActivityObjectModel> lstExpected = new List<SiteActivityObjectModel>();
            lstExpected.Add(new SiteActivityObjectModel()
            {
                SiteActivityId = 1,

                BadRequestParameterName = "Test_1",
                BadRequestIssueDescription = "InvalidXBRLTaxonomyLevelExternalID",
                BadRequestParameterValue = "34628P193",
                ReferrerUriString = "Test_URI",
                RequestUtcDate = DateTime.Today,
                ClientIPAddress = "10.20.300.30",
                UserAgentString = "S_test2",
                SiteName = "Site_Test",
                BadRequestIssue = 3,

            });
            ValidateListData(lstExpected, result.ToList());

            // Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region SaveEntity_with_ModifyBy
        ///// <summary>
        ///// SaveEntity_with_ModifyBy
        /////</summary>
        //[TestMethod]
        //public void SaveEntity_with_ModifyBy()
        //{
        //    //  Arrange  
        //    SiteActivityObjectModel objSiteactivitysearchtmodel = new SiteActivityObjectModel();
        //    objSiteactivitysearchtmodel.UserId = 1;
        //    objSiteactivitysearchtmodel.SiteName = "Test1";


        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    BadRequestFactory objBadRequestFactory = new BadRequestFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objBadRequestFactory.SaveEntity(objSiteactivitysearchtmodel, 32);
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

        //#region Delete_with_Key
        /////  <summary>
        ///// Delete_with_Key
        ///// </summary>
        //[TestMethod]
        //public void Delete_with_Key()
        //{
        //    //   Arrange  

        //    ClientData();
        //    Exception exe = null; ;

        //    //   Act
        //    BadRequestFactory objBadRequestFactory = new BadRequestFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objBadRequestFactory.DeleteEntity(2);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //   Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion

        //#region Delete_with_ModifyBy
        ///// <summary>
        ///// Delete_with_ModifyBy
        ///// </summary>
        //[TestMethod]
        //public void Delete_with_ModifyBy()
        //{
        //    // Arrange  

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    BadRequestFactory objBadRequestFactory = new BadRequestFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objBadRequestFactory.DeleteEntity(2, 32);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    // Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion

        //#region Delete_Returns_SiteActivityObjectModel
        /////  <summary>
        /////  Delete_Returns_SiteActivityObjectModel
        /////  </summary>
        //[TestMethod]
        //public void Delete_Returns_SiteActivityObjectModel()
        //{
        //    //     Arrange  

        //    SiteActivityObjectModel objSiteactivitysearchtmodel = new SiteActivityObjectModel();
        //    objSiteactivitysearchtmodel.UserId = 1;
        //    objSiteactivitysearchtmodel.SiteName = "Test1";


        //    ClientData();
        //    Exception exe = null; ;

        //    //  Act
        //    BadRequestFactory objBadRequestFactory = new BadRequestFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objBadRequestFactory.DeleteEntity(objSiteactivitysearchtmodel);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //  Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion

        //#region DeleteBadRequestFactory_with_DeleteBy
        /////  <summary>
        /////   DeleteBadRequestFactory_with_DeleteBy
        /////  </summary>
        //[TestMethod]
        //public void DeleteBadRequestFactory_with_DeleteBy()
        //{
        //    //   Arrange  

        //    SiteActivityObjectModel objSiteactivitysearchtmodel = new SiteActivityObjectModel();
        //    objSiteactivitysearchtmodel.UserId = 1;
        //    objSiteactivitysearchtmodel.SiteName = "Test1";


        //    ClientData();
        //    Exception exe = null; ;

        //    //  Act
        //    BadRequestFactory objBadRequestFactory = new BadRequestFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objBadRequestFactory.DeleteEntity(objSiteactivitysearchtmodel, 4);
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

        //#region GetEntityByKey_with_Key
        /////  <summary>
        ///// GetEntityByKey_with_Key
        /////  </summary>
        //[TestMethod]
        //public void GetEntityByKey_with_Key()
        //{
        //    //  Arrange  

        //    ClientData();
        //    Exception exe = null; ;

        //    //  Act
        //    BadRequestFactory objBadRequestFactory = new BadRequestFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objBadRequestFactory.GetEntityByKey(2);
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
            BadRequestFactory objFactory = new BadRequestFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<SiteActivityObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion



    }
}
