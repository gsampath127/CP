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
    /// Test class for VerticalXmlExportFactory class
    /// </summary>
    [TestClass]
    public class VerticalXmlExportFactoryTests : BaseTestFactory<VerticalXmlExportObjectModel>
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

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dSet);
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_VerticalXmlExportObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_VerticalXmlExportObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_VerticalXmlExportObjectModel()
        {
            //Arrange
            VerticalXmlExportSortDetail objSortDtl = new VerticalXmlExportSortDetail();
            objSortDtl.Column = VerticalXmlExportSortColumn.ExportDate;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("VerticalXmlExportId", typeof(Int32));
            dt.Columns.Add("ExportTypes", typeof(Int32));
            dt.Columns.Add("ExportXml", typeof(string));
            dt.Columns.Add("ExportDate", typeof(DateTime));
            dt.Columns.Add("ExportedBy", typeof(Int32));
            dt.Columns.Add("ExportDescription", typeof(string));
            dt.Columns.Add("Status", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["VerticalXmlExportId"] = 1;
            dtrow["ExportTypes"] = 1;
            dtrow["ExportXml"] = "";
            dtrow["ExportDate"] = DateTime.Parse("01/01/2015");
            dtrow["ExportedBy"] = 3;
            dtrow["ExportDescription"] = "Test_001";
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
            VerticalXmlExportFactoryCache objVerticalXmlExportFactoryCache = new VerticalXmlExportFactoryCache(mockDataAccess.Object);
            objVerticalXmlExportFactoryCache.ClientName = "Forethought";
            objVerticalXmlExportFactoryCache.Mode = FactoryCacheMode.All;
            var result = objVerticalXmlExportFactoryCache.GetAllEntities(0, 0, objSortDtl);

            List<VerticalXmlExportObjectModel> lstExpected = new List<VerticalXmlExportObjectModel>();
            VerticalXmlExportObjectModel obj = new VerticalXmlExportObjectModel()
            {
                ExportDate = DateTime.Parse("01/01/2015"),
                ExportDescription = "Test_001",
                ExportedBy = 3,
                ExportTypes = 1,
                ExportXml = null,
                ExportedByName = "",
                Status = 1,
                VerticalXmlExportId = 1,
                Description = null,
                Name = null,

            };
            lstExpected.Add(obj);
            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
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
            VerticalXmlExportSortDetail objSortDtl = new VerticalXmlExportSortDetail();
            objSortDtl.Column = VerticalXmlExportSortColumn.ExportDate;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("VerticalXmlExportId", typeof(Int32));
            dt.Columns.Add("ExportTypes", typeof(Int32));
            dt.Columns.Add("ExportXml", typeof(string));
            dt.Columns.Add("ExportDate", typeof(DateTime));
            dt.Columns.Add("ExportedBy", typeof(Int32));
            dt.Columns.Add("ExportDescription", typeof(string));
            dt.Columns.Add("Status", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["VerticalXmlExportId"] = 1;
            dtrow["ExportTypes"] = 1;
            dtrow["ExportXml"] = "";
            dtrow["ExportDate"] = DateTime.Parse("01/01/2015");
            dtrow["ExportedBy"] = 3;
            dtrow["ExportDescription"] = "Test_001";
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
            VerticalXmlExportFactory objVerticalXmlExportFactory = new VerticalXmlExportFactory(mockDataAccess.Object);
            var result = objVerticalXmlExportFactory.GetAllEntities(0, 0, objSortDtl);

            //Assert
            List<VerticalXmlExportObjectModel> lstExpected = new List<VerticalXmlExportObjectModel>();
            VerticalXmlExportObjectModel obj = new VerticalXmlExportObjectModel()
            {
                ExportDate = DateTime.Parse("01/01/2015"),
                ExportDescription = "Test_001",
                ExportedBy = 3,
                ExportTypes = 1,
                ExportXml = null,
                ExportedByName = "",
                Status = 1,
                VerticalXmlExportId = 1,
                Description = null,
                Name = null,

            };
            lstExpected.Add(obj);
            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Factory_Empty
        /// <summary>
        /// GetAllEntities_Factory_Empty
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Factory_Empty()
        {
            //Arrange
            VerticalXmlExportSortDetail objSortDtl = new VerticalXmlExportSortDetail();
            objSortDtl.Column = VerticalXmlExportSortColumn.ExportDate;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();


            DataTable dtUser = new DataTable();

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt)
                .Returns(dtUser);

            //Act
            VerticalXmlExportFactory objVerticalXmlExportFactory = new VerticalXmlExportFactory(mockDataAccess.Object);
            var result = objVerticalXmlExportFactory.GetAllEntities(0, 0, objSortDtl);

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
            VerticalXmlExportObjectModel objObjectModel = new VerticalXmlExportObjectModel();
            objObjectModel.VerticalXmlExportId = 1;
            objObjectModel.ExportTypes = 1;
            objObjectModel.ExportXml = "TEST_001";
            objObjectModel.ExportedBy = 1;
            objObjectModel.ExportDescription = "TEST_001";
            objObjectModel.Status = 1;

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="VerticalXmlExportId", Value=1 },
                new SqlParameter(){ ParameterName="ExportTypes", Value=1 },
                new SqlParameter(){ ParameterName="ExportXml", Value="TEST_001" },
                 new SqlParameter(){ParameterName="ExportedBy", Value=1},
                new SqlParameter(){ ParameterName="ExportDescription", Value= "TEST_001" },
               new SqlParameter(){ ParameterName="Status", Value=1 }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4])
                .Returns(parameters[5]);


            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            VerticalXmlExportFactory objVerticalXmlExportFactoryCache = new VerticalXmlExportFactory(mockDataAccess.Object);
            objVerticalXmlExportFactoryCache.SaveEntity(objObjectModel);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity
        /// <summary>
        /// GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity()
        {
            //Arrange  
            VerticalXmlExportSearchDetail objSearchDtl = new VerticalXmlExportSearchDetail();

            ClientData();
            Exception exe = null; ;

            //Act
            VerticalXmlExportFactory objVerticalXmlExportFactory = new VerticalXmlExportFactory(mockDataAccess.Object);
            try
            {
                objVerticalXmlExportFactory.GetEntitiesBySearch(0, 0, objSearchDtl, 1);
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

        #region GetEntitiesBySearch_Returns_IEnumerable_Entity
        /// <summary>
        /// GetEntitiesBySearch_Returns_IEnumerable_Entity
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_IEnumerable_Entity()
        {
            //Arrange  
            VerticalXmlExportSearchDetail objSearchDtl = new VerticalXmlExportSearchDetail();
            VerticalXmlExportSortDetail objSortDtl = new VerticalXmlExportSortDetail();

            ClientData();
            Exception exe = null; ;

            //Act
            VerticalXmlExportFactory objVerticalXmlExportFactory = new VerticalXmlExportFactory(mockDataAccess.Object);
            try
            {
                objVerticalXmlExportFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, null);
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

        #region GetEntityByKey_Returns_VerticalXmlExportObjectModel
        /// <summary>
        /// GetEntityByKey_Returns_VerticalXmlExportObjectModel
        /// </summary>
        [TestMethod]
        public void GetEntityByKey_Returns_VerticalXmlExportObjectModel()
        {
            //Arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("VerticalXmlExportId", typeof(Int32));
            dt.Columns.Add("ExportTypes", typeof(Int32));
            dt.Columns.Add("ExportXml", typeof(string));
            dt.Columns.Add("ExportDate", typeof(DateTime));
            dt.Columns.Add("ExportedBy", typeof(Int32));
            dt.Columns.Add("ExportDescription", typeof(string));
            dt.Columns.Add("Status", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["VerticalXmlExportId"] = 1;
            dtrow["ExportTypes"] = 1;
            dtrow["ExportXml"] = "";
            dtrow["ExportDate"] = DateTime.Parse("01/01/2015");
            dtrow["ExportedBy"] = 3;
            dtrow["ExportDescription"] = "Test_001";
            dtrow["Status"] = 1;
            dt.Rows.Add(dtrow);

            //ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);
            //Act
            VerticalXmlExportFactory objVerticalXmlExportFactory = new VerticalXmlExportFactory(mockDataAccess.Object);

            List<VerticalXmlExportObjectModel> lstResult = new List<VerticalXmlExportObjectModel>
            {
                objVerticalXmlExportFactory.GetEntityByKey(1)
            };

            //Assert
            List<VerticalXmlExportObjectModel> lstExpected = new List<VerticalXmlExportObjectModel>();
            VerticalXmlExportObjectModel obj = new VerticalXmlExportObjectModel()
            {
                ExportDate = DateTime.Parse("01/01/2015"),
                ExportDescription = "Test_001",
                ExportedBy = 3,
                ExportTypes = 1,
                ExportXml = "",
                ExportedByName = null,
                Status = 1,
                VerticalXmlExportId = 1,
                Description = null,
                Name = null,

            };
            lstExpected.Add(obj);
            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, lstResult, lstExclude);

            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetEntityByKey_Returns_Empty
        /// <summary>
        /// GetEntityByKey_Returns_Empty
        /// </summary>
        [TestMethod]
        public void GetEntityByKey_Returns_Empty()
        {
            //Arrange
            DataTable dt = new DataTable();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);
            //Act
            VerticalXmlExportFactory objVerticalXmlExportFactory = new VerticalXmlExportFactory(mockDataAccess.Object);
            var result = objVerticalXmlExportFactory.GetEntityByKey(1);
            //Assert
            Assert.AreEqual(null, result);
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
            VerticalXmlExportFactory objFactory = new VerticalXmlExportFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<VerticalXmlExportObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

        #region DequeueAndSaveExportXML_Empty
        ///<summary>
        ///DequeueAndSaveExportXML_Empty
        ///<summary>
        [TestMethod]
        public void DequeueAndSaveExportXML_Empty()
        {
            //Arrange
            DataTable dtt = new DataTable();
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(dtt);
            //Act
            VerticalXmlExportFactory objVerticalXmlExportFactory = new VerticalXmlExportFactory(mockDataAccess.Object);
            objVerticalXmlExportFactory.DequeueAndSaveExportXML();
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region DequeueAndSaveExportXML
        /////<summary>
        /////DequeueAndSaveExportXML
        /////<summary>
        //[TestMethod]
        //public void DequeueAndSaveExportXML()
        //{
        //    //Arrange

        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("VerticalXmlExportId", typeof(Int32));
        //    dt.Columns.Add("ExportTypes", typeof(Int32));
        //    dt.Columns.Add("ExportXml", typeof(string));
        //    dt.Columns.Add("ExportDate", typeof(DateTime));
        //    dt.Columns.Add("ExportedBy", typeof(Int32));
        //    dt.Columns.Add("ExportDescription", typeof(string));
        //    dt.Columns.Add("Status", typeof(Int32));

        //    DataRow dtrow = dt.NewRow();
        //    dtrow["VerticalXmlExportId"] = 1;
        //    dtrow["ExportTypes"] = 1;
        //    dtrow["ExportXml"] = "";
        //    dtrow["ExportDate"] = DateTime.Parse("01/01/2015");
        //    dtrow["ExportedBy"] = 3;
        //    dtrow["ExportDescription"] = "Test_001";
        //    dtrow["Status"] = 1;
        //    dt.Rows.Add(dtrow);
        //    ClientData();

        //    mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
        //        .Returns(dt);

        //    //Act
        //    VerticalXmlExportFactory objVerticalXmlExportFactory = new VerticalXmlExportFactory(mockDataAccess.Object);
        //    objVerticalXmlExportFactory.DequeueAndSaveExportXML();
        //    //Assert
        //    mockDataAccess.VerifyAll();
        //}
        //#endregion
    }
}
