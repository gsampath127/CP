using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Factories.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Moq;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.SearchEntities.Client;


namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for DocumentTypeAssociationFactoryTests class
    /// </summary>
    [TestClass]
    public class DocumentTypeAssociationFactoryTests : BaseTestFactory<DocumentTypeAssociationObjectModel>
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
        #region GetAllEntities_Returns_IEnumerable_DocumentTypeAssociationObjectModel

        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_DocumentTypeAssociationObjectModel
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_DocumentTypeAssociationObjectModel()
        {
            //Arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("DocumentTypeAssociationId", typeof(int));
            dt.Columns.Add("DocumentTypeId", typeof(int));
            dt.Columns.Add("SiteId", typeof(int));
            dt.Columns.Add("TaxonomyAssociationId", typeof(int));
            dt.Columns.Add("Order", typeof(int));
            dt.Columns.Add("HeaderText", typeof(string));
            dt.Columns.Add("LinkText", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("CssClass", typeof(string));
            dt.Columns.Add("MarketId", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));

            DataRow dtrow1 = dt.NewRow();
            dtrow1["DocumentTypeAssociationId"] = 23;
            dtrow1["DocumentTypeId"] = 34;
            dtrow1["SiteId"] = 43;
            dtrow1["TaxonomyAssociationId"] = 9;
            dtrow1["Order"] = 223;
            dtrow1["HeaderText"] = "DTA";
            dtrow1["LinkText"] = "Test_1";
            dtrow1["DescriptionOverride"] = "Doc_test";
            dtrow1["CssClass"] = "Test_css";
            dtrow1["MarketId"] = "TEST_Mrkt";
            dtrow1["UtcLastModified"] = DateTime.Today;
            dtrow1["ModifiedBy"] = 32;
            dt.Rows.Add(dtrow1);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dt);

            //Act
            DocumentTypeAssociationFactory ObjDocumentTypeAssociationFactory = new DocumentTypeAssociationFactory(mockDataAccess.Object);
            var result = ObjDocumentTypeAssociationFactory.GetAllEntities();

            List<DocumentTypeAssociationObjectModel> lstExpected = new List<DocumentTypeAssociationObjectModel>();
            lstExpected.Add(new DocumentTypeAssociationObjectModel()
            {
                DocumentTypeAssociationId = 23,
                DocumentTypeId = 34,
                SiteId = 43,
                TaxonomyAssociationId = 9,
                Order = 223,
                HeaderText = "DTA",
                LinkText = "Test_1",
                DescriptionOverride = "Doc_test",
                CssClass = "Test_css",
                MarketId = "TEST_Mrkt",
                Description = null,
                Name = null
            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("LastModified");
            lstExclude.Add("Key");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region GetAllEntities_Returns_IEnumerable_With_TwoParameters

        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_With_TwoParameters
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_With_TwoParameters()
        {
            //Arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("DocumentTypeAssociationId", typeof(int));
            dt.Columns.Add("DocumentTypeId", typeof(int));
            dt.Columns.Add("SiteId", typeof(int));
            dt.Columns.Add("TaxonomyAssociationId", typeof(int));
            dt.Columns.Add("Order", typeof(int));
            dt.Columns.Add("HeaderText", typeof(string));
            dt.Columns.Add("LinkText", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("CssClass", typeof(string));
            dt.Columns.Add("MarketId", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(int));

            DataRow dtrow1 = dt.NewRow();
            dtrow1["DocumentTypeAssociationId"] = 23;
            dtrow1["DocumentTypeId"] = 34;
            dtrow1["SiteId"] = 43;
            dtrow1["TaxonomyAssociationId"] = 9;
            dtrow1["Order"] = 223;
            dtrow1["HeaderText"] = "DTA";
            dtrow1["LinkText"] = "Test_1";
            dtrow1["DescriptionOverride"] = "Doc_test";
            dtrow1["CssClass"] = "Test_css";
            dtrow1["MarketId"] = "TEST_Mrkt";
            dtrow1["UtcLastModified"] = DateTime.Today;
            dtrow1["ModifiedBy"] = 32;
            dt.Rows.Add(dtrow1);

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
               .Returns(dt);

            //Act
            DocumentTypeAssociationFactory ObjDocumentTypeAssociationFactory = new DocumentTypeAssociationFactory(mockDataAccess.Object);
            var result = ObjDocumentTypeAssociationFactory.GetAllEntities<DocumentTypeAssociationObjectModel>(1, 1);

            List<DocumentTypeAssociationObjectModel> lstExpected = new List<DocumentTypeAssociationObjectModel>();
            lstExpected.Add(new DocumentTypeAssociationObjectModel()
            {
                DocumentTypeAssociationId = 23,
                DocumentTypeId = 34,
                SiteId = 43,
                TaxonomyAssociationId = 9,
                Order = 223,
                HeaderText = "DTA",
                LinkText = "Test_1",
                DescriptionOverride = "Doc_test",
                CssClass = "Test_css",
                MarketId = "TEST_Mrkt",
                Description = null,
                Name = null
            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("LastModified");
            lstExclude.Add("Key");
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
        #region SaveEntity_With_DocumentTypeAssociationObjectModel_Modify

        /// <summary>
        /// SaveEntity_With_DocumentTypeAssociationObjectModel_Modify
        /// </summary>
        [TestMethod]
        public void SaveEntity_With_DocumentTypeAssociationObjectModel_Modify()
        {
            //Arrange
            DocumentTypeAssociationObjectModel ObjDocumentTypeAssociationObjectModel = new DocumentTypeAssociationObjectModel();
            ObjDocumentTypeAssociationObjectModel.DocumentTypeAssociationId = 1;
            ObjDocumentTypeAssociationObjectModel.DocumentTypeId = 2;


            var parameters = new[]
            { 
               new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
               new SqlParameter(){ ParameterName="DocumentTypeAssociationId", Value=1 },
               new SqlParameter(){ ParameterName="DocumentTypeId", Value="TEST_001" },
               new SqlParameter(){ ParameterName="SiteId", Value=2 }  ,
               new SqlParameter(){ ParameterName="TaxonomyAssociationId", Value=3 }   ,  
               new SqlParameter(){ ParameterName="Order", Value=4 },     
               new SqlParameter(){ ParameterName="HeaderText", Value="Test1" }   ,  
               new SqlParameter(){ ParameterName="LinkText", Value="Test2" }  ,   
               new SqlParameter(){ ParameterName="DescriptionOverride", Value="Test3" }  ,   
               new SqlParameter(){ ParameterName="CssClass", Value="Test4" }   ,  
               new SqlParameter(){ ParameterName="MarketId", Value="Test5" }     
            };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4])
                .Returns(parameters[5])
                .Returns(parameters[6])
                .Returns(parameters[7])
                .Returns(parameters[8])
                .Returns(parameters[9])
                .Returns(parameters[10]);


            mockDataAccess.Setup(
                x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));


            //Act
            DocumentTypeAssociationFactory objDocumentTypeAssociationFactory = new DocumentTypeAssociationFactory(mockDataAccess.Object);
            objDocumentTypeAssociationFactory.SaveEntity(ObjDocumentTypeAssociationObjectModel, 32);
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
            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="deletedBy", Value=1 },
                new SqlParameter(){ ParameterName="DBCDocumentTypeAssociationId", Value=1 },           
            };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);


            mockDataAccess.Setup(
                x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            DocumentTypeAssociationFactory objDocumentTypeAssociationFactory = new DocumentTypeAssociationFactory(mockDataAccess.Object);
            objDocumentTypeAssociationFactory.DeleteEntity(3);
            //Assert
            mockDataAccess.VerifyAll();
        }

        #endregion
        #region CreateEntity_With_Null

        /// <summary>
        /// CreateEntity_With_Null
        /// </summary>
        [TestMethod]
        public void CreateEntity_With_Null()
        {
            //Arrange          
            DataRow dtrow1 = null;
            //Act
            DocumentTypeAssociationFactory objDocumentTypeAssociationFactory = new DocumentTypeAssociationFactory(mockDataAccess.Object);
            var result = objDocumentTypeAssociationFactory.CreateEntity<DocumentTypeAssociationObjectModel>(dtrow1);

            Assert.AreEqual(result, null);
            //Assert
            mockDataAccess.VerifyAll();
        }

        #endregion

    }
}
