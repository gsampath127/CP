using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RRD.DSA.Core.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.Cache.VerticalMarket;
using RRD.FSG.RP.Model.Cache;
using System.Web.Script.Serialization;
using System.Data.Common;
using System.Data.SqlClient;
using RRD.FSG.RP.Model.SearchEntities.VerticalMarket;

namespace RRD.FSG.RP.Model.Test.Factories.VerticalMarket
{
    /// <summary>
    /// Test Class for DocumentTypeFactory
    /// </summary>
    [TestClass]
    public class DocumentTypeFactoryTests :BaseTestFactory<DocumentTypeObjectModel>
    {
        Mock<IDataAccess> mockDataAccess { get; set; }
       

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

            //Mock.Arrange(() => mockDataAccess.ExecuteDataSet(string.Empty, string.Empty, null))
            // .IgnoreArguments()
            // .Returns(dSet)
            // .MustBeCalled();

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dSet);
        }
        #endregion

        #region mockValues_for_DataRow

        public DataRow mockDataRows()
        {
        DataTable mockDataTable = new DataTable();
        mockDataTable.Columns.Add("DocumentTypeId", typeof(Int32));
        mockDataTable.Columns.Add("DocumentTypeName", typeof(string));
        mockDataTable.Columns.Add("MarketID", typeof(string));

        DataRow mockDataRow = mockDataTable.NewRow();
        mockDataRow["DocumentTypeId"] = 0;
        mockDataRow["DocumentTypeName"] = "";
        mockDataRow["MarketID"] = "";

        return mockDataRow;
        }

        public DataTable mockDataTable()
        {
            DataTable mockDataTable = new DataTable();
            mockDataTable.Columns.Add("DocumentTypeId", typeof(Int32));
            mockDataTable.Columns.Add("DocumentTypeName", typeof(string));
            mockDataTable.Columns.Add("MarketID", typeof(string));

            DataRow mockDataRow = mockDataTable.NewRow();
            mockDataRow["DocumentTypeId"] = 0;
            mockDataRow["DocumentTypeName"] = "";
            mockDataRow["MarketID"] = "";

            mockDataTable.Rows.Add(mockDataRow);

            return mockDataTable;
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
            DocumentTypeFactory objFactory = new DocumentTypeFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<DocumentTypeObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

        #region GetAllEntities_of_DocumentTypeObjectModel_returns_IEnumerableof_DocumentTypeObjectModel
        /// <summary>
        /// GetAllEntities_of_DocumentTypeObjectModel_returns_IEnumerableof_DocumentTypeObjectModel
        /// </summary>
        [TestMethod]  
        public void GetAllEntities_of_DocumentTypeObjectModel_returns_IEnumerableof_DocumentTypeObjectModel()
        {
            //Arrange
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(mockDataTable());
            
            //Act
            ClientData();
            DocumentTypeFactoryCache objFactoryCache = new DocumentTypeFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;
            var result = objFactoryCache.GetAllEntities<DocumentTypeObjectModel>(It.IsAny<int>(), It.IsAny<int>());

            List<DocumentTypeObjectModel> lstExpected = new List<DocumentTypeObjectModel>();
            lstExpected.Add(new DocumentTypeObjectModel()
            {
                DocPriority = 0,
                DocumentTypeId = 0,
                MarketId="",
                Name = "",
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            

            //Assert
            mockDataAccess.VerifyAll(); 
            Assert.IsInstanceOfType(result, typeof(IEnumerable<DocumentTypeObjectModel>));

        }
        #endregion

        #region GetAllEntities_of_DocumentTypeObjectModel_WithEmptyDataTable_returns_IEnumerableof_DocumentTypeObjectModel
        /// <summary>
        /// GetAllEntities_of_DocumentTypeObjectModel_returns_IEnumerableof_DocumentTypeObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_of_DocumentTypeObjectModel_WithEmptyDataTable_returns_IEnumerableof_DocumentTypeObjectModel()
        {
            //Arrange
            DataTable mockDataTable = new DataTable();
            mockDataTable.Columns.Add("DocumentTypeId", typeof(Int32));
            mockDataTable.Columns.Add("DocumentTypeName", typeof(string));
            mockDataTable.Columns.Add("MarketID", typeof(string));

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(mockDataTable);

            //Act
            ClientData();
            DocumentTypeFactoryCache objFactoryCache = new DocumentTypeFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;
            var result = objFactoryCache.GetAllEntities<DocumentTypeObjectModel>(It.IsAny<int>(), It.IsAny<int>());

            ValidateEmptyData(result);


            //Assert
            mockDataAccess.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(IEnumerable<DocumentTypeObjectModel>));

        }
        #endregion

        #region GetAllEntities_of_DocumentTypeObjectModel_returns_IEnumerableof_DocumentTypeObjectModel_withsort
        /// <summary>
        /// GetAllEntities_of_DocumentTypeObjectModel_returns_IEnumerableof_DocumentTypeObjectModel_withsort
        /// </summary>
        [TestMethod]
        public void GetAllEntities_of_DocumentTypeObjectModel_returns_IEnumerableof_DocumentTypeObjectModel_withsort()
        {
            
            //Arrange
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(mockDataTable());

            //Act
            DocumentTypeFactory objDocumentTypeFactory = new DocumentTypeFactory(mockDataAccess.Object);
            var result = objDocumentTypeFactory.GetAllEntities<DocumentTypeObjectModel>(It.IsAny<int>(), It.IsAny<int>(),It.IsAny <ISortDetail<DocumentTypeObjectModel>>());

            List<DocumentTypeObjectModel> lstExpected = new List<DocumentTypeObjectModel>();
            lstExpected.Add(new DocumentTypeObjectModel()
            {
                DocPriority = 0,
                DocumentTypeId = 0,
                MarketId = "",
                Name = "",
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            //Verify and Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<DocumentTypeObjectModel>));

        }
        #endregion

        //#region SaveEntity_with_entity_throws_notimplemented
        ///// <summary>
        ///// SaveEntity_with_entity_throws_notimplemented
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_with_entity_throws_notimplemented()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    DocumentTypeObjectModel objModel = new DocumentTypeObjectModel();
        //    objModel.Description = "Test";
        //    objModel.DocPriority = 1;

        //    //Act
        //    DocumentTypeFactory objDocumentTypeFactory = new DocumentTypeFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objDocumentTypeFactory.SaveEntity(objModel);
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

        //#region DeleteEntity_with_key_throws_notimplemented
        ///// <summary>
        ///// DeleteEntity_with_key_throws_notimplemented
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_with_key_throws_notimplemented()
        //{
        //     //Arrange
        //    Exception exe = null;

        //    //Act
        //    DocumentTypeFactory objDocumentTypeFactory = new DocumentTypeFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objDocumentTypeFactory.DeleteEntity(It.IsAny<int>());
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

        //#region SaveEntity_with_entity_and_modifiedby_throws_notimplemented
        ///// <summary>
        ///// SaveEntity_with_entity_and_modifiedby_throws_notimplemented
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_with_entity_and_modifiedby_throws_notimplemented()
        //{
        //     //Arrange
        //    Exception exe = null;

        //    //Act
        //    DocumentTypeFactory objDocumentTypeFactory = new DocumentTypeFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objDocumentTypeFactory.SaveEntity(It.IsAny<DocumentTypeObjectModel>(), It.IsAny<int>());
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

        //#region DeleteEntity_with_key_and_modifiedby_throws_notimplemented
        ///// <summary>
        ///// DeleteEntity_with_key_and_modifiedby_throws_notimplemented
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_with_key_and_modifiedby_throws_notimplemented()
        //{
        //     //Arrange
        //    Exception exe = null;

        //    //Act
        //    DocumentTypeFactory objDocumentTypeFactory = new DocumentTypeFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objDocumentTypeFactory.DeleteEntity(It.IsAny<int>(), It.IsAny<int>());
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

        //#region DeleteEntity_with_entity_throws_notimplemented
        ///// <summary>
        ///// DeleteEntity_with_entity_throws_notimplemented
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_with_entity_throws_notimplemented()
        //{
        //     //Arrange
        //    Exception exe = null;

        //    //Act
        //    DocumentTypeFactory objDocumentTypeFactory = new DocumentTypeFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objDocumentTypeFactory.DeleteEntity(It.IsAny<DocumentTypeObjectModel>());
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

        //#region DeleteEntity_with_entity_and_deletedby_throws_notimplemented
        ///// <summary>
        ///// DeleteEntity_with_entity_and_deletedby_throws_notimplemented
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_with_entity_and_deletedby_throws_notimplemented()
        //{
        //     //Arrange
        //    Exception exe = null;

        //    //Act
        //    DocumentTypeFactory objDocumentTypeFactory = new DocumentTypeFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objDocumentTypeFactory.DeleteEntity(It.IsAny<DocumentTypeObjectModel>(), It.IsAny<int>());
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

        //#region GetEntitiesBySearch_throws_notimplemented
        ///// <summary>
        ///// GetEntitiesBySearch_throws_notimplemented
        ///// </summary>
        //[TestMethod]
        //public void GetEntitiesBySearch_throws_notimplemented()
        //{
        //     //Arrange
        //    Exception exe = null;
        //    int value = 0;
        //    //Act
        //    DocumentTypeFactory objDocumentTypeFactory = new DocumentTypeFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objDocumentTypeFactory.GetEntitiesBySearch<DocumentTypeObjectModel>(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ISearchDetail<DocumentTypeObjectModel>>(), It.IsAny<ISortDetail<DocumentTypeObjectModel>>(), out value, It.IsAny<int[]>());
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

        //#region GetEntityByKey_throws_notimplemented
        ///// <summary>
        ///// GetEntityByKey_throws_notimplemented
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_throws_notimplemented()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    int value = 0;
        //    //Act
        //    DocumentTypeFactory objDocumentTypeFactory = new DocumentTypeFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objDocumentTypeFactory.GetEntityByKey<DocumentTypeObjectModel>(It.IsAny<int>());
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
