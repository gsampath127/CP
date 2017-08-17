using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Keys;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Common;

namespace RRD.FSG.RP.Model.Test.Factories.System
{
    /// <summary>
    /// Test class for TemplatePageFeatureFactory class
    /// </summary>
    [TestClass]
    public class TemplatePageFeatureFactoryTests : BaseTestFactory<TemplatePageFeatureObjectModel>
    {

        private Mock<IDataAccess> mockDataAccess;

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

        #region ClientData
        /// <summary>
        /// ClientData
        /// </summary>
        private void ClientData()
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

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dSet);

        }
        #endregion

        #region GetAllEntities_Returns_IEnumerable_Entity
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_Entity
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_Entity()
        {
            //Arrange
            DataTable dt = new DataTable();
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("FeatureKey", typeof(string));
            dt.Columns.Add("FeatureDescription", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["TemplateId"] = 1;
            dtrow["PageId"] = 1;
            dtrow["FeatureKey"] = "RequestMaterial";
            dtrow["FeatureDescription"] = "DescRequestMaterial";
            dt.Rows.Add(dtrow);

            ClientData();

            mockDataAccess.Setup(
                x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);

            //Act
            TemplatePageFeatureFactoryCache objFactoryCache = new TemplatePageFeatureFactoryCache(mockDataAccess.Object)
            {
                ClientName = "Forethought",
                Mode = FactoryCacheMode.All
            };
            var result = objFactoryCache.GetAllEntities(0, 0);
            List<TemplatePageFeatureObjectModel> lstExpected = new List<TemplatePageFeatureObjectModel>
            {
                new TemplatePageFeatureObjectModel
                {
                    TemplateId = 1,
                    PageId = 1,
                    FeatureKey = "RequestMaterial",
                    FeatureDescription = "DescRequestMaterial"
                }
            };
            List<string> lstExclude = new List<string> {"ModifiedBy", "LastModified", "Key"};
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_IEnumerable_Entity_CallsFactory
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_Entity_CallsFactory
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_Entity_CallsFactory()
        {
            //Arrange

            DataTable dt = new DataTable();
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("PageId", typeof(Int32));
            dt.Columns.Add("FeatureKey", typeof(string));
            dt.Columns.Add("FeatureDescription", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["TemplateId"] = 1;
            dtrow["PageId"] = 1;
            dtrow["FeatureKey"] = "RequestMaterial";
            dtrow["FeatureDescription"] = "DescRequestMaterial";
            dt.Rows.Add(dtrow);

            mockDataAccess.Setup(
                x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);
            
            //Act
            TemplatePageFeatureFactory objFactory = new TemplatePageFeatureFactory(mockDataAccess.Object);
            var result = objFactory.GetAllEntities(0, 0, null);
            List<TemplatePageFeatureObjectModel> lstExpected = new List<TemplatePageFeatureObjectModel>
            {
                new TemplatePageFeatureObjectModel
                {
                    TemplateId = 1,
                    PageId = 1,
                    FeatureKey = "RequestMaterial",
                    FeatureDescription = "DescRequestMaterial"
                }
            };
            List<string> lstExclude = new List<string> { "ModifiedBy", "LastModified", "Key" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);

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
            TemplatePageFeatureFactory objTemplatePageFeatureFactory = new TemplatePageFeatureFactory(mockDataAccess.Object);
            var result = objTemplatePageFeatureFactory.CreateEntity<TemplatePageFeatureObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

    }
}
