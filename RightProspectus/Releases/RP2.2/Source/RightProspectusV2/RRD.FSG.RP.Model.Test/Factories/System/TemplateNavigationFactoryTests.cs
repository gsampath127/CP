using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Common;
using System.Data.SqlClient;

namespace RRD.FSG.RP.Model.Test.Factories.System
{
    /// <summary>
    /// Test class for TemplateNavigationFactory class
    /// </summary>
    [TestClass]
    public class TemplateNavigationFactoryTests :BaseTestFactory<TemplateNavigationObjectModel>
    {
        Mock<IDataAccess> mockDataAccess { get; set; }
        


        #region TestIntialize
        /// <summary>
        /// TestInitialze
        /// </summary>
        [TestInitialize]
        public void TestInitialze()
        {
            mockDataAccess = new Mock<IDataAccess>();
            
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_WithSort_TemplateNavigationObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_TemplateNavigationObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_WithSort_TemplateNavigationObjectModel()
        {
            //Arrange
           
            DataTable dt = new DataTable();
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("XslTransform", typeof(string));
            dt.Columns.Add("DefaultNavigationXml", typeof(string));
            //dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["TemplateId"] = 1;
            dtrow["NavigationKey"] = "test200";
            dtrow["XslTransform"] = "dedf2";
            dtrow["DefaultNavigationXml"] = "<test>test200</test>";
            //dtrow["PageId"] = 1;
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

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            //Act
            TemplateNavigationFactoryCache objFactoryCache = new TemplateNavigationFactoryCache(mockDataAccess.Object);
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;
            var result = objFactoryCache.GetAllEntities(0, 0, null);

            List<TemplateNavigationObjectModel> lstExpected = new List<TemplateNavigationObjectModel>();
            lstExpected.Add(new TemplateNavigationObjectModel()
            {
                TemplateID = 1,
                //PageID = 0,
                DefaultNavigationXml = "<test>test200</test>",
                NavigationKey = "test200",
                XslTransform = "dedf2",
                Name = null,
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            lstExclude.Add("Key");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<TemplateNavigationKey>(lstExpected, result.ToList());

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_TemplateNavigationObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_TemplateNavigationObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_TemplateNavigationObjectModel()
        {
            //Arrange

            DataTable dt = new DataTable();
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("NavigationKey", typeof(string));
            dt.Columns.Add("XslTransform", typeof(string));
            dt.Columns.Add("DefaultNavigationXml", typeof(string));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrow = dt.NewRow();
            dtrow["TemplateId"] = 1;
            dtrow["NavigationKey"] = "test200";
            dtrow["XslTransform"] = "dedf2";
            dtrow["DefaultNavigationXml"] = "<test>test200</test>";
            dtrow["PageId"] = 1;
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

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            //Act
            TemplateNavigationFactoryCache objFactoryCache = new TemplateNavigationFactoryCache(mockDataAccess.Object);
           
            objFactoryCache.ClientName = "Forethought";
            objFactoryCache.Mode = FactoryCacheMode.All;
            var result = objFactoryCache.GetAllEntities<TemplateNavigationObjectModel>(0, 0,null);

            List<TemplateNavigationObjectModel> lstExpected = new List<TemplateNavigationObjectModel>();
            lstExpected.Add(new TemplateNavigationObjectModel()
            {
                TemplateID = 1,
                PageID = 0,
                DefaultNavigationXml = "<test>test200</test>",
                NavigationKey = "test200",
                XslTransform = "dedf2",
                Name = null,
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            lstExclude.Add("Key");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<TemplateNavigationKey>(lstExpected, result.ToList());

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
            TemplateNavigationFactory objFactory = new TemplateNavigationFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<TemplateNavigationObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

        //#region SaveEntity_With_ModifiedBy
        ///// <summary>
        ///// SaveEntity_With_ModifiedBy
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_With_ModifiedBy()
        //{
        //    //Arrange
        //    TemplateNavigationObjectModel objObjectModel = new TemplateNavigationObjectModel();
        //    objObjectModel.TemplateID = 1;
        //    objObjectModel.NavigationKey = "test200";
        //    objObjectModel.XslTransform = "dedf2";
        //    objObjectModel.DefaultNavigationXml = "<test>test200</test>";
        //    objObjectModel.PageID = 1;
        //    Exception exe = null;

        //    ClientData();

        //    //Act
        //    TemplateNavigationFactory objTemplateNavigationFactory = new TemplateNavigationFactory(mockDataAccess);
        //    try
        //    {

        //        objTemplateNavigationFactory.SaveEntity(objObjectModel, 0);
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

        //#region DeleteEntity_With_Key
        ///// <summary>
        ///// DeleteEntity_With_Key
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Key()
        //{
        //    //Arrange
        //    TemplateNavigationKey objTemplateNavigationKey = new TemplateNavigationKey(1, "2");
        //    Exception exe = null;
        //    ClientData();

        //    //Act
        //    TemplateNavigationFactory objTemplateNavigationFactory = new TemplateNavigationFactory(mockDataAccess);
        //    try
        //    {
        //        objTemplateNavigationFactory.DeleteEntity(objTemplateNavigationKey);
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
        
        //#region DeleteEntity_With_Entity
        ///// <summary>
        ///// DeleteEntity_With_Entity
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Entity()
        //{
        //    //Arrange
        //    TemplateNavigationObjectModel entity = new TemplateNavigationObjectModel();
        //    entity.TemplateID = 1;
        //    entity.NavigationKey = "test200";
        //    entity.XslTransform = "dedf2";
        //    entity.DefaultNavigationXml = "<test>test200</test>";
        //    entity.PageID = 1;
        //    Exception exe = null;

        //    ClientData();

        //    //Act
        //    TemplateNavigationFactory objTemplateNavigationFactory = new TemplateNavigationFactory(mockDataAccess);
        //    try
        //    {
        //        objTemplateNavigationFactory.DeleteEntity(entity);
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
        //#region DeleteEntity_With_Entity_twoparams
        ///// <summary>
        ///// DeleteEntity_With_Entity_twoparams
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Entity_twoparams()
        //{
        //    //Arrange
        //    TemplateNavigationObjectModel entity = new TemplateNavigationObjectModel();
        //    entity.TemplateID = 1;
        //    entity.NavigationKey = "test200";
        //    entity.XslTransform = "dedf2";
        //    entity.DefaultNavigationXml = "<test>test200</test>";
        //    entity.PageID = 1;
        //    Exception exe = null;

        //    ClientData();

        //    //Act
        //    TemplateNavigationFactory objTemplateNavigationFactory = new TemplateNavigationFactory(mockDataAccess);
        //    try
        //    {
        //        objTemplateNavigationFactory.DeleteEntity(entity,0);
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
        //#region DeleteEntity_twoparams
        ///// <summary>
        ///// DeleteEntity_twoparams
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_twoparams()
        //{
        //    //Arrange
        //    TemplateNavigationKey objTemplateNavigationKey = new TemplateNavigationKey(1, "2");
        //    Exception exe = null;

        //    ClientData();

        //    //Act
        //    TemplateNavigationFactory objTemplateNavigationFactory = new TemplateNavigationFactory(mockDataAccess);
        //    try
        //    {
        //        objTemplateNavigationFactory.DeleteEntity(objTemplateNavigationKey, 0);
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
        //#region SaveEntity_Notimplemented
        ///// <summary>
        /////  SaveEntity_Notimplemented
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_Notimplemented()
        //{
        //    //Arrange
        //    TemplateNavigationObjectModel objTemplateNavigationKey = new TemplateNavigationObjectModel();
        //    Exception exe = null;

        //    ClientData();

        //    //Act
        //    TemplateNavigationFactory objTemplateNavigationFactory = new TemplateNavigationFactory(mockDataAccess);
        //    try
        //    {
        //        objTemplateNavigationFactory.SaveEntity(objTemplateNavigationKey);
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

        //#region GetEntityByKey_Returns_TemplateNavigationObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_TemplateNavigationObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_TemplateNavigationObjectModel()
        //{
        //    //Arrange
        //    TemplateNavigationKey objTemplateNavigationKey = new TemplateNavigationKey(1, "1");
        //    ClientData();
        //    Exception exe = null;

        //    //Act
        //    TemplateNavigationFactory objTemplateNavigationFactory = new TemplateNavigationFactory(mockDataAccess);
        //    try
        //    {
        //        objTemplateNavigationFactory.GetEntityByKey(objTemplateNavigationKey);
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
        //    TemplateNavigationSearchDetail objSearchDtl = new TemplateNavigationSearchDetail();
        //    objSearchDtl.TemplateID = 1;
        //    objSearchDtl.NavigationKey = "test200";
        //    objSearchDtl.XslTransform = "dedf2";
        //    objSearchDtl.DefaultNavigationXml = "<test>test200</test>";
        //    Exception exe = null;

        //    ClientData();

        //    //Act
        //    TemplateNavigationFactory objTemplateNavigationFactory = new TemplateNavigationFactory(mockDataAccess);
        //    try
        //    {
        //        objTemplateNavigationFactory.GetEntitiesBySearch(0, 0, objSearchDtl);
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
