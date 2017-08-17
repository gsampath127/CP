using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Keys;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Model.Cache.Client;
using Moq;
using System.Data.Common;
using System.Data.SqlClient;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    [TestClass]
    public class CUDHistoryFactoryTests : BaseTestFactory<CUDHistoryObjectModel>
    {
        private Mock<IDataAccess> mockDataAccess;

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

        #region GetAllEntities_Return_IEnumerable_Handles_isCUDHistory
        /// <summary>
        ///GetAllEntities_Return_IEnumerable_Handles_isCUDHistory
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Return_IEnumerable_Handles_isCUDHistory()
        {
            //Arrange  
            DataTable dt = new DataTable();
            dt.Columns.Add("TableName", typeof(string));
            dt.Columns.Add("UserId", typeof(int));

            DataRow dtrow = dt.NewRow();
            dtrow["TableName"] = "ClientDocument";
            dtrow["UserId"] = 1;
            dt.Rows.Add(dtrow);

            ClientData();
            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt);

            //Act
            CUDHistoryFactoryCache objFactoryCache = new CUDHistoryFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;
            var result = objFactoryCache.GetAllEntities(0, 1);

            //Assert
           
            List<CUDHistoryObjectModel> lstExpected = new List<CUDHistoryObjectModel>();
            lstExpected.Add(new CUDHistoryObjectModel()
            {
                TableName = "ClientDocument",
                UserId = 1
            });

            List<string> lstExclude = new List<string>() { "Key", "UtcCUDDate" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<CUDHistoryKey>(lstExpected, result.ToList());

            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Return_IEnumerable_Handles_isCUDHistory_EmptyData
        /// <summary>
        ///GetAllEntities_Return_IEnumerable_Handles_isCUDHistory_EmptyData
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Return_IEnumerable_Handles_isCUDHistory_EmptyData()
        {
            //Arrange  
            DataTable dt = new DataTable();
            dt.Columns.Add("TableName", typeof(string));
            dt.Columns.Add("UserId", typeof(int));

            ClientData();
            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt);

            //Act
            CUDHistoryFactoryCache objFactoryCache = new CUDHistoryFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;
            var result = objFactoryCache.GetAllEntities(0, 1);

            //Assert
            mockDataAccess.VerifyAll();
            ValidateEmptyData(result);
        }
        #endregion

        #region GetAllEntities_Return_IEnumerable_Calls_Factory_Handles_isCUDHistory
        /// <summary>
        ///GetAllEntities_Return_IEnumerable_Calls_Factory_Handles_isCUDHistory
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Return_IEnumerable_Calls_Factory_Handles_isCUDHistory()
        {
            //Arrange  
            DataTable dt = new DataTable();
            dt.Columns.Add("TableName", typeof(string));
            dt.Columns.Add("UserId", typeof(int));

            DataRow dtrow = dt.NewRow();
            dtrow["TableName"] = "ClientDocument";
            dtrow["UserId"] = 1;
            dt.Rows.Add(dtrow);

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt);

            //Act
            CUDHistoryFactory objFactory = new CUDHistoryFactory(mockDataAccess.Object);
            var result = objFactory.GetAllEntities(0, 1);

            //Assert
            mockDataAccess.VerifyAll();
            List<CUDHistoryObjectModel> lstExpected = new List<CUDHistoryObjectModel>();
            lstExpected.Add(new CUDHistoryObjectModel()
            {
                TableName = "ClientDocument",
                UserId = 1
            });

            List<string> lstExclude = new List<string>() { "Key", "UtcCUDDate" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<CUDHistoryKey>(lstExpected, result.ToList());
        }
        #endregion

        #region GetAllEntities_Return_IEnumerable_Calls_Factory_Handles_isCUDHistory_EmptyData
        /// <summary>
        ///GetAllEntities_Return_IEnumerable_Calls_Factory_Handles_isCUDHistory_EmptyData
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Return_IEnumerable_Calls_Factory_Handles_isCUDHistory_EmptyData()
        {
            //Arrange  
            DataTable dt = new DataTable();
            dt.Columns.Add("TableName", typeof(string));
            dt.Columns.Add("UserId", typeof(int));

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dt);

            //Act
            CUDHistoryFactory objFactory = new CUDHistoryFactory(mockDataAccess.Object);
            var result = objFactory.GetAllEntities(0, 1);

            //Assert
            mockDataAccess.VerifyAll();
            ValidateEmptyData(result);
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
            CUDHistoryFactory objFactory = new CUDHistoryFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<CUDHistoryObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

        #region GetEntitiesBySearch_Returns_Ienumerable
        /// <summary>
        /// GetEntitiesBySearch_Returns_Ienumerable
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_Ienumerable()
        {
            //Arrange
            CUDHistorySearchDetail objcudhistorysearch = new CUDHistorySearchDetail();
            CUDHistorySortDetail objcudhistorysort = new CUDHistorySortDetail();
            CUDHistoryKey objCudHistoryKey = new CUDHistoryKey(1, "Test_column");

            DataSet dSet = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("RowRank", typeof(Int32));
            dt.Columns.Add("CUDHistoryId", typeof(Int32));
            dt.Columns.Add("TableName", typeof(string));
            dt.Columns.Add("CUDType", typeof(string));
            dt.Columns.Add("UtcCUDDate", typeof(DateTime));
            dt.Columns.Add("UserId", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["RowRank"] = 1;
            dtrow["CUDHistoryId"] = 1;
            dtrow["TableName"] = "ClientDocument";
            dtrow["CUDType"] = "D";
            dtrow["UtcCUDDate"] = DateTime.Now;
            dtrow["UserId"] = 2;
            dt.Rows.Add(dtrow);
            dSet.Tables.Add(dt);

            DataTable dtCount = new DataTable();
            dtCount.Columns.Add("Count", typeof(Int32));
            DataRow dtrowCnt = dtCount.NewRow();
            dtrowCnt["Count"] = 1;
            dtCount.Rows.Add(dtrowCnt);
            dSet.Tables.Add(dtCount);

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="CUDHistoryId", Value=1 },
                new SqlParameter(){ ParameterName="TableName", Value="ClientDocument" },
                new SqlParameter(){ ParameterName="CUDType", Value="D" },
                new SqlParameter(){ ParameterName="UTCFromCUDDate", Value=null },
                new SqlParameter(){ ParameterName="UTCToCUDDate", Value=null },
                new SqlParameter(){ ParameterName="PageSize", Value=1 },
                new SqlParameter(){ ParameterName="PageIndex", Value=1 },
                new SqlParameter(){ ParameterName="SortDirection", Value="Ascending" },
                new SqlParameter(){ ParameterName="SortColumn", Value="CUDHistoryId" },
                new SqlParameter(){ ParameterName="UserId", Value=1 }
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
                .Returns(parameters[9]);

            mockDataAccess.Setup(X => X.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dSet);

            //Act
            CUDHistoryFactory objCUDHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
            var result = objCUDHistoryFactory.GetEntitiesBySearch(1, 2, objcudhistorysearch, objcudhistorysort, objCudHistoryKey);

            //Assert
            mockDataAccess.VerifyAll();

            List<CUDHistoryObjectModel> lstExpected = new List<CUDHistoryObjectModel>();
            lstExpected.Add(new CUDHistoryObjectModel()
            {
                CUDHistoryId = 1,
                TableName = "ClientDocument",
                CUDType = "D",
                UserId = 2
            });

            List<string> lstExclude = new List<string>() { "Key", "UtcCUDDate" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<CUDHistoryKey>(lstExpected, result.ToList());
        }
        #endregion

        #region GetEntitiesBySearch_Returns_Ienumerable_WithOutUserId
        /// <summary>
        /// GetEntitiesBySearch_Returns_Ienumerable_WithOutUserId
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_Ienumerable_WithOutUserId()
        {
            //Arrange
            CUDHistorySearchDetail objcudhistorysearch = new CUDHistorySearchDetail();
            CUDHistorySortDetail objcudhistorysort = new CUDHistorySortDetail();
            CUDHistoryKey objCudHistoryKey = new CUDHistoryKey(1, "Test_column");

            DataSet dSet = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("RowRank", typeof(Int32));
            dt.Columns.Add("CUDHistoryId", typeof(Int32));
            dt.Columns.Add("TableName", typeof(string));
            dt.Columns.Add("CUDType", typeof(string));
            dt.Columns.Add("UtcCUDDate", typeof(DateTime));
            dt.Columns.Add("EmptyColumn", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["RowRank"] = 1;
            dtrow["CUDHistoryId"] = 1;
            dtrow["TableName"] = "ClientDocument";
            dtrow["CUDType"] = "D";
            dtrow["UtcCUDDate"] = DateTime.Now;
            dtrow["EmptyColumn"] = 2;
            dt.Rows.Add(dtrow);
            dSet.Tables.Add(dt);

            DataTable dtCount = new DataTable();
            dtCount.Columns.Add("Count", typeof(Int32));
            DataRow dtrowCnt = dtCount.NewRow();
            dtrowCnt["Count"] = 1;
            dtCount.Rows.Add(dtrowCnt);
            dSet.Tables.Add(dtCount);

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="CUDHistoryId", Value=1 },
                new SqlParameter(){ ParameterName="TableName", Value="ClientDocument" },
                new SqlParameter(){ ParameterName="CUDType", Value="D" },
                new SqlParameter(){ ParameterName="UTCFromCUDDate", Value=null },
                new SqlParameter(){ ParameterName="UTCToCUDDate", Value=null },
                new SqlParameter(){ ParameterName="PageSize", Value=1 },
                new SqlParameter(){ ParameterName="PageIndex", Value=1 },
                new SqlParameter(){ ParameterName="SortDirection", Value="Ascending" },
                new SqlParameter(){ ParameterName="SortColumn", Value="CUDHistoryId" },
                new SqlParameter(){ ParameterName="UserId", Value=1 }
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
                .Returns(parameters[9]);

            mockDataAccess.Setup(X => X.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dSet);

            //Act
            CUDHistoryFactory objCUDHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
            var result = objCUDHistoryFactory.GetEntitiesBySearch(1, 2, objcudhistorysearch, objcudhistorysort, objCudHistoryKey);

            //Assert
            mockDataAccess.VerifyAll();

            List<CUDHistoryObjectModel> lstExpected = new List<CUDHistoryObjectModel>();
            lstExpected.Add(new CUDHistoryObjectModel()
            {
                CUDHistoryId = 1,
                TableName = "ClientDocument",
                CUDType = "D"
            });

            List<string> lstExclude = new List<string>() { "Key", "UtcCUDDate" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<CUDHistoryKey>(lstExpected, result.ToList());
        }
        #endregion

        #region GetEntitiesBySearch_Returns_Ienumerable_EmptyUserId
        /// <summary>
        /// GetEntitiesBySearch_Returns_Ienumerable_EmptyUserId
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_Ienumerable_EmptyUserId()
        {
            //Arrange
            CUDHistorySearchDetail objcudhistorysearch = new CUDHistorySearchDetail();
            CUDHistorySortDetail objcudhistorysort = new CUDHistorySortDetail();
            CUDHistoryKey objCudHistoryKey = new CUDHistoryKey(1, "Test_column");

            DataSet dSet = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("RowRank", typeof(Int32));
            dt.Columns.Add("CUDHistoryId", typeof(Int32));
            dt.Columns.Add("TableName", typeof(string));
            dt.Columns.Add("CUDType", typeof(string));
            dt.Columns.Add("UtcCUDDate", typeof(DateTime));
            dt.Columns.Add("UserId", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["RowRank"] = 1;
            dtrow["CUDHistoryId"] = 1;
            dtrow["TableName"] = "ClientDocument";
            dtrow["CUDType"] = "D";
            dtrow["UtcCUDDate"] = DateTime.Now;
            dtrow["UserId"] = DBNull.Value;
            dt.Rows.Add(dtrow);
            dSet.Tables.Add(dt);

            DataTable dtCount = new DataTable();
            dtCount.Columns.Add("Count", typeof(Int32));
            DataRow dtrowCnt = dtCount.NewRow();
            dtrowCnt["Count"] = 1;
            dtCount.Rows.Add(dtrowCnt);
            dSet.Tables.Add(dtCount);

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="CUDHistoryId", Value=1 },
                new SqlParameter(){ ParameterName="TableName", Value="ClientDocument" },
                new SqlParameter(){ ParameterName="CUDType", Value="D" },
                new SqlParameter(){ ParameterName="UTCFromCUDDate", Value=null },
                new SqlParameter(){ ParameterName="UTCToCUDDate", Value=null },
                new SqlParameter(){ ParameterName="PageSize", Value=1 },
                new SqlParameter(){ ParameterName="PageIndex", Value=1 },
                new SqlParameter(){ ParameterName="SortDirection", Value="Ascending" },
                new SqlParameter(){ ParameterName="SortColumn", Value="CUDHistoryId" },
                new SqlParameter(){ ParameterName="UserId", Value=1 }
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
                .Returns(parameters[9]);

            mockDataAccess.Setup(X => X.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dSet);

            //Act
            CUDHistoryFactory objCUDHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
            var result = objCUDHistoryFactory.GetEntitiesBySearch(1, 2, objcudhistorysearch, objcudhistorysort, objCudHistoryKey);

            //Assert
            mockDataAccess.VerifyAll();

            List<CUDHistoryObjectModel> lstExpected = new List<CUDHistoryObjectModel>();
            lstExpected.Add(new CUDHistoryObjectModel()
            {
                CUDHistoryId = 1,
                TableName = "ClientDocument",
                CUDType = "D"
            });

            List<string> lstExclude = new List<string>() { "Key", "UtcCUDDate" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<CUDHistoryKey>(lstExpected, result.ToList());
        }
        #endregion

        #region GetEntitiesBySearch_Returns_Ienumerable_WithSearchData
        /// <summary>
        /// GetEntitiesBySearch_Returns_Ienumerable_WithSearchData
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_Ienumerable_WithSearchData()
        {
            //Arrange
            CUDHistorySearchDetail objcudhistorysearch = new CUDHistorySearchDetail();
            objcudhistorysearch.IsHistoryData = true;
            CUDHistorySortDetail objcudhistorysort = new CUDHistorySortDetail();
            CUDHistoryKey objCudHistoryKey = new CUDHistoryKey(1, "Test_column");

            DataSet dSet = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("RowRank", typeof(Int32));
            dt.Columns.Add("CUDHistoryId", typeof(Int32));
            dt.Columns.Add("ColumnName", typeof(string));
            dt.Columns.Add("SqlDbType", typeof(Int32));
            dt.Columns.Add("OldValue", typeof(string));
            dt.Columns.Add("NewValue", typeof(string));
            dt.Columns.Add("OldValueBinary", typeof(byte[]));
            dt.Columns.Add("NewValueBinary", typeof(byte[]));
           // dt.Columns.Add("UserId", typeof(Int32));

            var OldBinary = new byte[] { 0x20 };
            var NewBinary = new byte[] { 0x20, 0x20 };

            DataRow dtrow = dt.NewRow();
            dtrow["RowRank"] = 1;
            dtrow["CUDHistoryId"] = 1;
            dtrow["ColumnName"] = "CUdHistoryid";
            dtrow["SqlDbType"] = 165;
            dtrow["OldValue"] = "Old";
            dtrow["NewValue"] = "New";
            dtrow["OldValueBinary"] = OldBinary;
            dtrow["NewValueBinary"] = NewBinary;
           // dtrow["UserId"] = 2;
            dt.Rows.Add(dtrow);
            dSet.Tables.Add(dt);

            DataTable dtCount = new DataTable();
            dtCount.Columns.Add("Count", typeof(Int32));
            DataRow dtrowCnt = dtCount.NewRow();
            dtrowCnt["Count"] = 1;
            dtCount.Rows.Add(dtrowCnt);
            dSet.Tables.Add(dtCount);
             
            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="CUDHistoryId", Value=1 },
                new SqlParameter(){ ParameterName="PageSize", Value=1 },
                new SqlParameter(){ ParameterName="PageIndex", Value=1 },
                new SqlParameter(){ ParameterName="SortDirection", Value="Ascending" },
                new SqlParameter(){ ParameterName="SortColumn", Value="CUDHistoryId" },
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4]);

            mockDataAccess.Setup(X => X.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dSet);

            //Act
            CUDHistoryFactory objCUDHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
            var result = objCUDHistoryFactory.GetEntitiesBySearch(1, 2, objcudhistorysearch, objcudhistorysort, objCudHistoryKey);

            //Assert
            mockDataAccess.VerifyAll();

            List<CUDHistoryObjectModel> lstExpected = new List<CUDHistoryObjectModel>();
            lstExpected.Add(new CUDHistoryObjectModel()
            {
                CUDHistoryId = 1,
                ColumnName="CUdHistoryid",
                OldValue= "Old",
                NewValue= "New",
                SqlDbType = 165,
                IsBinaryImage=true,
                NewImageDataURL="data:image/png;base64,ICA=",
                OldImageDataURL="data:image/png;base64,IA=="
            });

            List<string> lstExclude = new List<string>() { "Key","UtcCUDDate" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<CUDHistoryKey>(lstExpected, result.ToList());
        }
        #endregion

        #region GetEntitiesBySearch_Returns_Ienumerable_WithSearchData_EmptyBinaryData
        /// <summary>
        /// GetEntitiesBySearch_Returns_Ienumerable_WithSearchData_EmptyBinaryData
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_Ienumerable_WithSearchData_EmptyBinaryData()
        {
            //Arrange
            CUDHistorySearchDetail objcudhistorysearch = new CUDHistorySearchDetail();
            objcudhistorysearch.IsHistoryData = true;
            CUDHistorySortDetail objcudhistorysort = new CUDHistorySortDetail();
            CUDHistoryKey objCudHistoryKey = new CUDHistoryKey(1, "Test_column");

            DataSet dSet = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("RowRank", typeof(Int32));
            dt.Columns.Add("CUDHistoryId", typeof(Int32));
            dt.Columns.Add("ColumnName", typeof(string));
            dt.Columns.Add("SqlDbType", typeof(Int32));
            dt.Columns.Add("OldValue", typeof(string));
            dt.Columns.Add("NewValue", typeof(string));
            dt.Columns.Add("OldValueBinary", typeof(byte[]));
            dt.Columns.Add("NewValueBinary", typeof(byte[]));
            // dt.Columns.Add("UserId", typeof(Int32));

            var OldBinary = new byte[] {  };
            var NewBinary = new byte[] {  };

            DataRow dtrow = dt.NewRow();
            dtrow["RowRank"] = 1;
            dtrow["CUDHistoryId"] = 1;
            dtrow["ColumnName"] = "CUdHistoryid";
            dtrow["SqlDbType"] = 165;
            dtrow["OldValue"] = "Old";
            dtrow["NewValue"] = "New";
            dtrow["OldValueBinary"] = DBNull.Value;
            dtrow["NewValueBinary"] = DBNull.Value;
            // dtrow["UserId"] = 2;
            dt.Rows.Add(dtrow);
            dSet.Tables.Add(dt);

            DataTable dtCount = new DataTable();
            dtCount.Columns.Add("Count", typeof(Int32));
            DataRow dtrowCnt = dtCount.NewRow();
            dtrowCnt["Count"] = 1;
            dtCount.Rows.Add(dtrowCnt);
            dSet.Tables.Add(dtCount);

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="CUDHistoryId", Value=1 },
                new SqlParameter(){ ParameterName="PageSize", Value=1 },
                new SqlParameter(){ ParameterName="PageIndex", Value=1 },
                new SqlParameter(){ ParameterName="SortDirection", Value="Ascending" },
                new SqlParameter(){ ParameterName="SortColumn", Value="CUDHistoryId" },
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4]);

            mockDataAccess.Setup(X => X.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dSet);

            //Act
            CUDHistoryFactory objCUDHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
            var result = objCUDHistoryFactory.GetEntitiesBySearch(1, 2, objcudhistorysearch, objcudhistorysort, objCudHistoryKey);

            //Assert
            mockDataAccess.VerifyAll();

            List<CUDHistoryObjectModel> lstExpected = new List<CUDHistoryObjectModel>();
            lstExpected.Add(new CUDHistoryObjectModel()
            {
                CUDHistoryId = 1,
                ColumnName = "CUdHistoryid",
                OldValue = "Old",
                NewValue = "New",
                SqlDbType = 165,
                IsBinaryImage = true
            });

            List<string> lstExclude = new List<string>() { "Key", "UtcCUDDate" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<CUDHistoryKey>(lstExpected, result.ToList());
        }
        #endregion

        #region GetEntitiesBySearch_Returns_Ienumerable_WithSearchData_InvalidSqlDbType
        /// <summary>
        /// GetEntitiesBySearch_Returns_Ienumerable_WithSearchData_InvalidSqlDbType
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_Ienumerable_WithSearchData_InvalidSqlDbType()
        {
            //Arrange
            CUDHistorySearchDetail objcudhistorysearch = new CUDHistorySearchDetail();
            objcudhistorysearch.IsHistoryData = true;
            CUDHistorySortDetail objcudhistorysort = new CUDHistorySortDetail();
            CUDHistoryKey objCudHistoryKey = new CUDHistoryKey(1, "Test_column");

            DataSet dSet = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("RowRank", typeof(Int32));
            dt.Columns.Add("CUDHistoryId", typeof(Int32));
            dt.Columns.Add("ColumnName", typeof(string));
            dt.Columns.Add("SqlDbType", typeof(Int32));
            dt.Columns.Add("OldValue", typeof(string));
            dt.Columns.Add("NewValue", typeof(string));
            dt.Columns.Add("OldValueBinary", typeof(byte[]));
            dt.Columns.Add("NewValueBinary", typeof(byte[]));
            // dt.Columns.Add("UserId", typeof(Int32));

            var OldBinary = new byte[] { };
            var NewBinary = new byte[] { };

            DataRow dtrow = dt.NewRow();
            dtrow["RowRank"] = 1;
            dtrow["CUDHistoryId"] = 1;
            dtrow["ColumnName"] = "CUdHistoryid";
            dtrow["SqlDbType"] = 100;
            dtrow["OldValue"] = "Old";
            dtrow["NewValue"] = "New";
            dtrow["OldValueBinary"] = DBNull.Value;
            dtrow["NewValueBinary"] = DBNull.Value;
            // dtrow["UserId"] = 2;
            dt.Rows.Add(dtrow);
            dSet.Tables.Add(dt);

            DataTable dtCount = new DataTable();
            dtCount.Columns.Add("Count", typeof(Int32));
            DataRow dtrowCnt = dtCount.NewRow();
            dtrowCnt["Count"] = 1;
            dtCount.Rows.Add(dtrowCnt);
            dSet.Tables.Add(dtCount);

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="CUDHistoryId", Value=1 },
                new SqlParameter(){ ParameterName="PageSize", Value=1 },
                new SqlParameter(){ ParameterName="PageIndex", Value=1 },
                new SqlParameter(){ ParameterName="SortDirection", Value="Ascending" },
                new SqlParameter(){ ParameterName="SortColumn", Value="CUDHistoryId" },
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4]);

            mockDataAccess.Setup(X => X.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dSet);

            //Act
            CUDHistoryFactory objCUDHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
            var result = objCUDHistoryFactory.GetEntitiesBySearch(1, 2, objcudhistorysearch, objcudhistorysort, objCudHistoryKey);

            //Assert
            mockDataAccess.VerifyAll();

            List<CUDHistoryObjectModel> lstExpected = new List<CUDHistoryObjectModel>();
            lstExpected.Add(new CUDHistoryObjectModel()
            {
                CUDHistoryId = 1,
                ColumnName = "CUdHistoryid",
                OldValue = "Old",
                NewValue = "New",
                SqlDbType = 100
            });

            List<string> lstExclude = new List<string>() { "Key", "UtcCUDDate" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<CUDHistoryKey>(lstExpected, result.ToList());
        }
        #endregion

        //#region GetEntityByKey_with_CUDHistoryKey
        ///// <summary>
        /////  GetEntityByKey_with_CUDHistoryKey
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_with_CUDHistoryKey()
        //{
        //    //Arrange  

        //    CUDHistoryKey objCUDHistory = new CUDHistoryKey(1,"Test");         
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    CUDHistoryFactory objCUDHistoryobj = new CUDHistoryFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objCUDHistoryobj.GetEntityByKey(objCUDHistory);
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

        //#region GetEntityByKey_With_Key_Return_CUDHistoryObjectModel
        ///// <summary>
        /////GetEntityByKey_With_Key_Return_CUDHistoryObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_With_Key_Return_CUDHistoryObjectModel()
        //{
        //    //Arrange  
        //    CUDHistoryKey objCudHistoryKey = new CUDHistoryKey(1, "Test_column");
        //    ClientData();
        //    Mock.Arrange(() => mockDataAccess.ExecuteDataTable(string.Empty, string.Empty, null))
        //      .IgnoreArguments()
        //      .MustBeCalled();

        //    //Act
        //    CUDHistoryFactory objCudHistoryCache = new CUDHistoryFactory(mockDataAccess);
        //    objCudHistoryCache.ClientName = "Forethought";
        //    objCudHistoryCache.GetEntityByKey(objCudHistoryKey);
        //    //Assert
        //    Mock.Assert(mockDataAccess);
        //}
        //#endregion

        //#region GetCUDHistoryEntity_With_Key_Return_CUDHistoryObjectModel
        ///// <summary>
        /////GetCUDHistoryEntity_With_Key_Return_CUDHistoryObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetCUDHistoryEntity_With_Key_Return_CUDHistoryObjectModel()
        //{//Arrange  

        //    CUDHistoryKey objCudHistoryKey = new CUDHistoryKey(1, "Test_column");

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    CUDHistoryFactory objCudHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objCudHistoryFactory.GetCUDHistoryEntity(objCudHistoryKey);
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

        //#region GetAllEntities_Return_IEnumerable_With_SortDetailParameter
        ///// <summary>
        /////GetAllEntities_Return_IEnumerable_With_SortDetailParameter
        ///// </summary>
        //[TestMethod]
        //public void GetAllEntities_Return_IEnumerable_With_SortDetailParameter()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("CUDHistoryId", typeof(Int32));
        //    dt.Columns.Add("TableName", typeof(string));
        //    dt.Columns.Add("Key", typeof(int));
        //    dt.Columns.Add("SecondKey", typeof(string));
        //    dt.Columns.Add("ThirdKey", typeof(string));
        //    dt.Columns.Add("CUDType", typeof(char));
        //    dt.Columns.Add("UtcCUDDate", typeof(DateTime));
        //    dt.Columns.Add("BatchId", typeof(string));
        //    dt.Columns.Add("UserId", typeof(Int32));
        //    dt.Columns.Add("isTableName", typeof(Int32));
        //    dt.Columns.Add("isCUDHistory", typeof(Int32));

        //    DataRow dtrow = dt.NewRow();

        //    dtrow["CUDHistoryId"] = 1;
        //    dtrow["TableName"] = "Test_1";
        //    dtrow["Key"] = 3;
        //    dtrow["SecondKey"] = "34628P193";
        //    dtrow["ThirdKey"] = "34628P193";
        //    dtrow["CUDType"] = 'A';
        //    dtrow["UtcCUDDate"] = DateTime.Today;
        //    dtrow["BatchId"] = "R1";
        //    dtrow["UserId"] = 2;
        //    dtrow["isTableName"] = "2";
        //    dtrow["isCUDHistory"] = "6";
        //    dt.Rows.Add(dtrow);
        //    //Arrange 
        //    CUDHistorySortDetail objCUDSortdetail = new CUDHistorySortDetail();

        //    CUDHistoryKey objCudHistoryKey = new CUDHistoryKey(1, "Test_column");
        //    ClientData();
        //    Mock.Arrange(() => mockDataAccess.ExecuteDataTable(string.Empty, string.Empty, null))
        //      .IgnoreArguments()
        //   //   .Returns(dt)
        //      .MustBeCalled();

        //    //Act
        //    CUDHistoryFactory objCudHistoryCache = new CUDHistoryFactory(mockDataAccess);
        //    objCudHistoryCache.ClientName = "Forethought";
        //    objCudHistoryCache.GetAllEntities(0, 1, objCUDSortdetail);
        //    //Assert
        //    Mock.Assert(mockDataAccess);
        //}
        //#endregion

        //#region SaveEntity_with_ModifyBy
        ///// <summary>
        ///// SaveEntity_with_ModifyBy
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_with_ModifyBy()
        //{
        //    //Arrange  
        //    CUDHistoryObjectModel objCUDHistorymodel=new CUDHistoryObjectModel();
        //    objCUDHistorymodel.CUDHistoryId = 1;
        //    objCUDHistorymodel.UserId = 2;
        //    objCUDHistorymodel.CUDKey = 1;
        //    objCUDHistorymodel.SecondKey = "Test";
        //    objCUDHistorymodel.ThirdKey = "Test";
        //    objCUDHistorymodel.BatchId = Guid.Empty;
        //    objCUDHistorymodel.IsAdmin = false;

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    CUDHistoryFactory objCUDHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objCUDHistoryFactory.SaveEntity(objCUDHistorymodel, 32);
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

        //#region DeleteEntity_With_CUDHistoryObjectModel
        ///// <summary>
        ///// DeleteEntity_With_CUDHistoryObjectModel
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_CUDHistoryObjectModel()
        //{
        //    //Arrange  
        //    CUDHistoryObjectModel objCUDHistorymodel = new CUDHistoryObjectModel();
        //    objCUDHistorymodel.CUDHistoryId = 1;
        //    objCUDHistorymodel.UserId = 2;
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    CUDHistoryFactory objCUDHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objCUDHistoryFactory.DeleteEntity(objCUDHistorymodel);
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

        //#region DeleteEntity_With_deletedBy
        ///// <summary>
        ///// DeleteEntity_With_deletedBy
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_deletedBy()
        //{
        //    //Arrange  
        //    CUDHistoryObjectModel objCUDHistorymodel = new CUDHistoryObjectModel();
        //    objCUDHistorymodel.CUDHistoryId = 1;
        //    objCUDHistorymodel.UserId = 2;
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    CUDHistoryFactory objCUDHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objCUDHistoryFactory.DeleteEntity(objCUDHistorymodel,32);
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

        //#region DeleteEntity_With_CUDHistoryKey
        ///// <summary>
        ///// DeleteEntity_With_CUDHistoryKey
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_CUDHistoryKey()
        //{
        //    //Arrange  
        //    CUDHistoryKey objCudHistoryKey = new CUDHistoryKey(1, "Test_column");
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    CUDHistoryFactory objCUDHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objCUDHistoryFactory.DeleteEntity(objCudHistoryKey);
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

        //#region DeleteEntity_With_modifiedBy
        ///// <summary>
        ///// DeleteEntity_With_modifiedBy
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_modifiedBy()
        //{
        //    //Arrange  
        //    CUDHistoryKey objCudHistoryKey = new CUDHistoryKey(1, "Test_column");
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    CUDHistoryFactory objCUDHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objCUDHistoryFactory.DeleteEntity(objCudHistoryKey,32);
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

        //#region GetEntityByKey_With_CUDHistoryKey
        ///// <summary>
        ///// GetEntityByKey_With_CUDHistoryKey
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_With_CUDHistoryKey()
        //{
        //    //Arrange  
        //    CUDHistoryKey objCudHistoryKey = new CUDHistoryKey(1, "Test_column");
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    CUDHistoryFactory objCUDHistoryFactory = new CUDHistoryFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objCUDHistoryFactory.GetEntityByKey(objCudHistoryKey);
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
