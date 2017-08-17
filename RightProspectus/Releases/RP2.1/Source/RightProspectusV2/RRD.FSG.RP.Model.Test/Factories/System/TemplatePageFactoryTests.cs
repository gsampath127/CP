using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Test.Factories.System
{
    /// <summary>
    /// Test class for TemplatePageFactory class
    /// </summary>
    [TestClass]
    public class TemplatePageFactoryTests : BaseTestFactory<TemplatePageObjectModel>
    {
        Mock<IDataAccess> mockDataAccess;

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
            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dSet);
            
        }
        #endregion

        #region GetAllEntities_Returns_IEnumerable_TemplatePageObjectModel
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_TemplatePageObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_TemplatePageObjectModel()
        {
            //Arrange

            DataTable dt = new DataTable();
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("TemplateName", typeof(string));
            dt.Columns.Add("PageID", typeof(Int32));
            dt.Columns.Add("PageName", typeof(string));
            dt.Columns.Add("PageDescription", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["TemplateId"] = 1;
            dtrow["TemplateName"] = "Default";
            dtrow["PageID"] = 1;
            dtrow["PageName"] = "TAL";
            dtrow["PageDescription"] = "Taxonomy Association Link";

            dt.Rows.Add(dtrow);

            ClientData();
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Mock.Arrange(() => mockDataAccess.ExecuteDataTable(string.Empty, string.Empty, null))
            //.IgnoreArguments()
            //.Returns(dt)
            //.MustBeCalled();

            //Act
            TemplatePageFactoryCache objTemplatePageFactoryCache = new TemplatePageFactoryCache(mockDataAccess.Object);
            objTemplatePageFactoryCache.ClientName = "Forethought";
            objTemplatePageFactoryCache.Mode = FactoryCacheMode.All;
           var result = objTemplatePageFactoryCache.GetAllEntities(0, 0);

            //Assert
           List<TemplatePageObjectModel> lstExpected = new List<TemplatePageObjectModel>();
           lstExpected.Add(new TemplatePageObjectModel()
           {
            TemplateID=1,
            TemplateName = "Default",
            PageID = 1,
            PageName = "TAL",
            PageDescription= "Taxonomy Association Link"



           });
           ValidateListData(lstExpected, result.ToList());
            //Assert
           mockDataAccess.VerifyAll();

        }
        #endregion

        #region GetAllEntities_Returns_IEnumerable_TemplatePageObjectModel_WithSortData
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_TemplatePageObjectModel_WithSortData
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_TemplatePageObjectModel_WithSortData()
        {
            //Arrange

            DataTable dt = new DataTable();
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("TemplateName", typeof(string));
            dt.Columns.Add("PageID", typeof(Int32));
            dt.Columns.Add("PageName", typeof(string));
            dt.Columns.Add("PageDescription", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["TemplateId"] = 1;
            dtrow["TemplateName"] = "Default";
            dtrow["PageID"] = 1;
            dtrow["PageName"] = "TAL";
            dtrow["PageDescription"] = "Taxonomy Association Link";

            dt.Rows.Add(dtrow);

           // ClientData();
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act

            //Assert
            
            TemplatePageFactory objFactory = new TemplatePageFactory(mockDataAccess.Object);
           var result= objFactory.GetAllEntities(0, 0, null);
            List<TemplatePageObjectModel> lstExpected = new List<TemplatePageObjectModel>();
            lstExpected.Add(new TemplatePageObjectModel()
            {
                TemplateID = 1,
                TemplateName = "Default",
                PageID = 1,
                PageName = "TAL",
                PageDescription = "Taxonomy Association Link"



            });
            ValidateListData(lstExpected, result.ToList());

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region SaveEntity_With_Entity
        ///// <summary>
        ///// SaveEntity_With_Entity
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_With_Entity()
        //{
        //    //Arrange
        //    Exception exe = null;
            
        //    //Act
        //    TemplatePageFactory objFactory = new TemplatePageFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.SaveEntity(null);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }
        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //    mockDataAccess.Verify();
        //}
        //#endregion

        //#region SaveEntity_With_EntityModBy
        ///// <summary>
        ///// SaveEntity_With_EntityModBy
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_With_EntityModBy()
        //{
        //    //Arrange
        //    Exception exe = null;
           

        //    //Act
        //    TemplatePageFactory objFactory = new TemplatePageFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.SaveEntity(null, 1);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }
        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //    mockDataAccess.VerifyAll();
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
        //    Exception exe = null;
        //    TemplatePageKey objKey = new TemplatePageKey(1, 1);
        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));
        //    //Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //    //  .IgnoreArguments();

        //    //Act
        //    TemplatePageFactoryCache objFactoryCache = new TemplatePageFactoryCache(mockDataAccess.Object);
        //    objFactoryCache.ClientName = "Forethought";
        //    objFactoryCache.Mode = FactoryCacheMode.All;
        //    try
        //    {
        //        objFactoryCache.DeleteEntity(objKey);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //    mockDataAccess.VerifyAll();
        //}
        //#endregion

        //#region DeleteEntity_With_KeyModBy
        ///// <summary>
        ///// DeleteEntity_With_KeyModBy
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_KeyModBy()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    TemplatePageKey objKey = new TemplatePageKey(1, 1);
        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));

        //    //Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //    //  .IgnoreArguments();

        //    //Act
        //    TemplatePageFactory objFactory = new TemplatePageFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.DeleteEntity(objKey, 1);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //    mockDataAccess.Verify();
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
        //    Exception exe = null;
        //    TemplatePageObjectModel entity = new TemplatePageObjectModel();
        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));
        //    //Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //    //  .IgnoreArguments();

        //    //Act
        //    TemplatePageFactory objFactory = new TemplatePageFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.DeleteEntity(entity);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //    mockDataAccess.VerifyAll();
        //}
        //#endregion

        //#region DeleteEntity_With_EntityDelBy
        ///// <summary>
        ///// DeleteEntity_With_EntityDelBy
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_EntityDelBy()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    TemplatePageObjectModel entity = new TemplatePageObjectModel();
        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));
        //    //Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //    //  .IgnoreArguments();

        //    //Act
        //    TemplatePageFactory objFactory = new TemplatePageFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.DeleteEntity(entity, 1);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //    mockDataAccess.VerifyAll();
        //}
        //#endregion

        //#region GetEntityByKey_Returns_TemplatePageObjectModel
        ///// <summary>
        ///// GetEntityByKey_Returns_TemplatePageObjectModel
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_TemplatePageObjectModel()
        //{
        //    Exception exe = null;
        //    TemplatePageKey key = new TemplatePageKey(1, 1);
        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));
        //    //Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //    //  .IgnoreArguments();

        //    //Act
        //    TemplatePageFactory objFactory = new TemplatePageFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.GetEntityByKey(key);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //    mockDataAccess.VerifyAll();
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
        //    TemplatePageSearchDetail objSearchDtl = new TemplatePageSearchDetail();
        //    TemplatePageSortDetail objSortDtl = new TemplatePageSortDetail();
        //    TemplatePageKey objKey = new TemplatePageKey(1, 1);

        //    Exception exe = null; ;

        //    //Act
        //    TemplatePageFactory objFactory = new TemplatePageFactory(mockDataAccess.Object);
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
        //    mockDataAccess.VerifyAll();
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
            TemplatePageFactory objFactory = new TemplatePageFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<TemplatePageObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.Verify();
        }
        #endregion

    }
}
