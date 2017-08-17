using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.Client;
using RRD.FSG.RP.Model.SortDetail.System;
using Moq;
using System.Data.Common;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for TemplateTextFactory class
    /// </summary>
    [TestClass]
    public class TemplateTextTests : BaseTestFactory<TemplateTextObjectModel>
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

        #region GetAllEntities_Returns_Ienumerable
        /// <summary>
        /// GetAllEntities_Returns_Ienumerable
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_Ienumerable()
        {
            //Arrange
            TemplateTextSortDetail objSortDtl = new TemplateTextSortDetail();
            objSortDtl.Column = TemplateTextSortColumn.DefaultText;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("TemplateId", typeof(Int32));
            dt.Columns.Add("ResourceKey", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("IsHtml", typeof(Boolean));
            dt.Columns.Add("DefaultText", typeof(string));
            dt.Columns.Add("Description", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["TemplateId"] = 1;
            dtrow["ResourceKey"] = 26682;
            dtrow["Name"] = null;
            dtrow["IsHtml"] = true;
            dtrow["DefaultText"] = DateTime.Parse("12/14/2015");
            dtrow["Description"] = 1;
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
            TemplateTextFactoryCache objTemplateTextFactoryCache = new TemplateTextFactoryCache(mockDataAccess.Object);
            objTemplateTextFactoryCache.ClientName = "Forethought";
            objTemplateTextFactoryCache.Mode = FactoryCacheMode.All;
            var result = objTemplateTextFactoryCache.GetAllEntities(0, 0, objSortDtl);

            List<TemplateTextObjectModel> lstExpected = new List<TemplateTextObjectModel>();
            lstExpected.Add(new TemplateTextObjectModel()
            {
                DefaultText = DateTime.Parse("12/14/2015").ToString(),
                IsHTML = true,
                ResourceKey = "26682",
                TemplateID = 1,
                Description = "1",
                Name = null

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("LastModified");
            ValidateListData(lstExpected, result.ToList(), lstExclude);
            ValidateKeyData<TemplateTextKey>(lstExpected, result.ToList());
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
            TemplateTextFactory objTemplateTextFactoryCache = new TemplateTextFactory(mockDataAccess.Object);
            var result = objTemplateTextFactoryCache.CreateEntity<TemplateTextObjectModel>(entity);

            //Assert
            Assert.AreEqual(result, null);
            mockDataAccess.VerifyAll();

        }

        #endregion

    }
}
