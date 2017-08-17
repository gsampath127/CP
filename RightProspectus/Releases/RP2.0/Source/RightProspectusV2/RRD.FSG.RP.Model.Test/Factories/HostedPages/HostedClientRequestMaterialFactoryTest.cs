using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.VerticalMarket;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Factories.HostedPages;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.Interfaces.System;
using RRD.FSG.RP.Model.Interfaces.VerticalMarket;
using RRD.FSG.RP.Model.SearchEntities.VerticalMarkets;
using RRD.FSG.RP.Model.SortDetail.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace RRD.FSG.RP.Model.Test.Factories.HostedPages
{    /// <summary>
    /// Test class for  HostedClientRequestMaterialFactory class
    /// </summary>
    [TestClass]
    public class HostedClientRequestMaterialFactoryTest : BaseTestFactory<RequestMaterialPrintHistory>
{
        private Mock<IDataAccess> mockDataAccess;
        private Mock<IHostedVerticalPageScenariosFactory> mockHostedVerticalPageScenariosFactory;
        private Mock<ISystemCommonFactory> mockSystemCommonFactory;
        /// <summary>
        /// TestInitialze
        /// </summary>
        [TestInitialize]
        public void TestInitialze()
        {
            mockDataAccess = new Mock<IDataAccess>();
            mockHostedVerticalPageScenariosFactory = new Mock<IHostedVerticalPageScenariosFactory>();
            mockSystemCommonFactory = new Mock<ISystemCommonFactory>();
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

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dSet);

        }
        #endregion

        #region SaveEmailDetails
        /// <summary>
        ///SaveEmailDetails
        /// </summary>
        [TestMethod]
        public void SaveEmailDetails()
        {
            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData
            {
                TaxonomyID = 2,
                TaxonomyName = "TAL",
                TaxonomyDescriptionOverride = "tst",
                TaxonomyCssClass = "tst",
                IsObjectinVerticalMarket = true,
                TaxonomyAssociationID = 3
            };
            RequestMaterialEmailHistory objRequestMaterialEmailHistory = new RequestMaterialEmailHistory
            {
                RequestMaterialEmailHistoryId = 1,
                RequestDateUtc = DateTime.Now,
                FClickDate = DateTime.Now,
                RecipEmail = "",
                UniqueID = Guid.Empty,
                UserAgent = "",
                IPAddress = "",
                RequestUriString = "",
                Referer = "",
                RequestBatchId = Guid.Empty,
                TaxonomyAssociationData = objTaxonomyAssociationData
            };

            objTaxonomyAssociationData.DocumentTypes = new List<HostedDocumentType>
            {
                new HostedDocumentType
                {
                    DocumentTypeLinkText = "tst",
                    DocumentTypeDescriptionOverride = "tst",
                    DocumentTypeCssClass = "tst",
                    DocumentTypeOrder = 2,
                    DocumentTypeId = 1,
                    IsObjectinVerticalMarket = false,
                    ContentURI = "twst",
                    VerticalMarketID = "tst",
                    SKUName = "tst",
                    DocumentTypeExternalID = null
                }
            };

            ClientData();

            var parameters = new[]
            {
                new SqlParameter {ParameterName = "SiteName", Value = string.Empty},
                new SqlParameter {ParameterName = "RecipEmail", Value = string.Empty},
                new SqlParameter {ParameterName = "UniqueID", Value = null},
                new SqlParameter {ParameterName = "RequestBatchId", Value = null},
                new SqlParameter {ParameterName = "RequestUriString", Value = null},
                new SqlParameter {ParameterName = "UserAgent", Value = null},
                new SqlParameter {ParameterName = "IPAddress", Value = null},
                new SqlParameter {ParameterName = "Referer", Value = null},
                new SqlParameter {ParameterName = "Sent", Value = false},
                new SqlParameter {ParameterName = "RequestMaterialEmailProsDetail", Value = null}
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
                .Returns(parameters[9]);

            mockDataAccess.Setup(
                x =>
                    x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter>(),
                        It.IsAny<DbParameter>(), It.IsAny<DbParameter>(), It.IsAny<DbParameter>(),
                        It.IsAny<DbParameter>(), It.IsAny<DbParameter>(), It.IsAny<DbParameter>(),
                        It.IsAny<DbParameter>(), It.IsAny<DbParameter>(),
                        It.IsAny<DbParameter>()));

            HostedClientRequestMaterialFactory objFactory = new HostedClientRequestMaterialFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            objFactory.SaveEmailDetails("ForeThought", "ForeThought", objRequestMaterialEmailHistory);

            //Assert
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region SavePrintDetails
        /// <summary>
        ///SavePrintDetails
        /// </summary>
        [TestMethod]
        public void SavePrintDetails()
        {
            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData
            {
                TaxonomyID = 2,
                TaxonomyName = "TAL",
                TaxonomyDescriptionOverride = "tst",
                TaxonomyCssClass = "tst",
                IsObjectinVerticalMarket = true,
                TaxonomyAssociationID = 3
            };
            RequestMaterialPrintHistory objRequestMaterialPrintHistory = new RequestMaterialPrintHistory
            {
                TaxonomyAssociationData = objTaxonomyAssociationData
            };

            objTaxonomyAssociationData.DocumentTypes = new List<HostedDocumentType>
            {
                new HostedDocumentType
                {
                    DocumentTypeLinkText = "tst",
                    DocumentTypeDescriptionOverride = "tst",
                    DocumentTypeCssClass = "tst",
                    DocumentTypeOrder = 2,
                    DocumentTypeId = 1,
                    IsObjectinVerticalMarket = false,
                    ContentURI = "twst",
                    VerticalMarketID = "tst",
                    SKUName = "tst",
                    DocumentTypeExternalID = null
                }
            };

            ClientData();

            var parameters = new[]
            {
                new SqlParameter {ParameterName = "SiteName", Value = string.Empty},
                new SqlParameter {ParameterName = "ClientFullName", Value = string.Empty},
                new SqlParameter {ParameterName = "ClientCompanyName", Value = null},
                new SqlParameter {ParameterName = "ClientFirstName", Value = null},
                new SqlParameter {ParameterName = "ClientMiddleName", Value = null},
                new SqlParameter {ParameterName = "ClientLastName", Value = null},
                new SqlParameter {ParameterName = "Address1", Value = null},
                new SqlParameter {ParameterName = "Address2", Value = null},
                new SqlParameter {ParameterName = "StateOrProvince", Value = null},
                new SqlParameter {ParameterName = "PostalCode", Value = null},
                new SqlParameter {ParameterName = "City", Value = null},
                new SqlParameter {ParameterName = "UniqueID", Value = null},
                new SqlParameter {ParameterName = "RequestBatchId", Value = null},
                new SqlParameter {ParameterName = "RequestUriString", Value = null},
                new SqlParameter {ParameterName = "UserAgent", Value = null},
                new SqlParameter {ParameterName = "IPAddress", Value = null},
                new SqlParameter {ParameterName = "Referer", Value = null},
                new SqlParameter {ParameterName = "RequestMaterialEmailProsDetail", Value = null}
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
                .Returns(parameters[10])
                .Returns(parameters[11])
                .Returns(parameters[12])
                .Returns(parameters[13])
                .Returns(parameters[14])
                .Returns(parameters[15])
                .Returns(parameters[16])
                .Returns(parameters[17]);

            mockDataAccess.Setup(
                x =>
                    x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter>(),
                        It.IsAny<DbParameter>(), It.IsAny<DbParameter>(), It.IsAny<DbParameter>(),
                        It.IsAny<DbParameter>(), It.IsAny<DbParameter>(), It.IsAny<DbParameter>(),
                        It.IsAny<DbParameter>(), It.IsAny<DbParameter>(), It.IsAny<DbParameter>(),
                        It.IsAny<DbParameter>(), It.IsAny<DbParameter>(), It.IsAny<DbParameter>(),
                        It.IsAny<DbParameter>(), It.IsAny<DbParameter>(), It.IsAny<DbParameter>(),
                        It.IsAny<DbParameter>(), It.IsAny<DbParameter>()));

            HostedClientRequestMaterialFactory objFactory = new HostedClientRequestMaterialFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            objFactory.SavePrintDetails("ForeThought", "ForeThought", objRequestMaterialPrintHistory);

            //Assert
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region UpdateEmailClickDate_With_Empty_Collection
        /// <summary>
        ///UpdateEmailClickDate_With_Empty_Collection
        /// </summary>
        [TestMethod]
        public void UpdateEmailClickDate_With_Empty_Collection()
        {
            DbParameterCollection obj = null;

            ClientData();

            var parameters = new[]
            {
                new SqlParameter {ParameterName = "UniqueID", Value = null},
                new SqlParameter {ParameterName = "DocumentTypeId", Value = 1},
                new SqlParameter {ParameterName = "TaxonomyAssociationId", Value = 1}
            };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2]);

            mockDataAccess.Setup(
                x =>
                    x.ExecuteNonQueryReturnOutputParams(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter>(),
                        It.IsAny<DbParameter>(), It.IsAny<DbParameter>()));

            HostedClientRequestMaterialFactory objFactory = new HostedClientRequestMaterialFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objFactory.UpdateEmailClickDate("ForeThought", Guid.Empty, 1);
            //assert
            Assert.AreEqual(result, 0);
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region GetRequestMaterialPrintRequests
        /// <summary>
        ///GetRequestMaterialPrintRequests
        /// </summary>
        [TestMethod]
        public void GetRequestMaterialPrintRequests()
        {
            List<RequestMaterialPrintHistory> lstRequestMaterialPrintHistory = new List<RequestMaterialPrintHistory>
            {
                new RequestMaterialPrintHistory
                {
                    ClientFullName = "Forethought"
                }
            };
            ClientData();
            DataTable dt = new DataTable();
            dt.Columns.Add("RequestMaterialPrintHistoryID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("UniqueID", typeof(Guid));
            dt.Columns.Add("RequestDateUtc", typeof(DateTime));
            dt.Columns.Add("ClientCompanyName", typeof(string));
            dt.Columns.Add("ClientFirstName", typeof(string));
            dt.Columns.Add("ClientLastName", typeof(string));
            dt.Columns.Add("Address1", typeof(string));
            dt.Columns.Add("Address2", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("StateOrProvince", typeof(string));
            dt.Columns.Add("PostalCode", typeof(string));
            dt.Columns.Add("Quantity", typeof(Int32));
            dt.Columns.Add("TaxonomyAssociationID", typeof(Int32));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("DocumentTypeLinkText", typeof(string));
            dt.Columns.Add("SKUName", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["RequestMaterialPrintHistoryID"] = 2;
            dtrow["NameOverride"] = "Forethought";
            dtrow["UniqueID"] = Guid.Empty; ;
            dtrow["RequestDateUtc"] = DateTime.Now;
            dtrow["ClientCompanyName"] = "Forethought";
            dtrow["ClientFirstName"] = "forethought";
            dtrow["ClientLastName"] = "forethought";
            dtrow["Address1"] = "forethought";
            dtrow["Address2"] = "forethought";
            dtrow["City"] = "forethought";
            dtrow["StateOrProvince"] = "forethought";
            dtrow["PostalCode"] = "forethought";
            dtrow["Quantity"] = 2;
            dtrow["TaxonomyAssociationID"] = 2;
            dtrow["TaxonomyID"] = 2;
            dtrow["DocumentTypeId"] = 2;
            dtrow["DocumentTypeLinkText"] = "forethought";
            dtrow["SKUName"] = "forethought";
            dt.Rows.Add(dtrow);

            DataRow dtrow1 = dt.NewRow();
            dtrow1["RequestMaterialPrintHistoryID"] = -1;
            dtrow1["NameOverride"] = "Forethought";
            dtrow1["UniqueID"] = Guid.Empty; ;
            dtrow1["RequestDateUtc"] = DateTime.Now;
            dtrow1["ClientCompanyName"] = "Forethought";
            dtrow1["ClientFirstName"] = "forethought";
            dtrow1["ClientLastName"] = "forethought";
            dtrow1["Address1"] = "forethought";
            dtrow1["Address2"] = "forethought";
            dtrow1["City"] = "forethought";
            dtrow1["StateOrProvince"] = "forethought";
            dtrow1["PostalCode"] = "forethought";
            dtrow1["Quantity"] = 2;
            dtrow1["TaxonomyAssociationID"] = 2;
            dtrow1["TaxonomyID"] = 2;
            dtrow1["DocumentTypeId"] = 2;
            dtrow1["DocumentTypeLinkText"] = "forethought";
            dtrow1["SKUName"] = "forethought";
            dt.Rows.Add(dtrow1);

            var parameters = new[]
            {
                new SqlParameter {ParameterName = "ReportFromDate", Value = DateTime.Now},
                new SqlParameter {ParameterName = "ReportToDate", Value = DateTime.Now}
            };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);

            mockDataAccess.Setup(
                x =>
                    x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter>(),
                        It.IsAny<DbParameter>()))
                        .Returns(dt);

            mockHostedVerticalPageScenariosFactory.Setup(
                x =>
                    x.GetRequestMaterialPrintRequests(It.IsAny<DataTable>(), It.IsAny<string>(), It.IsAny<List<RequestMaterialPrintHistory>>()))
                        .Returns(lstRequestMaterialPrintHistory);

            HostedClientRequestMaterialFactory objFactory = new HostedClientRequestMaterialFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objFactory.GetRequestMaterialPrintRequests("ForeThought", DateTime.Now, DateTime.Now);
            List<RequestMaterialPrintHistory> lstExpected = new List<RequestMaterialPrintHistory>
            {
                new RequestMaterialPrintHistory
                {
                    ClientFullName = "Forethought"
                }
            };
            List<string> lstExceptions = new List<string>();
            ValidateListData(lstExpected, result.ToList(), lstExceptions);
            //assert
            Assert.IsInstanceOfType(result, typeof(List<RequestMaterialPrintHistory>));
            mockDataAccess.VerifyAll();
            mockHostedVerticalPageScenariosFactory.VerifyAll();

        }
        #endregion

    }
}
