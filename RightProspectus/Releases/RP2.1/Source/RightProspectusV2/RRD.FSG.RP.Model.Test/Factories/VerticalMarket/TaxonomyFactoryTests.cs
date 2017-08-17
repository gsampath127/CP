using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.VerticalMarket;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.SearchEntities.VerticalMarkets;
using RRD.FSG.RP.Model.SortDetail.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RRD.FSG.RP.Model.Test.Factories.VerticalMarket
{
    /// <summary>
    /// Test class for SiteFactory class
    /// </summary>
    [TestClass]
    public class TaxonomyFactoryTests : BaseTestFactory<TaxonomyObjectModel>
    {
        Mock<IDataAccess> mockDataAccess;

        /// <summary>
        /// TestInitialize
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
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

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(),It.IsAny<DbParameter[]>())).Returns(dSet);
            

           
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
            SiteSortDetail objSortDtl = new SiteSortDetail();
            objSortDtl.Column = SiteSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;
            
            DataTable dt = new DataTable();
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("Level", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["TaxonomyID"] = 1;
            dtrow["CompanyName"] = "Forethought";
            dtrow["Level"] = 1;
            dtrow["TaxonomyName"] = null;
            dt.Rows.Add(dtrow);

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TaxonomyID", typeof(Int32));
            dt2.Columns.Add("Level", typeof(Int32));
            dt2.Columns.Add("IsNameProvided", typeof(bool));
            dt2.Columns.Add("TaxonomyName", typeof(string));

            DataRow dtrow2= dt2.NewRow();
            dtrow2["TaxonomyID"] = 1;
            dtrow2["Level"] = 1;
            dtrow2["IsNameProvided"] = false;
            dtrow2["TaxonomyName"] = null;
            dt2.Rows.Add(dtrow2);
            
            ClientData();

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt2)
                .Returns(dt);
           //mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(),It.IsAny<DbParameter[]>())).Returns(dt);

           
           
            //Act
             TaxonomyFactoryCache objFactoryCache = new TaxonomyFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;
          
            var result= objFactoryCache.GetAllEntities(0, 0);
            ValidateEmptyData(result);
            mockDataAccess.VerifyAll();
           
     
        }
        #endregion

        #region GetAllEntities_Calls_Factory_taxonomyname
        /// <summary>
        /// GetAllEntities_Calls_Factory
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Calls_Factory_taxonomyname()
        {
            //Arrange
            SiteSortDetail objSortDtl = new SiteSortDetail();
            objSortDtl.Column = SiteSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("Level", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["TaxonomyID"] = 1;
            dtrow["CompanyName"] = "Forethought";
            dtrow["Level"] = 1;
            dtrow["TaxonomyName"] = "Tst";
            dt.Rows.Add(dtrow);

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TaxonomyID", typeof(Int32));
            dt2.Columns.Add("Level", typeof(Int32));
            dt2.Columns.Add("IsNameProvided", typeof(bool));
            dt2.Columns.Add("TaxonomyName", typeof(string));

            DataRow dtrow2 = dt2.NewRow();
            dtrow2["TaxonomyID"] = 1;
            dtrow2["Level"] = 1;
            dtrow2["IsNameProvided"] = false;
            dtrow2["TaxonomyName"] = "Tst";
            dt2.Rows.Add(dtrow2);

            ClientData();
            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt2)
                .Returns(dt);
            //mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            TaxonomyFactoryCache objFactoryCache = new TaxonomyFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;

            var result = objFactoryCache.GetAllEntities(0, 0);
            List<TaxonomyObjectModel> lstExpected = new List<TaxonomyObjectModel>();
            lstExpected.Add(new TaxonomyObjectModel()
            {
                TaxonomyId = 1,
                Level = 1,
                TaxonomyName = "Tst"
            });

            ValidateListData(lstExpected, result.ToList());
            //Assert
            mockDataAccess.VerifyAll();
           



        }
        #endregion

        #region GetAllEntities_Calls_Factory_rowcount
        /// <summary>
        /// GetAllEntities_Calls_Factory
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Calls_Factory_rowcount()
        {
            //Arrange
            SiteSortDetail objSortDtl = new SiteSortDetail();
            objSortDtl.Column = SiteSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("Level", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));

            //DataRow dtrow = dt.NewRow();
            //dtrow["TaxonomyID"] = 1;
            //dtrow["CompanyName"] = "Forethought";
            //dtrow["Level"] = 1;
            //dtrow["TaxonomyName"] = null;
            //dt.Rows.Add(dtrow);

            //DataTable dt2 = new DataTable();
            //dt2.Columns.Add("TaxonomyID", typeof(Int32));
            //dt2.Columns.Add("Level", typeof(Int32));
            //dt2.Columns.Add("IsNameProvided", typeof(bool));
            //dt2.Columns.Add("TaxonomyName", typeof(string));

            ClientData();

           // mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt2);
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            TaxonomyFactoryCache objFactoryCache = new TaxonomyFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;

            var result = objFactoryCache.GetAllEntities(0, 0);
            ValidateEmptyData(result);
            mockDataAccess.VerifyAll();
           


        }
        #endregion

        #region GetAllEntities_Calls_Factory_objectmodelrowcount
        /// <summary>
        /// GetAllEntities_Calls_Factory
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Calls_Factory_objectmodelrowcount()
        {
            //Arrange
            SiteSortDetail objSortDtl = new SiteSortDetail();
            objSortDtl.Column = SiteSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("Level", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TaxonomyID", typeof(Int32));
            dt2.Columns.Add("Level", typeof(Int32));
            dt2.Columns.Add("IsNameProvided", typeof(bool));
            dt2.Columns.Add("TaxonomyName", typeof(string));

            DataRow dtrow2 = dt2.NewRow();
            dtrow2["TaxonomyID"] = 1;
            dtrow2["Level"] = 1;
            dtrow2["IsNameProvided"] = false;
            dtrow2["TaxonomyName"] = null;
            dt2.Rows.Add(dtrow2);

            ClientData();

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt2)
                .Returns(dt);
            //mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt2);
            //mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            TaxonomyFactoryCache objFactoryCache = new TaxonomyFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;

            var result = objFactoryCache.GetAllEntities(0, 0);
            ValidateEmptyData(result);
            mockDataAccess.VerifyAll();
           


        }
        #endregion

        #region GetAllEntities_Calls_Factory_differenttaxid
        /// <summary>
        /// GetAllEntities_Calls_Factory
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Calls_Factory_differenttaxid()
        {
            //Arrange
            SiteSortDetail objSortDtl = new SiteSortDetail();
            objSortDtl.Column = SiteSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("Level", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["TaxonomyID"] = 5;
            dtrow["CompanyName"] = "Forethought";
            dtrow["Level"] = 3;
            dtrow["TaxonomyName"] = null;
            dt.Rows.Add(dtrow);

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TaxonomyID", typeof(Int32));
            dt2.Columns.Add("Level", typeof(Int32));
            dt2.Columns.Add("IsNameProvided", typeof(bool));
            dt2.Columns.Add("TaxonomyName", typeof(string));

            DataRow dtrow2 = dt2.NewRow();
            dtrow2["TaxonomyID"] = 2;
            dtrow2["Level"] = 1;
            dtrow2["IsNameProvided"] = false;
            dtrow2["TaxonomyName"] = null;
            dt2.Rows.Add(dtrow2);

            ClientData();
            //Mock.Arrange(() => mockDataAccess.ExecuteDataTable(string.Empty, string.Empty, null))
            // .IgnoreArguments()
            // .Returns(dt2)
            // .InSequence()
            // .MustBeCalled();
            //Mock.Arrange(() => mockDataAccess.ExecuteDataTable(string.Empty, string.Empty, null))
            //  .IgnoreArguments()
            //  .Returns(dt)
            //  .InSequence()
            //  .MustBeCalled();
            //mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt2);
            //mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);
            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt2)
                .Returns(dt);
            //Act
            TaxonomyFactoryCache objFactoryCache = new TaxonomyFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;

            var result = objFactoryCache.GetAllEntities(1, 0);
            ValidateEmptyData(result);
            mockDataAccess.VerifyAll();
           

        }
        #endregion


        //#region GetAllEntities_Returns_IEnumerable_Entity
        ///// <summary>
        ///// GetAllEntities_Returns_IEnumerable_Entity
        ///// </summary>
        //[TestMethod]
        //public void GetAllEntities_Returns_IEnumerable_Entity()
        //{
        //    //Arrange  

        //    TaxonomySortDetail objSortDtl = new TaxonomySortDetail();


        //    ClientData();
        //    Exception exe = null;

        //    //Act
        //    TaxonomyFactory objFactory = new TaxonomyFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.GetAllEntities(0, 0, objSortDtl);
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

        #region GetTaxonomyNameForTaxonomyIDs_Returns_Ienumerable_SiteObjectModel
        /// <summary>
        /// GetTaxonomyNameForTaxonomyIDs_Returns_Ienumerable_taxonomyObjectModels
        /// </summary>
        [TestMethod]
        public void GetTaxonomyNameForTaxonomyIDs_Returns_Ienumerable_taxonomyObjectModels()
        {
            //Arrange
            DataTable dtIn = new DataTable();


            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TaxonomyId", typeof(Int32));
            dt2.Columns.Add("CompanyName", typeof(string));
            dt2.Columns.Add("ProsName", typeof(string));
            dt2.Columns.Add("TaxonomyName", typeof(string));
            dt2.Columns.Add("LEVEL", typeof(Int32));


            DataRow dtrow2 = dt2.NewRow();

            dtrow2["TaxonomyId"] = 2;
            dtrow2["CompanyName"] = "Test";
            dtrow2["ProsName"] = "Test";
            dtrow2["TaxonomyName"] = "Test";
            dtrow2["LEVEL"] = 1;
            dt2.Rows.Add(dtrow2);

            
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt2);
          
            //Act
            TaxonomyFactory objFactory = new TaxonomyFactory(mockDataAccess.Object);
            var result = objFactory.GetTaxonomyNameForTaxonomyIDs(dtIn);
            List<TaxonomyObjectModel> lstExpected = new List<TaxonomyObjectModel>();
            lstExpected.Add(new TaxonomyObjectModel()
            {
                TaxonomyId = 2,
                Level = 1,
                TaxonomyName = "Test"
            });

            ValidateListData(lstExpected, result.ToList());
            //Assert 
            mockDataAccess.VerifyAll();
           
        }
        #endregion

        #region GetTaxonomyNameForTaxonomyIDs_Returns_Ienumerable_SiteObjectModelnull
        /// <summary>
        /// GetTaxonomyNameForTaxonomyIDs_Returns_Ienumerable_taxonomyObjectModels
        /// </summary>
        [TestMethod]
        public void GetTaxonomyNameForTaxonomyIDs_Returns_Ienumerable_taxonomyObjectModelsnull()
        {
            //Arrange
            DataTable dtIn = new DataTable();

            DataTable dt = new DataTable();
            dt.Columns.Add("TaxonomyId", typeof(Int32));
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("ProsName", typeof(string));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("LEVEL", typeof(Int32));

            //ClientData();

           
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);



            //Act
            TaxonomyFactory objFactory = new TaxonomyFactory(mockDataAccess.Object);
            var result = objFactory.GetTaxonomyNameForTaxonomyIDs(dtIn);
            ValidateEmptyData(result);
            mockDataAccess.VerifyAll();
           


        }
        #endregion

        //#region SaveEntity_Returns_void_TaxonomyObjectModel
        ///// <summary>
        ///// SaveEntity_Returns_void_TaxonomyObjectModel
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_Returns_void_TaxonomyObjectModel()
        //{
        //    //Arrange  

        //    TaxonomyObjectModel objmodel = new TaxonomyObjectModel();


        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    TaxonomyFactory objFactory = new TaxonomyFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.SaveEntity(objmodel);
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

        //#region SaveEntity_Returns_void_two parameters
        ///// <summary>
        ///// SaveEntity_Returns_void_two parameters
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_Returns_void_twoparameters()
        //{
        //    //Arrange  

        //    TaxonomyObjectModel objmodel = new TaxonomyObjectModel();


        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    TaxonomyFactory objFactory = new TaxonomyFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.SaveEntity(objmodel, 0);
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

        //#region DeleteEntity_Returns_Void
        ///// <summary>
        ///// DeleteEntity_Returns_Void
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_Returns_Void()
        //{
        //    //Arrange  

        //    TaxonomyObjectModel objmodel = new TaxonomyObjectModel();


        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    TaxonomyFactory objFactory = new TaxonomyFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.DeleteEntity(objmodel);
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

        //#region DeleteEntity_Returns_void_two parameters
        ///// <summary>
        ///// DeleteEntityEntity_Returns_void_two parameters
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_Returns_void_twoparameters()
        //{
        //    //Arrange  

        //    TaxonomyObjectModel objmodel = new TaxonomyObjectModel();


        //    ClientData();
        //    Exception exe = null; 

        //    //Act
        //    TaxonomyFactory objFactory = new TaxonomyFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.DeleteEntity(objmodel, 0);
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

        //#region DeleteEntity_Returns_Void_Taxonomykey
        ///// <summary>
        ///// DeleteEntity_Returns_Void
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_Returns_Void_Taxonomykey()
        //{
        //    //Arrange  

        //    TaxonomyKey objmodel = new TaxonomyKey(0, 0);


        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    TaxonomyFactory objFactory = new TaxonomyFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.DeleteEntity(objmodel);
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

        //#region DeleteEntity_Returns_Void_Taxonomykey_Twoparameters
        ///// <summary>
        ///// DeleteEntity_Returns_Void_Taxonomykey_Twoparameters
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_Returns_Void_Taxonomykey_Twoparameters()
        //{
        //    //Arrange  

        //    TaxonomyKey objmodel = new TaxonomyKey(0, 0);


        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    TaxonomyFactory objFactory = new TaxonomyFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.DeleteEntity(objmodel, 1);
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

        //#region GetEntityByKey_Returns_IEnumerable_Entity
        ///// <summary>
        ///// GetEntityByKey_Returns_IEnumerable_Entity
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_IEnumerable_Entity()
        //{
        //    //Arrange  

        //    TaxonomyKey objkey = new TaxonomyKey(0, 0);


        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    TaxonomyFactory objFactory = new TaxonomyFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.GetEntityByKey(objkey);
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

        //#region GetEntitiesBySearch_Returns_IEnumerable_Entity
        ///// <summary>
        ///// GetEntitiesBySearch_Returns_IEnumerable_Entity
        ///// </summary>
        //[TestMethod]
        //public void GetEntitiesBySearch_Returns_IEnumerable_Entity()
        //{
        //    //Arrange  

        //    TaxonomySearchDetail objSearchDtl = new TaxonomySearchDetail();
        //    TaxonomySortDetail objSortDtl = new TaxonomySortDetail();
        //    TaxonomyKey objKey = new TaxonomyKey(1, 2);


        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    TaxonomyFactory objFactory = new TaxonomyFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, objKey);
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
            TaxonomyFactory objFactory = new TaxonomyFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<TaxonomyObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();
        }
        #endregion

    }
}
