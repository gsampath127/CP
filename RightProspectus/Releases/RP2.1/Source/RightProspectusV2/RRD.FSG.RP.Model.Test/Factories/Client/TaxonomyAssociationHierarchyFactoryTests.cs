using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    [TestClass]
    public class TaxonomyAssociationHierarchyFactoryTests : BaseTestFactory<TaxonomyAssociationHierarchyObjectModel>
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

        #region GetAllEntities_Returns_IEnumerable
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable()
        {
            //Arrange  
            DataTable dt = new DataTable();
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("RelationshipType", typeof(int));
            dt.Columns.Add("Order", typeof(Int32));
            dt.Columns.Add("UtcModifiedDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow dtrow = dt.NewRow();
            dtrow["ParentTaxonomyAssociationId"] = 11;
            dtrow["ChildTaxonomyAssociationId"] = 24;
            dtrow["RelationshipType"] = 33;
            dtrow["Order"] = 59;
            dtrow["UtcModifiedDate"] = DateTime.Today;
            dtrow["ModifiedBy"] = 32;
            dtrow["UtcLastModified"] = DateTime.Today;
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

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            //Act
            TaxonomyAssociationHierarchyFactory objTaxonomyAssociationHierarchyFactory = new TaxonomyAssociationHierarchyFactory(mockDataAccess.Object);
            objTaxonomyAssociationHierarchyFactory.ClientName = "Forethought";
            var result = objTaxonomyAssociationHierarchyFactory.GetAllEntities<TaxonomyAssociationHierarchyObjectModel>(1, 2);


            List<TaxonomyAssociationHierarchyObjectModel> lstExpected = new List<TaxonomyAssociationHierarchyObjectModel>();
            lstExpected.Add(new TaxonomyAssociationHierarchyObjectModel()
            {

                ChildTaxonomyAssociationId = 24,
                Order = 59,
                ParentTaxonomyAssociationId = 11,
                RelationshipType = 33

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetAllEntities_Returns_IEnumerable_With_ThreeParameters
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_With_ThreeParameters
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_With_ThreeParameters()
        {
            //Arrange  
            DataTable dt = new DataTable();
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("RelationshipType", typeof(int));
            dt.Columns.Add("Order", typeof(Int32));
            dt.Columns.Add("UtcModifiedDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow dtrow = dt.NewRow();
            dtrow["ParentTaxonomyAssociationId"] = 11;
            dtrow["ChildTaxonomyAssociationId"] = 24;
            dtrow["RelationshipType"] = 33;
            dtrow["Order"] = 59;
            dtrow["UtcModifiedDate"] = DateTime.Today;
            dtrow["ModifiedBy"] = 32;
            dtrow["UtcLastModified"] = DateTime.Today;
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

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            //Act
            TaxonomyAssociationHierarchyFactory objTaxonomyAssociationHierarchyFactory = new TaxonomyAssociationHierarchyFactory(mockDataAccess.Object);
            objTaxonomyAssociationHierarchyFactory.ClientName = "Forethought";
            var result = objTaxonomyAssociationHierarchyFactory.GetAllEntities(1, 2, null);


            List<TaxonomyAssociationHierarchyObjectModel> lstExpected = new List<TaxonomyAssociationHierarchyObjectModel>();
            lstExpected.Add(new TaxonomyAssociationHierarchyObjectModel()
            {

                ChildTaxonomyAssociationId = 24,
                Order = 59,
                ParentTaxonomyAssociationId = 11,
                RelationshipType = 33

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region CreateEntity_With_NullParameter

        /// <summary>
        /// CreateEntity_With_NullParameter
        /// </summary>
        [TestMethod]
        public void CreateEntity_With_NullParameter()
        {
            //Arrange
            DataRow entity = null;
            //Act
            TaxonomyAssociationHierarchyFactory objTaxonomyAssociationHierarchyFactory = new TaxonomyAssociationHierarchyFactory(mockDataAccess.Object);
            var result = objTaxonomyAssociationHierarchyFactory.CreateEntity<TaxonomyAssociationHierarchyObjectModel>(entity);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();

        }

        #endregion
        #region SaveEntity_TaxonomyAssociationHierarchyObjectModel_ModifyBy
        /// <summary>
        /// SaveEntity_TaxonomyAssociationHierarchyObjectModel_ModifyBy
        /// </summary>
        [TestMethod]
        public void SaveEntity_TaxonomyAssociationHierarchyObjectModel_ModifyBy()
        {
            //Arrange  
            DataTable dt = new DataTable();
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("RelationshipType", typeof(int));
            dt.Columns.Add("Order", typeof(Int32));
            dt.Columns.Add("UtcModifiedDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow dtrow = dt.NewRow();
            dtrow["ParentTaxonomyAssociationId"] = 11;
            dtrow["ChildTaxonomyAssociationId"] = 24;
            dtrow["RelationshipType"] = 33;
            dtrow["Order"] = 59;
            dtrow["UtcModifiedDate"] = DateTime.Today;
            dtrow["ModifiedBy"] = 32;
            dtrow["UtcLastModified"] = DateTime.Today;
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


            TaxonomyAssociationHierarchyObjectModel objTaxonomyAssociationHierarchyObjectModel = new TaxonomyAssociationHierarchyObjectModel();
            objTaxonomyAssociationHierarchyObjectModel.ParentTaxonomyAssociationId = 2;
            objTaxonomyAssociationHierarchyObjectModel.Name = "Test_001";


            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ParentTaxonomyAssociationId", Value=1 },
                new SqlParameter(){ ParameterName="ChildTaxonomyAssociationId", Value=2 },
                new SqlParameter(){ ParameterName="RelationshipType", Value=4 },
                new SqlParameter(){ ParameterName="Order", Value=6},
                new SqlParameter(){ ParameterName="ModifiedBy", Value=32} 
                
                             
            };

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4]);


            mockDataAccess.Setup(
                x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            TaxonomyAssociationHierarchyFactory objTaxonomyAssociationHierarchyFactory = new TaxonomyAssociationHierarchyFactory(mockDataAccess.Object);
            objTaxonomyAssociationHierarchyFactory.ClientName = "Forethought";
            objTaxonomyAssociationHierarchyFactory.SaveEntity(objTaxonomyAssociationHierarchyObjectModel, 32);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region DeleteEntity_With_TaxonomyAssociationHierarchyObjectModelParameter
        /// <summary>
        /// DeleteEntity_With_TaxonomyAssociationHierarchyObjectModelParameter
        /// </summary>
        [TestMethod]
        public void DeleteEntity_With_TaxonomyAssociationHierarchyObjectModelParameter()
        {
            //Arrange  
            DataTable dt = new DataTable();
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("RelationshipType", typeof(int));
            dt.Columns.Add("Order", typeof(Int32));
            dt.Columns.Add("UtcModifiedDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow dtrow = dt.NewRow();
            dtrow["ParentTaxonomyAssociationId"] = 11;
            dtrow["ChildTaxonomyAssociationId"] = 24;
            dtrow["RelationshipType"] = 33;
            dtrow["Order"] = 59;
            dtrow["UtcModifiedDate"] = DateTime.Today;
            dtrow["ModifiedBy"] = 32;
            dtrow["UtcLastModified"] = DateTime.Today;
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


            TaxonomyAssociationHierarchyObjectModel objTaxonomyAssociationHierarchyObjectModel = new TaxonomyAssociationHierarchyObjectModel();
            objTaxonomyAssociationHierarchyObjectModel.ParentTaxonomyAssociationId = 2;
            objTaxonomyAssociationHierarchyObjectModel.Name = "Test_001";


            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ParentTaxonomyAssociationId", Value=1 },
                new SqlParameter(){ ParameterName="ChildTaxonomyAssociationId", Value=2 },
                new SqlParameter(){ ParameterName="RelationshipType", Value=4 },
                new SqlParameter(){ ParameterName="DeletedBy", Value=6} 
                
                             
            };

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3]);


            mockDataAccess.Setup(
                x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            TaxonomyAssociationHierarchyFactory objTaxonomyAssociationHierarchyFactory = new TaxonomyAssociationHierarchyFactory(mockDataAccess.Object);
            objTaxonomyAssociationHierarchyFactory.ClientName = "Forethought";
            objTaxonomyAssociationHierarchyFactory.DeleteEntity(objTaxonomyAssociationHierarchyObjectModel);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region DeleteEntity_TaxonomyAssociationHierarchyKey
        /// <summary>
        /// DeleteEntity_TaxonomyAssociationHierarchyKey
        /// </summary>
        [TestMethod]
        public void DeleteEntity_TaxonomyAssociationHierarchyKey()
        {
            //Arrange  
            DataTable dt = new DataTable();
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("RelationshipType", typeof(int));
            dt.Columns.Add("Order", typeof(Int32));
            dt.Columns.Add("UtcModifiedDate", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow dtrow = dt.NewRow();
            dtrow["ParentTaxonomyAssociationId"] = 11;
            dtrow["ChildTaxonomyAssociationId"] = 24;
            dtrow["RelationshipType"] = 33;
            dtrow["Order"] = 59;
            dtrow["UtcModifiedDate"] = DateTime.Today;
            dtrow["ModifiedBy"] = 32;
            dtrow["UtcLastModified"] = DateTime.Today;
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


            TaxonomyAssociationHierarchyKey objTaxonomyAssociationHierarchyKey = new TaxonomyAssociationHierarchyKey(2, 2, 3);



            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ParentTaxonomyAssociationId", Value=1 },
                new SqlParameter(){ ParameterName="ChildTaxonomyAssociationId", Value=2 },
                new SqlParameter(){ ParameterName="RelationshipType", Value=4 },
                new SqlParameter(){ ParameterName="DeletedBy", Value=6} 
                
                             
            };

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dSet);

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3]);


            mockDataAccess.Setup(
                x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            TaxonomyAssociationHierarchyFactory objTaxonomyAssociationHierarchyFactory = new TaxonomyAssociationHierarchyFactory(mockDataAccess.Object);
            objTaxonomyAssociationHierarchyFactory.ClientName = "Forethought";
            objTaxonomyAssociationHierarchyFactory.DeleteEntity(objTaxonomyAssociationHierarchyKey);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

    }
}
