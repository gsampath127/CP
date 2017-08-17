using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Interfaces;
using RRD.FSG.RP.Model.SortDetail.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using System.Data.Common;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test Class for RolesFactory Class
    ///</summary>
    [TestClass]
    public class RolesTests : BaseTestFactory<RolesObjectModel>
    {
        Mock<IDataAccess> mockDataAccess;

        #region TestInitialize
        ///<summary>
        /// TestInitialize
        ///</summary>
        [TestInitialize]
        public void TestInitialze()
        {
            mockDataAccess = new Mock<IDataAccess>();
        }
        #endregion

        #region ClientData
        ///<summary>
        /// Client Data
        ///<summary>
        [TestMethod]
        public void ClientData()
        {
            DataSet ds = new DataSet();
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
            ds.Tables.Add(dt1);

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
            ds.Tables.Add(dt2);

            //Table 2
            DataTable dt3 = new DataTable();
            dt3.Columns.Add("TemplateId", typeof(Int32));
            dt3.Columns.Add("DefaultNavigationXml", typeof(string));
            dt3.Columns.Add("NavigationKey", typeof(string));
            dt3.Columns.Add("XslTransform", typeof(string));

            ds.Tables.Add(dt3);

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
            ds.Tables.Add(dt4);
            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(ds);

        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable

        /// <summary>
        /// GetAllEntities_Returns_Ienumerable
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_WithTwoParameters()
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

           

            DataTable dtRoles = new DataTable();
            dtRoles.Columns.Add("RoleId", typeof(Int32));
            dtRoles.Columns.Add("Name", typeof(string));
            dtRoles.Columns.Add("ModifiedBy", typeof(Int32));
            dtRoles.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow drRole = dtRoles.NewRow();
            drRole["RoleId"] = 1;
            drRole["Name"] = "Default";
            drRole["ModifiedBy"] = 1;
            drRole["UtcLastModified"] = DateTime.Now;

            dtRoles.Rows.Add(drRole);
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dtRoles);

           
            //Act
            RolesFactoryCache objRolesCache = new RolesFactoryCache(mockDataAccess.Object);
            objRolesCache.ClientName = "Forethought";
            objRolesCache.Mode = FactoryCacheMode.All;
            var result=objRolesCache.GetAllEntities(1, 2);
            ValidateEmptyData(result);

            //Assert
            mockDataAccess.VerifyAll();
          

        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_WiththreeParameters

        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_WiththreeParameters
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_WiththreeParameters()
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



            DataTable dtRoles = new DataTable();
            dtRoles.Columns.Add("RoleId", typeof(Int32));
            dtRoles.Columns.Add("Name", typeof(string));
            dtRoles.Columns.Add("ModifiedBy", typeof(Int32));
            dtRoles.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow drRole = dtRoles.NewRow();
            drRole["RoleId"] = 1;
            drRole["Name"] = "Default";
            drRole["ModifiedBy"] = 1;
            drRole["UtcLastModified"] = DateTime.Now;

            dtRoles.Rows.Add(drRole);
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dtRoles);


            //Act
            RolesSortDetail obj = new RolesSortDetail();
            RolesFactoryCache objRolesCache = new RolesFactoryCache(mockDataAccess.Object);
            objRolesCache.ClientName = "Forethought";
            objRolesCache.Mode = FactoryCacheMode.All;
            var result = objRolesCache.GetAllEntities(1, 2,obj);
            ValidateEmptyData(result);
           //Assert
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_WiththreeParametersfactoryobj

        /// <summary>
        /// GetAllEntities_Returns_Ienumerable
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_WiththreeParametersfactoryobj()
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



            DataTable dtRoles = new DataTable();
            dtRoles.Columns.Add("RoleId", typeof(Int32));
            dtRoles.Columns.Add("Name", typeof(string));
            dtRoles.Columns.Add("ModifiedBy", typeof(Int32));
            dtRoles.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow drRole = dtRoles.NewRow();
            drRole["RoleId"] = 1;
            drRole["Name"] = null;
            drRole["ModifiedBy"] = DBNull.Value;
           // drRole["UtcLastModified"] = DateTime.Now;

            dtRoles.Rows.Add(drRole);
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dtRoles);


            //Act
            RolesSortDetail obj = new RolesSortDetail();
            RolesFactory objRolesCache = new RolesFactory(mockDataAccess.Object);
            objRolesCache.ClientName = "Forethought";
           // objRolesCache.Mode = FactoryCacheMode.All;
            var result = objRolesCache.GetAllEntities(1, 2, obj);
            List<RolesObjectModel> lstExpected = new List<RolesObjectModel>();
            lstExpected.Add(new RolesObjectModel()
            {
                
                RoleId=1,
                
                RoleName=null,
                
               
                
            });
            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("UtcLastModified");
            lstExclude.Add("Key");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

         

            //Assert
            mockDataAccess.VerifyAll();
  

        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_factoryobj_twoparams

        /// <summary>
        /// GetAllEntities_Returns_Ienumerable
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_factoryobj_twoparams()
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
           // dtrow4["NavigationKey"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow4["XslTransform"] = DBNull.Value;
            dtrow4["DefaultNavigationXml"] = DBNull.Value;
            dt4.Rows.Add(dtrow4);
            dSet.Tables.Add(dt4);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dSet);



            DataTable dtRoles = new DataTable();
            dtRoles.Columns.Add("RoleId", typeof(Int32));
            dtRoles.Columns.Add("Name", typeof(string));
            dtRoles.Columns.Add("ModifiedBy", typeof(Int32));
            dtRoles.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow drRole = dtRoles.NewRow();
            drRole["RoleId"] = 1;
          //  drRole["Name"] = "Default";
           // drRole["ModifiedBy"] = 1;
          //  drRole["UtcLastModified"] = DateTime.Now;

            dtRoles.Rows.Add(drRole);
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dtRoles);


            //Act
            
            RolesFactory objRolesCache = new RolesFactory(mockDataAccess.Object);
            objRolesCache.ClientName = "Forethought";
         
            var result = objRolesCache.GetAllEntities(1, 2);
           
            List<RolesObjectModel> lstExpected = new List<RolesObjectModel>();
            lstExpected.Add(new RolesObjectModel()
            {

                RoleId = 1,

                RoleName = null,



            });
            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("UtcLastModified");
            lstExclude.Add("Key");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            mockDataAccess.VerifyAll();
        


        }
        #endregion

        //#region SavEntity_Returns_RolesObjectModel
        /////<summary>
        /////SaveEntity_Returns_RolesObjectModel
        /////<summary>

        //[TestMethod]
        //public void SaveEntity_Returns_RolesObjectModel()
        //{
        //    RolesObjectModel objRolesObject = new RolesObjectModel();
        //    objRolesObject.RoleId = 1;
        //    objRolesObject.Name = "Test0002";

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
        //    ClientData();
        //    mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dSet);
            
        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));
       

        //    //Act
        //    RolesFactoryCache objRolesFactoryCache = new RolesFactoryCache(mockDataAccess.Object);
        //    objRolesFactoryCache.ClientName = "Forethought";
        //    objRolesFactoryCache.Mode = FactoryCacheMode.All;
        //   objRolesFactoryCache.SaveEntity(objRolesObject);
        //}
        //#endregion

        #region SavEntity_Returns_ModifyBy
        ///<summary>
        ///SaveEntity_Returns_ModifyBy
        ///<summary>

        [TestMethod]
        public void SaveEntity_Returns_ModifyBy()
        {
            RolesObjectModel objRolesObject = new RolesObjectModel();
            objRolesObject.RoleId = 1;
            objRolesObject.Name = "Test0002";

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

            var parameters = new[]
            {  
                 new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="Name", Value="TEST_001" }, 
                new SqlParameter(){ ParameterName="RoleId", Value=1 },
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
               .Returns(parameters[0])
               .Returns(parameters[1])
               .Returns(parameters[2])
               ;
            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dSet);
          
            //Act
            RolesFactoryCache objRolesFactoryCache = new RolesFactoryCache(mockDataAccess.Object);
            objRolesFactoryCache.ClientName = "Forethought";
            objRolesFactoryCache.Mode = FactoryCacheMode.All;
           objRolesFactoryCache.SaveEntity(objRolesObject, 38);
            //Assert
           mockDataAccess.VerifyAll();
        }
        #endregion

        //#region DeleteEntity_With_DeleteBy
        ///// <summary>
        ///// DeleteEntity_With_DeleteBy
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_DeleteBy()
        //{
        //    //Arrange
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
        //    mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dSet);
            
          
        //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));
            

        //    //Mock.Arrange(() => mockDataAccess.ExecuteNonQuery(string.Empty, string.Empty, null))
        //    //    .IgnoreArguments()
        //    //    .MustBeCalled();

        //    RolesFactoryCache objRoleFactoryCache = new RolesFactoryCache(mockDataAccess.Object);
        //    objRoleFactoryCache.ClientName = "Forethought";
        //    objRoleFactoryCache.Mode = FactoryCacheMode.All;
        // objRoleFactoryCache.DeleteEntity(1, 3);
        //}
        //#endregion

        #region DeleteEntity_With_Key
        ///<summary>
        ///DeleteEntity_With_Key
        ///</summary>
        [TestMethod]
        public void DeleteEntity_With_Key()
        {
            //Arrange
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
            var parameters = new[]
            {  
                 new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                new SqlParameter(){ ParameterName="RoleId", Value=2 }, 
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
               .Returns(parameters[0])
               .Returns(parameters[1])
               ;

            RolesFactoryCache objRolesFactoryCache = new RolesFactoryCache(mockDataAccess.Object);
            objRolesFactoryCache.ClientName = "Forethought";
            objRolesFactoryCache.Mode = FactoryCacheMode.All;
            objRolesFactoryCache.DeleteEntity(1);

            //Assert
            mockDataAccess.VerifyAll();
          

        }
        #endregion

        #region DeleteEntity_With_Entity
        ///<summary>
        ///DeleteEntity_With_Entity
        ///</summary>
        [TestMethod]
        public void DeleteEntity_With_Entity()
        {
            //Arrange
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



            //Act

            RolesFactoryCache objRolesFactoryCache = new RolesFactoryCache(mockDataAccess.Object);
            objRolesFactoryCache.ClientName = "Forethought";
            objRolesFactoryCache.Mode = FactoryCacheMode.All;

            Exception exe = null;
            try
            {
                objRolesFactoryCache.DeleteEntity(new RolesObjectModel());
            }
            catch (Exception e)
            {
                if (e is NotImplementedException)
                    exe = e;
            }

            //Assert
            Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region DeleteEntity_With_EntityDelBy
        ///<summary>
        ///DeleteEntity_With_EntityDelBy
        ///</summary>
        [TestMethod]
        public void DeleteEntity_With_EntityDelBy()
        {
            //Arrange
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

            //Act

            RolesFactoryCache objRolesFactoryCache = new RolesFactoryCache(mockDataAccess.Object);
            objRolesFactoryCache.ClientName = "Forethought";
            objRolesFactoryCache.Mode = FactoryCacheMode.All;

            Exception exe = null;
            try
            {
                objRolesFactoryCache.DeleteEntity(new RolesObjectModel(), 1);
            }
            catch (Exception e)
            {
                if (e is NotImplementedException)
                    exe = e;
            }

            //Assert
            Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region GetEntityByKey_Returns_RolesObjectModel
        ///<summary>
        ///GetEntityByKey_Returns_RolesObjectModel
        ///</summary>
        [TestMethod]
        public void GetEntityByKey_Returns_RolesObjectModel()
        {
            //Arrange
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



            //Act
            RolesFactory objRolesFactory = new RolesFactory(mockDataAccess.Object);
            objRolesFactory.ClientName = "Forethought";

            Exception exe = null;
            try
            {
           var result=     objRolesFactory.GetEntityByKey(1);
            }
            catch (Exception e)
            {
                if (e is NotImplementedException)
                    exe = e;
            }

            //Assert
            Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
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
        //    // Arrange
        //    RolesSearchDetail objRolesSearchDetail = new RolesSearchDetail();
        //    objRolesSearchDetail.RolesID = 1;
        //    objRolesSearchDetail.Name = "Test_1";
        //    RolesSortDetail objRolesSortDetail = new RolesSortDetail();
        //    Exception exe = null;

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
        //    mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dSet);

           
            


        //    //Act
        //    RolesFactory ObjRolesFacTory = new RolesFactory(mockDataAccess.Object);
        //    try
        //    {
        //     var result =   ObjRolesFacTory.GetEntitiesBySearch(0, 0, objRolesSearchDetail, objRolesSortDetail, 2, 3);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

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
            RolesFactory objFactory = new RolesFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<RolesObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();
        }
        #endregion
      
        #region CreateEntities_NullValue_datarecord
        /// <summary>
        /// CreateEntities_NullValue
        /// </summary>
        [TestMethod]
        public void CreateEntities_NullValue_datarecord()
        {
            //Arrange
            IDataRecord datarec = null;
        
            //Act

            RolesFactory objFactory = new RolesFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<RolesObjectModel>(datarec);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();
        }
        #endregion
    }
}
