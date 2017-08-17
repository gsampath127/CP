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
using RRD.FSG.RP.Model.Cache.Client;
using System.Data.Common;
using System.Data.SqlClient;
using RRD.FSG.RP.Model.Factories;
using System.Web.Script.Serialization;
using Moq;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    [TestClass]
    public class ReportContentFactoryTests : BaseTestFactory<ReportContentObjectModel>
    {
        Mock<IDataAccess> mockDataAccess;

        [TestInitialize]
        public void TestInitialze()
        {
            mockDataAccess = new Mock<IDataAccess>();
        }
        //#region ClientData
        ///// <summary>
        ///// ClientData
        ///// </summary>
        //[TestMethod]
        //public void ClientData()
        //{
        //    DataSet dSet = new DataSet();
        //    //Table 0
        //    DataTable dt1 = new DataTable();
        //    dt1.Columns.Add("ClientID", typeof(Int32));
        //    dt1.Columns.Add("ClientName", typeof(string));
        //    dt1.Columns.Add("ClientDNS", typeof(string));
        //    dt1.Columns.Add("ClientConnectionStringName", typeof(string));
        //    dt1.Columns.Add("ClientDatabaseName", typeof(string));
        //    dt1.Columns.Add("VerticalMarketConnectionStringName", typeof(string));
        //    dt1.Columns.Add("VerticalMarketsDatabaseName", typeof(string));

        //    DataRow dtrow1 = dt1.NewRow();
        //    dtrow1["ClientID"] = 2;
        //    dtrow1["ClientName"] = "Forethought";
        //    dtrow1["ClientDNS"] = null;
        //    dtrow1["ClientConnectionStringName"] = "ClientDBInstance1";
        //    dtrow1["ClientDatabaseName"] = "RPV2ClientDb1";
        //    dtrow1["VerticalMarketConnectionStringName"] = "USVerticalMarketDBInstance";
        //    dtrow1["VerticalMarketsDatabaseName"] = "RPV2USDB";
        //    dt1.Rows.Add(dtrow1);
        //    dSet.Tables.Add(dt1);

        //    //Table 1
        //    DataTable dt2 = new DataTable();
        //    dt2.Columns.Add("TemplateId", typeof(Int32));
        //    dt2.Columns.Add("TemplateName", typeof(string));
        //    dt2.Columns.Add("PageID", typeof(Int32));
        //    dt2.Columns.Add("PageName", typeof(string));

        //    DataRow dtrow2 = dt2.NewRow();
        //    dtrow2["TemplateId"] = 1;
        //    dtrow2["TemplateName"] = "Default";
        //    dtrow2["PageID"] = 1;
        //    dtrow2["PageName"] = "TAL";

        //    dt2.Rows.Add(dtrow2);
        //    dSet.Tables.Add(dt2);

        //    //Table 2
        //    DataTable dt3 = new DataTable();
        //    dt3.Columns.Add("TemplateId", typeof(Int32));
        //    dt3.Columns.Add("DefaultNavigationXml", typeof(string));
        //    dt3.Columns.Add("NavigationKey", typeof(string));
        //    dt3.Columns.Add("XslTransform", typeof(string));

        //    dSet.Tables.Add(dt3);

        //    //Table 3
        //    DataTable dt4 = new DataTable();
        //    dt4.Columns.Add("TemplateId", typeof(Int32));
        //    dt4.Columns.Add("PageId", typeof(Int32));
        //    dt4.Columns.Add("NavigationKey", typeof(string));
        //    dt4.Columns.Add("XslTransform", typeof(XmlReadMode));
        //    dt4.Columns.Add("DefaultNavigationXml", typeof(XmlReadMode));

        //    DataRow dtrow4 = dt4.NewRow();
        //    dtrow4["TemplateId"] = 1;
        //    dtrow4["PageId"] = 3;
        //    dtrow4["NavigationKey"] = "TaxonomySpecificDocumentFrame_DocumentType";
        //    dtrow4["XslTransform"] = DBNull.Value;
        //    dtrow4["DefaultNavigationXml"] = DBNull.Value;
        //    dt4.Rows.Add(dtrow4);
        //    dSet.Tables.Add(dt4);

        //    mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
        //     .Returns(dSet);


        //}
        //#endregion

        #region ClientData
        /// <summary>
        /// ClientData
        /// </summary>
        [TestMethod]
        public void ClientData_Insequence()
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

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>()))
           .Returns(dSet);

        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ReportContenttId", typeof(Int32));
            dt.Columns.Add("ReportScheduleId", typeof(Int32));
            dt.Columns.Add("MimeType", typeof(string));
            dt.Columns.Add("IsPrivate", typeof(Int32));
            dt.Columns.Add("ContentUri", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("ReportRunDate", typeof(DateTime));
            dt.Columns.Add("ModifiedDate", typeof(DateTime));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("Description", typeof(string));

            DataRow dr = dt.NewRow();
            dr["ReportContenttId"] = 2;
            dr["ReportScheduleId"] = 1;
            dr["MimeType"] = "application";
            dr["IsPrivate"] = 0;
            dr["ContentUri"] = null;
            dr["Name"] = "quarterlyreport";
            dr["ReportRunDate"] = DateTime.Parse("01/02/2015").ToString();
            dr["ModifiedDate"] = DateTime.Parse("01/02/2015").ToString();
            dr["UtcLastModified"] = DateTime.Now.ToString();
            dr["ModifiedBy"] = 1;
            dr["FileName"] = "quarterlyreport-10-29-2015";
            dr["Description"] = "quarterlyreport";
            dt.Rows.Add(dr);

            //mocking ClientData
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
            //mocking ClientData

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);
            //Act
            ReportContentFactory objFactory = new ReportContentFactory(mockDataAccess.Object);
            objFactory.ClientName = "Forethought";
            var result = objFactory.GetAllEntities(2, 3);
            //Assert
            List<ReportContentObjectModel> lstExpected = new List<ReportContentObjectModel>();
            ReportContentObjectModel obj = new ReportContentObjectModel()
            {
                ContentUri = null,
                Data = null,
                Description = "quarterlyreport",
                FileName = "quarterlyreport-10-29-2015",
                FrequencyType = null,
                EmailBody = null,
                EmailSubject = null,
                IsAttachedZipFile = false,
                IsPrivate = 0,
                MimeType = "application",
                Name = "quarterlyreport",
                ReportContentId = 2,
                ReportScheduleId = 1,
                ReportFromDate = DateTime.Parse("01/01/0001"),
                ReportRunDate = DateTime.Parse("01/02/2015"),
                ReportToDate = DateTime.Parse("01/01/0001"),

            };
            List<string> lstExclude = new List<string>()
            {
                "ModifiedBy",
                "LastModified",
                "Key"
            };
            lstExpected.Add(obj);
            ValidateListData(lstExpected, result.ToList(), lstExclude);

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
            DataTable dt = new DataTable();
            dt.Columns.Add("ReportContenttId", typeof(Int32));
            dt.Columns.Add("ReportScheduleId", typeof(Int32));
            dt.Columns.Add("MimeType", typeof(string));
            dt.Columns.Add("IsPrivate", typeof(Int32));
            dt.Columns.Add("ContentUri", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("ReportRunDate", typeof(DateTime));
            dt.Columns.Add("ModifiedDate", typeof(DateTime));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("Description", typeof(string));

            DataRow dr = dt.NewRow();
            dr["ReportContenttId"] = 2;
            dr["ReportScheduleId"] = 1;
            dr["MimeType"] = "application";
            dr["IsPrivate"] = 0;
            dr["ContentUri"] = null;
            dr["Name"] = "quarterlyreport";
            dr["ReportRunDate"] = DateTime.Parse("01/02/2015");
            dr["ModifiedDate"] = DateTime.Parse("01/02/2015");
            dr["UtcLastModified"] = DateTime.Parse("01/02/2015");
            dr["ModifiedBy"] = 1;
            dr["FileName"] = "quarterlyreport-10-29-2015";
            dr["Description"] = "quarterlyreport";
            dt.Rows.Add(dr);


            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);
            //Act
            ReportContentFactory objReportContentFactory = new ReportContentFactory(mockDataAccess.Object);
            var result = objReportContentFactory.GetAllEntities(2, 3);

            //Assert
            List<ReportContentObjectModel> lstExpected = new List<ReportContentObjectModel>();
            ReportContentObjectModel obj = new ReportContentObjectModel()
               {
                   ContentUri = null,
                   Data = null,
                   Description = "quarterlyreport",
                   FileName = "quarterlyreport-10-29-2015",
                   EmailBody = null,
                   EmailSubject = null,
                   IsAttachedZipFile = false,
                   IsPrivate = 0,
                   MimeType = "application",
                   Name = "quarterlyreport",
                   ReportContentId = 2,
                   ReportScheduleId = 1,
                   ReportRunDate = DateTime.Parse("01/02/2015"),

               };
            List<string> lstExclude = new List<string>()
            {
                "ModifiedBy",
                "LastModified",
                "Key"
            };
            lstExpected.Add(obj);
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            mockDataAccess.VerifyAll();
        }
        #endregion

        #region SaveEntity_Returns_Ienumerable_with_ReportContentObjectModel_Parameter
        /// <summary>
        /// SaveEntity_Returns_Ienumerable_with_ReportContentObjectModel_Parameter
        /// </summary>
        [TestMethod]
        public void SaveEntity_Returns_Ienumerable_with_ReportContentObjectModel_Parameter()
        {
            //Arrange       
            var OldBinary = new byte[] { 0x20 };
            ReportContentObjectModel objReportContentmodel = new ReportContentObjectModel()
            {
                FileName = "Test",
                ReportScheduleId = 1,
                MimeType = "Test_001",
                IsPrivate = 1,
                ContentUri = "Test_url",
                Name = "Test_2",
                Description = "TEST",
                ReportRunDate = DateTime.Now,
                Data = OldBinary
            };

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="FileName", Value="Test" },
                new SqlParameter(){ ParameterName="ReportScheduleId", Value=1 },
                new SqlParameter(){ ParameterName="MimeType", Value="Test_001" },
                new SqlParameter(){ ParameterName="IsPrivate", Value=1 },
                new SqlParameter(){ ParameterName="ContentUri", Value="Test_url" },
                new SqlParameter(){ ParameterName="Name", Value="Test_2" },
                new SqlParameter(){ ParameterName="Description", Value="TEST" },
                new SqlParameter(){ ParameterName="ReportRunDate", Value=DateTime.Now },
                new SqlParameter(){ParameterName="ModifiedBy", Value=32},
                new SqlParameter(){ParameterName="Data", Value=3},

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

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ReportContentFactory objReportContentFactory = new ReportContentFactory(mockDataAccess.Object);
            objReportContentFactory.SaveEntity(objReportContentmodel, 1);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region Delete_with_deletedBy
        /// <summary>
        /// Delete_with_deletedBy
        /// </summary>
        [TestMethod]
        public void Delete_with_deletedBy()
        {

            //Arrange           
            ReportContentObjectModel objReportContentmodel = new ReportContentObjectModel()
            {
                ReportContentId = 1,
            };

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ReportContentId", Value=1 },
                new SqlParameter(){ ParameterName="DeletedBy", Value=8},

             
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
            .Returns(parameters[1]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));


            //Act
            ReportContentFactory objReportContentFactory = new ReportContentFactory(mockDataAccess.Object);
            objReportContentFactory.DeleteEntity(objReportContentmodel, 2);
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
            ReportContentObjectModel obj = new ReportContentObjectModel()
            {
                ReportContentId = 1,
            };
            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ReportContentId", Value=1 },
             
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            ReportContentFactory objReportContentFactory = new ReportContentFactory(mockDataAccess.Object);
            objReportContentFactory.DeleteEntity(1, 1);

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
            ReportContentFactory objReportContentFactory = new ReportContentFactory(mockDataAccess.Object);
            var result = objReportContentFactory.CreateEntity<ReportContentObjectModel>(dr);
            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion
    }
}
