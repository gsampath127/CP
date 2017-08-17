using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.SqlClient;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for UrlRewriteFactory class
    /// </summary>
    [TestClass]
    public class SiteAdministrationFactoryTests : BaseTestFactory<SiteAdministrationObjectModel>
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

        #region GetAllEntityByEntitySearch_Returns_IEnumerable_SiteAdministrationObjectModel
        /// <summary>
        /// GetAllEntityByEntitySearch_Returns_IEnumerable_SiteAdministrationObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntityByEntitySearch_Returns_IEnumerable_SiteAdministrationObjectModel()
        {
            //Arrange
            SiteAdministrationObjectSearchModel objSearchModel = new SiteAdministrationObjectSearchModel();
            objSearchModel.ClientName = "Forethought";
            objSearchModel.SiteID = 1;
            objSearchModel.SiteName = "Test";
            objSearchModel.TemplateName = "Test";
            objSearchModel.DefaultPageName = "Test";
            objSearchModel.ParentSiteID = 1;
            objSearchModel.Description = "Test";
            objSearchModel.PageSize = 10;
            objSearchModel.PageIndex = 1;
            objSearchModel.SortDirection = "Test";
            objSearchModel.SortColumn = "Test";

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TemplateName", typeof(Int32));
            dt.Columns.Add("DefaultPageName", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));


            DataRow dtrow = dt.NewRow();
            dtrow["SiteID"] = 1;
            dtrow["SiteName"] = "Default";
            dtrow["TemplateName"] = 1;
            dtrow["DefaultPageName"] = 1;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
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

            var parameters = new[]
            { 
               
                new SqlParameter(){ ParameterName="DBCSiteId", Value=1 },   
                new SqlParameter(){ ParameterName="DBCSiteName", Value="Test_name" }, 
                new SqlParameter(){ ParameterName="DBCTemplateId", Value="Test_temp" }, 
                new SqlParameter(){ ParameterName="DBCDefaultPageId", Value="test_dpage" }, 
                new SqlParameter(){ ParameterName="DBCParentSiteId", Value=1 }, 
                new SqlParameter(){ ParameterName="DBCDescription", Value="test_desc" }, 
                new SqlParameter(){ ParameterName="DBCUtcModifiedDate", Value=DateTime.Now }, 
                new SqlParameter(){ ParameterName="DBCModifiedBy", Value="32" }, 
                new SqlParameter(){ ParameterName="DBCPageSize", Value=10 }, 
                new SqlParameter(){ ParameterName="DBCPageIndex", Value=0 }, 
                new SqlParameter(){ ParameterName="DBCSortDirection", Value="test_sort" }, 
                new SqlParameter(){ ParameterName="DBCSortColumn", Value="test_sortcolumn" }, 
            };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
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
                .Returns(parameters[11]);
                     
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            //Act
            SiteAdministrationFactory objSiteAdministrationFactory = new SiteAdministrationFactory(mockDataAccess.Object);
            var result = objSiteAdministrationFactory.GetAllEntityByEntitySearch(objSearchModel);

            List<SiteAdministrationObjectModel> ListExpected = new List<SiteAdministrationObjectModel>();
            ListExpected.Add(new SiteAdministrationObjectModel
                {
                    SiteID = 1,
                    SiteName = "Default",
                    DefaultPageID = 1,
                    Description = null,
                    ParentSiteID = 0,
                    TemplateID = 1


                });
            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("LastModified");
            lstExclude.Add("Key");
            ValidateListData(ListExpected, result.ToList(), lstExclude);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetAllEntityByEntitySearch_Returns_IEnumerable_Empty_DataTable
        /// <summary>
        /// GetAllEntityByEntitySearch_Returns_IEnumerable_Empty_DataTable
        /// </summary>
        [TestMethod]
        public void GetAllEntityByEntitySearch_Returns_IEnumerable_Empty_DataTable()
        {
            //Arrange
            SiteAdministrationObjectSearchModel objSearchModel = new SiteAdministrationObjectSearchModel();
            objSearchModel.ClientName = "Forethought";
            objSearchModel.SiteID = 1;
            objSearchModel.SiteName = "Test";
            objSearchModel.TemplateName = "Test";
            objSearchModel.DefaultPageName = "Test";
            objSearchModel.ParentSiteID = 1;
            objSearchModel.Description = "Test";
            objSearchModel.PageSize = 10;
            objSearchModel.PageIndex = 1;
            objSearchModel.SortDirection = "Test";
            objSearchModel.SortColumn = "Test";

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteID", typeof(Int32));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TemplateName", typeof(Int32));
            dt.Columns.Add("DefaultPageName", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));


            DataRow dtrow = dt.NewRow();
            dtrow["SiteID"] = 1;
            dtrow["SiteName"] = "Default";
            dtrow["TemplateName"] = 1;
            dtrow["DefaultPageName"] = 1;
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
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

            DataTable emptyTable = new DataTable();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(emptyTable);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            //Act
            SiteAdministrationFactory objSiteAdministrationFactory = new SiteAdministrationFactory(mockDataAccess.Object);
            var result = objSiteAdministrationFactory.GetAllEntityByEntitySearch(objSearchModel);

            List<SiteAdministrationObjectModel> ListExpected = new List<SiteAdministrationObjectModel>();
            ListExpected.Add(new SiteAdministrationObjectModel
            {
                SiteID = 1,
                SiteName = "Default",
                DefaultPageID = 1,
                Description = null,
                ParentSiteID = 0,
                TemplateID = 1


            });
            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("LastModified");
            lstExclude.Add("Key");
            ValidateEmptyData(result);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region CreateEntity_DataRow_Null
        /// <summary>
        /// CreateEntity_DataRow_Null
        /// </summary>
        [TestMethod]
        public void CreateEntity_DataRow_Null()
        {
            //Arrange
            DataRow entityRow = null;

            //Act
            SiteAdministrationFactory objSiteAdministrationFactory = new SiteAdministrationFactory(mockDataAccess.Object);
            var result = objSiteAdministrationFactory.CreateEntity<SiteAdministrationObjectModel>(entityRow);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region CreateEntity_With_IDataRecord_Null
        /// <summary>
        /// CreateEntity_With_IDataRecord_Null
        /// </summary>
        [TestMethod]
        public void CreateEntity_With_IDataRecord_Null()
        {
            //Arrange           
            IDataRecord entity = null;

            //Act
            SiteAdministrationFactory objSiteAdministrationFactory = new SiteAdministrationFactory(mockDataAccess.Object);
            var result = objSiteAdministrationFactory.CreateEntity<SiteAdministrationObjectModel>(entity);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();
        }
        #endregion

    }
}

