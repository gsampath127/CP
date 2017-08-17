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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RRD.FSG.RP.Model.Factories;
using System.Web.Script.Serialization;
using Moq;
using System.Data.Common;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for VerticalXmlImportFactory class
    /// </summary>
    [TestClass]
    public class VerticalXmlImportFactoryTests : BaseTestFactory<VerticalXmlImportObjectModel>
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
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_VerticalXmlImportObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_VerticalXmlImportObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_VerticalXmlImportObjectModel()
        {
            //Arrange
            VerticalXmlImportSortDetail objSortDtl = new VerticalXmlImportSortDetail();
            objSortDtl.Column = VerticalXmlImportSortColumn.VerticalXmlImportId;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("VerticalXmlImportId", typeof(Int32));
            dt.Columns.Add("ImportTypes", typeof(Int32));
            dt.Columns.Add("ExportBackupId", typeof(Int32));
            dt.Columns.Add("ImportDate", typeof(DateTime));
            dt.Columns.Add("ImportedBy", typeof(Int32));
            dt.Columns.Add("ImportDescription", typeof(string));
            dt.Columns.Add("Status", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["VerticalXmlImportId"] = 1;
            dtrow["ImportTypes"] = 1;
            dtrow["ExportBackupId"] = 1;
            dtrow["ImportDate"] = DateTime.Parse("01/01/2015");
            dtrow["ImportedBy"] = 3;
            dtrow["ImportDescription"] = "Test_001";
            dtrow["Status"] = 1;

            dt.Rows.Add(dtrow);

            DataTable dtUser = new DataTable();
            dtUser.Columns.Add("UserId", typeof(Int32));
            dtUser.Columns.Add("Email", typeof(string));
            dtUser.Columns.Add("EmailConfirmed", typeof(bool));
            dtUser.Columns.Add("PasswordHash", typeof(string));
            dtUser.Columns.Add("SecurityStamp", typeof(string));
            dtUser.Columns.Add("PhoneNumber", typeof(string));
            dtUser.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dtUser.Columns.Add("UserName", typeof(string));
            dtUser.Columns.Add("FirstName", typeof(string));
            dtUser.Columns.Add("LastName", typeof(string));
            dtUser.Columns.Add("ModifiedBy", typeof(Int32));
            dtUser.Columns.Add("LastModified", typeof(DateTime));
            dtUser.Columns.Add("ClientId", typeof(Int32));
            dtUser.Columns.Add("RoleId", typeof(Int32));
            dtUser.Columns.Add("Name", typeof(string));

            DataRow dtrowUser = dtUser.NewRow();
            dtrowUser["UserId"] = 1;
            dtrowUser["Email"] = "";
            dtrowUser["EmailConfirmed"] = true;
            dtrowUser["PasswordHash"] = "";
            dtrowUser["SecurityStamp"] = "";
            dtrowUser["PhoneNumber"] = 1;
            dtrowUser["PhoneNumberConfirmed"] = true;
            dtrowUser["UserName"] = "";
            dtrowUser["FirstName"] = "";
            dtrowUser["LastName"] = "";
            dtrowUser["ModifiedBy"] = 1;
            dtrowUser["LastModified"] = DateTime.Parse("01/01/2015");
            dtrowUser["ClientId"] = 1;
            dtrowUser["RoleId"] = 1;
            dtrowUser["Name"] = "";
            dtUser.Rows.Add(dtrowUser);

            ClientData();

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt)
              .Returns(dtUser);


            //Act
            VerticalXmlImportFactoryCache objVerticalXmlImportFactoryCache = new VerticalXmlImportFactoryCache(mockDataAccess.Object);
            objVerticalXmlImportFactoryCache.ClientName = "Forethought";
            objVerticalXmlImportFactoryCache.Mode = FactoryCacheMode.All;
            var result = objVerticalXmlImportFactoryCache.GetAllEntities(0, 0, objSortDtl);
            //Assert

            List<VerticalXmlImportObjectModel> lstExpected = new List<VerticalXmlImportObjectModel>
            {
                new VerticalXmlImportObjectModel
            {
                
                ExportBackupId=1,
                ImportDate=DateTime.Parse("01/01/2015"),
                ImportDescription="Test_001",
                ImportedBy=3,
                ImportedByName="",
                ImportTypes=1,
                Status=1,
                VerticalXmlImportId=1,
                ImportXml = null
                
                
            }
            };
            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_VerticalXmlImportObjectModel_User_ImpotedBy
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_VerticalXmlImportObjectModel_User_ImpotedBy
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_VerticalXmlImportObjectModel_User_ImpotedBy()
        {
            //Arrange
            VerticalXmlImportSortDetail objSortDtl = new VerticalXmlImportSortDetail();
            objSortDtl.Column = VerticalXmlImportSortColumn.VerticalXmlImportId;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("VerticalXmlImportId", typeof(Int32));
            dt.Columns.Add("ImportTypes", typeof(Int32));
            dt.Columns.Add("ExportBackupId", typeof(Int32));
            dt.Columns.Add("ImportDate", typeof(DateTime));
            dt.Columns.Add("ImportedBy", typeof(Int32));
            dt.Columns.Add("ImportDescription", typeof(string));
            dt.Columns.Add("Status", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["VerticalXmlImportId"] = 1;
            dtrow["ImportTypes"] = 1;
            dtrow["ExportBackupId"] = 1;
            dtrow["ImportDate"] = DateTime.Parse("01/01/2015");
            dtrow["ImportedBy"] = 1;
            dtrow["ImportDescription"] = "Test_001";
            dtrow["Status"] = 1;

            dt.Rows.Add(dtrow);

            DataTable dtUser = new DataTable();
            dtUser.Columns.Add("UserId", typeof(Int32));
            dtUser.Columns.Add("Email", typeof(string));
            dtUser.Columns.Add("EmailConfirmed", typeof(bool));
            dtUser.Columns.Add("PasswordHash", typeof(string));
            dtUser.Columns.Add("SecurityStamp", typeof(string));
            dtUser.Columns.Add("PhoneNumber", typeof(string));
            dtUser.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dtUser.Columns.Add("UserName", typeof(string));
            dtUser.Columns.Add("FirstName", typeof(string));
            dtUser.Columns.Add("LastName", typeof(string));
            dtUser.Columns.Add("ModifiedBy", typeof(Int32));
            dtUser.Columns.Add("LastModified", typeof(DateTime));
            dtUser.Columns.Add("ClientId", typeof(Int32));
            dtUser.Columns.Add("RoleId", typeof(Int32));
            dtUser.Columns.Add("Name", typeof(string));

            DataRow dtrowUser = dtUser.NewRow();
            dtrowUser["UserId"] = 1;
            dtrowUser["Email"] = "";
            dtrowUser["EmailConfirmed"] = true;
            dtrowUser["PasswordHash"] = "";
            dtrowUser["SecurityStamp"] = "";
            dtrowUser["PhoneNumber"] = 1;
            dtrowUser["PhoneNumberConfirmed"] = true;
            dtrowUser["UserName"] = "";
            dtrowUser["FirstName"] = "";
            dtrowUser["LastName"] = "";
            dtrowUser["ModifiedBy"] = 1;
            dtrowUser["LastModified"] = DateTime.Parse("01/01/2015");
            dtrowUser["ClientId"] = 1;
            dtrowUser["RoleId"] = 1;
            dtrowUser["Name"] = "";
            dtUser.Rows.Add(dtrowUser);

            ClientData();

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt)
              .Returns(dtUser);


            //Act
            VerticalXmlImportFactoryCache objVerticalXmlImportFactoryCache = new VerticalXmlImportFactoryCache(mockDataAccess.Object);
            objVerticalXmlImportFactoryCache.ClientName = "Forethought";
            objVerticalXmlImportFactoryCache.Mode = FactoryCacheMode.All;
            var result = objVerticalXmlImportFactoryCache.GetAllEntities(0, 0, objSortDtl);
            //Assert

            List<VerticalXmlImportObjectModel> lstExpected = new List<VerticalXmlImportObjectModel>
            {
                new VerticalXmlImportObjectModel
            {
                
                ExportBackupId=1,
                ImportDate=DateTime.Parse("01/01/2015"),
                ImportDescription="Test_001",
                ImportedBy=1,
                ImportedByName=" ",
                ImportTypes=1,
                Status=1,
                VerticalXmlImportId=1,
                ImportXml = null
                
                
            }
            };
            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_Factory
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_Factory
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_Factory()
        {
            //Arrange
            VerticalXmlImportSortDetail objSortDtl = new VerticalXmlImportSortDetail();
            objSortDtl.Column = VerticalXmlImportSortColumn.VerticalXmlImportId;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("VerticalXmlImportId", typeof(Int32));
            dt.Columns.Add("ImportTypes", typeof(Int32));
            dt.Columns.Add("ExportBackupId", typeof(Int32));
            dt.Columns.Add("ImportDate", typeof(DateTime));
            dt.Columns.Add("ImportedBy", typeof(Int32));
            dt.Columns.Add("ImportDescription", typeof(string));
            dt.Columns.Add("Status", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["VerticalXmlImportId"] = 1;
            dtrow["ImportTypes"] = 1;
            dtrow["ExportBackupId"] = 1;
            dtrow["ImportDate"] = DateTime.Parse("01/01/2015");
            dtrow["ImportedBy"] = 3;
            dtrow["ImportDescription"] = "Test_001";
            dtrow["Status"] = 1;

            dt.Rows.Add(dtrow);

            DataTable dtUser = new DataTable();
            dtUser.Columns.Add("UserId", typeof(Int32));
            dtUser.Columns.Add("Email", typeof(string));
            dtUser.Columns.Add("EmailConfirmed", typeof(bool));
            dtUser.Columns.Add("PasswordHash", typeof(string));
            dtUser.Columns.Add("SecurityStamp", typeof(string));
            dtUser.Columns.Add("PhoneNumber", typeof(string));
            dtUser.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dtUser.Columns.Add("UserName", typeof(string));
            dtUser.Columns.Add("FirstName", typeof(string));
            dtUser.Columns.Add("LastName", typeof(string));
            dtUser.Columns.Add("ModifiedBy", typeof(Int32));
            dtUser.Columns.Add("LastModified", typeof(DateTime));
            dtUser.Columns.Add("ClientId", typeof(Int32));
            dtUser.Columns.Add("RoleId", typeof(Int32));
            dtUser.Columns.Add("Name", typeof(string));

            DataRow dtrowUser = dtUser.NewRow();
            dtrowUser["UserId"] = 1;
            dtrowUser["Email"] = "";
            dtrowUser["EmailConfirmed"] = true;
            dtrowUser["PasswordHash"] = "";
            dtrowUser["SecurityStamp"] = "";
            dtrowUser["PhoneNumber"] = 1;
            dtrowUser["PhoneNumberConfirmed"] = true;
            dtrowUser["UserName"] = "";
            dtrowUser["FirstName"] = "";
            dtrowUser["LastName"] = "";
            dtrowUser["ModifiedBy"] = 1;
            dtrowUser["LastModified"] = DateTime.Parse("01/01/2015");
            dtrowUser["ClientId"] = 1;
            dtrowUser["RoleId"] = 1;
            dtrowUser["Name"] = "";
            dtUser.Rows.Add(dtrowUser);

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
            .Returns(dt)
            .Returns(dtUser);


            //Act
            VerticalXmlImportFactory objVerticalXmlImportFactory = new VerticalXmlImportFactory(mockDataAccess.Object);
            var result = objVerticalXmlImportFactory.GetAllEntities(0, 0);
            //Assert

            List<VerticalXmlImportObjectModel> lstExpected = new List<VerticalXmlImportObjectModel>
            {
                new VerticalXmlImportObjectModel
            {
                
                ExportBackupId=1,
                ImportDate=DateTime.Parse("01/01/2015"),
                ImportDescription="Test_001",
                ImportedBy=3,
                ImportedByName="",
                ImportTypes=1,
                Status=1,
                VerticalXmlImportId=1,
                ImportXml = null
                
                
            }
            };
            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetAllEntities_Returns_Ienumerable_Factory_ImportedBy
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_Factory_ImportedBy
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_Factory_ImportedBy()
        {
            //Arrange
            VerticalXmlImportSortDetail objSortDtl = new VerticalXmlImportSortDetail();
            objSortDtl.Column = VerticalXmlImportSortColumn.VerticalXmlImportId;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("VerticalXmlImportId", typeof(Int32));
            dt.Columns.Add("ImportTypes", typeof(Int32));
            dt.Columns.Add("ExportBackupId", typeof(Int32));
            dt.Columns.Add("ImportDate", typeof(DateTime));
            dt.Columns.Add("ImportedBy", typeof(Int32));
            dt.Columns.Add("ImportDescription", typeof(string));
            dt.Columns.Add("Status", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["VerticalXmlImportId"] = 1;
            dtrow["ImportTypes"] = 1;
            dtrow["ExportBackupId"] = 1;
            dtrow["ImportDate"] = DateTime.Parse("01/01/2015");
            dtrow["ImportedBy"] = 1;
            dtrow["ImportDescription"] = "Test_001";
            dtrow["Status"] = 1;

            dt.Rows.Add(dtrow);

            DataTable dtUser = new DataTable();
            dtUser.Columns.Add("UserId", typeof(Int32));
            dtUser.Columns.Add("Email", typeof(string));
            dtUser.Columns.Add("EmailConfirmed", typeof(bool));
            dtUser.Columns.Add("PasswordHash", typeof(string));
            dtUser.Columns.Add("SecurityStamp", typeof(string));
            dtUser.Columns.Add("PhoneNumber", typeof(string));
            dtUser.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dtUser.Columns.Add("UserName", typeof(string));
            dtUser.Columns.Add("FirstName", typeof(string));
            dtUser.Columns.Add("LastName", typeof(string));
            dtUser.Columns.Add("ModifiedBy", typeof(Int32));
            dtUser.Columns.Add("LastModified", typeof(DateTime));
            dtUser.Columns.Add("ClientId", typeof(Int32));
            dtUser.Columns.Add("RoleId", typeof(Int32));
            dtUser.Columns.Add("Name", typeof(string));

            DataRow dtrowUser = dtUser.NewRow();
            dtrowUser["UserId"] = 1;
            dtrowUser["Email"] = "";
            dtrowUser["EmailConfirmed"] = true;
            dtrowUser["PasswordHash"] = "";
            dtrowUser["SecurityStamp"] = "";
            dtrowUser["PhoneNumber"] = 1;
            dtrowUser["PhoneNumberConfirmed"] = true;
            dtrowUser["UserName"] = "";
            dtrowUser["FirstName"] = string.Empty;
            dtrowUser["LastName"] = string.Empty;
            dtrowUser["ModifiedBy"] = 1;
            dtrowUser["LastModified"] = DateTime.Parse("01/01/2015");
            dtrowUser["ClientId"] = 1;
            dtrowUser["RoleId"] = 1;
            dtrowUser["Name"] = "";
            dtUser.Rows.Add(dtrowUser);

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
            .Returns(dt)
            .Returns(dtUser);


            //Act
            VerticalXmlImportFactory objVerticalXmlImportFactory = new VerticalXmlImportFactory(mockDataAccess.Object);
            var result = objVerticalXmlImportFactory.GetAllEntities(0, 0);
            //Assert

            List<VerticalXmlImportObjectModel> lstExpected = new List<VerticalXmlImportObjectModel>
            {
                new VerticalXmlImportObjectModel
            {
                
                ExportBackupId=1,
                ImportDate=DateTime.Parse("01/01/2015"),
                ImportDescription="Test_001",
                ImportedBy=1,
                ImportedByName=" ",
                ImportTypes=1,
                Status=1,
                VerticalXmlImportId=1,
                ImportXml = null
                
                
            }
            };
            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_Factory_Null
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_Factory_Null
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_Factory_Null()
        {
            //Arrange
            VerticalXmlImportSortDetail objSortDtl = new VerticalXmlImportSortDetail();
            objSortDtl.Column = VerticalXmlImportSortColumn.VerticalXmlImportId;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("VerticalXmlImportId", typeof(Int32));

            DataTable dtUser = new DataTable();
          
            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
             .Returns(dt)
             .Returns(dtUser);

            //Act
            VerticalXmlImportFactory objVerticalXmlImportFactory = new VerticalXmlImportFactory(mockDataAccess.Object);
            var result = objVerticalXmlImportFactory.GetAllEntities(0, 0);
            //Assert

            List<VerticalXmlImportObjectModel> lstExpected = new List<VerticalXmlImportObjectModel>
            {
                new VerticalXmlImportObjectModel
            {
                
                ExportBackupId=1,
                ImportDate=DateTime.Parse("01/01/2015"),
                ImportDescription="Test_001",
                ImportedBy=3,
                ImportedByName="",
                ImportTypes=1,
                Status=1,
                VerticalXmlImportId=1,
                ImportXml = null
                
                
            }
            };
            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateEmptyData(result);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_Null
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_Null
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_Null()
        {
            //Arrange
            VerticalXmlImportSortDetail objSortDtl = new VerticalXmlImportSortDetail();
            objSortDtl.Column = VerticalXmlImportSortColumn.VerticalXmlImportId;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("VerticalXmlImportId", typeof(Int32));

            DataTable dtUser = new DataTable();
            ClientData();
            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
             .Returns(dt)
             .Returns(dtUser);

          
            //Act
            VerticalXmlImportFactoryCache objVerticalXmlImportFactoryCache = new VerticalXmlImportFactoryCache(mockDataAccess.Object);
            objVerticalXmlImportFactoryCache.ClientName = "Forethought";
            objVerticalXmlImportFactoryCache.Mode = FactoryCacheMode.All;
            var result = objVerticalXmlImportFactoryCache.GetAllEntities(0, 0, objSortDtl);
            //Assert
            ValidateEmptyData(result);
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
            VerticalXmlImportObjectModel objObjectModel = new VerticalXmlImportObjectModel()
            {
                VerticalXmlImportId = 1,
                ImportTypes = 1,
                ImportXml = "TEST_001",
                ImportedBy=1,
                ImportDescription = "TEST_001",
                Status = 1
            };
            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="VerticalXmlImportId", Value=1 },
                new SqlParameter(){ ParameterName="ImportTypes", Value=1 },
                new SqlParameter(){ ParameterName="ImportXml", Value="TEST_001" },
                new SqlParameter(){ ParameterName="ImportedBy", Value=1 },
                new SqlParameter(){ ParameterName="ImportDescription", Value= "TEST_001" },
                 new SqlParameter(){ ParameterName="Status", Value=1 }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4]);


            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            VerticalXmlImportFactory objVerticalXmlImportFactory = new VerticalXmlImportFactory(mockDataAccess.Object);
            objVerticalXmlImportFactory.SaveEntity(objObjectModel);

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
            Exception exe = null;
            ClientData();
            VerticalXmlImportObjectModel objObjectModel = new VerticalXmlImportObjectModel();

            VerticalXmlImportFactory objVerticalXmlImportFactory = new VerticalXmlImportFactory(mockDataAccess.Object);

            //Act
            try
            {
                objVerticalXmlImportFactory.SaveEntity(objObjectModel, 1);
            }
            catch (Exception e)
            {
                if (e is NotImplementedException)
                    exe = e;
            }

            //Assert
            Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        }
        #endregion

        #region GetEntityByKey_Returns_VerticalXmlImportObjectModel
        /// <summary>
        /// GetEntityByKey_Returns_VerticalXmlImportObjectModel
        /// </summary>
        [TestMethod]
        public void GetEntityByKey_Returns_VerticalXmlImportObjectModel()
        {
            //Arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("VerticalXmlImportId", typeof(Int32));
            dt.Columns.Add("ImportTypes", typeof(Int32));
            dt.Columns.Add("ImportXml", typeof(string));
            dt.Columns.Add("ImportDate", typeof(DateTime));
            dt.Columns.Add("ImportedBy", typeof(Int32));
            dt.Columns.Add("ImportDescription", typeof(string));
            dt.Columns.Add("Status", typeof(Int32));
            dt.Columns.Add("ExportBackupId", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["VerticalXmlImportId"] = 1;
            dtrow["ImportTypes"] = 1;
            dtrow["ImportXml"] = "";
            dtrow["ImportDate"] = DateTime.Parse("01/01/2015");
            dtrow["ImportedBy"] = 3;
            dtrow["ImportDescription"] = "Test_001";
            dtrow["Status"] = 1;
            dtrow["ExportBackupId"] = 2;
            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            VerticalXmlImportFactory objVerticalXmlImportFactory = new VerticalXmlImportFactory(mockDataAccess.Object);
            var result = objVerticalXmlImportFactory.GetEntityByKey(1);
            //Assert
            List<VerticalXmlImportObjectModel> lstExpected = new List<VerticalXmlImportObjectModel>
            {
                new VerticalXmlImportObjectModel
            {
                ExportBackupId=2,
                ImportDate=DateTime.Parse("01/01/2015"),
                ImportDescription="Test_001",
                ImportedBy=3,
                ImportedByName=null,
                ImportTypes=1,
                Status=1,
                VerticalXmlImportId=1,
                ImportXml = ""
            }
            };
            List<VerticalXmlImportObjectModel> lstResult = new List<VerticalXmlImportObjectModel> { result };
            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, lstResult, lstExclude);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetEntityByKey_Returns_VerticalXmlImportObjectModel_null
        /// <summary>
        /// GetEntityByKey_Returns_VerticalXmlImportObjectModel_null
        /// </summary>
        [TestMethod]
        public void GetEntityByKey_Returns_VerticalXmlImportObjectModel_null()
        {
            //Arrange
            DataTable dt = new DataTable();
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            VerticalXmlImportFactory objVerticalXmlImportFactory = new VerticalXmlImportFactory(mockDataAccess.Object);
            var result = objVerticalXmlImportFactory.GetEntityByKey(1);
            //Assert
            Assert.AreSame(result,null);
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
            VerticalXmlImportFactory objFactory = new VerticalXmlImportFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<VerticalXmlImportObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

        #region DequeueAndLoadImportXML_Empty
        ///<summary>
        ///DequeueAndLoadImportXML_Empty
        ///<summary>
        [TestMethod]
        public void DequeueAndLoadImportXML_Empty()
        {
            //Arrange
            DataTable dtt = new DataTable();
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(dtt);
            //Act
            VerticalXmlImportFactory objVerticalXmlExportFactory = new VerticalXmlImportFactory(mockDataAccess.Object);
            objVerticalXmlExportFactory.DequeueAndLoadImportXML();
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion


    }
}
