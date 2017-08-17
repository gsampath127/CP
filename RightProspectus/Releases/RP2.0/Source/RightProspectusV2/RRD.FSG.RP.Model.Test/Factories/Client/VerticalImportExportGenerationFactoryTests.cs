using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Cache;
using RRD.FSG.RP.Model.Cache.System;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.SortDetail.Client;
using System.Xml.Linq;
using Moq;
using System.Data.Common;



namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for TemplatePageFactory class
    /// </summary>
    [TestClass]
    public class VerticalImportExportGenerationFactoryTests 
    {
        private Mock<IDataAccess> mockDataAccess;

        [TestInitialize]
        public void TestInitialize()
        {
            mockDataAccess = new Mock<IDataAccess>();
        }

        [TestMethod]
        public void TestVerticalExport()
        {
            VerticalImportExportGenerationFactory export = new VerticalImportExportGenerationFactory(new DataAccess());
            export.ClientName = "GlobalAtlantic";
           XDocument doc = export.GenerateExportXML();
        }


        [TestMethod]
        public void TestVerticalImport()
        {
            VerticalXmlImportFactory importfactory = new VerticalXmlImportFactory(new DataAccess());
            importfactory.ClientName = "AMGN";
            importfactory.DequeueAndLoadImportXML();
        }

        #region LoadDataFromImportXML_returns_bool_VerticalImportExportGenerationFactory
        /// <summary>
        /// LoadDataFromImportXML_returns_bool_VerticalImportExportGenerationFactory
        /// </summary>
       
        [TestMethod]
        public void LoadDataFromImportXML_returns_bool_VerticalImportExportGenerationFactory()
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

            bool isBackup = false;
            int importedBy = 1;
            int verticalXmlImportId = 1;

           VerticalImportExportGenerationFactory factory = new VerticalImportExportGenerationFactory(mockDataAccess.Object);

            factory.ClientName = "Forethought";

            SiteSortDetail objSortDtl = new SiteSortDetail();
            objSortDtl.Column = SiteSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable tblSiteData = new DataTable();
            DataColumn SiteId = new DataColumn();
            SiteId.DataType = typeof(Int32);
            SiteId.ColumnName = "SiteId";
            SiteId.AllowDBNull = true;
            tblSiteData.Columns.Add(SiteId);
            tblSiteData.Columns.Add("NAME", typeof(string));
            tblSiteData.Columns.Add("TemplateId", typeof(Int32));
            tblSiteData.Columns.Add("DefaultPageID", typeof(Int32));
            tblSiteData.Columns.Add("ParentSiteId", typeof(Int32));
            tblSiteData.Columns.Add("Description", typeof(string));
            tblSiteData.Columns.Add("UtcLastModified", typeof(DateTime));
            tblSiteData.Columns.Add("ModifiedBy", typeof(Int32));
            tblSiteData.Columns.Add("IsDefaultSite" ,typeof(bool));
           
           for(int i= 0;i<5;i++)
            {
                DataRow rowSiteData = tblSiteData.NewRow();
              
                rowSiteData["SiteId"] = i;
               if(i==-1)
               {
                   rowSiteData["NAME"] = DBNull.Value;// i==-1? DBNull.Value:("Forethought"+i);
               }
               else
               {
                   rowSiteData["NAME"] = "Forethought"+i;
               }
               
                rowSiteData["TemplateId"] = 1;
                rowSiteData["DefaultPageID"] = 1;
                rowSiteData["ParentSiteId"] = DBNull.Value;
                rowSiteData["Description"] = "Forethought Site"+i;
                rowSiteData["UtcLastModified"] = DateTime.Now;
                rowSiteData["ModifiedBy"] = i;
                rowSiteData["IsDefaultSite"] = false;
                tblSiteData.Rows.Add(rowSiteData);
           }
           

            DataTable tblPageData = new DataTable();
            tblPageData.Columns.Add("TemplateId", typeof(Int32));
            tblPageData.Columns.Add("TemplateName", typeof(string));
            tblPageData.Columns.Add("PageID", typeof(Int32));
            tblPageData.Columns.Add("PageName", typeof(string));
            tblPageData.Columns.Add("PageDescription", typeof(string));

            DataRow rowPageData = tblPageData.NewRow();
            rowPageData["TemplateId"] = 1;
            rowPageData["TemplateName"] = "Default";
            rowPageData["PageID"] = 1;
            rowPageData["PageName"] = "TAL";
            rowPageData["PageDescription"] = "Taxonomy Association Link";

            tblPageData.Rows.Add(rowPageData);

            DataTable tblTaxonomyData = new DataTable();
            tblTaxonomyData.Columns.Add("level", typeof(Int32));
            tblTaxonomyData.Columns.Add("marketId", typeof(string));
            tblTaxonomyData.Columns.Add("TaxonomyID", typeof(Int32));
            

            DataRow rowTaxonomyData = tblTaxonomyData.NewRow();
            rowTaxonomyData["level"] = 1;
            rowTaxonomyData["marketId"] = "US";
            rowTaxonomyData["TaxonomyID"] = 1;
            tblTaxonomyData.Rows.Add(rowTaxonomyData);

            DataTable dtt7 = new DataTable();

            dtt7.Columns.Add("MarketID", typeof(string));
            DataRow dttrow7 = dtt7.NewRow();
            dttrow7["MarketID"] = "US";

            dtt7.Rows.Add(dttrow7);



            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                          .Returns(tblSiteData)
                          . Returns(tblPageData)
                         . Returns(tblTaxonomyData)
                          .Returns(dtt7);
          

            int result = factory.LoadDataFromImportXML(XDocument.Load(@"Factories\UnitTestDocs\VerticalImportExportGenerationFactory.xml"),
                    importedBy,
                    isBackup,
                    verticalXmlImportId);
            
            //Assert
            Assert.AreEqual(result, 2);
        }
        #endregion

        #region GenerateExportXML_WithInvalidData_returns_XDocument
        /// <summary>
        /// GenerateExportXML returns XDocument
        /// </summary>
        /// 
        [TestMethod]
        public void GenerateExportXML_WithInvalidData_returns_XDocument()
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

            VerticalImportExportGenerationFactory factory = new VerticalImportExportGenerationFactory(mockDataAccess.Object);

            factory.ClientName = "Forethought";

            SiteSortDetail objSortDtl = new SiteSortDetail();
            objSortDtl.Column = SiteSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;

            DataTable tblSiteData = new DataTable();
            DataColumn SiteId = new DataColumn();
            SiteId.DataType = typeof(Int32);
            SiteId.ColumnName = "SiteId";
            SiteId.AllowDBNull = true;
            tblSiteData.Columns.Add(SiteId);
            tblSiteData.Columns.Add("NAME", typeof(string));
            tblSiteData.Columns.Add("TemplateId", typeof(Int32));
            tblSiteData.Columns.Add("DefaultPageID", typeof(Int32));
            tblSiteData.Columns.Add("ParentSiteId", typeof(Int32));
            tblSiteData.Columns.Add("Description", typeof(string));
            tblSiteData.Columns.Add("UtcLastModified", typeof(DateTime));
            tblSiteData.Columns.Add("ModifiedBy", typeof(Int32));
            tblSiteData.Columns.Add("IsDefaultSite", typeof(bool));

            for (int i = 1; i < 2; i++)
            {
                DataRow rowSiteData = tblSiteData.NewRow();

                rowSiteData["SiteId"] = i;
                if (i == 2)
                {
                    rowSiteData["NAME"] = DBNull.Value;// i==-1? DBNull.Value:("Forethought"+i);
                    rowSiteData["Description"] = "";
                }
                else
                {
                    rowSiteData["NAME"] = "Forethought" + i;
                }

                rowSiteData["TemplateId"] = 1;
                rowSiteData["DefaultPageID"] = 1;
                rowSiteData["ParentSiteId"] = DBNull.Value;
                rowSiteData["Description"] = "Forethought Site" + i;
                rowSiteData["UtcLastModified"] = DateTime.Now;
                rowSiteData["ModifiedBy"] = i;
                rowSiteData["IsDefaultSite"] = false;
                tblSiteData.Rows.Add(rowSiteData);
            }

            DataTable tblPageData = new DataTable();
            tblPageData.Columns.Add("TemplateId", typeof(Int32));
            tblPageData.Columns.Add("TemplateName", typeof(string));
            tblPageData.Columns.Add("PageID", typeof(Int32));
            tblPageData.Columns.Add("PageName", typeof(string));
            tblPageData.Columns.Add("PageDescription", typeof(string));

            DataRow rowPageData = tblPageData.NewRow();
            rowPageData["TemplateId"] = 1;
            rowPageData["TemplateName"] = "Default";
            rowPageData["PageID"] = 1;
            rowPageData["PageName"] = "TAL";
            rowPageData["PageDescription"] = "Taxonomy Association Link";

            tblPageData.Rows.Add(rowPageData);

            DataTable tblDocumentTypeAssc = new DataTable();
            tblDocumentTypeAssc.Columns.Add("DocumentTypeAssociationId", typeof(int));
            tblDocumentTypeAssc.Columns.Add("DocumentTypeId", typeof(int));
            tblDocumentTypeAssc.Columns.Add("SiteId", typeof(int));
            tblDocumentTypeAssc.Columns.Add("TaxonomyAssociationId", typeof(int));
            tblDocumentTypeAssc.Columns.Add("Order", typeof(int));
            tblDocumentTypeAssc.Columns.Add("HeaderText", typeof(string));
            tblDocumentTypeAssc.Columns.Add("LinkText", typeof(string));
            tblDocumentTypeAssc.Columns.Add("DescriptionOverride", typeof(string));
            tblDocumentTypeAssc.Columns.Add("CssClass", typeof(string));
            tblDocumentTypeAssc.Columns.Add("MarketId", typeof(string));
            tblDocumentTypeAssc.Columns.Add("UtcLastModified", typeof(DateTime));
            tblDocumentTypeAssc.Columns.Add("ModifiedBy", typeof(int));

            DataRow rowDocumentTypeAsc = tblDocumentTypeAssc.NewRow();
            rowDocumentTypeAsc["DocumentTypeAssociationId"] = 23;
            rowDocumentTypeAsc["DocumentTypeId"] = 34;
            rowDocumentTypeAsc["SiteId"] = 2;
            rowDocumentTypeAsc["TaxonomyAssociationId"] = 9;
            rowDocumentTypeAsc["Order"] = 223;
            rowDocumentTypeAsc["HeaderText"] = "tblDocumentTypeAsscA";
            rowDocumentTypeAsc["LinkText"] = "Test_1";
            rowDocumentTypeAsc["DescriptionOverride"] = "Doc_test";
            rowDocumentTypeAsc["CssClass"] = "Test_css";
            rowDocumentTypeAsc["MarketId"] = "TEST_Mrkt";
            rowDocumentTypeAsc["UtcLastModified"] = DateTime.Today;
            rowDocumentTypeAsc["ModifiedBy"] = 32;
            tblDocumentTypeAssc.Rows.Add(rowDocumentTypeAsc);

            DataTable tblTaxonomyAsscData = new DataTable();
            tblTaxonomyAsscData.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            tblTaxonomyAsscData.Columns.Add("Level", typeof(Int32));
            tblTaxonomyAsscData.Columns.Add("TaxonomyId", typeof(Int32));
            tblTaxonomyAsscData.Columns.Add("SiteId", typeof(Int32));
            tblTaxonomyAsscData.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            tblTaxonomyAsscData.Columns.Add("NameOverride", typeof(string));
            tblTaxonomyAsscData.Columns.Add("DescriptionOverride", typeof(string));
            tblTaxonomyAsscData.Columns.Add("CssClass", typeof(string));
            tblTaxonomyAsscData.Columns.Add("MarketId", typeof(string));
            tblTaxonomyAsscData.Columns.Add("UtcLastModified", typeof(DateTime));
            tblTaxonomyAsscData.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow rowTaxonomyAsscData = tblTaxonomyAsscData.NewRow();
            rowTaxonomyAsscData["TaxonomyAssociationId"] = 9;
            rowTaxonomyAsscData["Level"] = 1;
            rowTaxonomyAsscData["TaxonomyId"] = 1;
            rowTaxonomyAsscData["SiteId"] = 2;
            rowTaxonomyAsscData["ParentTaxonomyAssociationId"] = 1;
            rowTaxonomyAsscData["NameOverride"] = "";
            rowTaxonomyAsscData["DescriptionOverride"] = "";
            rowTaxonomyAsscData["MarketId"] = "1";
            rowTaxonomyAsscData["CssClass"] = "test.css";
            rowTaxonomyAsscData["UtcLastModified"] = DateTime.Now;
            rowTaxonomyAsscData["ModifiedBy"] = 1;
            tblTaxonomyAsscData.Rows.Add(rowTaxonomyAsscData);

            DataTable tblTaxonomyAsscHierarchy = new DataTable();
            tblTaxonomyAsscHierarchy.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            tblTaxonomyAsscHierarchy.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            tblTaxonomyAsscHierarchy.Columns.Add("RelationshipType", typeof(int));
            tblTaxonomyAsscHierarchy.Columns.Add("Order", typeof(Int32));
            tblTaxonomyAsscHierarchy.Columns.Add("UtcModifiedDate", typeof(DateTime));
            tblTaxonomyAsscHierarchy.Columns.Add("ModifiedBy", typeof(Int32));
            tblTaxonomyAsscHierarchy.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow rowTaxonomyAsscHierarchy = tblTaxonomyAsscHierarchy.NewRow();
            rowTaxonomyAsscHierarchy["ParentTaxonomyAssociationId"] = 9;
            rowTaxonomyAsscHierarchy["ChildTaxonomyAssociationId"] = 9;
            rowTaxonomyAsscHierarchy["RelationshipType"] = 33;
            rowTaxonomyAsscHierarchy["Order"] = 59;
            rowTaxonomyAsscHierarchy["UtcModifiedDate"] = DateTime.Today;
            rowTaxonomyAsscHierarchy["ModifiedBy"] = 32;
            rowTaxonomyAsscHierarchy["UtcLastModified"] = DateTime.Today;
            tblTaxonomyAsscHierarchy.Rows.Add(rowTaxonomyAsscHierarchy);

            //Arrange  
            DataTable dtFootnote = new DataTable();
            dtFootnote.Columns.Add("FootnoteId", typeof(Int32));
            dtFootnote.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dtFootnote.Columns.Add("TaxonomyAssociationGroupId", typeof(Int32));
            dtFootnote.Columns.Add("LanguageCulture", typeof(string));
            dtFootnote.Columns.Add("Text", typeof(string));
            dtFootnote.Columns.Add("Order", typeof(Int32));
            dtFootnote.Columns.Add("UtcLastModified", typeof(DateTime));
            dtFootnote.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrowFootnote = dtFootnote.NewRow();
            dtrowFootnote["FootnoteId"] = 1;
            dtrowFootnote["TaxonomyAssociationId"] = 9 ;
            dtrowFootnote["TaxonomyAssociationGroupId"] = DBNull.Value;
            dtrowFootnote["LanguageCulture"] = null;
            dtrowFootnote["Text"] = "Footnote for American Century VP Growth Fund";
            dtrowFootnote["Order"] = 1;
            dtrowFootnote["UtcLastModified"] = DateTime.Now;
            dtrowFootnote["ModifiedBy"] = 1;
            dtFootnote.Rows.Add(dtrowFootnote);

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                        .Returns(tblSiteData)
                        .Returns(tblPageData)
                        .Returns(tblDocumentTypeAssc)
                        .Returns(tblTaxonomyAsscData)
                        .Returns(tblTaxonomyAsscHierarchy)
                        .Returns(dtFootnote);
            var result = factory.GenerateExportXML();

            Assert.IsInstanceOfType(result, typeof(XDocument));
        }
        [TestMethod]
        public void GenerateExportXML_returns_XDocument()
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

            VerticalImportExportGenerationFactory factory = new VerticalImportExportGenerationFactory(mockDataAccess.Object);

            factory.ClientName = "Forethought";

            SiteSortDetail objSortDtl = new SiteSortDetail();
            objSortDtl.Column = SiteSortColumn.Name;
            objSortDtl.Order = SortOrder.Ascending;


            DataTable tblSiteData = new DataTable();
            DataColumn SiteId = new DataColumn();
            SiteId.DataType = typeof(Int32);
            SiteId.ColumnName = "SiteId";
            SiteId.AllowDBNull = true;
            tblSiteData.Columns.Add(SiteId);
            tblSiteData.Columns.Add("NAME", typeof(string));
            tblSiteData.Columns.Add("TemplateId", typeof(Int32));
            tblSiteData.Columns.Add("DefaultPageID", typeof(Int32));
            tblSiteData.Columns.Add("ParentSiteId", typeof(Int32));
            tblSiteData.Columns.Add("Description", typeof(string));
            tblSiteData.Columns.Add("UtcLastModified", typeof(DateTime));
            tblSiteData.Columns.Add("ModifiedBy", typeof(Int32));
            tblSiteData.Columns.Add("IsDefaultSite", typeof(bool));

            for (int i = 1; i < 2; i++)
            {
                DataRow rowSiteData = tblSiteData.NewRow();

                rowSiteData["SiteId"] = i;
                if (i == 2)
                {
                    rowSiteData["NAME"] = DBNull.Value;// i==-1? DBNull.Value:("Forethought"+i);
                    rowSiteData["Description"] = "";
                }
                else
                {
                    rowSiteData["NAME"] = "Forethought" + i;
                }

                rowSiteData["TemplateId"] = 1;
                rowSiteData["DefaultPageID"] = 1;
                rowSiteData["ParentSiteId"] = DBNull.Value;
                rowSiteData["Description"] = "Forethought Site" + i;
                rowSiteData["UtcLastModified"] = DateTime.Now;
                rowSiteData["ModifiedBy"] = i;
                rowSiteData["IsDefaultSite"] = false;
                tblSiteData.Rows.Add(rowSiteData);
            }

            DataTable tblPageData = new DataTable();
            tblPageData.Columns.Add("TemplateId", typeof(Int32));
            tblPageData.Columns.Add("TemplateName", typeof(string));
            tblPageData.Columns.Add("PageID", typeof(Int32));
            tblPageData.Columns.Add("PageName", typeof(string));
            tblPageData.Columns.Add("PageDescription", typeof(string));

            DataRow rowPageData = tblPageData.NewRow();
            rowPageData["TemplateId"] = 1;
            rowPageData["TemplateName"] = "Default";
            rowPageData["PageID"] = 1;
            rowPageData["PageName"] = "TAL";
            rowPageData["PageDescription"] = "Taxonomy Association Link";

            tblPageData.Rows.Add(rowPageData);

            DataTable tblDocumentTypeAssc = new DataTable();
            tblDocumentTypeAssc.Columns.Add("DocumentTypeAssociationId", typeof(int));
            tblDocumentTypeAssc.Columns.Add("DocumentTypeId", typeof(int));
            tblDocumentTypeAssc.Columns.Add("SiteId", typeof(int));
            tblDocumentTypeAssc.Columns.Add("TaxonomyAssociationId", typeof(int));
            tblDocumentTypeAssc.Columns.Add("Order", typeof(int));
            tblDocumentTypeAssc.Columns.Add("HeaderText", typeof(string));
            tblDocumentTypeAssc.Columns.Add("LinkText", typeof(string));
            tblDocumentTypeAssc.Columns.Add("DescriptionOverride", typeof(string));
            tblDocumentTypeAssc.Columns.Add("CssClass", typeof(string));
            tblDocumentTypeAssc.Columns.Add("MarketId", typeof(string));
            tblDocumentTypeAssc.Columns.Add("UtcLastModified", typeof(DateTime));
            tblDocumentTypeAssc.Columns.Add("ModifiedBy", typeof(int));

            DataRow rowDocumentTypeAsc1 = tblDocumentTypeAssc.NewRow();
            rowDocumentTypeAsc1["DocumentTypeAssociationId"] = 23;
            rowDocumentTypeAsc1["DocumentTypeId"] = 34;
            rowDocumentTypeAsc1["SiteId"] = 1;
            rowDocumentTypeAsc1["TaxonomyAssociationId"] = 8;
            rowDocumentTypeAsc1["Order"] = DBNull.Value;
            rowDocumentTypeAsc1["HeaderText"] = DBNull.Value;
            rowDocumentTypeAsc1["LinkText"] = DBNull.Value;
            rowDocumentTypeAsc1["DescriptionOverride"] = DBNull.Value;
            rowDocumentTypeAsc1["CssClass"] = DBNull.Value;
            rowDocumentTypeAsc1["MarketId"] = DBNull.Value;
            rowDocumentTypeAsc1["UtcLastModified"] = DateTime.Today;
            rowDocumentTypeAsc1["ModifiedBy"] = 32;
            tblDocumentTypeAssc.Rows.Add(rowDocumentTypeAsc1);
            DataRow rowDocumentTypeAsc2 = tblDocumentTypeAssc.NewRow();
            rowDocumentTypeAsc2["DocumentTypeAssociationId"] = 24;
            rowDocumentTypeAsc2["DocumentTypeId"] = 35;
            rowDocumentTypeAsc2["SiteId"] = 1;
            rowDocumentTypeAsc2["TaxonomyAssociationId"] = 9;
            rowDocumentTypeAsc2["Order"] = 0;
            rowDocumentTypeAsc2["HeaderText"] = "";
            rowDocumentTypeAsc2["LinkText"] = "";
            rowDocumentTypeAsc2["DescriptionOverride"] = "";
            rowDocumentTypeAsc2["CssClass"] = "";
            rowDocumentTypeAsc2["MarketId"] = "";
            rowDocumentTypeAsc2["UtcLastModified"] = DateTime.Today;
            rowDocumentTypeAsc2["ModifiedBy"] = 32;
            tblDocumentTypeAssc.Rows.Add(rowDocumentTypeAsc2);
            DataRow rowDocumentTypeAsc3 = tblDocumentTypeAssc.NewRow();
            rowDocumentTypeAsc3["DocumentTypeAssociationId"] = 26;
            rowDocumentTypeAsc3["DocumentTypeId"] = 36;
            rowDocumentTypeAsc3["SiteId"] = 1;
            rowDocumentTypeAsc3["TaxonomyAssociationId"] = 10;
            rowDocumentTypeAsc3["Order"] = 223;
            rowDocumentTypeAsc3["HeaderText"] = "tblDocumentTypeAsscA";
            rowDocumentTypeAsc3["LinkText"] = "Test_1";
            rowDocumentTypeAsc3["DescriptionOverride"] = "Doc_test";
            rowDocumentTypeAsc3["CssClass"] = "Test_css";
            rowDocumentTypeAsc3["MarketId"] = "TEST_Mrkt";
            rowDocumentTypeAsc3["UtcLastModified"] = DateTime.Today;
            rowDocumentTypeAsc3["ModifiedBy"] = 32;
            tblDocumentTypeAssc.Rows.Add(rowDocumentTypeAsc3);

            DataTable tblTaxonomyAsscData = new DataTable();
            tblTaxonomyAsscData.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            tblTaxonomyAsscData.Columns.Add("Level", typeof(Int32));
            tblTaxonomyAsscData.Columns.Add("TaxonomyId", typeof(Int32));
            tblTaxonomyAsscData.Columns.Add("SiteId", typeof(Int32));
            tblTaxonomyAsscData.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            tblTaxonomyAsscData.Columns.Add("NameOverride", typeof(string));
            tblTaxonomyAsscData.Columns.Add("DescriptionOverride", typeof(string));
            tblTaxonomyAsscData.Columns.Add("CssClass", typeof(string));
            tblTaxonomyAsscData.Columns.Add("MarketId", typeof(string));
            tblTaxonomyAsscData.Columns.Add("UtcLastModified", typeof(DateTime));
            tblTaxonomyAsscData.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow rowTaxonomyAsscData = tblTaxonomyAsscData.NewRow();
            rowTaxonomyAsscData["TaxonomyAssociationId"] = 8;
            rowTaxonomyAsscData["Level"] = 1;
            rowTaxonomyAsscData["TaxonomyId"] = 1;
            rowTaxonomyAsscData["SiteId"] = 1;
            rowTaxonomyAsscData["ParentTaxonomyAssociationId"] = 1;
            rowTaxonomyAsscData["NameOverride"] = "test";
            rowTaxonomyAsscData["DescriptionOverride"] = DBNull.Value;
            rowTaxonomyAsscData["MarketId"] = DBNull.Value;
            rowTaxonomyAsscData["CssClass"] = DBNull.Value;
            rowTaxonomyAsscData["UtcLastModified"] = DateTime.Now;
            rowTaxonomyAsscData["ModifiedBy"] = 1;
            tblTaxonomyAsscData.Rows.Add(rowTaxonomyAsscData);

            DataRow rowTaxonomyAsscData1 = tblTaxonomyAsscData.NewRow();
            rowTaxonomyAsscData1["TaxonomyAssociationId"] = 9;
            rowTaxonomyAsscData1["Level"] = 1;
            rowTaxonomyAsscData1["TaxonomyId"] = 1;
            rowTaxonomyAsscData1["SiteId"] = 1;
            rowTaxonomyAsscData1["ParentTaxonomyAssociationId"] = 1;
            rowTaxonomyAsscData1["NameOverride"] = "test";
            rowTaxonomyAsscData1["DescriptionOverride"] = "";
            rowTaxonomyAsscData1["MarketId"] = "";
            rowTaxonomyAsscData1["CssClass"] = "";
            rowTaxonomyAsscData1["UtcLastModified"] = DateTime.Now;
            rowTaxonomyAsscData1["ModifiedBy"] = 1;
            tblTaxonomyAsscData.Rows.Add(rowTaxonomyAsscData1);

            DataRow rowTaxonomyAsscData2 = tblTaxonomyAsscData.NewRow();
            rowTaxonomyAsscData2["TaxonomyAssociationId"] = 10;
            rowTaxonomyAsscData2["Level"] = 1;
            rowTaxonomyAsscData2["TaxonomyId"] = 1;
            rowTaxonomyAsscData2["SiteId"] = 1;
            rowTaxonomyAsscData2["ParentTaxonomyAssociationId"] = 1;
            rowTaxonomyAsscData2["NameOverride"] = "test";
            rowTaxonomyAsscData2["DescriptionOverride"] = "test_Desc";
            rowTaxonomyAsscData2["MarketId"] = "1";
            rowTaxonomyAsscData2["CssClass"] = "test.css";
            rowTaxonomyAsscData2["UtcLastModified"] = DateTime.Now;
            rowTaxonomyAsscData2["ModifiedBy"] = 1;
            tblTaxonomyAsscData.Rows.Add(rowTaxonomyAsscData2);

            DataTable tblTaxonomyAsscHierarchy = new DataTable();
            tblTaxonomyAsscHierarchy.Columns.Add("ParentTaxonomyAssociationId", typeof(Int32));
            tblTaxonomyAsscHierarchy.Columns.Add("ChildTaxonomyAssociationId", typeof(Int32));
            tblTaxonomyAsscHierarchy.Columns.Add("RelationshipType", typeof(int));
            tblTaxonomyAsscHierarchy.Columns.Add("Order", typeof(Int32));
            tblTaxonomyAsscHierarchy.Columns.Add("UtcModifiedDate", typeof(DateTime));
            tblTaxonomyAsscHierarchy.Columns.Add("ModifiedBy", typeof(Int32));
            tblTaxonomyAsscHierarchy.Columns.Add("UtcLastModified", typeof(DateTime));

            DataRow rowTaxonomyAsscHierarchy = tblTaxonomyAsscHierarchy.NewRow();
            rowTaxonomyAsscHierarchy["ParentTaxonomyAssociationId"] = 8;
            rowTaxonomyAsscHierarchy["ChildTaxonomyAssociationId"] = 8;
            rowTaxonomyAsscHierarchy["RelationshipType"] = 33;
            rowTaxonomyAsscHierarchy["Order"] = 59;
            rowTaxonomyAsscHierarchy["UtcModifiedDate"] = DateTime.Today;
            rowTaxonomyAsscHierarchy["ModifiedBy"] = 32;
            rowTaxonomyAsscHierarchy["UtcLastModified"] = DateTime.Today;
            tblTaxonomyAsscHierarchy.Rows.Add(rowTaxonomyAsscHierarchy);
            DataRow rowTaxonomyAsscHierarchy1 = tblTaxonomyAsscHierarchy.NewRow();
            rowTaxonomyAsscHierarchy1["ParentTaxonomyAssociationId"] = 9;
            rowTaxonomyAsscHierarchy1["ChildTaxonomyAssociationId"] = 9;
            rowTaxonomyAsscHierarchy1["RelationshipType"] = 33;
            rowTaxonomyAsscHierarchy1["Order"] = 60;
            rowTaxonomyAsscHierarchy1["UtcModifiedDate"] = DateTime.Today;
            rowTaxonomyAsscHierarchy1["ModifiedBy"] = 32;
            rowTaxonomyAsscHierarchy1["UtcLastModified"] = DateTime.Today;
            tblTaxonomyAsscHierarchy.Rows.Add(rowTaxonomyAsscHierarchy1);
            DataRow rowTaxonomyAsscHierarchy2 = tblTaxonomyAsscHierarchy.NewRow();
            rowTaxonomyAsscHierarchy2["ParentTaxonomyAssociationId"] = 10;
            rowTaxonomyAsscHierarchy2["ChildTaxonomyAssociationId"] = 10;
            rowTaxonomyAsscHierarchy2["RelationshipType"] = 33;
            rowTaxonomyAsscHierarchy2["Order"] = 61;
            rowTaxonomyAsscHierarchy2["UtcModifiedDate"] = DateTime.Today;
            rowTaxonomyAsscHierarchy2["ModifiedBy"] = 32;
            rowTaxonomyAsscHierarchy2["UtcLastModified"] = DateTime.Today;
            tblTaxonomyAsscHierarchy.Rows.Add(rowTaxonomyAsscHierarchy2);

            //Arrange  
            DataTable dtFootnote = new DataTable();
            dtFootnote.Columns.Add("FootnoteId", typeof(Int32));
            dtFootnote.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dtFootnote.Columns.Add("TaxonomyAssociationGroupId", typeof(Int32));
            dtFootnote.Columns.Add("LanguageCulture", typeof(string));
            dtFootnote.Columns.Add("Text", typeof(string));
            dtFootnote.Columns.Add("Order", typeof(Int32));
            dtFootnote.Columns.Add("UtcLastModified", typeof(DateTime));
            dtFootnote.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrowFootnote = dtFootnote.NewRow();
            dtrowFootnote["FootnoteId"] = 1;
            dtrowFootnote["TaxonomyAssociationId"] = 8;
            dtrowFootnote["TaxonomyAssociationGroupId"] = DBNull.Value;
            dtrowFootnote["LanguageCulture"] = null;
            dtrowFootnote["Text"] = "Footnote for American Century VP Growth Fund";
            dtrowFootnote["Order"] = 1;
            dtrowFootnote["UtcLastModified"] = DateTime.Now;
            dtrowFootnote["ModifiedBy"] = 1;
            dtFootnote.Rows.Add(dtrowFootnote);

            DataRow dtrowFootnote1 = dtFootnote.NewRow();
            dtrowFootnote1["FootnoteId"] = 1;
            dtrowFootnote1["TaxonomyAssociationId"] = 9;
            dtrowFootnote1["TaxonomyAssociationGroupId"] = 10;
            dtrowFootnote1["LanguageCulture"] = DBNull.Value;
            dtrowFootnote1["Text"] = DBNull.Value;
            dtrowFootnote1["Order"] = DBNull.Value;
            dtrowFootnote1["UtcLastModified"] = DateTime.Now;
            dtrowFootnote1["ModifiedBy"] = 1;
            dtFootnote.Rows.Add(dtrowFootnote1);

            DataRow dtrowFootnote2 = dtFootnote.NewRow();
            dtrowFootnote2["FootnoteId"] = 2;
            dtrowFootnote2["TaxonomyAssociationId"] = 9;
            dtrowFootnote2["TaxonomyAssociationGroupId"] = 10;
            dtrowFootnote2["LanguageCulture"] = "";
            dtrowFootnote2["Text"] = "";
            dtrowFootnote2["Order"] = DBNull.Value;
            dtrowFootnote2["UtcLastModified"] = DateTime.Now;
            dtrowFootnote2["ModifiedBy"] = 1;
            dtFootnote.Rows.Add(dtrowFootnote2);



          

            mockDataAccess.SetupSequence(x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                        .Returns(tblSiteData)
                        .Returns(tblPageData)
                        .Returns(tblDocumentTypeAssc)
                        .Returns(tblTaxonomyAsscData)
                        .Returns(tblTaxonomyAsscHierarchy)
                        .Returns(dtFootnote);
            var result = factory.GenerateExportXML();

            Assert.IsInstanceOfType(result, typeof(XDocument));
        }



        #endregion
    }
}
