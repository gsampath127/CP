using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Entities;
using RRD.FSG.RP.Model.Entities.Client;
using System.Data.Common;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using System.Data.SqlClient;
using RRD.FSG.RP.Model.Factories.Client;
using System.Reflection;
using RRD.FSG.RP.Model.Factories;
using System.Web.Script.Serialization;
using Moq;

namespace RRD.FSG.RP.Model.Test.Factories
{
    /// <summary>
    /// Test class for DocumentTypeExternalIdFactory class
    /// </summary>
    [TestClass]
    public class DocumentTypeExternalIdFactoryTests : BaseTestFactory<DocumentTypeExternalIdObjectModel>
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

        #region GetAllEntities_Returns_Ienumerable_DocumentTypeExternalIdObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_DocumentTypeExternalIdObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_DocumentTypeExternalIdObjectModel()
        {
            //Arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("ExternalId", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("IsPrimary", typeof(bool));

            DataRow dtrow = dt.NewRow();
            dtrow["DocumentTypeId"] = 1;
            dtrow["ExternalId"] = "AR";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 28;
            dtrow["IsPrimary"] = 1;
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

            DocumentTypeExternalIdSortDetail objSortDtl = new DocumentTypeExternalIdSortDetail();
            objSortDtl.Column = DocumentTypeExternalIdSortColumn.DocumentTypeName;
            objSortDtl.Order = SortOrder.Ascending;

            //Act
            DocumentTypeExternalIdFactoryCache objDocumentTypeExternalIdFactoryCache = new DocumentTypeExternalIdFactoryCache(mockDataAccess.Object);
            objDocumentTypeExternalIdFactoryCache.ClientName = "Forethought";
            objDocumentTypeExternalIdFactoryCache.Mode = FactoryCacheMode.All;
            var result = objDocumentTypeExternalIdFactoryCache.GetAllEntities(0, 0, objSortDtl);

            List<DocumentTypeExternalIdObjectModel> lstExpected = new List<DocumentTypeExternalIdObjectModel>();
            lstExpected.Add(new DocumentTypeExternalIdObjectModel()
                {
                    DocumentTypeId = 1,
                    ExternalId = "AR",
                    IsPrimary = true
                });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "ModifiedDate", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
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
            DataTable dt = new DataTable();
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("ExternalId", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("IsPrimary", typeof(bool));

            DataRow dtrow = dt.NewRow();
            dtrow["DocumentTypeId"] = 1;
            dtrow["ExternalId"] = "AR";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 28;
            dtrow["IsPrimary"] = 1;
            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);

            DocumentTypeExternalIdSortDetail objSortDtl = new DocumentTypeExternalIdSortDetail();
            objSortDtl.Column = DocumentTypeExternalIdSortColumn.DocumentTypeName;
            objSortDtl.Order = SortOrder.Ascending;

            //Act
            DocumentTypeExternalIdFactory objDocumentTypeExternalIdFactory = new DocumentTypeExternalIdFactory(mockDataAccess.Object);
            var result = objDocumentTypeExternalIdFactory.GetAllEntities(0, 0, objSortDtl);

            //Verify and Assert
            mockDataAccess.VerifyAll();

            List<DocumentTypeExternalIdObjectModel> lstExpected = new List<DocumentTypeExternalIdObjectModel>();
            lstExpected.Add(new DocumentTypeExternalIdObjectModel()
            {
                DocumentTypeId = 1,
                ExternalId = "AR",
                IsPrimary = true
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "ModifiedDate", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);
        }

        #endregion

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

        #region SaveEntity_WithModifiedBy
        /// <summary>
        /// SaveEntity_WithModifiedBy
        /// </summary>
        [TestMethod]
        public void SaveEntity_WithModifiedBy()
        {
            //Arrange
            DocumentTypeExternalIdObjectModel objObjectModel = new DocumentTypeExternalIdObjectModel();
            objObjectModel.DocumentTypeId = 1;
            objObjectModel.ExternalId = "TEST_001";
            objObjectModel.IsPrimary = false;
            objObjectModel.ModifiedByName = "Test";

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="DocumentTypeId", Value=1 },
                new SqlParameter(){ ParameterName="ExternalId", Value="TEST_001" },
                new SqlParameter(){ ParameterName="IsPrimary", Value=false }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            DocumentTypeExternalIdFactory objDocumentTypeExternalIdFactory = new DocumentTypeExternalIdFactory(mockDataAccess.Object);
            objDocumentTypeExternalIdFactory.SaveEntity(objObjectModel, 1);

            //Verify And Assert
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
            DocumentTypeExternalIdObjectModel entity = new DocumentTypeExternalIdObjectModel();
            entity.DocumentTypeId = 1;
            entity.ExternalId = "QWE";

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                new SqlParameter(){ ParameterName="DocumentTypeId", Value=1 },
                new SqlParameter(){ ParameterName="ExternalId", Value="QWE" }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            DocumentTypeExternalIdFactory objDocumentTypeExternalIdFactory = new DocumentTypeExternalIdFactory(mockDataAccess.Object);
            objDocumentTypeExternalIdFactory.DeleteEntity(entity);

            //Verify and Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region DeleteEntity_With_DocumentTypeExternalIdKey
        /// <summary>
        /// DeleteEntity_With_DocumentTypeExternalIdKey
        /// </summary>
        [TestMethod]
        public void DeleteEntity_With_DocumentTypeExternalIdKey()
        {
            //Arrange
            DocumentTypeExternalIdKey objDocumentTypeExternalIdKey = new DocumentTypeExternalIdKey(1, "fdc");

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                new SqlParameter(){ ParameterName="DocumentTypeId", Value=1 },
                new SqlParameter(){ ParameterName="ExternalId", Value="QWE" }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2]);
            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            DocumentTypeExternalIdFactory objDocumentTypeExternalIdFactory = new DocumentTypeExternalIdFactory(mockDataAccess.Object);
            objDocumentTypeExternalIdFactory.DeleteEntity(objDocumentTypeExternalIdKey);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region GetEntitiesBySearch_Returns_IEnumerable_Entity
        ///// <summary>
        ///// GetEntitiesBySearch_Returns_IEnumerable_Entity
        ///// </summary>
        //[TestMethod]
        //public void GetEntitiesBySearch_Returns_IEnumerable_Entity()
        //{
        //    //Arrange  
        //    DocumentTypeExternalIdSearchDetail objSearchDtl = new DocumentTypeExternalIdSearchDetail();
        //    objSearchDtl.DocumentTypeID = 1;
        //    objSearchDtl.ExternalId = "TEST";

        //    DocumentTypeExternalIdSortDetail objSortDtl = new DocumentTypeExternalIdSortDetail();
        //    objSortDtl.Column = DocumentTypeExternalIdSortColumn.DocumentTypeName;
        //    objSortDtl.Order = SortOrder.Ascending;

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    DocumentTypeExternalIdFactory objDocumentTypeExternalIdFactory = new DocumentTypeExternalIdFactory(mockDataAccess);
        //    try
        //    {
        //        objDocumentTypeExternalIdFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, null);
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

        //#region GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity
        ///// <summary>
        ///// GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity
        ///// </summary>
        //[TestMethod]
        //public void GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity()
        //{
        //    //Arrange  
        //    DocumentTypeExternalIdSearchDetail objSearchDtl = new DocumentTypeExternalIdSearchDetail();
        //    objSearchDtl.DocumentTypeID = 1;
        //    objSearchDtl.ExternalId = "TEST";

        //    DocumentTypeExternalIdKey objDocumentTypeExternalIdKey = new DocumentTypeExternalIdKey(1, "TEST");

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    DocumentTypeExternalIdFactory objDocumentTypeExternalIdFactory = new DocumentTypeExternalIdFactory(mockDataAccess);
        //    try
        //    {
        //        objDocumentTypeExternalIdFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objDocumentTypeExternalIdKey);
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

        //#region GetEntityByKey_Returns_DocumentTypeExternalIdObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_DocumentTypeExternalIdObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_DocumentTypeExternalIdObjectModel()
        //{
        //    //Arrange
        //    DocumentTypeExternalIdKey objDocumentTypeExternalIdKey = new DocumentTypeExternalIdKey(1, "TEST");
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    DocumentTypeExternalIdFactory objDocumentTypeExternalIdFactory = new DocumentTypeExternalIdFactory(mockDataAccess);
        //    try
        //    {
        //        objDocumentTypeExternalIdFactory.GetEntityByKey(objDocumentTypeExternalIdKey);
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
            DocumentTypeExternalIdFactory objFactory = new DocumentTypeExternalIdFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<DocumentTypeExternalIdObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

    }
}

