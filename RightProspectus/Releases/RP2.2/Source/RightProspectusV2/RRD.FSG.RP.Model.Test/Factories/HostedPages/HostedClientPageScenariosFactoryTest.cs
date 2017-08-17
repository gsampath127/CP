
using RRD.DSA.Core.DAL;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using RRD.FSG.RP.Model.Factories.HostedPages;
using Moq;
using RRD.FSG.RP.Model.SortDetail.Client;
using System.Data.SqlClient;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Interfaces.VerticalMarket;
using RRD.FSG.RP.Model.Interfaces.System;
using RRD.FSG.RP.Model.Entities.HostedPages;
using System.Collections.Generic;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Entities.Client;
using System.Data.Common;
using System.Linq;


namespace RRD.FSG.RP.Model.Test.Factories.HostedPages
{

    /// <summary>
    /// Test class for  HostedClientPageScenariosFactory class
    /// </summary>
    [TestClass]
    public class HostedClientPageScenariosFactoryTest : BaseTestFactory<TaxonomyAssociationDocumentsModel>
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

        #region ClientData_And_ClientSiteData

        private void ClientData_And_ClientSiteData()
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

            DataSet dtSet = new DataSet();
            //Table 0
            DataTable dt5 = new DataTable();
            dt5.Columns.Add("IsCurrentProductionVersion", typeof(Int32));
            dt5.Columns.Add("ResourceKey", typeof(string));
            dt5.Columns.Add("Text", typeof(string));
            dt5.Columns.Add("SiteID", typeof(Int32));
            DataRow dtrow5 = dt5.NewRow();
            dtrow5["IsCurrentProductionVersion"] = 2;
            dtrow5["ResourceKey"] = "Forethought";
            dtrow5["Text"] = null;
            dtrow5["SiteID"] = 2;
            dt5.Rows.Add(dtrow5);
            dtSet.Tables.Add(dt5);

            //Table 1
            DataTable dt6 = new DataTable();
            dt6.Columns.Add("IsCurrentProductionVersion", typeof(Int32));
            dt6.Columns.Add("ResourceKey", typeof(string));
            dt6.Columns.Add("Text", typeof(string));
            dt6.Columns.Add("SiteID", typeof(Int32));
            dt6.Columns.Add("PageID", typeof(Int32));

            DataRow dtrow6 = dt6.NewRow();
            dtrow6["IsCurrentProductionVersion"] = 2;
            dtrow6["ResourceKey"] = "Forethought";
            dtrow6["Text"] = null;
            dtrow6["SiteID"] = 2;
            dtrow6["PageID"] = 1;


            dt6.Rows.Add(dtrow6);
            dtSet.Tables.Add(dt6);

            //Table 2
            DataTable dt7 = new DataTable();
            dt7.Columns.Add("SiteID", typeof(Int32));
            dt7.Columns.Add("SiteName", typeof(string));
            dt7.Columns.Add("DefaultPageId", typeof(Int32));
            dt7.Columns.Add("TemplateId", typeof(Int32));
            dt7.Columns.Add("IsDefaultSite", typeof(Int32));

            DataRow dtrow7 = dt7.NewRow();
            dtrow7["SiteID"] = 2;
            dtrow7["SiteName"] = "Forethought";
            dtrow7["DefaultPageId"] = 1;
            dtrow7["TemplateId"] = 1;
            dtrow7["IsDefaultSite"] = 1;


            dt7.Rows.Add(dtrow7);
            dtSet.Tables.Add(dt7);


            //Table 3
            DataTable dt8 = new DataTable();
            dt8.Columns.Add("FileName", typeof(string));
            dt8.Columns.Add("Size", typeof(Int32));
            dt8.Columns.Add("MimeType", typeof(string));
            dt8.Columns.Add("Data", typeof(byte[]));
            dt8.Columns.Add("UtcModifiedDate", typeof(DateTime));

            DataRow dtrow8 = dt8.NewRow();
            dtrow8["FileName"] = 1;
            dtrow8["Size"] = 3;
            dtrow8["MimeType"] = "TaxonomySpecificDocumentFrame_DocumentType";
            dtrow8["Data"] = 2;
            dtrow8["UtcModifiedDate"] = DateTime.Now;
            dt8.Rows.Add(dtrow8);
            dtSet.Tables.Add(dt8);
            //Table 4
            DataTable dt10 = new DataTable();
            dt10.Columns.Add("SiteID", typeof(Int32));
            dt10.Columns.Add("NavigationKey", typeof(string));
            dt10.Columns.Add("NavigationXml", typeof(string));
            dt10.Columns.Add("IsCurrentProductionVersion", typeof(Int32));


            DataRow dtrow10 = dt10.NewRow();
            dtrow10["SiteID"] = 2;
            dtrow10["NavigationKey"] = "tst";
            dtrow10["NavigationXml"] = "tst";
            dtrow10["IsCurrentProductionVersion"] = 1;
            dt10.Rows.Add(dtrow10);
            dtSet.Tables.Add(dt10);

            //Table 5
            DataTable dt11 = new DataTable();
            dt11.Columns.Add("SiteID", typeof(Int32));
            dt11.Columns.Add("PageId", typeof(Int32));
            dt11.Columns.Add("NavigationKey", typeof(string));
            dt11.Columns.Add("NavigationXml", typeof(string));
            dt11.Columns.Add("IsCurrentProductionVersion", typeof(Int32));


            DataRow dtrow11 = dt11.NewRow();
            dtrow11["SiteID"] = 2;
            dtrow11["PageId"] = 1;
            dtrow11["NavigationKey"] = "tst";
            dtrow11["NavigationXml"] = "tst";
            dtrow11["IsCurrentProductionVersion"] = 1;
            dt11.Rows.Add(dtrow11);
            dtSet.Tables.Add(dt11);

            //Table 6
            DataTable dt12 = new DataTable();
            dt12.Columns.Add("SiteID", typeof(Int32));

            dt12.Columns.Add("Key", typeof(string));
            dt12.Columns.Add("FeatureMode", typeof(Int32));


            DataRow dtrow12 = dt12.NewRow();
            dtrow12["SiteID"] = 2;
            dtrow12["Key"] = 1;
            dtrow12["FeatureMode"] = 1;
            dt12.Rows.Add(dtrow12);
            dtSet.Tables.Add(dt12);

            //Table 7
            DataTable dt13 = new DataTable();
            dt13.Columns.Add("SiteID", typeof(Int32));
            dt13.Columns.Add("PageId", typeof(Int32));
            dt13.Columns.Add("Key", typeof(string));
            dt13.Columns.Add("FeatureMode", typeof(Int32));

            DataRow dtrow13 = dt13.NewRow();
            dtrow13["SiteID"] = 2;
            dtrow13["PageId"] = 3;
            dtrow13["Key"] = 1;
            dtrow13["FeatureMode"] = 1;
            dt13.Rows.Add(dtrow13);
            dtSet.Tables.Add(dt13);

            mockDataAccess.SetupSequence(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>()))
                            .Returns(dSet)
                            .Returns(dSet)
                            .Returns(dtSet);
        }

        #endregion

        #region GetTaxonomyAssociationHierarchyDocuments_VerticalData
        /// <summary>
        ///GetTaxonomyAssociationHierarchyDocuments_VerticalData
        /// </summary>
        [TestMethod]
        public void GetTaxonomyAssociationHierarchyDocuments_VerticalData()
        {
            
            TaxonomyAssociationHierarchyModel objTaxonomyAssociationHierarchyModels = new TaxonomyAssociationHierarchyModel
            {
                ParentHeaders = new List<HostedDocumentTypeHeader>
                {
                    new HostedDocumentTypeHeader
                    {
                        HeaderName = "HeadName"
                    }
                },
                ChildHeaders = new List<HostedDocumentTypeHeader>
                {
                    new HostedDocumentTypeHeader
                    {
                        HeaderName = "HeadName"
                    }
                },
                ParentTaxonomyAssociationData = new List<TaxonomyAssociationData>
                {
                    new TaxonomyAssociationData
                    {
                        TaxonomyDescriptionOverride = "TaxonomyDescriptionOverride"
                    }
                },
                ChildTaxonomyAssociationData = new List<TaxonomyAssociationData>
                {
                    new TaxonomyAssociationData
                    {
                        TaxonomyDescriptionOverride = "TaxonomyDescriptionOverride"
                    }
                },
                FootNotes = new List<HostedSiteFootNotes>
                {
                    new HostedSiteFootNotes
                    {
                        Text = "Text"
                    }
                }
            };
            SiteSortDetail objSortDtl = new SiteSortDetail();
            objSortDtl.Column = SiteSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("IsNameOverrideProvided", typeof(bool));
            dt.Columns.Add("DocumentTypeID", typeof(Int32));
            dt.Columns.Add("IsDocumentTypeNameOverrideProvided", typeof(bool));
            dt.Columns.Add("TaxonomyId", typeof(Int32));
            dt.Columns.Add("IsParent", typeof(Int32));
            dt.Columns.Add("FootNoteText", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("TaxonomyAssociationID", typeof(Int32));
            dt.Columns.Add("TaxonomyDescriptionOverride", typeof(string));
            dt.Columns.Add("TaxonomyCssClass", typeof(string));
            dt.Columns.Add("taxonomyNameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("DocumentTypeHeaderText", typeof(string));
            dt.Columns.Add("DocumentTypeOrder", typeof(Int32));
            dt.Columns.Add("DocumentTypeMarketId", typeof(string));
            dt.Columns.Add("DocumentTypeLinkText", typeof(string));
            dt.Columns.Add("DocumentTypeDescriptionOverride", typeof(string));
            dt.Columns.Add("DocumentTypeCssClass", typeof(string));
            DataRow dtrow = dt.NewRow();

            dtrow["IsNameOverrideProvided"] = false;
            dtrow["DocumentTypeID"] = 2;
            dtrow["IsDocumentTypeNameOverrideProvided"] = false;
            dtrow["TaxonomyId"] = 2;
            dtrow["IsParent"] = 1;
            dtrow["FootNoteText"] = "FootnoteText";
            dtrow["FootnoteOrder"] = 0;
            dtrow["NameOverride"] = "FootnoteText";
            dtrow["TaxonomyAssociationID"] = 0;
            dtrow["TaxonomyDescriptionOverride"] = "FootnoteText";
            dtrow["TaxonomyCssClass"] = "FootnoteText";
            dtrow["taxonomyNameOverride"] = "FootnoteText";
            dtrow["DescriptionOverride"] = "FootnoteText";
            dtrow["DocumentTypeHeaderText"] = "FootnoteText";
            dtrow["DocumentTypeOrder"] = 0;
            dtrow["DocumentTypeMarketId"] = "FootnoteText";
            dtrow["DocumentTypeLinkText"] = "FootnoteText";
            dtrow["DocumentTypeDescriptionOverride"] = "FootnoteText";
            dtrow["DocumentTypeCssClass"] = "FootnoteText";
            
            dt.Rows.Add(dtrow);

            //Arrange
            ClientData_And_ClientSiteData();

            var parameters = new[]
            {
                new SqlParameter {ParameterName = "ExternalID", Value = string.Empty},
                new SqlParameter {ParameterName = "SiteName", Value = string.Empty}
            };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);

            mockDataAccess.Setup(
                x =>
                    x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                        .Returns(dt);

            mockHostedVerticalPageScenariosFactory.Setup(
                x =>
                    x.GetTaxonomyAssociationHierarchyDocumentsVerticalData(It.IsAny<DataTable>(), It.IsAny<string>(), It.IsAny<TaxonomyAssociationHierarchyModel>()))
                        .Returns(objTaxonomyAssociationHierarchyModels);

            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            
            var result = objHostedClientPageScenariosFactory.GetTaxonomyAssociationHierarchyDocuments("forethought", "google", "1", 1);
            TaxonomyAssociationHierarchyModel objExpected = new TaxonomyAssociationHierarchyModel
            {
                ParentHeaders = new List<HostedDocumentTypeHeader>
                {
                    new HostedDocumentTypeHeader
                    {
                        HeaderName = "HeadName"
                    }
                },
                ChildHeaders = new List<HostedDocumentTypeHeader>
                {
                    new HostedDocumentTypeHeader
                    {
                        HeaderName = "HeadName"
                    }
                },
                ParentTaxonomyAssociationData = new List<TaxonomyAssociationData>
                {
                    new TaxonomyAssociationData
                    {
                        TaxonomyDescriptionOverride = "TaxonomyDescriptionOverride"
                    }
                },
                ChildTaxonomyAssociationData = new List<TaxonomyAssociationData>
                {
                    new TaxonomyAssociationData
                    {
                        TaxonomyDescriptionOverride = "TaxonomyDescriptionOverride"
                    }
                },
                FootNotes = new List<HostedSiteFootNotes>
                {
                    new HostedSiteFootNotes
                    {
                        Text = "Text"
                    }
                }
            };
            ValidateObjectModelData<TaxonomyAssociationHierarchyModel>(result, objExpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomyAssociationHierarchyDocuments_With_Empty_ExternalId
        /// <summary>
        ///GetTaxonomyAssociationHierarchyDocuments_With_Empty_ExternalId
        /// </summary>
        [TestMethod]
        public void GetTaxonomyAssociationHierarchyDocuments_With_Empty_ExternalId()
        {
            SiteSortDetail objSortDtl = new SiteSortDetail();
            objSortDtl.Column = SiteSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable dt = new DataTable();
            dt.Columns.Add("IsNameOverrideProvided", typeof(bool));
            dt.Columns.Add("DocumentTypeID", typeof(Int32));
            dt.Columns.Add("IsDocumentTypeNameOverrideProvided", typeof(bool));
            dt.Columns.Add("TaxonomyId", typeof(Int32));
            dt.Columns.Add("IsParent", typeof(Int32));
            dt.Columns.Add("FootNoteText", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("TaxonomyAssociationID", typeof(Int32));
            dt.Columns.Add("TaxonomyDescriptionOverride", typeof(string));
            dt.Columns.Add("TaxonomyCssClass", typeof(string));
            dt.Columns.Add("taxonomyNameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("DocumentTypeHeaderText", typeof(string));
            dt.Columns.Add("DocumentTypeOrder", typeof(Int32));
            dt.Columns.Add("DocumentTypeMarketId", typeof(string));
            dt.Columns.Add("DocumentTypeLinkText", typeof(string));
            dt.Columns.Add("DocumentTypeDescriptionOverride", typeof(string));
            dt.Columns.Add("DocumentTypeCssClass", typeof(string));
            DataRow dtrow = dt.NewRow();

            dtrow["IsNameOverrideProvided"] = false;
            dtrow["DocumentTypeID"] = 2;
            dtrow["IsDocumentTypeNameOverrideProvided"] = false;
            dtrow["TaxonomyId"] = 2;
            dtrow["IsParent"] = 2;
            dtrow["FootNoteText"] = "";
            dtrow["FootnoteOrder"] = 0;
            dtrow["NameOverride"] = "FootnoteText";
            dtrow["TaxonomyAssociationID"] = 0;
            dtrow["TaxonomyDescriptionOverride"] = "FootnoteText";
            dtrow["TaxonomyCssClass"] = "FootnoteText";
            dtrow["taxonomyNameOverride"] = "FootnoteText";
            dtrow["DescriptionOverride"] = "FootnoteText";
            dtrow["DocumentTypeHeaderText"] = "";
            dtrow["DocumentTypeOrder"] = 0;
            dtrow["DocumentTypeMarketId"] = "FootnoteText";
            dtrow["DocumentTypeLinkText"] = "FootnoteText";
            dtrow["DocumentTypeDescriptionOverride"] = "FootnoteText";
            dtrow["DocumentTypeCssClass"] = "FootnoteText";

            dt.Rows.Add(dtrow);

            //Arrange
            ClientData_And_ClientSiteData();

            var parameters = new[]
            {
                new SqlParameter {ParameterName = "ExternalID", Value = string.Empty},
                new SqlParameter {ParameterName = "SiteName", Value = string.Empty}
            };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);

            mockDataAccess.Setup(
                x =>
                    x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                        .Returns(dt);

            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);

            var result = objHostedClientPageScenariosFactory.GetTaxonomyAssociationHierarchyDocuments("forethought", "google", "", 1);

            //Assert
            Assert.AreEqual(null, result);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomyAssociationDocuments
        /// <summary>
        ///GGetTaxonomyAssociationDocument
        /// </summary>
        [TestMethod]
        public void GetTaxonomyAssociationDocument()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TaxonomyId", typeof(Int32));
            dt.Columns.Add("FootNoteText", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("TaxonomyAssociationID", typeof(Int32));
            dt.Columns.Add("TaxonomyCssClass", typeof(string));
            dt.Columns.Add("taxonomyNameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("DocumentTypeHeaderText", typeof(string));
            dt.Columns.Add("DocumentTypeOrder", typeof(Int32));
            dt.Columns.Add("DocumentTypeMarketId", typeof(string));
            dt.Columns.Add("DocumentTypeLinkText", typeof(string));
            dt.Columns.Add("DocumentTypeDescriptionOverride", typeof(string));
            dt.Columns.Add("DocumentTypeCssClass", typeof(string));
            DataRow dtrow = dt.NewRow();

            dtrow["TaxonomyId"] = 2;
            dtrow["FootNoteText"] = "FootnoteText";
            dtrow["FootnoteOrder"] = 0;
            dtrow["NameOverride"] = "FootnoteText";
            dtrow["TaxonomyAssociationID"] = 0;
            dtrow["TaxonomyCssClass"] = "FootnoteText";
            dtrow["taxonomyNameOverride"] = "FootnoteText";
            dtrow["DescriptionOverride"] = "FootnoteText";
            dtrow["DocumentTypeId"] = 2;
            dtrow["DocumentTypeHeaderText"] = "FootnoteText";
            dtrow["DocumentTypeOrder"] = 0;
            dtrow["DocumentTypeMarketId"] = "FootnoteText";
            dtrow["DocumentTypeLinkText"] = "FootnoteText";
            dtrow["DocumentTypeDescriptionOverride"] = "FootnoteText";
            dtrow["DocumentTypeCssClass"] = "FootnoteText";

            dt.Rows.Add(dtrow);

            ClientData_And_ClientSiteData();

            TaxonomyAssociationDocumentsModel objTaxonomyAssociationDocumentsModel = new TaxonomyAssociationDocumentsModel
            {
                DocumentTypeHeaders = new List<HostedDocumentTypeHeader>
                {
                    new HostedDocumentTypeHeader
                    {
                        HeaderName = "HeaderName"
                    }
                },
                TaxonomyAssociationDocumentsData = new List<TaxonomyAssociationData>
                {
                    new TaxonomyAssociationData
                    {
                        TaxonomyDescriptionOverride = "TaxonomyDescriptionOverride"
                    }
                },
                FootNotes = new List<HostedSiteFootNotes>
                {
                    new HostedSiteFootNotes
                    {
                        Text = "Text"
                    }
                }
            };

            var parameters = new[]
            {
                new SqlParameter {ParameterName = "SiteName", Value = string.Empty}
            };

            mockDataAccess.Setup(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<string>()))
                .Returns(parameters[0]);

            mockDataAccess.Setup(
                x =>
                    x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                        .Returns(dt);

            mockHostedVerticalPageScenariosFactory.Setup(
                x =>
                    x.GetTaxonomyAssociationDocumentsVerticalData(It.IsAny<DataTable>(), It.IsAny<string>(), It.IsAny<TaxonomyAssociationDocumentsModel>()))
                        .Returns(objTaxonomyAssociationDocumentsModel);

            HostedClientPageScenariosFactory objSiteFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);

            var result = objSiteFactory.GetTaxonomyAssociationDocuments("Forethought", "Forethought");
            TaxonomyAssociationDocumentsModel objExpected = new TaxonomyAssociationDocumentsModel
            {
                DocumentTypeHeaders = new List<HostedDocumentTypeHeader>
                {
                    new HostedDocumentTypeHeader
                    {
                        HeaderName = "HeaderName"
                    }
                },
                TaxonomyAssociationDocumentsData = new List<TaxonomyAssociationData>
                {
                    new TaxonomyAssociationData
                    {
                        TaxonomyDescriptionOverride = "TaxonomyDescriptionOverride"
                    }
                },
                FootNotes = new List<HostedSiteFootNotes>
                {
                    new HostedSiteFootNotes
                    {
                        Text = "Text"
                    }
                }
            };
            ValidateObjectModelData(result,objExpected);
            //Assert
            mockDataAccess.VerifyAll();
            mockHostedVerticalPageScenariosFactory.VerifyAll();
        }
        #endregion

        #region GetTaxonomyAssociationDocument_With_Empty_FootNote
        /// <summary>
        ///GetTaxonomyAssociationDocument_With_Empty_FootNote
        /// </summary>
        [TestMethod]
        public void GetTaxonomyAssociationDocument_With_Empty_FootNote()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TaxonomyId", typeof(Int32));
            dt.Columns.Add("FootNoteText", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("TaxonomyAssociationID", typeof(Int32));
            dt.Columns.Add("TaxonomyCssClass", typeof(string));
            dt.Columns.Add("taxonomyNameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("DocumentTypeHeaderText", typeof(string));
            dt.Columns.Add("DocumentTypeOrder", typeof(Int32));
            dt.Columns.Add("DocumentTypeMarketId", typeof(string));
            dt.Columns.Add("DocumentTypeLinkText", typeof(string));
            dt.Columns.Add("DocumentTypeDescriptionOverride", typeof(string));
            dt.Columns.Add("DocumentTypeCssClass", typeof(string));
            DataRow dtrow = dt.NewRow();

            dtrow["TaxonomyId"] = 2;
            dtrow["FootNoteText"] = "";
            dtrow["FootnoteOrder"] = 0;
            dtrow["NameOverride"] = "FootnoteText";
            dtrow["TaxonomyAssociationID"] = 0;
            dtrow["TaxonomyCssClass"] = "FootnoteText";
            dtrow["taxonomyNameOverride"] = "FootnoteText";
            dtrow["DescriptionOverride"] = "FootnoteText";
            dtrow["DocumentTypeId"] = 2;
            dtrow["DocumentTypeHeaderText"] = "FootnoteText";
            dtrow["DocumentTypeOrder"] = 0;
            dtrow["DocumentTypeMarketId"] = "FootnoteText";
            dtrow["DocumentTypeLinkText"] = "FootnoteText";
            dtrow["DocumentTypeDescriptionOverride"] = "FootnoteText";
            dtrow["DocumentTypeCssClass"] = "FootnoteText";

            dt.Rows.Add(dtrow);

            ClientData_And_ClientSiteData();

            TaxonomyAssociationDocumentsModel objTaxonomyAssociationDocumentsModel = new TaxonomyAssociationDocumentsModel
            {
                DocumentTypeHeaders = new List<HostedDocumentTypeHeader>
                {
                    new HostedDocumentTypeHeader
                    {
                        HeaderName = "HeaderName"
                    }
                },
                TaxonomyAssociationDocumentsData = new List<TaxonomyAssociationData>
                {
                    new TaxonomyAssociationData
                    {
                        TaxonomyDescriptionOverride = "TaxonomyDescriptionOverride"
                    }
                },
                FootNotes = new List<HostedSiteFootNotes>
                {
                    new HostedSiteFootNotes
                    {
                        Text = "Text"
                    }
                }
            };

            var parameters = new[]
            {
                new SqlParameter {ParameterName = "SiteName", Value = string.Empty}
            };

            mockDataAccess.Setup(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<string>()))
                .Returns(parameters[0]);

            mockDataAccess.Setup(
                x =>
                    x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                        .Returns(dt);

            mockHostedVerticalPageScenariosFactory.Setup(
                x =>
                    x.GetTaxonomyAssociationDocumentsVerticalData(It.IsAny<DataTable>(), It.IsAny<string>(), It.IsAny<TaxonomyAssociationDocumentsModel>()))
                        .Returns(objTaxonomyAssociationDocumentsModel);

            HostedClientPageScenariosFactory objSiteFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);

            var result = objSiteFactory.GetTaxonomyAssociationDocuments("Forethought", "Forethought");
            TaxonomyAssociationDocumentsModel objExpected = new TaxonomyAssociationDocumentsModel
            {
                DocumentTypeHeaders = new List<HostedDocumentTypeHeader>
                {
                    new HostedDocumentTypeHeader
                    {
                        HeaderName = "HeaderName"
                    }
                },
                TaxonomyAssociationDocumentsData = new List<TaxonomyAssociationData>
                {
                    new TaxonomyAssociationData
                    {
                        TaxonomyDescriptionOverride = "TaxonomyDescriptionOverride"
                    }
                },
                FootNotes = new List<HostedSiteFootNotes>
                {
                    new HostedSiteFootNotes
                    {
                        Text = "Text"
                    }
                }
            };
            ValidateObjectModelData(result, objExpected);
            //Assert
            mockDataAccess.VerifyAll();
            mockHostedVerticalPageScenariosFactory.VerifyAll();
        }
        #endregion

        #region GetTaxonomySpecificDocumentsreturn_list
        /// <summary>
        ///GetTaxonomySpecificDocuments_return_list
        /// </summary>
        [TestMethod]
        public void GetTaxonomySpecificDocuments_return_list()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("TaxonomyAssociationID", typeof(Int32));
            dt.Columns.Add("TaxonomyNameOverRide", typeof(string));
            dt.Columns.Add("TaxonomyDesciptionOverRide", typeof(string));
            dt.Columns.Add("TaxonomyCssClass", typeof(string));
            dt.Columns.Add("DocumentTypeHeaderText", typeof(string));
            dt.Columns.Add("DocumentTypeNameOverride", typeof(string));
            dt.Columns.Add("DocumentTypeDescriptionOverride", typeof(string));
            dt.Columns.Add("DocumentTypeCssClass", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("DocumentTypeOrder", typeof(Int32));
            dt.Columns.Add("DocumentTypeExternalID", typeof(string));

            DataRow dtrow = dt.NewRow();
            dtrow["TaxonomyID"] = 1;
            dtrow["TaxonomyAssociationID"] = 1;
            dtrow["TaxonomyNameOverRide"] = "Forethought";
            dtrow["TaxonomyDesciptionOverRide"] = "Forethought";
            dtrow["TaxonomyCssClass"] = "Forethought";
            dtrow["DocumentTypeHeaderText"] = "Forethought";
            dtrow["DocumentTypeNameOverride"] = "Forethought";
            dtrow["DocumentTypeDescriptionOverride"] = "Forethought";
            dtrow["DocumentTypeCssClass"] = "Forethought";
            dtrow["DocumentTypeId"] = 1;
            dtrow["DocumentTypeOrder"] = 1;
            dtrow["DocumentTypeExternalID"] = "Forethought";
            dt.Rows.Add(dtrow);

            //Arrange
            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData
            {
                TaxonomyID = 2,
                TaxonomyName = "TAL",
                TaxonomyDescriptionOverride = "tst",
                TaxonomyCssClass = "tst",
                IsObjectinVerticalMarket = true,
                TaxonomyAssociationID = 3
            };

            ClientData_And_ClientSiteData();

            var parameters = new[]
            {
                new SqlParameter {ParameterName = "TAID", Value = 1},
                new SqlParameter {ParameterName = "SiteName", Value = string.Empty}
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
                    x.GetTaxonomySpecificDocumentsVerticalData(It.IsAny<DataTable>(), It.IsAny<string>(), It.IsAny<TaxonomyAssociationData>()))
                        .Returns(objTaxonomyAssociationData);

            HostedClientPageScenariosFactory objSiteFactoryCache = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);

            var result = objSiteFactoryCache.GetTaxonomySpecificDocuments("Forethought", "Forethought", "", 6);

            List<TaxonomyAssociationData> lstExpected = new List<TaxonomyAssociationData>
            {
                new TaxonomyAssociationData
                {
                    TaxonomyID = 2,
                    TaxonomyName = "TAL",
                    TaxonomyDescriptionOverride = "tst",
                    TaxonomyCssClass = "tst",
                    IsObjectinVerticalMarket = true,
                    TaxonomyAssociationID = 3
                }
            };
            List<TaxonomyAssociationData> lstResult = new List<TaxonomyAssociationData> { result };
            List<string> lstExceptions = new List<string>();
            ValidateListData<TaxonomyAssociationData>(lstExpected, lstResult, lstExceptions);

            //Assert
            mockDataAccess.VerifyAll();
            mockHostedVerticalPageScenariosFactory.VerifyAll();
        }
        #endregion

        #region GetStaticResourcesFromCache_return_list
        /// <summary>
        ///GetStaticResourcesFromCache_return_list
        /// </summary>
        [TestMethod]
        public void GetStaticResourcesFromCache_return_list()
        {

            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetStaticResourcesFromCache("Forethought");
            List<HostedStaticResource> lstExpected = new List<HostedStaticResource>
            {
                new HostedStaticResource
                {
                    FileName = "1",
                    Size = 3,
                    MimeType = "TaxonomySpecificDocumentFrame_DocumentType"
                }
            };
            List<string> lstExceptions = new List<string>
            {
                "UtcModifiedDate",
                "Data"
            };
            ValidateListData<HostedStaticResource>(lstExpected, result.ToList(), lstExceptions);
            
            //Assert
            mockDataAccess.VerifyAll();
            
        }
        #endregion

        #region GetStaticResourcesFromCache_With_Empty_ClientSiteData_return_list
        /// <summary>
        ///GetStaticResourcesFromCache_With_Empty_ClientSiteData_return_list
        /// </summary>
        [TestMethod]
        public void GetStaticResourcesFromCache_With_Empty_ClientSiteData_return_list()
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

            mockDataAccess.SetupSequence(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>()))
                            .Returns(dSet)
                            .Returns(dSet)
                            .Returns(null);

            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            
            var result = objHostedClientPageScenariosFactory.GetStaticResourcesFromCache("Forethought");
            
            //Assert
            Assert.AreEqual(result,null);
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region GetSiteTextFromCache_return_list
        /// <summary>
        ///GetSitesFromCache_return_list
        /// </summary>
        [TestMethod]
        public void GetSiteTextFromCache_return_list()
        {

            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetSiteTextFromCache("Forethought", "Forethought");

            List<HostedSiteText> lstExpected = new List<HostedSiteText>
            {
                new HostedSiteText
                {
                    SiteID = 2,
                    ResourceKey = "Forethought"
                }
            };
            List<string> lstExceptions = new List<string>();
            ValidateListData<HostedSiteText>(lstExpected, result.ToList(), lstExceptions);

            //Assert
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region GetSiteTextFromCache_With_Empty_Sitename_return_list
        /// <summary>
        ///GetSiteTextFromCache_With_Empty_Sitename_return_list
        /// </summary>
        [TestMethod]
        public void GetSiteTextFromCache_With_Empty_Sitename_return_list()
        {

            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetSiteTextFromCache("Forethought", "");

            List<HostedSiteText> lstExpected = new List<HostedSiteText>
            {
                new HostedSiteText
                {
                    SiteID = 2,
                    ResourceKey = "Forethought"
                }
            };
            List<string> lstExceptions = new List<string>();
            ValidateListData<HostedSiteText>(lstExpected, result.ToList(), lstExceptions);

            //Assert
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region GetPageTextFromCache_return_list
        /// <summary>
        ///GetPageTextFromCache_return_list
        /// </summary>
        [TestMethod]
        public void GetPageTextFromCache_return_list()
        {

            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetPageTextFromCache("Forethought", "Forethought", 1);
            List<HostedPageText> lstExpected = new List<HostedPageText>
            {
                new HostedPageText
                {
                    PageID = 1,
                    SiteID = 2,
                    ResourceKey = "Forethought"
                }
            };
            List<string> lstExceptions = new List<string>();
            ValidateListData<HostedPageText>(lstExpected, result.ToList(), lstExceptions);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetPageTextFromCache_With_Diff_PageId_return_list
        /// <summary>
        ///GetPageTextFromCache_With_Diff_PageId_return_list
        /// </summary>
        [TestMethod]
        public void GetPageTextFromCache_With_Diff_PageId_return_list()
        {

            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetPageTextFromCache("Forethought", "Forethought", 2);
            ValidateEmptyData<HostedPageText>(result);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetPageTextFromCache_With_Empty_Sitename_return_list
        /// <summary>
        ///GetPageTextFromCache_With_Empty_Sitename_return_list
        /// </summary>
        [TestMethod]
        public void GetPageTextFromCache_With_Empty_Sitename_return_list()
        {

            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetPageTextFromCache("Forethought", "", 1);
            List<HostedPageText> lstExpected = new List<HostedPageText>
            {
                new HostedPageText
                {
                    PageID = 1,
                    SiteID = 2,
                    ResourceKey = "Forethought"
                }
            };
            List<string> lstExceptions = new List<string>();
            ValidateListData<HostedPageText>(lstExpected, result.ToList(), lstExceptions);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetSitesFromCachereturn_list
        /// <summary>
        ///GetSitesFromCache_return_list
        /// </summary>
        [TestMethod]
        public void GetSitesFromCache_return_list()
        {
            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetSitesFromCache("Forethought");
            List<HostedSite> lstExpected = new List<HostedSite>
            {
                new HostedSite
                {
                    TemplateId = 1,
                    DefaultPageId = 1,
                    SiteId = 2,
                    SiteName = "Forethought",
                    IsDefaultSite = true
                }
            };
            List<string> lstExceptions = new List<string>{"UtcModifiedDate"};
            ValidateListData<HostedSite>(lstExpected, result.ToList(), lstExceptions);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetCurrentSiteInforeturn_list
        /// <summary>
        ///GetCurrentSiteInfo_return_list
        /// </summary>
        [TestMethod]
        public void GetCurrentSiteInfo_return_list()
        {
            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetCurrentSiteInfo("Forethought", "Forethought");
            List<HostedSite> lstExpected = new List<HostedSite>
            {
                new HostedSite
                {
                    TemplateId = 1,
                    DefaultPageId = 1,
                    SiteId = 2,
                    SiteName = "Forethought",
                    IsDefaultSite = true
                }
            };
            List<HostedSite> lstResult = new List<HostedSite> {result};
            List<string> lstExceptions = new List<string> { "UtcModifiedDate" };
            ValidateListData<HostedSite>(lstExpected, lstResult, lstExceptions);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetCurrentSiteInfo_With_Empty_Sitename_return_list
        /// <summary>
        ///GetCurrentSiteInfo_With_Empty_Sitename_return_list
        /// </summary>
        [TestMethod]
        public void GetCurrentSiteInfo_With_Empty_Sitename_return_list()
        {
            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetCurrentSiteInfo("Forethought", "");
            List<HostedSite> lstExpected = new List<HostedSite>
            {
                new HostedSite
                {
                    TemplateId = 1,
                    DefaultPageId = 1,
                    SiteId = 2,
                    SiteName = "Forethought",
                    IsDefaultSite = true
                }
            };
            List<HostedSite> lstResult = new List<HostedSite> { result };
            List<string> lstExceptions = new List<string> { "UtcModifiedDate" };
            ValidateListData<HostedSite>(lstExpected, lstResult, lstExceptions);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetCurrentPageNamereturn_string
        /// <summary>
        ///GetCurrentPageName_return_list
        /// </summary>
        [TestMethod]
        public void GetCurrentPageName_return_list()
        {
            ClientDataFromSystem objClientDataFromSystem = new ClientDataFromSystem
            {
                TemplatePages = new List<HostedTemplatePage>
                {
                    new HostedTemplatePage
                    {
                        TemplateID = 1,
                        TemplateName = "Forethought",
                        PageID = 1,
                        PageName = "Forethought"
                    }
                }
            };
            ClientData_And_ClientSiteData();
            mockSystemCommonFactory.Setup(x => x.GetClientsDataFromCache())
                .Returns(objClientDataFromSystem);
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetCurrentPageName("Forethought", "Forethought");
            
            //Assert
            Assert.AreEqual("Forethought", result);
            mockDataAccess.VerifyAll();
            mockSystemCommonFactory.VerifyAll();
        }
        #endregion

        #region GetPageIDForPageNamereturn_int
        /// <summary>
        ///GetPageIDForPageName_return_int
        /// </summary>
        [TestMethod]
        public void GetPageIDForPageName_return_int()
        {
            ClientDataFromSystem objClientDataFromSystem = new ClientDataFromSystem
            {
                TemplatePages = new List<HostedTemplatePage>
                {
                    new HostedTemplatePage
                    {
                        TemplateID = 1,
                        TemplateName = "Forethought",
                        PageID = 1,
                        PageName = "Forethought"
                    }
                }
            };
            ClientData_And_ClientSiteData();
            mockSystemCommonFactory.Setup(x => x.GetClientsDataFromCache())
                .Returns(objClientDataFromSystem);
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetPageIDForPageName("Forethought");

            //Assert
            Assert.AreEqual(1, result);
            mockDataAccess.VerifyAll();
            mockSystemCommonFactory.VerifyAll();
        }
        #endregion

        #region GetTemplateNamereturn_string
        /// <summary>
        ///GetTemplateName_return_string
        /// </summary>
        [TestMethod]
        public void GetTemplateName_return_string()
        {
            ClientDataFromSystem objClientDataFromSystem = new ClientDataFromSystem
            {
                TemplatePages = new List<HostedTemplatePage>
                {
                    new HostedTemplatePage
                    {
                        TemplateID = 1,
                        TemplateName = "ForethoughtTemplate",
                        PageID = 1,
                        PageName = "Forethought"
                    }
                }
            };
            ClientData_And_ClientSiteData();
            mockSystemCommonFactory.Setup(x => x.GetClientsDataFromCache())
                .Returns(objClientDataFromSystem);
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetTemplateName("Forethought", "Forethought");

            //Assert
            Assert.AreEqual("ForethoughtTemplate", result);
            mockDataAccess.VerifyAll();
            mockSystemCommonFactory.VerifyAll();
        }
        #endregion

        #region GetTaxonomyAssociationLinksreturn_list
        /// <summary>
        ///GetTaxonomyAssociationLinks_return_list
        /// </summary>
        [TestMethod]
        public void GetTaxonomyAssociationLinks_return_list()
        {
            List<TaxonomyAssociationLinkModel> lstTaxonomyAssociationLinkModels = new List<TaxonomyAssociationLinkModel>
            {
                new TaxonomyAssociationLinkModel
                {
                    ParentTaxonomyAssociaitonID = 1,
                    TaxonomyID = 1,
                    TaxonomyDescriptionOverride = "Forethought"
                }
            };
            ClientData_And_ClientSiteData();
            DataTable dt = new DataTable();
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            DataRow dtrow = dt.NewRow();
            dtrow["ParentTaxonomyAssociationId"] = 1;
            dtrow["TaxonomyID"] = 1;
            dtrow["NameOverride"] = "Forethought";
            dtrow["DescriptionOverride"] = "Forethought";
            dt.Rows.Add(dtrow);
            var parameters = new[]
            {
                new SqlParameter {ParameterName = "SiteName", Value = string.Empty}
            };

            mockDataAccess.Setup(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<string>()))
                .Returns(parameters[0]);
            mockDataAccess.Setup(
                x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                .Returns(dt);
            mockHostedVerticalPageScenariosFactory.Setup(
                x =>
                    x.GetTaxonomyAssociationLinksVerticalData(It.IsAny<DataTable>(), It.IsAny<string>(), It.IsAny<List<TaxonomyAssociationLinkModel>>()))
                        .Returns(lstTaxonomyAssociationLinkModels);
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetTaxonomyAssociationLinks("Forethought", "Forethought");
            List<TaxonomyAssociationLinkModel> lstExpected = new List<TaxonomyAssociationLinkModel>
            {
                new TaxonomyAssociationLinkModel
                {
                    ParentTaxonomyAssociaitonID = 1,
                    TaxonomyID = 1,
                    TaxonomyDescriptionOverride = "Forethought"
                }
            };
            List<string> lstExceptions = new List<string>();
            ValidateListData<TaxonomyAssociationLinkModel>(lstExpected, result.ToList(), lstExceptions);
            //Assert
            mockDataAccess.VerifyAll();
            mockHostedVerticalPageScenariosFactory.VerifyAll();
        }
        #endregion

        #region GetPageFeatureModeFromCachesreturn_int
        /// <summary>
        ///GetPageFeatureModeFromCache_return_int
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModeFromCache_return_int()
        {

            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetPageFeatureModeFromCache("Forethought", "Forethought", 1, "");

            //Assert
            Assert.AreEqual(0, result);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetPageFeatureModeFromCachesreturn_With_Empty_Sitename_int
        /// <summary>
        ///GetPageFeatureModeFromCachesreturn_With_Empty_Sitename_int
        /// </summary>
        [TestMethod]
        public void GetPageFeatureModeFromCachesreturn_With_Empty_Sitename_int()
        {

            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetPageFeatureModeFromCache("Forethought", "", 3, "1");

            //Assert
            Assert.AreEqual(1, result);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetSiteFeatureModeFromCache_return_int
        /// <summary>
        ///GetSiteFeatureModeFromCache_return_int
        /// </summary>
        [TestMethod]
        public void GetSiteFeatureModeFromCache_return_int()
        {

            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetSiteFeatureModeFromCache("Forethought", "Forethought", "");
            //Assert
            Assert.AreEqual(0, result);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetSiteFeatureModeFromCache_With_Empty_Sitename_return_int
        /// <summary>
        ///GetSiteFeatureModeFromCache_With_Empty_Sitename_return_int
        /// </summary>
        [TestMethod]
        public void GetSiteFeatureModeFromCache_With_Empty_Sitename_return_int()
        {

            ClientData_And_ClientSiteData();
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetSiteFeatureModeFromCache("Forethought", "", "1");
            //Assert
            Assert.AreEqual(1, result);
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetXBRLByTaxonomyAssociationIDOrExternalIDreturn_list
        /// <summary>
        ///GetXBRLByTaxonomyAssociationIDOrExternalID_return_list
        /// </summary>
        [TestMethod]
        public void GetXBRLByTaxonomyAssociationIDOrExternalID_return_list()
        {
            List<XBRLObjectModel> lstXbrlObjectModels = new List<XBRLObjectModel>
            {
                new XBRLObjectModel
                {
                    TaxonomyId = 2
                }
            };
            
            DataTable dt = new DataTable();
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            DataRow dtrow = dt.NewRow();
            dtrow["TaxonomyID"] = 2;
            dtrow["NameOverride"] = "tst";
            dt.Rows.Add(dtrow);
            //Arrange
            ClientData_And_ClientSiteData();
            var parameters = new[]
            {
                new SqlParameter {ParameterName = "TAID", Value = 2},
                new SqlParameter {ParameterName = "ExternalID", Value = string.Empty},
                new SqlParameter {ParameterName = "Level", Value = 1}
            };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<string>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2]);

            mockDataAccess.Setup(
                x =>
                    x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                        .Returns(dt);

            mockHostedVerticalPageScenariosFactory.Setup(
                x => x.GetXBRLDetailsForTaxonomyID(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(lstXbrlObjectModels);
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetXBRLByTaxonomyAssociationIDOrExternalID("forethought", 2, "tst", false);
            List<XBRLObjectModel> lstExpected = new List<XBRLObjectModel>
            {
                new XBRLObjectModel
                {
                    TaxonomyId = 2
                }
            };
            List<string> lstExceptions = new List<string>();
            ValidateListData<XBRLObjectModel>(lstExpected, result.ToList(), lstExceptions);
            //assert
            
            mockDataAccess.VerifyAll();
            mockHostedVerticalPageScenariosFactory.VerifyAll();

        }
        #endregion

        #region GetXBRLByTaxonomyAssociationIDOrExternalID_With_PID_True_return_list
        /// <summary>
        ///GetXBRLByTaxonomyAssociationIDOrExternalID_With_PID_True_return_list
        /// </summary>
        [TestMethod]
        public void GetXBRLByTaxonomyAssociationIDOrExternalID_With_PID_True_return_list()
        {
            List<XBRLObjectModel> lstXbrlObjectModels = new List<XBRLObjectModel>
            {
                new XBRLObjectModel
                {
                    TaxonomyId = 2
                }
            };

            mockHostedVerticalPageScenariosFactory.Setup(
                x => x.GetXBRLDetailsForTaxonomyID(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(lstXbrlObjectModels);
            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetXBRLByTaxonomyAssociationIDOrExternalID("forethought", 2, "tst", true);
            result = objHostedClientPageScenariosFactory.GetXBRLByTaxonomyAssociationIDOrExternalID("forethought", 2, "1", true);
            //assert
            List<XBRLObjectModel> lstExpected = new List<XBRLObjectModel>
            {
                new XBRLObjectModel
                {
                    TaxonomyId = 2
                }
            };
            List<string> lstExceptions = new List<string>();
            ValidateListData<XBRLObjectModel>(lstExpected, result.ToList(), lstExceptions);
            mockDataAccess.VerifyAll();
            mockHostedVerticalPageScenariosFactory.VerifyAll();

        }
        #endregion

        #region GetPageNavigationFromCachereturn_tuple
        /// <summary>
        ///GetPageFeatureModeFromCache_return_tuple
        /// </summary>
        [TestMethod]
        public void GetPageNavigationFromCache_return_tuple()
        {
            ClientData_And_ClientSiteData();
            ClientDataFromSystem objClientDataFromSystem = new ClientDataFromSystem
            {
                TemplatePages = new List<HostedTemplatePage>
                {
                    new HostedTemplatePage
                    {
                        TemplateID = 1,
                        TemplateName = "Forethought",
                        PageID = 1,
                        PageName = "Forethought"
                    }
                },
                HostedTemplatePageNavigations = new List<HostedTemplatePageNavigation>
                {
                    new HostedTemplatePageNavigation
                    {
                        TemplateID = 1,
                        PageID = 1,
                        NavigationKey = "tst"
                    }
                }
            };
            ClientData_And_ClientSiteData();
            mockSystemCommonFactory.Setup(x => x.GetClientsDataFromCache())
                .Returns(objClientDataFromSystem);

            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetPageNavigationFromCache("Forethought", "Forethought", "tst", false, 1);
            //Assert
            Tuple<string,string> tplExpected = new Tuple<string, string>("tst",null);
            Assert.AreEqual(tplExpected, result);
            mockDataAccess.VerifyAll();
            mockSystemCommonFactory.VerifyAll();

        }
        #endregion

        #region GetPageNavigationFromCachee_With_Empty_SiteName_return_tuple
        /// <summary>
        ///GetPageNavigationFromCachee_With_Empty_SiteName_return_tuple
        /// </summary>
        [TestMethod]
        public void GetPageNavigationFromCachee_With_Empty_SiteName_return_tuple()
        {
            ClientData_And_ClientSiteData();
            ClientDataFromSystem objClientDataFromSystem = new ClientDataFromSystem
            {
                TemplatePages = new List<HostedTemplatePage>
                {
                    new HostedTemplatePage
                    {
                        TemplateID = 1,
                        TemplateName = "Forethought",
                        PageID = 1,
                        PageName = "Forethought"
                    }
                },
                HostedTemplatePageNavigations = new List<HostedTemplatePageNavigation>
                {
                    new HostedTemplatePageNavigation
                    {
                        TemplateID = 1,
                        PageID = 1,
                        NavigationKey = ""
                    }
                }
            };
            ClientData_And_ClientSiteData();
            mockSystemCommonFactory.Setup(x => x.GetClientsDataFromCache())
                .Returns(objClientDataFromSystem);

            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetPageNavigationFromCache("Forethought", "", "", true, 1);
            //Assert
            Tuple<string, string> tplExpected = new Tuple<string, string>(null, null);
            Assert.AreEqual(tplExpected, result);
            mockDataAccess.VerifyAll();
            mockSystemCommonFactory.VerifyAll();

        }
        #endregion

        #region GetSiteNavigationFromCache_return_Tuple
        /// <summary>
        ///GetSiteNavigationFromCache_return_Tuple
        /// </summary>
        [TestMethod]
        public void GetSiteNavigationFromCache_return_Tuple()
        {

            ClientData_And_ClientSiteData();
            
            ClientDataFromSystem objClientDataFromSystem = new ClientDataFromSystem
            {
                TemplatePages = new List<HostedTemplatePage>
                {
                    new HostedTemplatePage
                    {
                        TemplateID = 1,
                        TemplateName = "Forethought",
                        PageID = 1,
                        PageName = "Forethought"
                    }
                },
                HostedTemplatePageNavigations = new List<HostedTemplatePageNavigation>
                {
                    new HostedTemplatePageNavigation
                    {
                        TemplateID = 1,
                        PageID = 1,
                        NavigationKey = ""
                    }
                },
                HostedTemplateNavigations = new List<HostedTemplateNavigation>
                {
                    new HostedTemplateNavigation
                    {
                        NavigationKey = "tst",
                        TemplateID = 1
                    }
                }
            };
            ClientData_And_ClientSiteData();
            mockSystemCommonFactory.Setup(x => x.GetClientsDataFromCache())
                .Returns(objClientDataFromSystem);

            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetSiteNavigationFromCache("Forethought", "Forethought", "tst", false, 1); 
            //Assert
            Tuple<string, string> tplExpected = new Tuple<string, string>("tst", null);
            Assert.AreEqual(tplExpected, result);
            mockDataAccess.VerifyAll();
            mockSystemCommonFactory.VerifyAll();

        }
        #endregion

        #region GetSiteNavigationFromCache_With_Empty_SiteName_return_Tuple
        /// <summary>
        ///GetSiteNavigationFromCache_With_Empty_SiteName_return_Tuple
        /// </summary>
        [TestMethod]
        public void GetSiteNavigationFromCache_With_Empty_SiteName_return_Tuple()
        {

            ClientData_And_ClientSiteData();

            ClientDataFromSystem objClientDataFromSystem = new ClientDataFromSystem
            {
                TemplatePages = new List<HostedTemplatePage>
                {
                    new HostedTemplatePage
                    {
                        TemplateID = 1,
                        TemplateName = "Forethought",
                        PageID = 1,
                        PageName = "Forethought"
                    }
                },
                HostedTemplatePageNavigations = new List<HostedTemplatePageNavigation>
                {
                    new HostedTemplatePageNavigation
                    {
                        TemplateID = 1,
                        PageID = 1,
                        NavigationKey = ""
                    }
                },
                HostedTemplateNavigations = new List<HostedTemplateNavigation>
                {
                    new HostedTemplateNavigation
                    {
                        NavigationKey = "TaxonomySpecificDocumentFrame_DocumentType",
                        TemplateID = 1
                    }
                }
            };
            ClientData_And_ClientSiteData();
            mockSystemCommonFactory.Setup(x => x.GetClientsDataFromCache())
                .Returns(objClientDataFromSystem);

            HostedClientPageScenariosFactory objHostedClientPageScenariosFactory = new HostedClientPageScenariosFactory(mockDataAccess.Object, mockHostedVerticalPageScenariosFactory.Object, mockSystemCommonFactory.Object);
            var result = objHostedClientPageScenariosFactory.GetSiteNavigationFromCache("Forethought", "", "TaxonomySpecificDocumentFrame_DocumentType", true, 1); ;
            //Assert
            Tuple<string, string> tplExpected = new Tuple<string, string>(null, null);
            Assert.AreEqual(tplExpected, result);
            mockDataAccess.VerifyAll();
            mockSystemCommonFactory.VerifyAll();

        }
        #endregion
        
    }

}

