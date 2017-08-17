using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for TaxonomyLevelExternalIdFactory class
    /// </summary>
    [TestClass]
    public class TaxonomyLevelExternalIdFactoryTests : BaseTestFactory<TaxonomyLevelExternalIdObjectModel>
    {
        Mock<IDataAccess> mockDataAccess;

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
            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>())).Returns(dSet);



        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel()
        {
            //Arrange
            TaxonomyLevelExternalIdSortDetail objSortDtl = new TaxonomyLevelExternalIdSortDetail();
            objSortDtl.Column = TaxonomyLevelExternalIdSortColumn.TaxonomyName;

            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("Level", typeof(Int32));
            dt.Columns.Add("TaxonomyId", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("ExternalId", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("IsPrimary", typeof(bool));

            DataRow dtrow = dt.NewRow();
            dtrow["Level"] = 1;
            dtrow["TaxonomyId"] = 26682;
            dtrow["TaxonomyName"] = null;
            dtrow["ExternalId"] = "34628V193";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["IsPrimary"] = false;

            dt.Rows.Add(dtrow);

            ClientData();
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(dt);


            //Act
            TaxonomyLevelExternalIdFactoryCache objTaxonomyLevelExternalIdFactoryCache = new TaxonomyLevelExternalIdFactoryCache(mockDataAccess.Object);
            objTaxonomyLevelExternalIdFactoryCache.ClientName = "Forethought";
            objTaxonomyLevelExternalIdFactoryCache.Mode = FactoryCacheMode.All;
            var result = objTaxonomyLevelExternalIdFactoryCache.GetAllEntities(0, 0, objSortDtl);
            List<TaxonomyLevelExternalIdObjectModel> lstExpected = new List<TaxonomyLevelExternalIdObjectModel>();
            lstExpected.Add(new TaxonomyLevelExternalIdObjectModel()
            {
                Level = 1,
                TaxonomyId = 26682,
                ExternalId = "34628V193",
                TaxonomyName = null,
                IsPrimary = false
            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            // Mock.Assert(mockDataAccess);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel_withoutvalue
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel_withoutvalue()
        {
            //Arrange
            TaxonomyLevelExternalIdSortDetail objSortDtl = new TaxonomyLevelExternalIdSortDetail();
            objSortDtl.Column = TaxonomyLevelExternalIdSortColumn.TaxonomyName;

            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("Level", typeof(Int32));
            dt.Columns.Add("TaxonomyId", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("ExternalId", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("IsPrimary", typeof(bool));



            ClientData();

            // mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);
            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(dt);


            //Act
            TaxonomyLevelExternalIdFactoryCache objTaxonomyLevelExternalIdFactoryCache = new TaxonomyLevelExternalIdFactoryCache(mockDataAccess.Object);
            objTaxonomyLevelExternalIdFactoryCache.ClientName = "Forethought";
            objTaxonomyLevelExternalIdFactoryCache.Mode = FactoryCacheMode.All;
            var result = objTaxonomyLevelExternalIdFactoryCache.GetAllEntities(0, 0, objSortDtl);
            ValidateEmptyData(result);
            //assert
            mockDataAccess.VerifyAll();


        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel_withtaxonomyname
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel_withtaxonomyname
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel_withtaxonomyname()
        {
            //Arrange
            TaxonomyLevelExternalIdSortDetail objSortDtl = new TaxonomyLevelExternalIdSortDetail();
            objSortDtl.Column = TaxonomyLevelExternalIdSortColumn.TaxonomyName;

            objSortDtl.Order = SortOrder.Ascending;




            DataTable dt = new DataTable();
            dt.Columns.Add("Level", typeof(Int32));
            dt.Columns.Add("TaxonomyId", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("ExternalId", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("IsPrimary", typeof(bool));

            DataRow dtrow = dt.NewRow();
            dtrow["Level"] = 1;
            dtrow["TaxonomyId"] = 26682;
            dtrow["TaxonomyName"] = "Tst";
            dtrow["ExternalId"] = "34628V193";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["IsPrimary"] = false;

            dt.Rows.Add(dtrow);

            ClientData();
            //     mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(dt);


            //Mock.Arrange(() => mockDataAccess.ExecuteDataTable(string.Empty, string.Empty, null))
            //  .IgnoreArguments()
            //  .Returns(dt)
            //  .MustBeCalled();
            //Mock.Arrange(() => mockDataAccess.ExecuteDataTable(string.Empty, string.Empty))
            // .IgnoreArguments()
            // .Returns(dt)
            // .MustBeCalled();


            //Act

            TaxonomyLevelExternalIdFactoryCache objTaxonomyLevelExternalIdFactoryCache = new TaxonomyLevelExternalIdFactoryCache(mockDataAccess.Object);
            objTaxonomyLevelExternalIdFactoryCache.ClientName = "Forethought";
            objTaxonomyLevelExternalIdFactoryCache.Mode = FactoryCacheMode.All;
            var result = objTaxonomyLevelExternalIdFactoryCache.GetAllEntities(0, 0, objSortDtl);
            List<TaxonomyLevelExternalIdObjectModel> lstExpected = new List<TaxonomyLevelExternalIdObjectModel>();
            lstExpected.Add(new TaxonomyLevelExternalIdObjectModel()
            {
                Level = 1,
                TaxonomyId = 26682,
                ExternalId = "34628V193",
                TaxonomyName = "Tst",
                IsPrimary = false
            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            // Mock.Assert(mockDataAccess);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel_withobjectmodelcountlessthanzero
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel_withobjectmodelcountlessthanzero
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel_withobjectmodelcountlessthanzero()
        {
            //Arrange
            TaxonomyLevelExternalIdSortDetail objSortDtl = new TaxonomyLevelExternalIdSortDetail();
            objSortDtl.Column = TaxonomyLevelExternalIdSortColumn.TaxonomyName;

            objSortDtl.Order = SortOrder.Ascending;
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TaxonomyId", typeof(Int32));
            dt2.Columns.Add("CompanyName", typeof(string));
            dt2.Columns.Add("ProsName", typeof(string));
            dt2.Columns.Add("TaxonomyName", typeof(string));
            dt2.Columns.Add("LEVEL", typeof(Int32));


            DataRow dtrow2 = dt2.NewRow();

            //dtrow2["TaxonomyId"] = 2;
            //dtrow2["CompanyName"] = "Test";
            //dtrow2["ProsName"] = "Test";
            //dtrow2["TaxonomyName"] = "Test";
            //dtrow2["LEVEL"] = 1;
            //dt2.Rows.Add(dtrow2);




            DataTable dt = new DataTable();
            dt.Columns.Add("Level", typeof(Int32));
            dt.Columns.Add("TaxonomyId", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("ExternalId", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("IsPrimary", typeof(bool));

            DataRow dtrow = dt.NewRow();
            dtrow["Level"] = 1;
            dtrow["TaxonomyId"] = 26682;
            dtrow["TaxonomyName"] = null;
            dtrow["ExternalId"] = "34628V193";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["IsPrimary"] = false;

            dt.Rows.Add(dtrow);

            ClientData();
            //mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt2);

            //mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);
            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt2)
                .Returns(dt);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(dt);


            //Act
            TaxonomyLevelExternalIdFactoryCache objTaxonomyLevelExternalIdFactoryCache = new TaxonomyLevelExternalIdFactoryCache(mockDataAccess.Object);
            objTaxonomyLevelExternalIdFactoryCache.ClientName = "Forethought";
            objTaxonomyLevelExternalIdFactoryCache.Mode = FactoryCacheMode.All;
            var result = objTaxonomyLevelExternalIdFactoryCache.GetAllEntities(0, 0, objSortDtl);
            List<TaxonomyLevelExternalIdObjectModel> lstExpected = new List<TaxonomyLevelExternalIdObjectModel>();
            lstExpected.Add(new TaxonomyLevelExternalIdObjectModel()
            {
                Level = 1,
                TaxonomyId = 26682,
                ExternalId = "34628V193",
                TaxonomyName = null,
                IsPrimary = false
            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            // Mock.Assert(mockDataAccess);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel_withdifferenttaxonomyname
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel_withdifferenttaxonomyname
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable_TaxonomyLevelExternalIdObjectModel_withdifferenttaxonomyname()
        {
            //Arrange
            TaxonomyLevelExternalIdSortDetail objSortDtl = new TaxonomyLevelExternalIdSortDetail();
            objSortDtl.Column = TaxonomyLevelExternalIdSortColumn.TaxonomyName;

            objSortDtl.Order = SortOrder.Ascending;
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




            DataTable dt = new DataTable();
            dt.Columns.Add("Level", typeof(Int32));
            dt.Columns.Add("TaxonomyId", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("ExternalId", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("IsPrimary", typeof(bool));

            DataRow dtrow = dt.NewRow();
            dtrow["Level"] = 1;
            dtrow["TaxonomyId"] = 26682;
            dtrow["TaxonomyName"] = null;
            dtrow["ExternalId"] = "34628V193";
            dtrow["UtcLastModified"] = DateTime.Now;
            dtrow["ModifiedBy"] = 1;
            dtrow["IsPrimary"] = false;

            dt.Rows.Add(dtrow);

            ClientData();



            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt2)
                .Returns(dt);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>())).Returns(dt);


            //Act
            TaxonomyLevelExternalIdFactoryCache objTaxonomyLevelExternalIdFactoryCache = new TaxonomyLevelExternalIdFactoryCache(mockDataAccess.Object);
            objTaxonomyLevelExternalIdFactoryCache.ClientName = "Forethought";
            objTaxonomyLevelExternalIdFactoryCache.Mode = FactoryCacheMode.All;
            var result = objTaxonomyLevelExternalIdFactoryCache.GetAllEntities(0, 0, objSortDtl);
            List<TaxonomyLevelExternalIdObjectModel> lstExpected = new List<TaxonomyLevelExternalIdObjectModel>();
            lstExpected.Add(new TaxonomyLevelExternalIdObjectModel()
            {
                Level = 1,
                TaxonomyId = 26682,
                ExternalId = "34628V193",
                TaxonomyName = null,
                IsPrimary = false
            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            // Mock.Assert(mockDataAccess);
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
            TaxonomyLevelExternalIdObjectModel objObjectModel = new TaxonomyLevelExternalIdObjectModel();
            objObjectModel.Level = 1;
            objObjectModel.TaxonomyId = 20869;
            objObjectModel.ExternalId = "TEST_001";
            objObjectModel.IsPrimary = true;

            var parameters = new[]
            {  
                 new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                 new SqlParameter(){ ParameterName="Level", Value=1 },
                new SqlParameter(){ ParameterName="TaxonomyId", Value=20869 },
                new SqlParameter(){ ParameterName="ExternalId", Value="TEST_001" },
                new SqlParameter(){ ParameterName="IsPrimary", Value=true },
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
               .Returns(parameters[0])
               .Returns(parameters[1])
               .Returns(parameters[2])
                .Returns(parameters[3])
               .Returns(parameters[4])
               ;
            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IEnumerable<DbParameter>>()));

            //Act
            TaxonomyLevelExternalIdFactory objTaxonomyLevelExternalIdFactory = new TaxonomyLevelExternalIdFactory(mockDataAccess.Object);

            objTaxonomyLevelExternalIdFactory.SaveEntity(objObjectModel, 1);

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
            TaxonomyLevelExternalIdObjectModel entity = new TaxonomyLevelExternalIdObjectModel()
            {

            };
            var parameters = new[]
            {  
                 new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                 new SqlParameter(){ ParameterName="Level", Value=1 },
                new SqlParameter(){ ParameterName="TaxonomyId", Value=1 },
                new SqlParameter(){ ParameterName="ExternalId", Value=1 },
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
               .Returns(parameters[0])
               .Returns(parameters[1])
               .Returns(parameters[2])
                .Returns(parameters[3]);


            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IEnumerable<DbParameter>>()));


            //Act
            TaxonomyLevelExternalIdFactory objTaxonomyLevelExternalIdFactory = new TaxonomyLevelExternalIdFactory(mockDataAccess.Object);
            objTaxonomyLevelExternalIdFactory.DeleteEntity(entity);

            //Assert    
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region DeleteEntity_With_Key
        /// <summary>
        /// DeleteEntity_With_Key
        /// </summary>
        [TestMethod]
        public void DeleteEntity_With_Key()
        {
            //Arrange
            TaxonomyLevelExternalIdKey objTaxonomyLevelExternalIdKey = new TaxonomyLevelExternalIdKey(1, 123, "TEST_001");
            ClientData();
            var parameters = new[]
            {  
                 new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                 new SqlParameter(){ ParameterName="Level", Value=1 },
                new SqlParameter(){ ParameterName="TaxonomyId", Value=1 },
                new SqlParameter(){ ParameterName="ExternalId", Value=1 },
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
               .Returns(parameters[0])
               .Returns(parameters[1])
               .Returns(parameters[2])
                .Returns(parameters[3])
               ;


            //    mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()));

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IEnumerable<DbParameter>>()));

            //Act
            TaxonomyLevelExternalIdFactoryCache objTaxonomyLevelExternalIdFactoryCache = new TaxonomyLevelExternalIdFactoryCache(mockDataAccess.Object);
            objTaxonomyLevelExternalIdFactoryCache.ClientName = "Forethought";
            objTaxonomyLevelExternalIdFactoryCache.Mode = FactoryCacheMode.All;
            objTaxonomyLevelExternalIdFactoryCache.DeleteEntity(objTaxonomyLevelExternalIdKey);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion


        #region GetEntityByKey_Returns_TaxonomyLevelExternalIdObjectModel
        /// <summary>
        /// GetEntityByKey_Returns_TaxonomyLevelExternalIdObjectModel
        /// </summary>
        [TestMethod]
        public void GetEntityByKey_Returns_TaxonomyLevelExternalIdObjectModel()
        {
            //Arrange
            TaxonomyLevelExternalIdKey objTaxonomyLevelExternalIdKey = new TaxonomyLevelExternalIdKey(1, 1, "TEST_001");

            Exception exe = null; ;

            //Act
            TaxonomyLevelExternalIdFactory objTaxonomyLevelExternalIdFactory = new TaxonomyLevelExternalIdFactory(mockDataAccess.Object);
            try
            {
                objTaxonomyLevelExternalIdFactory.GetEntityByKey(objTaxonomyLevelExternalIdKey);
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

        #region GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity
        /// <summary>
        /// GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_WithKey_Returns_IEnumerable_Entity()
        {
            //Arrange  
            TaxonomyLevelExternalIdSearchDetail objSearchDtl = new TaxonomyLevelExternalIdSearchDetail();
            TaxonomyLevelExternalIdKey objTaxonomyLevelExternalIdKey = new TaxonomyLevelExternalIdKey(1, 1, "TEST");

            Exception exe = null; ;

            //Act
            TaxonomyLevelExternalIdFactory objTaxonomyLevelExternalIdFactory = new TaxonomyLevelExternalIdFactory(mockDataAccess.Object);
            try
            {
                objTaxonomyLevelExternalIdFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objTaxonomyLevelExternalIdKey);
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

        #region GetEntitiesBySearch_Returns_IEnumerable_Entity
        /// <summary>
        /// GetEntitiesBySearch_Returns_IEnumerable_Entity
        /// </summary>
        [TestMethod]
        public void GetEntitiesBySearch_Returns_IEnumerable_Entity()
        {
            //Arrange  
            TaxonomyLevelExternalIdSearchDetail objSearchDtl = new TaxonomyLevelExternalIdSearchDetail();
            TaxonomyLevelExternalIdSortDetail objSortDtl = new TaxonomyLevelExternalIdSortDetail();

            //ClientData();
            Exception exe = null; ;

            //Act
            TaxonomyLevelExternalIdFactory objTaxonomyLevelExternalIdFactory = new TaxonomyLevelExternalIdFactory(mockDataAccess.Object);
            try
            {
                objTaxonomyLevelExternalIdFactory.GetEntitiesBySearch(0, 0, objSearchDtl, objSortDtl, null);
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
            TaxonomyLevelExternalIdFactory objFactory = new TaxonomyLevelExternalIdFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<TaxonomyLevelExternalIdObjectModel>(dr);

            //Assert

            Assert.AreEqual(result, null);

            mockDataAccess.VerifyAll();
        }
        #endregion
    }
}
