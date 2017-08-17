using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using Moq;
using System.Data.Common;
using System.Data.SqlClient;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    [TestClass]
    public class TaxonomyAssociationFactoryTests : BaseTestFactory<TaxonomyAssociationObjectModel>
    {
        private Mock<IDataAccess> mockDataAccess;

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

            mockDataAccess.Setup(X => X.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
           .Returns(dSet);
        }
        #endregion

        #region GetAllEntities_Returns_Ienumerable
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable()
        {
            //Arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("Level", typeof(Int32));
            dt.Columns.Add("TaxonomyId", typeof(Int32));
            dt.Columns.Add("SiteId", typeof(Int32));
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("CssClass", typeof(string));
            dt.Columns.Add("MarketId", typeof(string));
            dt.Columns.Add("UtcLastModified", typeof(DateTime));
            dt.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow drow = dt.NewRow();
            drow["TaxonomyAssociationId"] = 1;
            drow["Level"] = 1;
            drow["TaxonomyId"] = 1;
            drow["SiteId"] = 1;
            drow["ParentTaxonomyAssociationId"] = 1;
            drow["NameOverride"] = "test";
            drow["DescriptionOverride"] = "test_Desc";
            drow["MarketId"] = "1";
            drow["CssClass"] = "test.css";
            drow["UtcLastModified"] = DateTime.Now;
            drow["ModifiedBy"] = 1;
            dt.Rows.Add(drow);

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
         .Returns(dt);

            //Act
            TaxonomyAssociationFactory objTaxanomyAssociationFactory = new TaxonomyAssociationFactory(mockDataAccess.Object);
            var result = objTaxanomyAssociationFactory.GetAllEntities(1, 2);

            //Assert
            mockDataAccess.VerifyAll();
            List<TaxonomyAssociationObjectModel> lstExpected = new List<TaxonomyAssociationObjectModel>();
            lstExpected.Add(new TaxonomyAssociationObjectModel()
            {
                TaxonomyAssociationId = 1,
                Level = 1,
                TaxonomyId = 1,
                SiteId = 1,
                ParentTaxonomyAssociationId = 1,
                NameOverride = "test",
                DescriptionOverride = "test_Desc",
                CssClass = "test.css",
                MarketId = "1"
            });

            List<string> lstExclude = new List<string>() { "ModifiedBy", "Key", "LastModified" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);

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
            TaxonomyAssociationFactory objFactory = new TaxonomyAssociationFactory(mockDataAccess.Object);
            var result = objFactory.CreateEntity<TaxonomyAssociationObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

        #region SaveEntity_with_TaxonomyAssociationObjectModel
        /// <summary>
        /// SaveEntity_with_TaxonomyAssociationObjectModel
        /// </summary>
        [TestMethod]
        public void SaveEntity_with_TaxonomyAssociationObjectModel()
        {
            //Arrange
            TaxonomyAssociationObjectModel objTaxonomyAssociationObjectModel = new TaxonomyAssociationObjectModel()

            {
                TaxonomyId = 1,
                MarketId = "1",
                TaxonomyAssociationId = 1,
                Level = 1,
                SiteId = 1,
                ParentTaxonomyAssociationId = 1,
                NameOverride = "test",
                DescriptionOverride = "test_Desc",
                CssClass = "test.css",
                
            };
            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
                new SqlParameter(){ ParameterName="TaxonomyAssociationId", Value=1 },
                new SqlParameter(){ ParameterName="Level", Value=1 },
                new SqlParameter(){ ParameterName="TaxonomyId", Value=1 },
                new SqlParameter(){ ParameterName="SiteId", Value=1 },
                new SqlParameter(){ ParameterName="ParentTaxonomyAssociationId", Value=1 },
                new SqlParameter(){ ParameterName="NameOverride", Value="test" },
                new SqlParameter(){ ParameterName="DescriptionOverride", Value="test_Desc" },
                new SqlParameter(){ ParameterName="CssClass", Value="test.css" },
                new SqlParameter(){ ParameterName="MarketId", Value="1" }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4])
                .Returns(parameters[5])
                .Returns(parameters[6])
                .Returns(parameters[7])
                .Returns(parameters[8])
                .Returns(parameters[9]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            TaxonomyAssociationFactory objTaxonomyAssociationFactory = new TaxonomyAssociationFactory(mockDataAccess.Object);
            objTaxonomyAssociationFactory.SaveEntity(objTaxonomyAssociationObjectModel, 1);

            //Assert
            mockDataAccess.Verify();
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
                new SqlParameter(){ ParameterName="DeletedBy", Value=1 },
                new SqlParameter(){ ParameterName="TaxonomyAssociationId", Value=1 }
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            TaxonomyAssociationFactory objTaxanomyAssociationFactory = new TaxonomyAssociationFactory(mockDataAccess.Object);
            objTaxanomyAssociationFactory.DeleteEntity(1);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region DeleteEntity_With_DeletedBy
        ///// <summary>
        ///// DeleteEntity_With_DeletedBy
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_DeletedBy()
        //{
        //    //Arrange

        //    ClientData();

        //    //Act
        //    TaxonomyAssociationFactory objTaxanomyAssociationFactoryCache = new TaxonomyAssociationFactory(mockDataAccess);
        //    objTaxanomyAssociationFactoryCache.ClientName = "Forethought";
        //    // objuserFactoryCache. = FactoryCacheMode.All;

        //    objTaxanomyAssociationFactoryCache.DeleteEntity(1, 32);
        //    //Assert
        //    Mock.Assert(mockDataAccess);
        //}
        //#endregion

        //#region DeleteEntity_With_TaxonomyAssociationObjectModel
        ///// <summary>
        ///// DeleteEntity_With_TaxonomyAssociationObjectModel
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_TaxonomyAssociationObjectModel()
        //{
        //    ClientData();
        //    Exception exe = null; ;
        //    TaxonomyAssociationObjectModel objTaxonomyAssociationObjectModel = new TaxonomyAssociationObjectModel();
        //    objTaxonomyAssociationObjectModel.TaxonomyAssociationId = 1;
        //    objTaxonomyAssociationObjectModel.TaxonomyId = 2;
        //    //Act
        //    TaxonomyAssociationFactory objTaxanomyAssociationFactoryFactory = new TaxonomyAssociationFactory(mockDataAccess);
        //    try
        //    {
        //        objTaxanomyAssociationFactoryFactory.GetEntityByKey(2);
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

        //#region GetEntityByKey
        ///// <summary>
        ///// GetEntityByKey
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey()
        //{
        //    //Arrange              
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    TaxonomyAssociationFactory objTaxanomyAssociationFactoryFactory = new TaxonomyAssociationFactory(mockDataAccess);
        //    try
        //    {
        //        objTaxanomyAssociationFactoryFactory.GetEntityByKey(2);
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
