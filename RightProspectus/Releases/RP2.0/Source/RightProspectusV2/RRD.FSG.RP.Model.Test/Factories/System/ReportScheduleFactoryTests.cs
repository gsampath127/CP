using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.System;
using Moq;
using System.Data.Common;
using System.Data.SqlClient;

namespace RRD.FSG.RP.Model.Test.Factories.System
{
    [TestClass]
    public class ReportScheduleFactoryTests : BaseTestFactory<ReportScheduleObjectModel>
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

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dSet);
        }
        #endregion

        #region GetEntityByKey_Returns_ObjectModel
        /// <summary>
        /// GetEntityByKey_Returns_ObjectModel
        /// </summary>
        [TestMethod]
        public void GetEntityByKey_Returns_ObjectModel()
        {
            //Arrange  
            DataTable dt = new DataTable();
            dt.Columns.Add("ReportScheduleId", typeof(Int32));
            dt.Columns.Add("ReportId", typeof(Int32));
            dt.Columns.Add("ReportName", typeof(string));
            dt.Columns.Add("ClientId", typeof(Int32));
            dt.Columns.Add("IsEnabled", typeof(bool));
            dt.Columns.Add("Status", typeof(Int32));
            dt.Columns.Add("ServiceName", typeof(string));
            dt.Columns.Add("FrequencyType", typeof(Int32));
            dt.Columns.Add("FrequencyInterval", typeof(Int32));
            dt.Columns.Add("UtcFirstScheduledRunDate", typeof(DateTime));
            dt.Columns.Add("UtcNextScheduledRunDate", typeof(DateTime));
            dt.Columns.Add("UtcLastScheduledRunDate", typeof(DateTime));
            dt.Columns.Add("UtcLastActualRunDate", typeof(string));
            dt.Columns.Add("FrequencyDescription", typeof(string));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("FTPServerIP", typeof(string));
            dt.Columns.Add("FTPFilePath", typeof(string));
            dt.Columns.Add("FTPUsername", typeof(string));
            dt.Columns.Add("FTPPassword", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["ReportScheduleId"] = 11;
            dtrow["ReportId"] = 24;
            dtrow["ReportName"] = "Test";
            dtrow["ClientId"] = 42;
            dtrow["IsEnabled"] = true;
            dtrow["Status"] = 41;
            dtrow["ServiceName"] = "ReportScheduleFactoryUnitTest";
            dtrow["FrequencyType"] = 59;
            dtrow["FrequencyInterval"] = 34;
            dtrow["UtcFirstScheduledRunDate"] = DateTime.Today;
            dtrow["UtcNextScheduledRunDate"] = DateTime.Today;
            dtrow["UtcLastScheduledRunDate"] = DateTime.Today;
            dtrow["UtcLastActualRunDate"] = null;
            dtrow["FrequencyDescription"] = "ReportScheduleFactoryUnitTest";
            dtrow["ModifiedBy"] = 32;
            dtrow["UtcLastModified"] = DateTime.Today;
            dtrow["Email"] = "test@wipro.com";
            dtrow["FTPServerIP"] = "10.10.10.10";
            dtrow["FTPFilePath"] = "test";
            dtrow["FTPUsername"] = "test";
            dtrow["FTPPassword"] = "32";

            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt);

            //Act
            ReportScheduleFactory objReportScheduleFactoryFactory = new ReportScheduleFactory(mockDataAccess.Object);
            var result = objReportScheduleFactoryFactory.GetEntityByKey(1);

            //Assert
            mockDataAccess.VerifyAll();

            ReportScheduleObjectModel objExpected = new ReportScheduleObjectModel()
            {
                ClientId = 42,
                Email = "test@wipro.com",
                FrequencyDescription = "ReportScheduleFactoryUnitTest",
                FrequencyInterval = 34,
                FrequencyType = 59,
                FTPFilePath = "test",
                FTPPassword = "32",
                FTPServerIP = "10.10.10.10",
                FTPUsername = "test",
                IsEnabled = true,
                ReportId = 24,
                ReportName = "Test",
                ReportScheduleId = 11,
                UtcFirstScheduledRunDate = DateTime.Today,
                UtcLastActualRunDate = null,
                UtcNextScheduledRunDate = DateTime.Today
            };
            ValidateObjectModelData(result, objExpected);
        }
        #endregion

        #region GetEntityByKey_Returns_ObjectModel_EmptyResult
        /// <summary>
        ///GetEntityByKey_Returns_ObjectModel_EmptyResult
        /// </summary>
        [TestMethod]
        public void GetEntityByKey_Returns_ObjectModel_EmptyResult()
        {
            //Arrange  
            DataTable dt = new DataTable();
            dt.Columns.Add("ReportScheduleId", typeof(Int32));
            dt.Columns.Add("ReportId", typeof(Int32));
            dt.Columns.Add("ReportName", typeof(string));
            dt.Columns.Add("ClientId", typeof(Int32));
            dt.Columns.Add("IsEnabled", typeof(bool));
            dt.Columns.Add("Status", typeof(Int32));
            dt.Columns.Add("ServiceName", typeof(string));
            dt.Columns.Add("FrequencyType", typeof(Int32));
            dt.Columns.Add("FrequencyInterval", typeof(Int32));
            dt.Columns.Add("UtcFirstScheduledRunDate", typeof(DateTime));
            dt.Columns.Add("UtcNextScheduledRunDate", typeof(DateTime));
            dt.Columns.Add("UtcLastScheduledRunDate", typeof(DateTime));
            dt.Columns.Add("UtcLastActualRunDate", typeof(string));
            dt.Columns.Add("FrequencyDescription", typeof(string));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("FTPServerIP", typeof(string));
            dt.Columns.Add("FTPFilePath", typeof(string));
            dt.Columns.Add("FTPUsername", typeof(string));
            dt.Columns.Add("FTPPassword", typeof(string));

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt);

            //Act
            ReportScheduleFactory objReportScheduleFactoryFactory = new ReportScheduleFactory(mockDataAccess.Object);
            var result = objReportScheduleFactoryFactory.GetEntityByKey(1);

            //Assert
            mockDataAccess.VerifyAll();
            Assert.AreEqual(result, null);
        }
        #endregion

        //#region GetAllEntities_Returns_IEnumerable
        ///// <summary>
        ///// GetAllEntities_Returns_IEnumerable
        ///// </summary>
        //[TestMethod]
        //public void GetAllEntities_Returns_IEnumerable()
        //{
        //    //Arrange  
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("ReportName", typeof(string));
        //    dt.Columns.Add("IsEnabled", typeof(bool));
        //    dt.Columns.Add("FrequencyType", typeof(Int32));
        //    dt.Columns.Add("FrequencyInterval", typeof(Int32));
        //    dt.Columns.Add("UtcFirstScheduledRunDate", typeof(DateTime));
        //    dt.Columns.Add("UtcLastScheduledRunDate", typeof(DateTime));
        //    dt.Columns.Add("UtcLastActualRunDate", typeof(string));
        //    dt.Columns.Add("UtcNextScheduledRunDate", typeof(DateTime));
        //    dt.Columns.Add("ModifiedBy", typeof(Int32));
        //    dt.Columns.Add("UtcLastModified", typeof(DateTime));


        //    //dt.Columns.Add("ReportScheduleId", typeof(Int32));
        //    //dt.Columns.Add("ReportId", typeof(Int32));

        //    //dt.Columns.Add("UtcFirstRunDate", typeof(DateTime));
        //    //dt.Columns.Add("UtcLastRunDate", typeof(DateTime));
        //    //// dt.Columns.Add("UtcModifiedDate", typeof(DateTime));
        //    //dt.Columns.Add("UtcNextRunDate", typeof(DateTime));
        //    //dt.Columns.Add("FrequencyDescription", typeof(string));
        //    //dt.Columns.Add("Status", typeof(Int32));
        //    //dt.Columns.Add("ClientId", typeof(Int32));
        //    //dt.Columns.Add("Email", typeof(string));
        //    //dt.Columns.Add("FTPServerIP", typeof(string));
        //    //dt.Columns.Add("FTPFilePath", typeof(string));
        //    //dt.Columns.Add("FTPName", typeof(string));
        //    //dt.Columns.Add("FTPUsername", typeof(string));
        //    //dt.Columns.Add("FTPPassword", typeof(string));
        //    //// dt.Columns.Add("UtcLastActualRunDate", typeof(DateTime));
        //    //dt.Columns.Add("ServiceName", typeof(string));
        //    //dt.Columns.Add("ExecutionCount", typeof(Int32));
        //    //dt.Columns.Add("UtcModifiedDate", typeof(string));


        //    DataRow dtrow = dt.NewRow();
        //    dtrow["ReportName"] = "test";
        //    dtrow["IsEnabled"] = true;
        //    dtrow["FrequencyType"] = 59;
        //    dtrow["FrequencyInterval"] = 34;
        //    dtrow["UtcFirstScheduledRunDate"] = DateTime.Today;
        //    dtrow["UtcLastScheduledRunDate"] = DateTime.Today;
        //    dtrow["UtcLastActualRunDate"] = null;
        //    dtrow["UtcNextScheduledRunDate"] = DateTime.Today;
        //    dtrow["ModifiedBy"] = 32;
        //    dtrow["UtcLastModified"] = DateTime.Today;


        //    //dtrow["ReportScheduleId"] = 11;
        //    //dtrow["ReportId"] = 24;

        //    //dtrow["UtcFirstRunDate"] = DateTime.Today;
        //    //dtrow["UtcLastRunDate"] = DateTime.Today;
        //    ////  dtrow["UtcModifiedDate"] = DateTime.Today;
        //    //dtrow["UtcNextRunDate"] = DateTime.Today;
        //    //dtrow["FrequencyDescription"] = "ReportScheduleFactoryUnitTest";
        //    //dtrow["Status"] = 41;
        //    //dtrow["Email"] = 34;
        //    //dtrow["FTPServerIP"] = DateTime.Today;
        //    //dtrow["FTPFilePath"] = DateTime.Today;
        //    //dtrow["FTPName"] = DateTime.Today;
        //    //dtrow["FTPUsername"] = DateTime.Today;
        //    //dtrow["FTPPassword"] = 32;
        //    ////   dtrow["UtcLastActualRunDate"] = DateTime.Today;
        //    //dtrow["ServiceName"] = "ReportScheduleFactoryUnitTest";
        //    //dtrow["ClientId"] = 42;
        //    //dtrow["ExecutionCount"] = 42;
        //    //dtrow["UtcModifiedDate"] = "12-3-2015";



        //    dt.Rows.Add(dtrow);
        //    ClientData();
        //    mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
        //                  .Returns(dt);

        //    //Act
        //    ReportScheduleFactoryCache objFactoryCache = new ReportScheduleFactoryCache(mockDataAccess.Object);
        //    objFactoryCache.ClientName = "Forethought";
        //    objFactoryCache.Mode = FactoryCacheMode.All;
        //    var result = objFactoryCache.GetAllEntities(1, 2);

        //    //Assert
        //    mockDataAccess.VerifyAll();

        //    List<ReportScheduleObjectModel> lstExpected = new List<ReportScheduleObjectModel>();
        //    ReportScheduleObjectModel objExpected = new ReportScheduleObjectModel()
        //   {
        //       FrequencyInterval = 34,
        //       FrequencyType = 59,
        //       IsEnabled = true,
        //       ReportName = "Test",
        //       UtcFirstScheduledRunDate = DateTime.Today,
        //       UtcLastActualRunDate = null,
        //       UtcNextScheduledRunDate = DateTime.Today
        //   };
        //    lstExpected.Add(objExpected);
        //    ValidateListData(lstExpected, result.ToList());
        //}
        //#endregion

        #region SaveEntity_With_modifiedBy
        /// <summary>
        /// SaveEntity_With_modifiedBy
        /// </summary>
        [TestMethod]
        public void SaveEntity_With_modifiedBy()
        {
            //Arrange
            ReportScheduleObjectModel objReportScheduleObjectModel = new ReportScheduleObjectModel();
            objReportScheduleObjectModel.ClientId = 2;
            objReportScheduleObjectModel.Name = "Test_001";
            objReportScheduleObjectModel.ReportId = 23;
            objReportScheduleObjectModel.ReportName = "Doc_1";

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="ReportScheduleId", Value=1 },
                new SqlParameter(){ ParameterName="ReportId", Value=1 },
                new SqlParameter(){ ParameterName="ClientId", Value=1 },
                new SqlParameter(){ ParameterName="IsEnabled", Value=true },
                 new SqlParameter(){ ParameterName="FrequencyType", Value=1 },
                new SqlParameter(){ ParameterName="FrequencyInterval", Value=1 },
                 new SqlParameter(){ ParameterName="UtcFirstScheduledRunDate", Value=DateTime.Today },
                new SqlParameter(){ ParameterName="UtcLastScheduledRunDate", Value=DateTime.Today },
                new SqlParameter(){ ParameterName="email", Value="test@wipro.com" },
                new SqlParameter(){ ParameterName="ftpServerIP", Value="10.10.10.10" },
                new SqlParameter(){ ParameterName="ftpFilePath", Value="10.10.10.10" },
                new SqlParameter(){ ParameterName="ftpUsername", Value="test" },
                new SqlParameter(){ ParameterName="ftpPassword", Value="asc" }
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
                  .Returns(parameters[13]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ReportScheduleFactory objReportScheduleFactory = new ReportScheduleFactory(mockDataAccess.Object);
            objReportScheduleFactory.SaveEntity(objReportScheduleObjectModel, 32);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region DeleteEntity_With_Key
        /// <summary>
        /// DeleteEntity_With_Key
        /// </summary>
        [TestMethod]
        public void DeleteEntity_With_Key()
        {
            //Arrange   

            //Act
            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                new SqlParameter(){ ParameterName="ReportScheduleId", Value=1 }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ReportScheduleFactory objReportScheduleFactory = new ReportScheduleFactory(mockDataAccess.Object);
            objReportScheduleFactory.DeleteEntity(32);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion       

        #region GetEntitiesBySearch_Returns_IEnumerable
        /// <summary>
        /// GetEntitiesBySearch_Returns_IEnumerable
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_IEnumerable()
        {
            //Arrange 
            ReportScheduleSearchDetail objReportScheduleSearchDetail = new ReportScheduleSearchDetail();
            ReportScheduleSortDetail objReportScheduleSortDetail = new ReportScheduleSortDetail();
            ClientData();
            DataTable dt = new DataTable();
            dt.Columns.Add("RowRank", typeof(Int32));
            dt.Columns.Add("ReportScheduleId", typeof(Int32));
            dt.Columns.Add("ReportId", typeof(Int32));
            dt.Columns.Add("ReportName", typeof(string));
            dt.Columns.Add("ClientId", typeof(Int32));
            dt.Columns.Add("IsEnabled", typeof(bool));
            dt.Columns.Add("FrequencyType", typeof(Int32));
            dt.Columns.Add("FrequencyInterval", typeof(Int32));
            dt.Columns.Add("UtcFirstScheduledRunDate", typeof(DateTime));
            dt.Columns.Add("UtcNextScheduledRunDate", typeof(DateTime));
            dt.Columns.Add("UtcLastScheduledRunDate", typeof(DateTime));
            dt.Columns.Add("UtcLastActualRunDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow dtrow = dt.NewRow();
            dtrow["RowRank"] = 1;
            dtrow["ReportScheduleId"] = 11;
            dtrow["ReportId"] = 24;
            dtrow["ReportName"] = "test";
            dtrow["ClientId"] = 42;
            dtrow["IsEnabled"] = true;
            dtrow["FrequencyType"] = 59;
            dtrow["FrequencyInterval"] = 34;
            dtrow["UtcFirstScheduledRunDate"] = DateTime.Today;
            dtrow["UtcNextScheduledRunDate"] = DateTime.Today;
            dtrow["UtcLastScheduledRunDate"] = DateTime.Today;
            dtrow["UtcLastActualRunDate"] = DateTime.Today;
            dtrow["ModifiedBy"] = 32;
            dtrow["UtcLastModified"] = DateTime.Today;

            dt.Rows.Add(dtrow);
            DataSet dSet1 = new DataSet();
            dSet1.Tables.Add(dt);

            DataTable dtCount = new DataTable();
            dtCount.Columns.Add("Count", typeof(Int32));
            DataRow dtrowCnt = dtCount.NewRow();
            dtrowCnt["Count"] = 1;
            dtCount.Rows.Add(dtrowCnt);
            dSet1.Tables.Add(dtCount);

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ReportName", Value="test" },
                new SqlParameter(){ ParameterName="FrequencyType", Value=1 },
                new SqlParameter(){ ParameterName="FrequencyInterval", Value=1 },
                new SqlParameter(){ ParameterName="UtcFirstScheduledRunDate", Value=DateTime.Today },
                new SqlParameter(){ ParameterName="UtcLastScheduledRunDate", Value=DateTime.Today },
                new SqlParameter(){ ParameterName="UtcNextScheduledRunDate", Value=DateTime.Today },
                new SqlParameter(){ ParameterName="IsEnabled", Value=true },
                new SqlParameter(){ ParameterName="PageSize", Value=1 },
                new SqlParameter(){ ParameterName="PageIndex", Value=1 },
                new SqlParameter(){ ParameterName="SortDirection", Value="asc" },
                new SqlParameter(){ ParameterName="SortColumn", Value="A" },
                new SqlParameter(){ ParameterName="PageSize", Value=1 },
 new SqlParameter(){ ParameterName="ClientId", Value=1 }
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
                 .Returns(parameters[12]);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet1);

            //Act
            ReportScheduleFactory objReportScheduleFactoryFactory = new ReportScheduleFactory(mockDataAccess.Object);
            var result = objReportScheduleFactoryFactory.GetEntitiesBySearch(3, 2, objReportScheduleSearchDetail, objReportScheduleSortDetail, null);

            //Assert
            mockDataAccess.VerifyAll();

            List<ReportScheduleObjectModel> lstExpected = new List<ReportScheduleObjectModel>();
            lstExpected.Add(new ReportScheduleObjectModel()
            {
                ClientId = 42,
                FrequencyType = 59,
                FrequencyInterval = 34,
                IsEnabled = true,
                ReportId = 24,
                ReportName = "test",
                ReportScheduleId = 11,

                UtcFirstScheduledRunDate = DateTime.Today,
                UtcNextScheduledRunDate = DateTime.Today,
                UtcLastActualRunDate = DateTime.Today
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
        }
        #endregion

        #region GetEntitiesBySearch_Returns_IEnumerable_MaximumRows_Case
        /// <summary>
        /// GetEntitiesBySearch_Returns_IEnumerable_MaximumRows_Case
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_IEnumerable_MaximumRows_Case()
        {
            //Arrange 
            ReportScheduleSearchDetail objReportScheduleSearchDetail = new ReportScheduleSearchDetail();
            ReportScheduleSortDetail objReportScheduleSortDetail = new ReportScheduleSortDetail();

            DataTable dt = new DataTable();
            dt.Columns.Add("ReportId", typeof(Int32));
            dt.Columns.Add("ReportName", typeof(string));
            dt.Columns.Add("ReportDescription", typeof(string));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow dtrow = dt.NewRow();
            dtrow["ReportId"] = 24;
            dtrow["ReportName"] = "test";
            dtrow["ReportDescription"] = "test";
            dtrow["ModifiedBy"] = 32;
            dtrow["UtcLastModified"] = DateTime.Today;

            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt);

            //Act
            ReportScheduleFactory objReportScheduleFactoryFactory = new ReportScheduleFactory(mockDataAccess.Object);
            var result = objReportScheduleFactoryFactory.GetEntitiesBySearch(3, -1, objReportScheduleSearchDetail, objReportScheduleSortDetail, null);

            //Assert
            mockDataAccess.VerifyAll();

            List<ReportScheduleObjectModel> lstExpected = new List<ReportScheduleObjectModel>();
            lstExpected.Add(new ReportScheduleObjectModel()
            {
                ReportId = 24,
                ReportName = "test"
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
        }
        #endregion

        #region GetEntitiesBySearch_Returns_IEnumerable_MaximumRows_Case_LargerNegativeValue
        /// <summary>
        /// GetEntitiesBySearch_Returns_IEnumerable_MaximumRows_Case_LargerNegativeValue
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_IEnumerable_MaximumRows_Case_LargerNegativeValue()
        {
            //Arrange 
            ReportScheduleSearchDetail objReportScheduleSearchDetail = new ReportScheduleSearchDetail();
            ReportScheduleSortDetail objReportScheduleSortDetail = new ReportScheduleSortDetail();

            DataTable dt = new DataTable();
            dt.Columns.Add("ReportName", typeof(string));
            dt.Columns.Add("IsEnabled", typeof(bool));
            dt.Columns.Add("FrequencyType", typeof(Int32));
            dt.Columns.Add("FrequencyInterval", typeof(Int32));
            dt.Columns.Add("UtcFirstScheduledRunDate", typeof(DateTime));
            dt.Columns.Add("UtcLastScheduledRunDate", typeof(DateTime));
            dt.Columns.Add("UtcLastActualRunDate", typeof(DateTime));
            dt.Columns.Add("UtcNextScheduledRunDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow dtrow = dt.NewRow();
            dtrow["ReportName"] = "test";
            dtrow["IsEnabled"] = true;
            dtrow["FrequencyType"] = 59;
            dtrow["FrequencyInterval"] = 34;
            dtrow["UtcFirstScheduledRunDate"] = DateTime.Today;
            dtrow["UtcLastScheduledRunDate"] = DateTime.Today;
            dtrow["UtcLastActualRunDate"] = DateTime.Today;
            dtrow["UtcNextScheduledRunDate"] = DateTime.Today;
            dtrow["ModifiedBy"] = 32;
            dtrow["UtcLastModified"] = DateTime.Today;

            dt.Rows.Add(dtrow);

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ClientId", Value=1 }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0]);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt);

            //Act
            ReportScheduleFactory objReportScheduleFactoryFactory = new ReportScheduleFactory(mockDataAccess.Object);
            var result = objReportScheduleFactoryFactory.GetEntitiesBySearch(3, -10, objReportScheduleSearchDetail, objReportScheduleSortDetail, null);

            //Assert
            mockDataAccess.VerifyAll();

            List<ReportScheduleObjectModel> lstExpected = new List<ReportScheduleObjectModel>();
            lstExpected.Add(new ReportScheduleObjectModel()
            {
                ReportName = "test",
                IsEnabled = true,
                FrequencyType = 59,
                FrequencyInterval = 34,
                UtcFirstScheduledRunDate = DateTime.Today,
                UtcLastActualRunDate = DateTime.Today,
                UtcNextScheduledRunDate = DateTime.Today
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
        }
        #endregion
    }
}
