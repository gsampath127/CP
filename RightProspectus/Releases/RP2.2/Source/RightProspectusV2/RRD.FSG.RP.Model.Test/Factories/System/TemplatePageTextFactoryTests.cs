using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using RRD.FSG.RP.Model.Factories;
using System.Web.Script.Serialization;
using Moq;

namespace RRD.FSG.RP.Model.Test.Factories.System
{
    [TestClass]
    public class TemplatePageTextFactoryTests : BaseTestFactory<TemplatePageTextObjectModel>
    {
        Mock<IDataAccess> mockDataAccess { get; set; }

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

        #region GetAllEntities_Returns_IEnumerable_TemplatePageObjectModel_TemplatePageTextFactoryTests
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_TemplatePageObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_TemplatePageObjectModel_TemplatePageTextFactoryTests()
        {
            //Arrange

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TemplateId", typeof(Int32));
            dt2.Columns.Add("PageID", typeof(Int32));
            dt2.Columns.Add("Name", typeof(string));
            dt2.Columns.Add("ResourceKey", typeof(string));
            dt2.Columns.Add("IsHtml", typeof(bool));
            dt2.Columns.Add("DefaultText", typeof(string));
            dt2.Columns.Add("Description", typeof(string));

            DataRow dtrow = dt2.NewRow();
            dtrow["TemplateId"] = 1;
            dtrow["PageID"] = 1;
            dtrow["ResourceKey"] = "TAL_ProductHeaderText";
            dtrow["Name"] = "TaxonomyAssociationLink_ProductHeaderText";
            dtrow["IsHtml"] = 1;
            dtrow["DefaultText"] = "Test";
            dtrow["Description"] = DBNull.Value;

            dt2.Rows.Add(dtrow);

            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt2);
            //Act
            TemplatePageTextFactoryCache objTemplatePageTextFactoryCache = new TemplatePageTextFactoryCache(mockDataAccess.Object);
            objTemplatePageTextFactoryCache.ClientName = "Forethought";
            objTemplatePageTextFactoryCache.Mode = FactoryCacheMode.All;
            var result = objTemplatePageTextFactoryCache.GetAllEntities(0, 0);


            List<TemplatePageTextObjectModel> lstExpected = new List<TemplatePageTextObjectModel>();
            lstExpected.Add(new TemplatePageTextObjectModel()
            {
                DefaultText = "Test",
                IsHTML = true,
                PageID = 1,
                ResourceKey = "TAL_ProductHeaderText",
                TemplateID = 1,
                Name = "TaxonomyAssociationLink_ProductHeaderText",
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_IEnumerable_TemplatePageObjectModel_WithSortData_TemplatePageTextFactoryTests
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_TemplatePageObjectModel_WithSortData_TemplatePageTextFactoryTests
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_TemplatePageObjectModel_WithSortData_TemplatePageTextFactoryTests()
        {
            //Arrange

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TemplateId", typeof(Int32));
            dt2.Columns.Add("PageID", typeof(Int32));
            dt2.Columns.Add("Name", typeof(string));
            dt2.Columns.Add("ResourceKey", typeof(string));
            dt2.Columns.Add("IsHtml", typeof(bool));
            dt2.Columns.Add("DefaultText", typeof(string));
            dt2.Columns.Add("Description", typeof(string));

            DataRow dtrow = dt2.NewRow();
            dtrow["TemplateId"] = 1;
            dtrow["PageID"] = 1;
            dtrow["ResourceKey"] = "TAL_ProductHeaderText";
            dtrow["Name"] = "TaxonomyAssociationLink_ProductHeaderText";
            dtrow["IsHtml"] = 1;
            dtrow["DefaultText"] = "Test";
            dtrow["Description"] = DBNull.Value;

            dt2.Rows.Add(dtrow);

            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt2);

            //Act
            TemplatePageTextFactory objTemplatePageTextFactory = new TemplatePageTextFactory(mockDataAccess.Object);
            objTemplatePageTextFactory.ClientName = "Forethought";

            var result = objTemplatePageTextFactory.GetAllEntities(0, 0, null);

            List<TemplatePageTextObjectModel> lstExpected = new List<TemplatePageTextObjectModel>();
            lstExpected.Add(new TemplatePageTextObjectModel()
            {
                DefaultText = "Test",
                IsHTML = true,
                PageID = 1,
                ResourceKey = "TAL_ProductHeaderText",
                TemplateID = 1,
                Name = "TaxonomyAssociationLink_ProductHeaderText",
                Description = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region SaveEntity_With_Entity_TemplatePageTextFactory
        ///// <summary>
        ///// SaveEntity_With_Entity_TemplatePageTextFactory
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_With_Entity_TemplatePageTextFactory()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    ClientData();

        //    //Act
        //    TemplatePageTextFactory objTemplatePageTextFactory = new TemplatePageTextFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objTemplatePageTextFactory.SaveEntity(null);
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

        //#region SaveEntity_With_EntityModBy_TemplatePageTextFactory
        ///// <summary>
        ///// SaveEntity_With_EntityModBy_TemplatePageTextFactory
        ///// </summary>
        //[TestMethod]
        //public void SaveEntity_With_EntityModBy_TemplatePageTextFactory()
        //{
        //    //Arrange
        //    Exception exe = null;
        //    ClientData();

        //    //Act
        //    TemplatePageTextFactory objTemplatePageTextFactory = new TemplatePageTextFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objTemplatePageTextFactory.SaveEntity(null, 1);
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

        //#region DeleteEntity_With_Key_TemplatePageTextFactory
        ///// <summary>
        ///// DeleteEntity_With_Key_TemplatePageTextFactory
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Key_TemplatePageTextFactory()
        //{
        //    //Arrange
        //    ClientData();
        //    Exception exe = null;
        //    TemplatePageTextKey objTemplatePageTextKey = new TemplatePageTextKey(1, 1, string.Empty);

        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));
        //    //Act
        //    TemplatePageTextFactoryCache objTemplatePageTextFactoryCache = new TemplatePageTextFactoryCache(mockDataAccess.Object);
        //    objTemplatePageTextFactoryCache.ClientName = "Forethought";
        //    objTemplatePageTextFactoryCache.Mode = FactoryCacheMode.All;
        //    try
        //    {
        //        objTemplatePageTextFactoryCache.DeleteEntity(objTemplatePageTextKey);
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
        //#region DeleteEntity__TemplatePageTextFactory
        ///// <summary>
        ///// DeleteEntity_TemplatePageTextFactory
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity__TemplatePageTextFactory()
        //{
        //    //Arrange
        //    ClientData();
        //    Exception exe = null;
        //    TemplatePageTextObjectModel objTemplatePageTextObjectModel = new TemplatePageTextObjectModel();

        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));
        //    //Act
        //    TemplatePageTextFactoryCache objTemplatePageTextFactoryCache = new TemplatePageTextFactoryCache(mockDataAccess.Object);
        //    objTemplatePageTextFactoryCache.ClientName = "Forethought";
        //    objTemplatePageTextFactoryCache.Mode = FactoryCacheMode.All;
        //    try
        //    {
        //        objTemplatePageTextFactoryCache.DeleteEntity(objTemplatePageTextObjectModel);
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

        //#region DeleteEntity_With_KeyModBy_TemplatePageTextFactory
        ///// <summary>
        ///// DeleteEntity_With_KeyModBy_TemplatePageTextFactory
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_KeyModBy_TemplatePageTextFactory()
        //{
        //    //Arrange
        //    ClientData();
        //    Exception exe = null;
        //    TemplatePageTextKey objTemplatePageTextKey = new TemplatePageTextKey(1, 1, string.Empty);

        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));
        //    //Act
        //    TemplatePageTextFactory objTemplatePageTextFactory = new TemplatePageTextFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objTemplatePageTextFactory.DeleteEntity(objTemplatePageTextKey, 1);
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

        //#region DeleteEntity_With_EntityDelBy_TemplatePageTextFactory
        ///// <summary>
        ///// DeleteEntity_With_EntityDelBy_TemplatePageTextFactory
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_EntityDelBy_TemplatePageTextFactory()
        //{
        //    //Arrange
        //    ClientData();
        //    Exception exe = null;
        //    TemplatePageTextObjectModel entityTemplatePageTextObjectModel = new TemplatePageTextObjectModel();

        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));
        //    //Act
        //    TemplatePageTextFactory objTemplatePageTextFactory = new TemplatePageTextFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objTemplatePageTextFactory.DeleteEntity(entityTemplatePageTextObjectModel, 1);
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

        //#region GetEntityByKey_Returns_TemplatePageObjectModel_TemplatePageTextFactory
        ///// <summary>
        ///// GetEntityByKey_Returns_TemplatePageObjectModel_TemplatePageTextFactory
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_Returns_TemplatePageObjectModel_TemplatePageTextFactory()
        //{
        //    //Arrange
        //    ClientData();
        //    Exception exe = null;
        //    TemplatePageTextKey keyTemplatePageTextKey = new TemplatePageTextKey(1, 1, string.Empty);

        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));

        //    //Act
        //    TemplatePageTextFactory objFactory = new TemplatePageTextFactory(mockDataAccess.Object);
        //    try
        //    {
        //        objFactory.GetEntityByKey(keyTemplatePageTextKey);
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

        //#region GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity_TemplatePageTextFactory
        ///// <summary>
        ///// GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity_TemplatePageTextFactory
        ///// </summary>
        //[TestMethod]
        //public void GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity_TemplatePageTextFactory()
        //{
        //    //Arrange  
        //    TemplatePageTextSearchDetail objSearchDtl = new TemplatePageTextSearchDetail();
        //    TemplatePageTextSortDetail objSortDtl = new TemplatePageTextSortDetail();
        //    TemplatePageTextKey objKey = new TemplatePageTextKey(1, 1, string.Empty);

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    TemplatePageTextFactory objFactory = new TemplatePageTextFactory(mockDataAccess.Object);
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
            TemplatePageTextFactory objFactory = new TemplatePageTextFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<TemplatePageTextObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

    }
}
