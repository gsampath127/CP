using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using RRD.FSG.RP.Model.Cache.Client;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model;
using RRD.FSG.RP.Model.Entities;
using RRD.FSG.RP.Model.Entities.Client;
using System.Data.Common;
using RRD.FSG.RP.Model.SearchEntities.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using System.Data.SqlClient;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.VerticalMarket;
using RRD.FSG.RP.Model.Entities.HostedPages;
using Moq;
using System.Data.Common;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.Entities.System;

namespace RRD.FSG.RP.Model.Test.Factories.VerticalMarket
{
    /// <summary>
    /// Test class for HostedVerticalBasePageScenariosFactory class
    /// </summary>
    [TestClass]
    public class HostedVerticalBasePageScenariosFactoryTests : BaseTestFactory<TaxonomyAssociationLinkModel>
    {
        Mock<IDataAccess> mockDataAccess { get; set; }

        /// <summary>
        /// TestInitialze
        /// </summary>
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

            mockDataAccess.Setup(x => x.ExecuteDataSet(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
              .Returns(dSet);
        }
        #endregion

        #region GetTaxonomyAssociationLinksVerticalData
        ///<summary>
        // GetTaxonomyAssociationLinksVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomyAssociationLinksVerticalData()
        {

            List<TaxonomyAssociationLinkModel> objTaxonomyAssociationLinkModel = new List<TaxonomyAssociationLinkModel>();
            TaxonomyAssociationLinkModel objModel = new TaxonomyAssociationLinkModel();
            objModel.TaxonomyID = 1;
            objModel.TaxonomyAssocationName = "Test_001";
            objModel.IsObjectinVerticalMarket = false;
            objTaxonomyAssociationLinkModel.Add(objModel);

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("RelationshipType", typeof(Int32));
            dt.Columns.Add("Order", typeof(Int32));


            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyID"] = 1;
            dtrow["TaxonomyName"] = "Test_001";
            dtrow["ParentTaxonomyAssociationId"] = 2;
            dtrow["ChildTaxonomyAssociationId"] = 3;
            dtrow["RelationshipType"] = 1;
            dtrow["Order"] = 2;


            dt.Rows.Add(dtrow);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory obj = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = obj.GetTaxonomyAssociationLinksVerticalData(dt, "ForeThought", objTaxonomyAssociationLinkModel);

            List<TaxonomyAssociationLinkModel> lstExpected = new List<TaxonomyAssociationLinkModel>();
            lstExpected.Add(new TaxonomyAssociationLinkModel()
            {
                TaxonomyAssocationName ="Test_001",
                TaxonomyID = 1,
                IsObjectinVerticalMarket = true,
                ParentTaxonomyAssociaitonID = 0,
                TaxonomyDescriptionOverride = null

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

        #region GetTaxonomyAssociationLinksVerticalData_WithEmptyTaxonomyName
        ///<summary>
        // GetTaxonomyAssociationLinksVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomyAssociationLinksVerticalData_WithEmptyTaxonomyName()
        {

            List<TaxonomyAssociationLinkModel> objTaxonomyAssociationLinkModel = new List<TaxonomyAssociationLinkModel>();
            TaxonomyAssociationLinkModel objModel = new TaxonomyAssociationLinkModel();
            objModel.TaxonomyID = 1;
            objModel.TaxonomyAssocationName = "Test_001";
            objModel.IsObjectinVerticalMarket = false;
            objTaxonomyAssociationLinkModel.Add(objModel);

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("RelationshipType", typeof(Int32));
            dt.Columns.Add("Order", typeof(Int32));


            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyID"] = 1;
            dtrow["TaxonomyName"] = "";
            dtrow["ParentTaxonomyAssociationId"] = 2;
            dtrow["ChildTaxonomyAssociationId"] = 3;
            dtrow["RelationshipType"] = 1;
            dtrow["Order"] = 2;


            dt.Rows.Add(dtrow);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory obj = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = obj.GetTaxonomyAssociationLinksVerticalData(dt, "ForeThought", objTaxonomyAssociationLinkModel);

            List<TaxonomyAssociationLinkModel> lstExpected = new List<TaxonomyAssociationLinkModel>();
            lstExpected.Add(new TaxonomyAssociationLinkModel()
            {
                TaxonomyAssocationName = "Test_001",
                TaxonomyID = 1,
                IsObjectinVerticalMarket = true,
                ParentTaxonomyAssociaitonID = 0,
                TaxonomyDescriptionOverride = null

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

        #region GetTaxonomyAssociationLinksVerticalData_TaxonomyAssociationLinkModel_Null
        ///<summary>
        // GetTaxonomyAssociationLinksVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomyAssociationLinksVerticalData_TaxonomyAssociationLinkModel_Null()
        {

            List<TaxonomyAssociationLinkModel> objTaxonomyAssociationLinkModel = new List<TaxonomyAssociationLinkModel>();
            TaxonomyAssociationLinkModel objModel = new TaxonomyAssociationLinkModel();
            objModel.TaxonomyID = 0;
            objModel.TaxonomyAssocationName = "Test_001";
            objModel.IsObjectinVerticalMarket = false;
            objTaxonomyAssociationLinkModel.Add(objModel);

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("RelationshipType", typeof(Int32));
            dt.Columns.Add("Order", typeof(Int32));


            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyID"] = 1;
            dtrow["TaxonomyName"] = "Test_001";
            dtrow["ParentTaxonomyAssociationId"] = 2;
            dtrow["ChildTaxonomyAssociationId"] = 3;
            dtrow["RelationshipType"] = 1;
            dtrow["Order"] = 2;


            dt.Rows.Add(dtrow);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory obj = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = obj.GetTaxonomyAssociationLinksVerticalData(dt, "ForeThought", objTaxonomyAssociationLinkModel);

            ValidateEmptyData(result);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomyAssociationLinksVerticalData_With_EmptyDataTable
        ///<summary>
        // GetTaxonomyAssociationLinksVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomyAssociationLinksVerticalData_With_EmptyDataTable()
        {

            List<TaxonomyAssociationLinkModel> objTaxonomyAssociationLinkModel = new List<TaxonomyAssociationLinkModel>();
            TaxonomyAssociationLinkModel objModel = new TaxonomyAssociationLinkModel();
            objModel.TaxonomyID = 1;
            objModel.TaxonomyAssocationName = "Test_001";
            objModel.IsObjectinVerticalMarket = false;
            objTaxonomyAssociationLinkModel.Add(objModel);

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("RelationshipType", typeof(Int32));
            dt.Columns.Add("Order", typeof(Int32));

           
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory obj = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = obj.GetTaxonomyAssociationLinksVerticalData(dt, "ForeThought", objTaxonomyAssociationLinkModel);

            ValidateEmptyData(result);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomyAssociationHierarchyDocumentsVerticalData
        ///<summary>
        ///GetTaxonomyAssociationHierarchyDocumentsVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomyAssociationHierarchyDocumentsVerticalData()
        {


            List<TaxonomyAssociationData> lstHostedVerticalBasePageScenarios = new List<TaxonomyAssociationData>();
            List<HostedSiteFootNotes> lstHostedSiteFootNotes = new List<HostedSiteFootNotes>();
            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            List<HostedDocumentTypeHeader> lstHostedDocumentTypeHeader = new List<HostedDocumentTypeHeader>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 5;
            objHostedDocumentType.VerticalMarketID = "5";
            HostedSiteFootNotes objHostedSiteFootNotes = new HostedSiteFootNotes();
            objHostedSiteFootNotes.TaxonomyID = 32;
            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData();
            objTaxonomyAssociationData.TaxonomyID = 32;
            lstHostedVerticalBasePageScenarios.Add(objTaxonomyAssociationData);
            lstHostedSiteFootNotes.Add(objHostedSiteFootNotes);
            lstHostedDocumentType.Add(objHostedDocumentType);
            HostedDocumentTypeHeader objHostedDocumentTypeHeader = new HostedDocumentTypeHeader();
            objHostedDocumentTypeHeader.DocumentTypeId = 5;
            objHostedDocumentTypeHeader.IsObjectinVerticalMarket = false;
            objTaxonomyAssociationData.DocumentTypes = lstHostedDocumentType;
            lstHostedDocumentTypeHeader.Add(objHostedDocumentTypeHeader);


            TaxonomyAssociationHierarchyModel objTaxonomyAssociationHierarchyModel = new TaxonomyAssociationHierarchyModel();

            objTaxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData = lstHostedVerticalBasePageScenarios;
            objTaxonomyAssociationHierarchyModel.FootNotes = lstHostedSiteFootNotes;
            objTaxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData = lstHostedVerticalBasePageScenarios;
            objTaxonomyAssociationHierarchyModel.ParentHeaders = lstHostedDocumentTypeHeader;
            objTaxonomyAssociationHierarchyModel.ChildHeaders = lstHostedDocumentTypeHeader;


            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("TaxonomyNameOverRide", typeof(string));
            dt.Columns.Add("TaxonomyDescriptionOverRide", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("FootnoteText", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("IsParent", typeof(bool));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));



            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyAssociationId"] = 1;
            dtrow["TaxonomyName"] = "Test_001";
            dtrow["TaxonomyID"] = 32;
            dtrow["NameOverride"] = "Test";
            dtrow["DescriptionOverride"] = "";
            dtrow["TaxonomyNameOverRide"] = "";
            dtrow["TaxonomyDescriptionOverRide"] = "";
            dtrow["DocumentTypeId"] = 5;
            dtrow["FootnoteText"] = "ForeThought";
            dtrow["FootnoteOrder"] = 1;
            dtrow["IsParent"] = 1;
            dtrow["VerticalMarketID"] = "5";



            dt.Rows.Add(dtrow);


            DataRow dtrow1 = dt.NewRow();
            dtrow1["SiteName"] = "ForeThought";
            dtrow1["TaxonomyAssociationId"] = 1;
            dtrow1["TaxonomyName"] = "Test_001";
            dtrow1["TaxonomyID"] = 32;
            dtrow1["NameOverride"] = "Test";
            dtrow1["DescriptionOverride"] = "";
            dtrow1["TaxonomyNameOverRide"] = "";
            dtrow1["TaxonomyDescriptionOverRide"] = "";
            dtrow1["DocumentTypeId"] = 5;
            dtrow1["FootnoteText"] = "ForeThought";
            dtrow1["FootnoteOrder"] = 1;
            dtrow1["IsParent"] = 1;
            dtrow1["VerticalMarketID"] = "5";



            dt.Rows.Add(dtrow1);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory objPageScenarios = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = objPageScenarios.GetTaxonomyAssociationHierarchyDocumentsVerticalData(dt, "ForeThought", objTaxonomyAssociationHierarchyModel);
            var resultActual = result as TaxonomyAssociationHierarchyModel;
            List<HostedDocumentTypeHeader> lstExpected1 = new List<HostedDocumentTypeHeader>();
            lstExpected1.Add(new HostedDocumentTypeHeader()
            {
                DocumentTypeId = 5,
                HeaderName = null,
                IsObjectinVerticalMarket = false,
                Order = 0,
                VerticalMarketID = "5"

            });

            List<HostedSiteFootNotes> lstExpected4 = new List<HostedSiteFootNotes>();
            lstExpected4.Add(new HostedSiteFootNotes()
            {
                TaxonomyID = 32,
                IsObjectinVerticalMarket = true,
                Order = 0,
                Text = null

            });
            List<TaxonomyAssociationData> lstExpected5 = new List<TaxonomyAssociationData>();
            lstExpected5.Add(new TaxonomyAssociationData()
            {
                TaxonomyID = 32,
                DocumentTypes = lstHostedDocumentType,
                TaxonomyAssociationID = 0,
                IsObjectinVerticalMarket = true,
                TaxonomyCssClass = null,
                TaxonomyDescriptionOverride = null,
                TaxonomyName = "Test_001"

            });

            TaxonomyAssociationHierarchyModel objTaxonomyAssociationHierarchyModelexpected = new TaxonomyAssociationHierarchyModel();

            objTaxonomyAssociationHierarchyModelexpected.ParentTaxonomyAssociationData = lstExpected5;
            objTaxonomyAssociationHierarchyModelexpected.FootNotes = lstExpected4;
            objTaxonomyAssociationHierarchyModelexpected.ChildTaxonomyAssociationData = lstExpected5;
            objTaxonomyAssociationHierarchyModelexpected.ParentHeaders = lstExpected1;
            objTaxonomyAssociationHierarchyModelexpected.ChildHeaders = lstExpected1;

            ValidateObjectModelData<TaxonomyAssociationHierarchyModel>(resultActual, objTaxonomyAssociationHierarchyModelexpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomyAssociationHierarchyDocumentsVerticalData_WithEmptyTaxonamyName
        ///<summary>
        ///GetTaxonomyAssociationHierarchyDocumentsVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomyAssociationHierarchyDocumentsVerticalData_WithEmptyTaxonamyName()
        {


            List<TaxonomyAssociationData> lstHostedVerticalBasePageScenarios = new List<TaxonomyAssociationData>();
            List<HostedSiteFootNotes> lstHostedSiteFootNotes = new List<HostedSiteFootNotes>();
            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            List<HostedDocumentTypeHeader> lstHostedDocumentTypeHeader = new List<HostedDocumentTypeHeader>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 5;
            objHostedDocumentType.VerticalMarketID = "5";
            HostedSiteFootNotes objHostedSiteFootNotes = new HostedSiteFootNotes();
            objHostedSiteFootNotes.TaxonomyID = 32;
            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData();
            objTaxonomyAssociationData.TaxonomyID = 32;
            lstHostedVerticalBasePageScenarios.Add(objTaxonomyAssociationData);
            lstHostedSiteFootNotes.Add(objHostedSiteFootNotes);
            lstHostedDocumentType.Add(objHostedDocumentType);
            HostedDocumentTypeHeader objHostedDocumentTypeHeader = new HostedDocumentTypeHeader();
            objHostedDocumentTypeHeader.DocumentTypeId = 5;
            objHostedDocumentTypeHeader.IsObjectinVerticalMarket = false;
            objTaxonomyAssociationData.DocumentTypes = lstHostedDocumentType;
            lstHostedDocumentTypeHeader.Add(objHostedDocumentTypeHeader);


            TaxonomyAssociationHierarchyModel objTaxonomyAssociationHierarchyModel = new TaxonomyAssociationHierarchyModel();

            objTaxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData = lstHostedVerticalBasePageScenarios;
            objTaxonomyAssociationHierarchyModel.FootNotes = lstHostedSiteFootNotes;
            objTaxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData = lstHostedVerticalBasePageScenarios;
            objTaxonomyAssociationHierarchyModel.ParentHeaders = lstHostedDocumentTypeHeader;
            objTaxonomyAssociationHierarchyModel.ChildHeaders = lstHostedDocumentTypeHeader;


            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("TaxonomyNameOverRide", typeof(string));
            dt.Columns.Add("TaxonomyDescriptionOverRide", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("FootnoteText", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("IsParent", typeof(bool));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));



            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyAssociationId"] = 1;
            dtrow["TaxonomyName"] = null;
            dtrow["TaxonomyID"] = 32;
            dtrow["NameOverride"] = "Test";
            dtrow["DescriptionOverride"] = "";
            dtrow["TaxonomyNameOverRide"] = "";
            dtrow["TaxonomyDescriptionOverRide"] = "";
            dtrow["DocumentTypeId"] = 5;
            dtrow["FootnoteText"] = "ForeThought";
            dtrow["FootnoteOrder"] = 1;
            dtrow["IsParent"] = 1;
            dtrow["VerticalMarketID"] = "5";



            dt.Rows.Add(dtrow);


            DataRow dtrow1 = dt.NewRow();
            dtrow1["SiteName"] = "ForeThought";
            dtrow1["TaxonomyAssociationId"] = 1;
            dtrow1["TaxonomyName"] = "Test_001";
            dtrow1["TaxonomyID"] = 32;
            dtrow1["NameOverride"] = "Test";
            dtrow1["DescriptionOverride"] = "";
            dtrow1["TaxonomyNameOverRide"] = "";
            dtrow1["TaxonomyDescriptionOverRide"] = "";
            dtrow1["DocumentTypeId"] = 5;
            dtrow1["FootnoteText"] = "ForeThought";
            dtrow1["FootnoteOrder"] = 1;
            dtrow1["IsParent"] = 1;
            dtrow1["VerticalMarketID"] = "5";



            dt.Rows.Add(dtrow1);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory objPageScenarios = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = objPageScenarios.GetTaxonomyAssociationHierarchyDocumentsVerticalData(dt, "ForeThought", objTaxonomyAssociationHierarchyModel);
            var resultActual = result as TaxonomyAssociationHierarchyModel;
            List<HostedDocumentTypeHeader> lstExpected1 = new List<HostedDocumentTypeHeader>();
            lstExpected1.Add(new HostedDocumentTypeHeader()
            {
                DocumentTypeId = 5,
                HeaderName = null,
                IsObjectinVerticalMarket = false,
                Order = 0,
                VerticalMarketID = "5"

            });

            List<HostedSiteFootNotes> lstExpected4 = new List<HostedSiteFootNotes>();
            lstExpected4.Add(new HostedSiteFootNotes()
            {
                TaxonomyID = 32,
                IsObjectinVerticalMarket = true,
                Order = 0,
                Text = null

            });
            List<TaxonomyAssociationData> lstExpected5 = new List<TaxonomyAssociationData>();
            lstExpected5.Add(new TaxonomyAssociationData()
            {
                TaxonomyID = 32,
                DocumentTypes = lstHostedDocumentType,
                TaxonomyAssociationID = 0,
                IsObjectinVerticalMarket = true,
                TaxonomyCssClass = null,
                TaxonomyDescriptionOverride = null,
                TaxonomyName = null

            });

            TaxonomyAssociationHierarchyModel objTaxonomyAssociationHierarchyModelexpected = new TaxonomyAssociationHierarchyModel();

            objTaxonomyAssociationHierarchyModelexpected.ParentTaxonomyAssociationData = lstExpected5;
            objTaxonomyAssociationHierarchyModelexpected.FootNotes = lstExpected4;
            objTaxonomyAssociationHierarchyModelexpected.ChildTaxonomyAssociationData = lstExpected5;
            objTaxonomyAssociationHierarchyModelexpected.ParentHeaders = lstExpected1;
            objTaxonomyAssociationHierarchyModelexpected.ChildHeaders = lstExpected1;

            ValidateObjectModelData<TaxonomyAssociationHierarchyModel>(resultActual, objTaxonomyAssociationHierarchyModelexpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomyAssociationHierarchyDocumentsVerticalData_WithNotISParent
        ///<summary>
        ///GetTaxonomyAssociationHierarchyDocumentsVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomyAssociationHierarchyDocumentsVerticalData_WithNotISParent()
        {


            List<TaxonomyAssociationData> lstHostedVerticalBasePageScenarios = new List<TaxonomyAssociationData>();
            List<HostedSiteFootNotes> lstHostedSiteFootNotes = new List<HostedSiteFootNotes>();
            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            List<HostedDocumentTypeHeader> lstHostedDocumentTypeHeader = new List<HostedDocumentTypeHeader>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 5;
            objHostedDocumentType.VerticalMarketID = "5";
            HostedSiteFootNotes objHostedSiteFootNotes = new HostedSiteFootNotes();
            objHostedSiteFootNotes.TaxonomyID = 32;
            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData();
            objTaxonomyAssociationData.TaxonomyID = 32;
            lstHostedVerticalBasePageScenarios.Add(objTaxonomyAssociationData);
            lstHostedSiteFootNotes.Add(objHostedSiteFootNotes);
            lstHostedDocumentType.Add(objHostedDocumentType);
            HostedDocumentTypeHeader objHostedDocumentTypeHeader = new HostedDocumentTypeHeader();
            objHostedDocumentTypeHeader.DocumentTypeId = 5;
            objHostedDocumentTypeHeader.IsObjectinVerticalMarket = false;
            objTaxonomyAssociationData.DocumentTypes = lstHostedDocumentType;
            lstHostedDocumentTypeHeader.Add(objHostedDocumentTypeHeader);


            TaxonomyAssociationHierarchyModel objTaxonomyAssociationHierarchyModel = new TaxonomyAssociationHierarchyModel();

            objTaxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData = lstHostedVerticalBasePageScenarios;
            objTaxonomyAssociationHierarchyModel.FootNotes = lstHostedSiteFootNotes;
            objTaxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData = lstHostedVerticalBasePageScenarios;
            objTaxonomyAssociationHierarchyModel.ParentHeaders = lstHostedDocumentTypeHeader;
            objTaxonomyAssociationHierarchyModel.ChildHeaders = lstHostedDocumentTypeHeader;


            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("TaxonomyNameOverRide", typeof(string));
            dt.Columns.Add("TaxonomyDescriptionOverRide", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("FootnoteText", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("IsParent", typeof(bool));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));



            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyAssociationId"] = 1;
            dtrow["TaxonomyName"] = "Test";
            dtrow["TaxonomyID"] = 32;
            dtrow["NameOverride"] = "Test";
            dtrow["DescriptionOverride"] = "";
            dtrow["TaxonomyNameOverRide"] = "";
            dtrow["TaxonomyDescriptionOverRide"] = "";
            dtrow["DocumentTypeId"] = 5;
            dtrow["FootnoteText"] = "ForeThought";
            dtrow["FootnoteOrder"] = 1;
            dtrow["IsParent"] = 0;
            dtrow["VerticalMarketID"] = "5";



            dt.Rows.Add(dtrow);


            DataRow dtrow1 = dt.NewRow();
            dtrow1["SiteName"] = "ForeThought";
            dtrow1["TaxonomyAssociationId"] = 1;
            dtrow1["TaxonomyName"] = "Test_001";
            dtrow1["TaxonomyID"] = 32;
            dtrow1["NameOverride"] = "Test";
            dtrow1["DescriptionOverride"] = "";
            dtrow1["TaxonomyNameOverRide"] = "";
            dtrow1["TaxonomyDescriptionOverRide"] = "";
            dtrow1["DocumentTypeId"] = 5;
            dtrow1["FootnoteText"] = "ForeThought";
            dtrow1["FootnoteOrder"] = 1;
            dtrow1["IsParent"] = 0;
            dtrow1["VerticalMarketID"] = "5";



            dt.Rows.Add(dtrow1);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory objPageScenarios = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = objPageScenarios.GetTaxonomyAssociationHierarchyDocumentsVerticalData(dt, "ForeThought", objTaxonomyAssociationHierarchyModel);
            var resultActual = result as TaxonomyAssociationHierarchyModel;
            List<HostedDocumentTypeHeader> lstExpected1 = new List<HostedDocumentTypeHeader>();
            lstExpected1.Add(new HostedDocumentTypeHeader()
            {
                DocumentTypeId = 5,
                HeaderName = null,
                IsObjectinVerticalMarket = false,
                Order = 0,
                VerticalMarketID = "5"

            });

            List<HostedSiteFootNotes> lstExpected4 = new List<HostedSiteFootNotes>();
            lstExpected4.Add(new HostedSiteFootNotes()
            {
                TaxonomyID = 32,
                IsObjectinVerticalMarket = true,
                Order = 0,
                Text = null

            });
            List<TaxonomyAssociationData> lstExpected5 = new List<TaxonomyAssociationData>();
            lstExpected5.Add(new TaxonomyAssociationData()
            {
                TaxonomyID = 32,
                DocumentTypes = lstHostedDocumentType,
                TaxonomyAssociationID = 0,
                IsObjectinVerticalMarket = true,
                TaxonomyCssClass = null,
                TaxonomyDescriptionOverride = null,
                TaxonomyName = "Test"

            });

            TaxonomyAssociationHierarchyModel objTaxonomyAssociationHierarchyModelexpected = new TaxonomyAssociationHierarchyModel();

            objTaxonomyAssociationHierarchyModelexpected.ParentTaxonomyAssociationData = lstExpected5;
            objTaxonomyAssociationHierarchyModelexpected.FootNotes = lstExpected4;
            objTaxonomyAssociationHierarchyModelexpected.ChildTaxonomyAssociationData = lstExpected5;
            objTaxonomyAssociationHierarchyModelexpected.ParentHeaders = lstExpected1;
            objTaxonomyAssociationHierarchyModelexpected.ChildHeaders = lstExpected1;

            ValidateObjectModelData<TaxonomyAssociationHierarchyModel>(resultActual, objTaxonomyAssociationHierarchyModelexpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomyAssociationHierarchyDocumentsVerticalData_WithNullHostedDocumentType
        ///<summary>
        ///GetTaxonomyAssociationHierarchyDocumentsVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomyAssociationHierarchyDocumentsVerticalData_WithNullHostedDocumentType()
        {


            List<TaxonomyAssociationData> lstHostedVerticalBasePageScenarios = new List<TaxonomyAssociationData>();
            List<HostedSiteFootNotes> lstHostedSiteFootNotes = new List<HostedSiteFootNotes>();
            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            List<HostedDocumentTypeHeader> lstHostedDocumentTypeHeader = new List<HostedDocumentTypeHeader>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 5;
            objHostedDocumentType.VerticalMarketID = "5";
            HostedSiteFootNotes objHostedSiteFootNotes = new HostedSiteFootNotes();
            objHostedSiteFootNotes.TaxonomyID = 12;
            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData();
            objTaxonomyAssociationData.TaxonomyID = 32;
            lstHostedVerticalBasePageScenarios.Add(objTaxonomyAssociationData);
            lstHostedSiteFootNotes.Add(objHostedSiteFootNotes);
            lstHostedDocumentType.Add(objHostedDocumentType);
            HostedDocumentTypeHeader objHostedDocumentTypeHeader = new HostedDocumentTypeHeader();
            objHostedDocumentTypeHeader.DocumentTypeId = 5;
            objHostedDocumentTypeHeader.IsObjectinVerticalMarket = false;
            objTaxonomyAssociationData.DocumentTypes = lstHostedDocumentType;
            lstHostedDocumentTypeHeader.Add(objHostedDocumentTypeHeader);


            TaxonomyAssociationHierarchyModel objTaxonomyAssociationHierarchyModel = new TaxonomyAssociationHierarchyModel();

            objTaxonomyAssociationHierarchyModel.ParentTaxonomyAssociationData = lstHostedVerticalBasePageScenarios;
            objTaxonomyAssociationHierarchyModel.FootNotes = lstHostedSiteFootNotes;
            objTaxonomyAssociationHierarchyModel.ChildTaxonomyAssociationData = lstHostedVerticalBasePageScenarios;
            objTaxonomyAssociationHierarchyModel.ParentHeaders = lstHostedDocumentTypeHeader;
            objTaxonomyAssociationHierarchyModel.ChildHeaders = lstHostedDocumentTypeHeader;


            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("TaxonomyNameOverRide", typeof(string));
            dt.Columns.Add("TaxonomyDescriptionOverRide", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("FootnoteText", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("IsParent", typeof(bool));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));


            DataRow dtrow1 = dt.NewRow();
            dtrow1["SiteName"] = "ForeThought";
            dtrow1["TaxonomyAssociationId"] = 1;
            dtrow1["TaxonomyName"] = null;
            dtrow1["TaxonomyID"] = 32;
            dtrow1["NameOverride"] = "Test";
            dtrow1["DescriptionOverride"] = "";
            dtrow1["TaxonomyNameOverRide"] = "";
            dtrow1["TaxonomyDescriptionOverRide"] = "";
            dtrow1["DocumentTypeId"] = 15;
            dtrow1["FootnoteText"] = "ForeThought";
            dtrow1["FootnoteOrder"] = 1;
            dtrow1["IsParent"] = 0;
            dtrow1["VerticalMarketID"] = "5";



            dt.Rows.Add(dtrow1);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory objPageScenarios = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = objPageScenarios.GetTaxonomyAssociationHierarchyDocumentsVerticalData(dt, "ForeThought", objTaxonomyAssociationHierarchyModel);
            var resultActual = result as TaxonomyAssociationHierarchyModel;
            List<HostedDocumentTypeHeader> lstExpected1 = new List<HostedDocumentTypeHeader>();
            lstExpected1.Add(new HostedDocumentTypeHeader()
            {
                DocumentTypeId = 5,
                HeaderName = null,
                IsObjectinVerticalMarket = false,
                Order = 0,
                VerticalMarketID = null

            });

            List<HostedSiteFootNotes> lstExpected4 = new List<HostedSiteFootNotes>();
        
            List<TaxonomyAssociationData> lstExpected5 = new List<TaxonomyAssociationData>();
            lstExpected5.Add(new TaxonomyAssociationData()
            {
                TaxonomyID = 32,
                DocumentTypes = lstHostedDocumentType,
                TaxonomyAssociationID = 0,
                IsObjectinVerticalMarket = true,
                TaxonomyCssClass = null,
                TaxonomyDescriptionOverride = null,
                TaxonomyName = null

            });

            TaxonomyAssociationHierarchyModel objTaxonomyAssociationHierarchyModelexpected = new TaxonomyAssociationHierarchyModel();

            objTaxonomyAssociationHierarchyModelexpected.ParentTaxonomyAssociationData = lstExpected5;
            objTaxonomyAssociationHierarchyModelexpected.FootNotes = lstExpected4;
            objTaxonomyAssociationHierarchyModelexpected.ChildTaxonomyAssociationData = lstExpected5;
            objTaxonomyAssociationHierarchyModelexpected.ParentHeaders = lstExpected1;
            objTaxonomyAssociationHierarchyModelexpected.ChildHeaders = lstExpected1;

            ValidateObjectModelData<TaxonomyAssociationHierarchyModel>(resultActual, objTaxonomyAssociationHierarchyModelexpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomyAssociationDocumentsVerticalData
        ///<summary>
        /// GetTaxonomyAssociationDocumentsVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomyAssociationDocumentsVerticalData()
        {
            List<TaxonomyAssociationData> lstTaxonomyAssociationData = new List<TaxonomyAssociationData>();
            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData();
            objTaxonomyAssociationData.TaxonomyID = 5;
            objTaxonomyAssociationData.TaxonomyName = "Test";
            objTaxonomyAssociationData.IsObjectinVerticalMarket = true;
            lstTaxonomyAssociationData.Add(objTaxonomyAssociationData);

            List<HostedSiteFootNotes> lstHostedSiteFootNotes = new List<HostedSiteFootNotes>();
            HostedSiteFootNotes objHostedSiteFootNotes = new HostedSiteFootNotes();
            objHostedSiteFootNotes.TaxonomyID = 5;
            lstHostedSiteFootNotes.Add(objHostedSiteFootNotes);

            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 2;
            objHostedDocumentType.VerticalMarketID = "1";
            lstHostedDocumentType.Add(objHostedDocumentType);

            objTaxonomyAssociationData.DocumentTypes = lstHostedDocumentType;

            List<HostedDocumentTypeHeader> lstHostedDocumentTypeHeader = new List<HostedDocumentTypeHeader>();
            HostedDocumentTypeHeader objHostedDocumentTypeHeader = new HostedDocumentTypeHeader();
            objHostedDocumentTypeHeader.VerticalMarketID = "1";
            objHostedDocumentTypeHeader.DocumentTypeId = 2;
            lstHostedDocumentTypeHeader.Add(objHostedDocumentTypeHeader);

           

            TaxonomyAssociationDocumentsModel objTaxonomyAssociationDocumentsModel = new TaxonomyAssociationDocumentsModel();
            objTaxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData = lstTaxonomyAssociationData;
            objTaxonomyAssociationDocumentsModel.FootNotes = lstHostedSiteFootNotes;
            objTaxonomyAssociationDocumentsModel.DocumentTypeHeaders = lstHostedDocumentTypeHeader;
            


            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("TaxonomyNameOverRide", typeof(string));
            dt.Columns.Add("TaxonomyDescriptionOverRide", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("FootnoteText", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("IsObjectinVerticalMarket", typeof(bool));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));



            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyAssociationId"] = 5;
            dtrow["TaxonomyName"] = "Test";
            dtrow["TaxonomyID"] = 5;
            dtrow["NameOverride"] = "Test";
            dtrow["DescriptionOverride"] = "";
            dtrow["TaxonomyNameOverRide"] = "";
            dtrow["TaxonomyDescriptionOverRide"] = "";
            dtrow["DocumentTypeId"] = 2;
            dtrow["FootnoteText"] = "ForeThought";
            dtrow["FootnoteOrder"] = 1;
            dtrow["IsObjectinVerticalMarket"] = true;
            dtrow["VerticalMarketID"] = "5";

            dt.Rows.Add(dtrow);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);


            //Act
            HostedVerticalBasePageScenariosFactory objPageScenarios = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = objPageScenarios.GetTaxonomyAssociationDocumentsVerticalData(dt, "ForeThought", objTaxonomyAssociationDocumentsModel);
            var resultActual = result as TaxonomyAssociationDocumentsModel;
            List<HostedDocumentTypeHeader> lstExpected1 = new List<HostedDocumentTypeHeader>();
            lstExpected1.Add(new HostedDocumentTypeHeader()
            {
                DocumentTypeId = 2,
                HeaderName = null,
                IsObjectinVerticalMarket = false,
                Order = 0,
                VerticalMarketID = "5"

            });

            List<HostedSiteFootNotes> lstExpected4 = new List<HostedSiteFootNotes>();
            lstExpected4.Add(new HostedSiteFootNotes()
            {
                TaxonomyID = 5,
                IsObjectinVerticalMarket = true,
                Order = 0,
                Text = null

            });
            List<TaxonomyAssociationData> lstExpected5 = new List<TaxonomyAssociationData>();
            lstExpected5.Add(new TaxonomyAssociationData()
            {
                TaxonomyID = 5,
                DocumentTypes = lstHostedDocumentType,
                TaxonomyAssociationID = 0,
                IsObjectinVerticalMarket = false,
                TaxonomyCssClass = null,
                TaxonomyDescriptionOverride = null,
                TaxonomyName = "Test"

            });

            TaxonomyAssociationDocumentsModel objTaxonomyAssociationDocumentsModelexpected = new TaxonomyAssociationDocumentsModel();

            objTaxonomyAssociationDocumentsModelexpected.TaxonomyAssociationDocumentsData = lstExpected5;
            objTaxonomyAssociationDocumentsModelexpected.FootNotes = lstExpected4;
            objTaxonomyAssociationDocumentsModelexpected.DocumentTypeHeaders = lstExpected1;

            ValidateObjectModelData<TaxonomyAssociationDocumentsModel>(resultActual, objTaxonomyAssociationDocumentsModelexpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomyAssociationDocumentsVerticalData_WithEmptyTaxonamyname
        ///<summary>
        /// GetTaxonomyAssociationDocumentsVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomyAssociationDocumentsVerticalData_WithEmptyTaxonamyname()
        {
            List<TaxonomyAssociationData> lstTaxonomyAssociationData = new List<TaxonomyAssociationData>();
            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData();
            objTaxonomyAssociationData.TaxonomyID = 5;
            objTaxonomyAssociationData.TaxonomyName = "Test";
            objTaxonomyAssociationData.IsObjectinVerticalMarket = true;
            lstTaxonomyAssociationData.Add(objTaxonomyAssociationData);

            List<HostedSiteFootNotes> lstHostedSiteFootNotes = new List<HostedSiteFootNotes>();
            HostedSiteFootNotes objHostedSiteFootNotes = new HostedSiteFootNotes();
            objHostedSiteFootNotes.TaxonomyID = 2;
            lstHostedSiteFootNotes.Add(objHostedSiteFootNotes);

            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 2;
            objHostedDocumentType.VerticalMarketID = "1";
            lstHostedDocumentType.Add(objHostedDocumentType);

            objTaxonomyAssociationData.DocumentTypes = lstHostedDocumentType;

            List<HostedDocumentTypeHeader> lstHostedDocumentTypeHeader = new List<HostedDocumentTypeHeader>();
            HostedDocumentTypeHeader objHostedDocumentTypeHeader = new HostedDocumentTypeHeader();
            objHostedDocumentTypeHeader.VerticalMarketID = "1";
            objHostedDocumentTypeHeader.DocumentTypeId = 2;
            lstHostedDocumentTypeHeader.Add(objHostedDocumentTypeHeader);



            TaxonomyAssociationDocumentsModel objTaxonomyAssociationDocumentsModel = new TaxonomyAssociationDocumentsModel();
            objTaxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData = lstTaxonomyAssociationData;
            objTaxonomyAssociationDocumentsModel.FootNotes = lstHostedSiteFootNotes;
            objTaxonomyAssociationDocumentsModel.DocumentTypeHeaders = lstHostedDocumentTypeHeader;



            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("TaxonomyNameOverRide", typeof(string));
            dt.Columns.Add("TaxonomyDescriptionOverRide", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("FootnoteText", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("IsObjectinVerticalMarket", typeof(bool));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));



            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyAssociationId"] = 5;
            dtrow["TaxonomyName"] = null;
            dtrow["TaxonomyID"] = 5;
            dtrow["NameOverride"] = "Test";
            dtrow["DescriptionOverride"] = "";
            dtrow["TaxonomyNameOverRide"] = "";
            dtrow["TaxonomyDescriptionOverRide"] = "";
            dtrow["DocumentTypeId"] = 2;
            dtrow["FootnoteText"] = "ForeThought";
            dtrow["FootnoteOrder"] = 1;
            dtrow["IsObjectinVerticalMarket"] = true;
            dtrow["VerticalMarketID"] = "5";

            dt.Rows.Add(dtrow);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);


            //Act
            HostedVerticalBasePageScenariosFactory objPageScenarios = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = objPageScenarios.GetTaxonomyAssociationDocumentsVerticalData(dt, "ForeThought", objTaxonomyAssociationDocumentsModel);
            var resultActual = result as TaxonomyAssociationDocumentsModel;
            List<HostedDocumentTypeHeader> lstExpected1 = new List<HostedDocumentTypeHeader>();
            lstExpected1.Add(new HostedDocumentTypeHeader()
            {
                DocumentTypeId = 2,
                HeaderName = null,
                IsObjectinVerticalMarket = false,
                Order = 0,
                VerticalMarketID = "5"

            });

            List<HostedSiteFootNotes> lstExpected4 = new List<HostedSiteFootNotes>();
            
            List<TaxonomyAssociationData> lstExpected5 = new List<TaxonomyAssociationData>();
            lstExpected5.Add(new TaxonomyAssociationData()
            {
                TaxonomyID = 5,
                DocumentTypes = lstHostedDocumentType,
                TaxonomyAssociationID = 0,
                IsObjectinVerticalMarket = true,
                TaxonomyCssClass = null,
                TaxonomyDescriptionOverride = null,
                TaxonomyName = "Test"

            });

            TaxonomyAssociationDocumentsModel objTaxonomyAssociationDocumentsModelexpected = new TaxonomyAssociationDocumentsModel();

            objTaxonomyAssociationDocumentsModelexpected.TaxonomyAssociationDocumentsData = lstExpected5;
            objTaxonomyAssociationDocumentsModelexpected.FootNotes = lstExpected4;
            objTaxonomyAssociationDocumentsModelexpected.DocumentTypeHeaders = lstExpected1;

            ValidateObjectModelData<TaxonomyAssociationDocumentsModel>(resultActual, objTaxonomyAssociationDocumentsModelexpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomyAssociationDocumentsVerticalData_WithHosteddocumentTypeNull
        ///<summary>
        /// GetTaxonomyAssociationDocumentsVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomyAssociationDocumentsVerticalData_WithHosteddocumentTypeNull()
        {
            List<TaxonomyAssociationData> lstTaxonomyAssociationData = new List<TaxonomyAssociationData>();
            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData();
            objTaxonomyAssociationData.TaxonomyID = 5;
            objTaxonomyAssociationData.TaxonomyName = "Test";
            objTaxonomyAssociationData.IsObjectinVerticalMarket = true;
            lstTaxonomyAssociationData.Add(objTaxonomyAssociationData);

            List<HostedSiteFootNotes> lstHostedSiteFootNotes = new List<HostedSiteFootNotes>();
            HostedSiteFootNotes objHostedSiteFootNotes = new HostedSiteFootNotes();
            objHostedSiteFootNotes.TaxonomyID = 5;
            lstHostedSiteFootNotes.Add(objHostedSiteFootNotes);

            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 2;
            objHostedDocumentType.VerticalMarketID = "1";
            lstHostedDocumentType.Add(objHostedDocumentType);

            objTaxonomyAssociationData.DocumentTypes = lstHostedDocumentType;

            List<HostedDocumentTypeHeader> lstHostedDocumentTypeHeader = new List<HostedDocumentTypeHeader>();
            HostedDocumentTypeHeader objHostedDocumentTypeHeader = new HostedDocumentTypeHeader();
            objHostedDocumentTypeHeader.VerticalMarketID = "1";
            objHostedDocumentTypeHeader.DocumentTypeId = 2;
            lstHostedDocumentTypeHeader.Add(objHostedDocumentTypeHeader);



            TaxonomyAssociationDocumentsModel objTaxonomyAssociationDocumentsModel = new TaxonomyAssociationDocumentsModel();
            objTaxonomyAssociationDocumentsModel.TaxonomyAssociationDocumentsData = lstTaxonomyAssociationData;
            objTaxonomyAssociationDocumentsModel.FootNotes = lstHostedSiteFootNotes;
            objTaxonomyAssociationDocumentsModel.DocumentTypeHeaders = lstHostedDocumentTypeHeader;



            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("TaxonomyNameOverRide", typeof(string));
            dt.Columns.Add("TaxonomyDescriptionOverRide", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("FootnoteText", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("IsObjectinVerticalMarket", typeof(bool));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));


            
            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyAssociationId"] = 5;
            dtrow["TaxonomyName"] = "Test";
            dtrow["TaxonomyID"] = 5;
            dtrow["NameOverride"] = "Test";
            dtrow["DescriptionOverride"] = "";
            dtrow["TaxonomyNameOverRide"] = "";
            dtrow["TaxonomyDescriptionOverRide"] = "";
            dtrow["DocumentTypeId"] = 2;
            dtrow["FootnoteText"] = "ForeThought";
            dtrow["FootnoteOrder"] = 1;
            dtrow["IsObjectinVerticalMarket"] = true;
            dtrow["VerticalMarketID"] = "5";

            dt.Rows.Add(dtrow);

            DataRow dtrow1 = dt.NewRow();
            dtrow1["SiteName"] = "ForeThought";
            dtrow1["TaxonomyAssociationId"] = 5;
            dtrow1["TaxonomyName"] = "Test";
            dtrow1["TaxonomyID"] = 5;
            dtrow1["NameOverride"] = "Test";
            dtrow1["DescriptionOverride"] = "";
            dtrow1["TaxonomyNameOverRide"] = "";
            dtrow1["TaxonomyDescriptionOverRide"] = "";
            dtrow1["DocumentTypeId"] = 1;
            dtrow1["FootnoteText"] = "ForeThought";
            dtrow1["FootnoteOrder"] = 1;
            dtrow1["IsObjectinVerticalMarket"] = true;
            dtrow1["VerticalMarketID"] = "5";

            dt.Rows.Add(dtrow1);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);


            //Act
            HostedVerticalBasePageScenariosFactory objPageScenarios = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = objPageScenarios.GetTaxonomyAssociationDocumentsVerticalData(dt, "ForeThought", objTaxonomyAssociationDocumentsModel);
            var resultActual = result as TaxonomyAssociationDocumentsModel;
            List<HostedDocumentTypeHeader> lstExpected1 = new List<HostedDocumentTypeHeader>();
            lstExpected1.Add(new HostedDocumentTypeHeader()
            {
                DocumentTypeId = 2,
                HeaderName = null,
                IsObjectinVerticalMarket = false,
                Order = 0,
                VerticalMarketID = "5"

            });

            List<HostedSiteFootNotes> lstExpected4 = new List<HostedSiteFootNotes>();
            lstExpected4.Add(new HostedSiteFootNotes()
            {
                TaxonomyID = 5,
                IsObjectinVerticalMarket = true,
                Order = 0,
                Text = null

            });
            List<TaxonomyAssociationData> lstExpected5 = new List<TaxonomyAssociationData>();
            lstExpected5.Add(new TaxonomyAssociationData()
            {
                TaxonomyID = 5,
                DocumentTypes = lstHostedDocumentType,
                TaxonomyAssociationID = 0,
                IsObjectinVerticalMarket = true,
                TaxonomyCssClass = null,
                TaxonomyDescriptionOverride = null,
                TaxonomyName = "Test"

            });

            TaxonomyAssociationDocumentsModel objTaxonomyAssociationDocumentsModelexpected = new TaxonomyAssociationDocumentsModel();

            objTaxonomyAssociationDocumentsModelexpected.TaxonomyAssociationDocumentsData = lstExpected5;
            objTaxonomyAssociationDocumentsModelexpected.FootNotes = lstExpected4;
            objTaxonomyAssociationDocumentsModelexpected.DocumentTypeHeaders = lstExpected1;

            ValidateObjectModelData<TaxonomyAssociationDocumentsModel>(resultActual, objTaxonomyAssociationDocumentsModelexpected);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomySpecificDocumentsVerticalData
        ///<summary>
        ///GetTaxonomySpecificDocumentsVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomySpecificDocumentsVerticalData()
        {
            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 2;
            objHostedDocumentType.ContentURI = "Test";
            objHostedDocumentType.VerticalMarketID = "5";
            objHostedDocumentType.IsObjectinVerticalMarket = true;
            lstHostedDocumentType.Add(objHostedDocumentType);

            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData();
            objTaxonomyAssociationData.TaxonomyID = 5;
            objTaxonomyAssociationData.DocumentTypes = lstHostedDocumentType;


            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("TaxonomyNameOverRide", typeof(string));
            dt.Columns.Add("TaxonomyDescriptionOverRide", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("ContentURI", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("IsObjectinVerticalMarket", typeof(bool));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));



            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyAssociationId"] = 5;
            dtrow["TaxonomyName"] = "Test";
            dtrow["TaxonomyID"] = 5;
            dtrow["NameOverride"] = "Test";
            dtrow["DescriptionOverride"] = "";
            dtrow["TaxonomyNameOverRide"] = "";
            dtrow["TaxonomyDescriptionOverRide"] = "";
            dtrow["DocumentTypeId"] = 2;
            dtrow["ContentURI"] = "Test";
            dtrow["FootnoteOrder"] = 1;
            dtrow["IsObjectinVerticalMarket"] = true;
            dtrow["VerticalMarketID"] = "5";

            dt.Rows.Add(dtrow);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory objPageScenarios = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = objPageScenarios.GetTaxonomySpecificDocumentsVerticalData(dt, "ForeThought", objTaxonomyAssociationData);
            var resultActual = result as TaxonomyAssociationData;

            
            TaxonomyAssociationData objTaxonomyAssociationData1 = new TaxonomyAssociationData()
            
            {
                TaxonomyID = 5,
                DocumentTypes = lstHostedDocumentType,
                TaxonomyAssociationID = 0,
                IsObjectinVerticalMarket = true,
                TaxonomyCssClass = null,
                TaxonomyDescriptionOverride = null,
                TaxonomyName = "Test"

            };



            ValidateObjectModelData<TaxonomyAssociationData>(resultActual, objTaxonomyAssociationData1);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomySpecificDocumentsVerticalData_WithEmptytaxonamyName
        ///<summary>
        ///GetTaxonomySpecificDocumentsVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomySpecificDocumentsVerticalData_WithEmptytaxonamyName()
        {
            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 5;
            objHostedDocumentType.ContentURI = "Test";
            objHostedDocumentType.VerticalMarketID = "5";
            objHostedDocumentType.IsObjectinVerticalMarket = false;
            lstHostedDocumentType.Add(objHostedDocumentType);

            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData();
            objTaxonomyAssociationData.TaxonomyID = 5;
            objTaxonomyAssociationData.DocumentTypes = lstHostedDocumentType;


            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("TaxonomyNameOverRide", typeof(string));
            dt.Columns.Add("TaxonomyDescriptionOverRide", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("ContentURI", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("IsObjectinVerticalMarket", typeof(bool));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));



            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyAssociationId"] = 5;
            dtrow["TaxonomyName"] = null;
            dtrow["TaxonomyID"] = 5;
            dtrow["NameOverride"] = "Test";
            dtrow["DescriptionOverride"] = "";
            dtrow["TaxonomyNameOverRide"] = "";
            dtrow["TaxonomyDescriptionOverRide"] = "";
            dtrow["DocumentTypeId"] = 2;
            dtrow["ContentURI"] = "Test";
            dtrow["FootnoteOrder"] = 1;
            dtrow["IsObjectinVerticalMarket"] = true;
            dtrow["VerticalMarketID"] = "5";

            dt.Rows.Add(dtrow);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory objPageScenarios = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = objPageScenarios.GetTaxonomySpecificDocumentsVerticalData(dt, "ForeThought", objTaxonomyAssociationData);
            var resultActual = result as TaxonomyAssociationData;


            TaxonomyAssociationData objTaxonomyAssociationData1 = new TaxonomyAssociationData()

            {
                TaxonomyID = 5,
                DocumentTypes = lstHostedDocumentType,
                TaxonomyAssociationID = 0,
                IsObjectinVerticalMarket = true,
                TaxonomyCssClass = null,
                TaxonomyDescriptionOverride = null,
                TaxonomyName = null

            };



            ValidateObjectModelData<TaxonomyAssociationData>(resultActual, objTaxonomyAssociationData1);
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetTaxonomySpecificDocumentsVerticalData_WithEmptytaxonamyName_WithPreviousIdNotEqual
        ///<summary>
        ///GetTaxonomySpecificDocumentsVerticalData
        ///</summary>
        [TestMethod]
        public void GetTaxonomySpecificDocumentsVerticalData_WithEmptytaxonamyName_WithPreviousIdNotEqual()
        {
            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 5;
            objHostedDocumentType.ContentURI = "Test";
            objHostedDocumentType.VerticalMarketID = "5";
            objHostedDocumentType.IsObjectinVerticalMarket = false;
            lstHostedDocumentType.Add(objHostedDocumentType);

            TaxonomyAssociationData objTaxonomyAssociationData = new TaxonomyAssociationData();
            objTaxonomyAssociationData.TaxonomyID = 2;
            objTaxonomyAssociationData.DocumentTypes = lstHostedDocumentType;
            objTaxonomyAssociationData.IsObjectinVerticalMarket = false;


            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("NameOverride", typeof(string));
            dt.Columns.Add("DescriptionOverride", typeof(string));
            dt.Columns.Add("TaxonomyNameOverRide", typeof(string));
            dt.Columns.Add("TaxonomyDescriptionOverRide", typeof(string));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("ContentURI", typeof(string));
            dt.Columns.Add("FootnoteOrder", typeof(Int32));
            dt.Columns.Add("IsObjectinVerticalMarket", typeof(bool));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));



            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyAssociationId"] = 5;
            dtrow["TaxonomyName"] = null;
            dtrow["TaxonomyID"] = 5;
            dtrow["NameOverride"] = "Test";
            dtrow["DescriptionOverride"] = "";
            dtrow["TaxonomyNameOverRide"] = "";
            dtrow["TaxonomyDescriptionOverRide"] = "";
            dtrow["DocumentTypeId"] = 2;
            dtrow["ContentURI"] = "Test";
            dtrow["FootnoteOrder"] = 1;
            dtrow["IsObjectinVerticalMarket"] = true;
            dtrow["VerticalMarketID"] = "5";

            dt.Rows.Add(dtrow);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory objPageScenarios = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = objPageScenarios.GetTaxonomySpecificDocumentsVerticalData(dt, "ForeThought", objTaxonomyAssociationData);

            Assert.AreEqual(null, result);
         
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetXBRLDetailsForTaxonomyID
        ///<summary>
        // GetXBRLDetailsForTaxonomyID
        ///</summary>
        [TestMethod]
        public void GetXBRLDetailsForTaxonomyID()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Acc#", typeof(string));
            dt.Columns.Add("ZipFileName", typeof(string));
            dt.Columns.Add("Path", typeof(string));
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("DocDate", typeof(DateTime));
            dt.Columns.Add("FilingDate", typeof(DateTime));
            dt.Columns.Add("OrderDate", typeof(DateTime));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("FormType", typeof(string));
            dt.Columns.Add("CreatedDate", typeof(DateTime));
            dt.Columns.Add("DocumentType", typeof(string));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("XBRLViewerCompany", typeof(bool));
            dt.Columns.Add("IsXBRLViewerReady", typeof(bool));


            DataRow dtrow = dt.NewRow();
            dtrow["Acc#"] = "Test";
            dtrow["ZipFileName"] = "Test";
            dtrow["Path"] = "Test";
            dtrow["CompanyName"] = "Test";
            dtrow["SiteName"] = "Test";
            dtrow["DocDate"] = DateTime.Today;
            dtrow["FilingDate"] = DateTime.Today;
            dtrow["OrderDate"] = DateTime.Today;
            dtrow["TaxonomyName"] = "Test";
            dtrow["FormType"] = "Test";
            dtrow["CreatedDate"] = DateTime.Today;
            dtrow["DocumentType"] = "Test";
            dtrow["ChildTaxonomyAssociationId"] = 2;
            dtrow["XBRLViewerCompany"] = false;
            dtrow["IsXBRLViewerReady"] = false;
            


            dt.Rows.Add(dtrow);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory obj = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = obj.GetXBRLDetailsForTaxonomyID("Forethought", 1, "Test");

            List<XBRLObjectModel> lstExpected = new List<XBRLObjectModel>();
            lstExpected.Add(new XBRLObjectModel()
            {
                AccessionNumber ="Test" ,
                CompanyName = "Test",
                CreatedDate=DateTime.Today,
                Description = null,
                DocumentDate =DateTime.Today ,
                DocumentType ="Test" ,
                FilingDate = DateTime.Today,
                FormType="Test",
                IsViewerEnabled=false,
                IsViewerReadyForXBRL = false,
                Name = null,
                OrderDate =DateTime.Today ,
                Path ="Test" ,
                TaxonomyId =1 ,
                TaxonomyName ="Test" ,
                ZipFileName = "Test"

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

        #region GetXBRLDetailsForTaxonomyID_withNullValues
        ///<summary>
        // GetXBRLDetailsForTaxonomyID
        ///</summary>
        [TestMethod]
        public void GetXBRLDetailsForTaxonomyID_withNullValues()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Acc#", typeof(string));
            dt.Columns.Add("ZipFileName", typeof(string));
            dt.Columns.Add("Path", typeof(string));
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("DocDate", typeof(DateTime));
            dt.Columns.Add("FilingDate", typeof(DateTime));
            dt.Columns.Add("OrderDate", typeof(DateTime));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("FormType", typeof(string));
            dt.Columns.Add("CreatedDate", typeof(DateTime));
            dt.Columns.Add("DocumentType", typeof(string));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("XBRLViewerCompany", typeof(bool));
            dt.Columns.Add("IsXBRLViewerReady", typeof(bool));


            DataRow dtrow = dt.NewRow();
            dtrow["Acc#"] = "Test";
            dtrow["ZipFileName"] = "Test";
            dtrow["Path"] = "Test";
            dtrow["CompanyName"] = "Test";
            dtrow["SiteName"] = "Test";
            dtrow["DocDate"] = DateTime.Today;
            dtrow["FilingDate"] = DateTime.Today;
            dtrow["OrderDate"] = DateTime.Today;
            dtrow["TaxonomyName"] = null;
            dtrow["FormType"] = "Test";
            dtrow["CreatedDate"] = DateTime.Today;
            dtrow["DocumentType"] = "Test";
            dtrow["ChildTaxonomyAssociationId"] = 2;
            dtrow["XBRLViewerCompany"] = DBNull.Value;
            dtrow["IsXBRLViewerReady"] = DBNull.Value;



            dt.Rows.Add(dtrow);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory obj = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = obj.GetXBRLDetailsForTaxonomyID("Forethought", 1, "Test");

            List<XBRLObjectModel> lstExpected = new List<XBRLObjectModel>();
            lstExpected.Add(new XBRLObjectModel()
            {
                AccessionNumber = "Test",
                CompanyName = "Test",
                CreatedDate = DateTime.Today,
                Description = null,
                DocumentDate = DateTime.Today,
                DocumentType = "Test",
                FilingDate = DateTime.Today,
                FormType = "Test",
                IsViewerEnabled = false,
                IsViewerReadyForXBRL = false,
                Name = null,
                OrderDate = DateTime.Today,
                Path = "Test",
                TaxonomyId = 1,
                TaxonomyName = "Test",
                ZipFileName = "Test"

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

        #region GetRequestMaterialPrintRequests
        ///<summary>
        // GetRequestMaterialPrintRequests
        ///</summary>
        [TestMethod]
        public void GetRequestMaterialPrintRequests()
        {

            List<RequestMaterialPrintHistory> lstRequestMaterialPrintHistory = new List<RequestMaterialPrintHistory>();
            RequestMaterialPrintHistory objRequestMaterialPrintHistory = new RequestMaterialPrintHistory();
            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 1;
            objHostedDocumentType.ContentURI = "Test1";
            objHostedDocumentType.VerticalMarketID = "5";
            objHostedDocumentType.IsObjectinVerticalMarket = true;
            lstHostedDocumentType.Add(objHostedDocumentType);
            TaxonomyAssociationData objTaxonomyAssociationdata = new TaxonomyAssociationData();
            objTaxonomyAssociationdata.TaxonomyID = 1;
            objTaxonomyAssociationdata.DocumentTypes = lstHostedDocumentType;
            objRequestMaterialPrintHistory.TaxonomyAssociationData = objTaxonomyAssociationdata;
            lstRequestMaterialPrintHistory.Add(objRequestMaterialPrintHistory);

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("RelationshipType", typeof(Int32));
            dt.Columns.Add("Order", typeof(Int32));
            dt.Columns.Add("ContentURI", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));


            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyID"] = 1;
            dtrow["DocumentTypeId"] = 2;
            dtrow["TaxonomyName"] = "Test_001";
            dtrow["ParentTaxonomyAssociationId"] = 2;
            dtrow["ChildTaxonomyAssociationId"] = 3;
            dtrow["RelationshipType"] = 1;
            dtrow["Order"] = 2;
            dtrow["ContentURI"] = "Test";
            dtrow["VerticalMarketID"] = "5";


            dt.Rows.Add(dtrow);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory obj = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = obj.GetRequestMaterialPrintRequests(dt, "ForeThought", lstRequestMaterialPrintHistory);

            List<HostedDocumentType> lstHostedDocumentTypeexpected = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentTypeexpected = new HostedDocumentType();
            objHostedDocumentTypeexpected.DocumentTypeId = 1;
            objHostedDocumentTypeexpected.ContentURI = "Test1";
            objHostedDocumentTypeexpected.VerticalMarketID = "5";
            objHostedDocumentTypeexpected.IsObjectinVerticalMarket = true;
            lstHostedDocumentTypeexpected.Add(objHostedDocumentTypeexpected);
            

            TaxonomyAssociationData objTaxonomyAssociationData1 = new TaxonomyAssociationData()
            {
                TaxonomyID = 1,
                DocumentTypes = lstHostedDocumentTypeexpected,
                TaxonomyAssociationID = 0,
                IsObjectinVerticalMarket = true,
                TaxonomyCssClass = null,
                TaxonomyDescriptionOverride = null,
                TaxonomyName = "Test_001"

            };

            List<RequestMaterialPrintHistory> lstExpected = new List<RequestMaterialPrintHistory>();
            lstExpected.Add(new RequestMaterialPrintHistory()
            {
               Address1 = null,
               Address2 = null,
               City = null,
               ClientCompanyName = null,
               ClientFirstName = null,
               ClientFullName = null,
               ClientLastName = null,
               ClientMiddleName = null,
               IPAddress = null,
               PostalCode = null,
               Quantity = 0,
               Referer = null,
               RequestBatchId = Guid.Empty,
               RequestDateUtc = Convert.ToDateTime("1/1/0001 12:00:00 AM"),
               RequestMaterialPrintHistoryID = 0,
               RequestUriString = null,
               StateOrProvince = null,
               TaxonomyAssociationData = objTaxonomyAssociationData1,
               UniqueID = Guid.Empty,
               UserAgent = null
            

            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData<RequestMaterialPrintHistory>(lstExpected, result.ToList());
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetRequestMaterialPrintRequests_WithEmptyTaxonamyName
        ///<summary>
        // GetRequestMaterialPrintRequests
        ///</summary>
        [TestMethod]
        public void GetRequestMaterialPrintRequests_WithEmptyTaxonamyName()
        {

            List<RequestMaterialPrintHistory> lstRequestMaterialPrintHistory = new List<RequestMaterialPrintHistory>();
            RequestMaterialPrintHistory objRequestMaterialPrintHistory = new RequestMaterialPrintHistory();
            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 2;
            objHostedDocumentType.ContentURI = "Test1";
            objHostedDocumentType.VerticalMarketID = "5";
            objHostedDocumentType.IsObjectinVerticalMarket = true;
            lstHostedDocumentType.Add(objHostedDocumentType);
            TaxonomyAssociationData objTaxonomyAssociationdata = new TaxonomyAssociationData();
            objTaxonomyAssociationdata.TaxonomyID = 1;
            objTaxonomyAssociationdata.DocumentTypes = lstHostedDocumentType;
            objRequestMaterialPrintHistory.TaxonomyAssociationData = objTaxonomyAssociationdata;
            lstRequestMaterialPrintHistory.Add(objRequestMaterialPrintHistory);

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("RelationshipType", typeof(Int32));
            dt.Columns.Add("Order", typeof(Int32));
            dt.Columns.Add("ContentURI", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));


            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyID"] = 1;
            dtrow["DocumentTypeId"] = 2;
            dtrow["TaxonomyName"] = null;
            dtrow["ParentTaxonomyAssociationId"] = 2;
            dtrow["ChildTaxonomyAssociationId"] = 3;
            dtrow["RelationshipType"] = 1;
            dtrow["Order"] = 2;
            dtrow["ContentURI"] = "Test";
            dtrow["VerticalMarketID"] = "5";


            dt.Rows.Add(dtrow);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory obj = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = obj.GetRequestMaterialPrintRequests(dt, "ForeThought", lstRequestMaterialPrintHistory);

            List<HostedDocumentType> lstHostedDocumentTypeexpected = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentTypeexpected = new HostedDocumentType();
            objHostedDocumentTypeexpected.DocumentTypeId = 2;
            objHostedDocumentTypeexpected.ContentURI = "Test";
            objHostedDocumentTypeexpected.VerticalMarketID = "5";
            objHostedDocumentTypeexpected.IsObjectinVerticalMarket = true;
            lstHostedDocumentTypeexpected.Add(objHostedDocumentTypeexpected);


            TaxonomyAssociationData objTaxonomyAssociationData1 = new TaxonomyAssociationData()
            {
                TaxonomyID = 1,
                DocumentTypes = lstHostedDocumentTypeexpected,
                TaxonomyAssociationID = 0,
                IsObjectinVerticalMarket = true,
                TaxonomyCssClass = null,
                TaxonomyDescriptionOverride = null,
                TaxonomyName = null

            };

            List<RequestMaterialPrintHistory> lstExpected = new List<RequestMaterialPrintHistory>();
            lstExpected.Add(new RequestMaterialPrintHistory()
            {
                Address1 = null,
                Address2 = null,
                City = null,
                ClientCompanyName = null,
                ClientFirstName = null,
                ClientFullName = null,
                ClientLastName = null,
                ClientMiddleName = null,
                IPAddress = null,
                PostalCode = null,
                Quantity = 0,
                Referer = null,
                RequestBatchId = Guid.Empty,
                RequestDateUtc = Convert.ToDateTime("1/1/0001 12:00:00 AM"),
                RequestMaterialPrintHistoryID = 0,
                RequestUriString = null,
                StateOrProvince = null,
                TaxonomyAssociationData = objTaxonomyAssociationData1,
                UniqueID = Guid.Empty,
                UserAgent = null


            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData<RequestMaterialPrintHistory>(lstExpected, result.ToList());
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetRequestMaterialPrintRequests_WithNotEqualPreviousId
        ///<summary>
        // GetRequestMaterialPrintRequests
        ///</summary>
        [TestMethod]
        public void GetRequestMaterialPrintRequests_WithNotEqualPreviousId()
        {

            List<RequestMaterialPrintHistory> lstRequestMaterialPrintHistory = new List<RequestMaterialPrintHistory>();
            RequestMaterialPrintHistory objRequestMaterialPrintHistory = new RequestMaterialPrintHistory();
            List<HostedDocumentType> lstHostedDocumentType = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentType = new HostedDocumentType();
            objHostedDocumentType.DocumentTypeId = 5;
            objHostedDocumentType.ContentURI = "Test1";
            objHostedDocumentType.VerticalMarketID = "5";
            objHostedDocumentType.IsObjectinVerticalMarket = true;
            lstHostedDocumentType.Add(objHostedDocumentType);
            TaxonomyAssociationData objTaxonomyAssociationdata = new TaxonomyAssociationData();
            objTaxonomyAssociationdata.TaxonomyID = 1;
            objTaxonomyAssociationdata.DocumentTypes = lstHostedDocumentType;
            objRequestMaterialPrintHistory.TaxonomyAssociationData = objTaxonomyAssociationdata;
            lstRequestMaterialPrintHistory.Add(objRequestMaterialPrintHistory);

            DataTable dt = new DataTable();
            dt.Columns.Add("SiteName", typeof(string));
            dt.Columns.Add("TaxonomyID", typeof(Int32));
            dt.Columns.Add("DocumentTypeId", typeof(Int32));
            dt.Columns.Add("TaxonomyName", typeof(string));
            dt.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            dt.Columns.Add("RelationshipType", typeof(Int32));
            dt.Columns.Add("Order", typeof(Int32));
            dt.Columns.Add("ContentURI", typeof(string));
            dt.Columns.Add("VerticalMarketID", typeof(string));


            DataRow dtrow = dt.NewRow();
            dtrow["SiteName"] = "ForeThought";
            dtrow["TaxonomyID"] = 1;
            dtrow["DocumentTypeId"] = 2;
            dtrow["TaxonomyName"] = "Test_001";
            dtrow["ParentTaxonomyAssociationId"] = 2;
            dtrow["ChildTaxonomyAssociationId"] = 3;
            dtrow["RelationshipType"] = 1;
            dtrow["Order"] = 2;
            dtrow["ContentURI"] = "Test";
            dtrow["VerticalMarketID"] = "5";

            DataRow dtrow1 = dt.NewRow();
            dtrow1["SiteName"] = "ForeThought";
            dtrow1["TaxonomyID"] = 1;
            dtrow1["DocumentTypeId"] = 2;
            dtrow1["TaxonomyName"] = "Test_001";
            dtrow1["ParentTaxonomyAssociationId"] = 2;
            dtrow1["ChildTaxonomyAssociationId"] = 3;
            dtrow1["RelationshipType"] = 1;
            dtrow1["Order"] = 2;
            dtrow1["ContentURI"] = "Test";
            dtrow1["VerticalMarketID"] = "5";


            dt.Rows.Add(dtrow1);
            ClientData();

            mockDataAccess.Setup(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>())).Returns(dt);

            //Act
            HostedVerticalBasePageScenariosFactory obj = new HostedVerticalBasePageScenariosFactory(mockDataAccess.Object);

            var result = obj.GetRequestMaterialPrintRequests(dt, "ForeThought", lstRequestMaterialPrintHistory);

            List<HostedDocumentType> lstHostedDocumentTypeexpected = new List<HostedDocumentType>();
            HostedDocumentType objHostedDocumentTypeexpected = new HostedDocumentType();
            objHostedDocumentTypeexpected.DocumentTypeId = 5;
            objHostedDocumentTypeexpected.ContentURI = "Test1";
            objHostedDocumentTypeexpected.VerticalMarketID = "5";
            objHostedDocumentTypeexpected.IsObjectinVerticalMarket = true;
            lstHostedDocumentTypeexpected.Add(objHostedDocumentTypeexpected);


            TaxonomyAssociationData objTaxonomyAssociationData1 = new TaxonomyAssociationData()
            {
                TaxonomyID = 1,
                DocumentTypes = lstHostedDocumentTypeexpected,
                TaxonomyAssociationID = 0,
                IsObjectinVerticalMarket = true,
                TaxonomyCssClass = null,
                TaxonomyDescriptionOverride = null,
                TaxonomyName = "Test_001"

            };

            List<RequestMaterialPrintHistory> lstExpected = new List<RequestMaterialPrintHistory>();
            lstExpected.Add(new RequestMaterialPrintHistory()
            {
                Address1 = null,
                Address2 = null,
                City = null,
                ClientCompanyName = null,
                ClientFirstName = null,
                ClientFullName = null,
                ClientLastName = null,
                ClientMiddleName = null,
                IPAddress = null,
                PostalCode = null,
                Quantity = 0,
                Referer = null,
                RequestBatchId = Guid.Empty,
                RequestDateUtc = Convert.ToDateTime("1/1/0001 12:00:00 AM"),
                RequestMaterialPrintHistoryID = 0,
                RequestUriString = null,
                StateOrProvince = null,
                TaxonomyAssociationData = objTaxonomyAssociationData1,
                UniqueID = Guid.Empty,
                UserAgent = null


            });

            List<string> lstExclude = new List<string>();
            lstExclude.Add("ModifiedBy");
            lstExclude.Add("ModifiedDate");
            lstExclude.Add("LastModified");
            ValidateListData<RequestMaterialPrintHistory>(lstExpected, result.ToList());
            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion
    }
}
