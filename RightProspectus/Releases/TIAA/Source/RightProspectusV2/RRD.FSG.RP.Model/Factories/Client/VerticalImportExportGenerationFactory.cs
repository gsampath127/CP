// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace RRD.FSG.RP.Model.Factories.Client
{
    /// <summary>
    /// Class VerticalImportExportGenerationFactory.
    /// </summary>
    public class VerticalImportExportGenerationFactory : BaseFactory
    {
        #region Constants
        /// <summary>
        /// SPGetTaxonomyIdsForMarketIDs
        /// </summary>
            private const string SPGetTaxonomyIdsForMarketIDs = "RPV2HostedAdmin_GetTaxonomyIdsForMarketIDs";

            /// <summary>
            /// SPGetDocumentTypeIdsForMarketIDs
            /// </summary>
            private const string SPGetDocumentTypeIdsForMarketIDs = "RPV2HostedAdmin_GetDocumentTypeIdsForMarketIDs";

            /// <summary>
            /// SPSaveVerticalDataFromImportXmlData
            /// </summary>
            private const string SPSaveVerticalDataFromImportXmlData = "RPV2HostedAdmin_SaveVerticalDataFromImportXmlData";

            /// <summary>
            /// TaxonomyMarketIDs
            /// </summary>
            private const string DBCTaxonomyMarketIDs = "TaxonomyMarketIDs";

            /// <summary>
            /// DocumentTypeMarketIds
            /// </summary>
            private const string DBCDocumentTypeMarketIds = "DocumentTypeMarketIds";


            /// <summary>
            /// TT_DocumentTypeAssociation
            /// </summary>
            private const string DBCTT_DocumentTypeAssociation = "TT_DocumentTypeAssociation";

            /// <summary>
            /// TT_TaxonomyAssociation
            /// </summary>
            private const string DBCTT_TaxonomyAssociation = "TT_TaxonomyAssociation";

            /// <summary>
            /// TT_TaxonomyAssociationFootnotes
            /// </summary>
            private const string DBCTT_TaxonomyAssociationFootnotes = "TT_TaxonomyAssociationFootnotes";

            /// <summary>
            /// TT_TaxonomyAssociationHierarchy
            /// </summary>
            private const string DBCTT_TaxonomyAssociationHierarchy = "TT_TaxonomyAssociationHierarchy";

            /// <summary>
            /// IsBackup
            /// </summary>
            private const string DBCIsBackup = "IsBackup";
            /// <summary>
            /// ImportedBy
            /// </summary>
            private const string DBCImportedBy = "ImportedBy";




        #endregion

            /// <summary>
            /// The site object models
            /// </summary>
        private IEnumerable<SiteObjectModel> siteObjectModels = null;

        /// <summary>
        /// The document type association object models
        /// </summary>
        private IEnumerable<DocumentTypeAssociationObjectModel> documentTypeAssociationObjectModels = null;

        /// <summary>
        /// The taxonomy association object models
        /// </summary>
        private IEnumerable<TaxonomyAssociationObjectModel> taxonomyAssociationObjectModels = null;

        /// <summary>
        /// The taxonomy association hierarchy object models
        /// </summary>
        private IEnumerable<TaxonomyAssociationHierarchyObjectModel> taxonomyAssociationHierarchyObjectModels = null;

        /// <summary>
        /// The footnote object models
        /// </summary>
        private IEnumerable<FootnoteObjectModel> footnoteObjectModels = null;

        /// <summary>
        /// The markettaxonomy associations
        /// </summary>
        private XElement markettaxonomyAssociations;

        /// <summary>
        /// The document type associations data table
        /// </summary>
        private DataTable documentTypeAssociationsDataTable = null;

        /// <summary>
        /// The verticalmarketdocument type associations data table
        /// </summary>
        private DataTable verticalmarketdocumentTypeAssociationsDataTable = null;

        /// <summary>
        /// The taxonomy associations data table
        /// </summary>
        private DataTable taxonomyAssociationsDataTable = null;

        /// <summary>
        /// The verticalmarket taxonomy data table
        /// </summary>
        private DataTable verticalmarketTaxonomyDataTable = null;

        /// <summary>
        /// The taxonomy association hierarchy data table
        /// </summary>
        private DataTable taxonomyAssociationHierarchyDataTable = null;

        /// <summary>
        /// The footnotes data table
        /// </summary>
        private DataTable footnotesDataTable = null;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlRewriteAdministrationFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public VerticalImportExportGenerationFactory(IDataAccess dataAccess)
            : base(dataAccess) { }
        #endregion



        # region "Export XML"



        /// <summary>
        /// Load all Required data from DB for generating the Export XML.
        /// </summary>
        private void LoadExportDataFromDB()
        {


            SiteFactory siteFactory = new SiteFactory(this.DataAccess);

            siteFactory.ClientName = this.ClientName;

            siteObjectModels = siteFactory.GetAllEntities();



            DocumentTypeAssociationFactory documentTypeAssociationFactory = new DocumentTypeAssociationFactory(this.DataAccess);

            documentTypeAssociationFactory.ClientName = this.ClientName;

            documentTypeAssociationObjectModels = documentTypeAssociationFactory.GetAllEntities();

            TaxonomyAssociationFactory taxonomyAssociationFactory = new TaxonomyAssociationFactory(this.DataAccess);

            taxonomyAssociationFactory.ClientName = this.ClientName;

            taxonomyAssociationObjectModels = taxonomyAssociationFactory.GetAllEntities();

            TaxonomyAssociationHierarchyFactory taxonomyAssociationHierarchyFactory = new TaxonomyAssociationHierarchyFactory(this.DataAccess);

            taxonomyAssociationHierarchyFactory.ClientName = this.ClientName;

            taxonomyAssociationHierarchyObjectModels = taxonomyAssociationHierarchyFactory.GetAllEntities();


            FootnoteFactory footnoteFactory = new FootnoteFactory(this.DataAccess);

            footnoteFactory.ClientName = this.ClientName;

            footnoteObjectModels = footnoteFactory.GetAllEntities();



        }

        /// <summary>
        /// Main Function for Generating Export XML
        /// </summary>
        /// <returns>XDocument.</returns>
        public XDocument GenerateExportXML()
        {


            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            XNamespace mstns = XNamespace.Get("http://rightprospectus.com/hostedSchema");
            XNamespace sql = XNamespace.Get("http://schemas.microsoft.com/sqlserver/2004/sqltypes");
            XNamespace my = XNamespace.Get("http://schemas.microsoft.com/office/infopath/2003/myXSD/2015-10-06T20:30:47");
            XNamespace xd = XNamespace.Get("http://schemas.microsoft.com/office/infopath/2003");

            

            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "no"),
                new XProcessingInstruction("mso-infoPathSolution", "solutionVersion=\"1.0.0.21\"  PIVersion=\"1.0.0.0\" href=\"" + ConfigValues.VerticalXMLImportExportInfopathFormName + "\" name=\"urn:schemas-microsoft-com:office:infopath:RPV2HostedInfoPathForm:http---rightprospectus-com-hostedSchema\" language=\"en-us\" productVersion=\"12.0.0\" "),
                new XProcessingInstruction("mso-application", "progid=\"InfoPath.Document\""),

                new XElement(mstns + "import",
                    new XComment("Generated on :" + DateTime.UtcNow.ToString("o")),
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XAttribute(XNamespace.Xmlns + "mstns", mstns),
                    new XAttribute(XNamespace.Xmlns + "sql", sql),
                    new XAttribute(XNamespace.Xmlns + "my", my),
                    new XAttribute(XNamespace.Xmlns + "xd", xd)
                 )
            );

             

            //market
            XElement market = new XElement(mstns + "market");

            doc.Element(mstns + "import").Add(market);

            //taxonomyAssociations under import/market

            markettaxonomyAssociations = new XElement(mstns + "taxonomyAssociations");

            market.Add(markettaxonomyAssociations);


            //sites
            XElement sites = new XElement(mstns + "sites");

            sites.Add(new XAttribute("mirror", "true"));

            doc.Element(mstns + "import").Add(sites);

            //Load the Export DB from the DB...
            LoadExportDataFromDB();

            if (siteObjectModels != null)
            {
                foreach (var item in siteObjectModels)
                {
                    //site
                    XElement site = new XElement(mstns + "site");

                    site.Add(new XAttribute("name", item.Name));

                    site.Add(new XAttribute("description", string.IsNullOrEmpty(item.Description) ? "" : item.Description));

                    site.Add(new XAttribute("templateId", item.TemplateId));

                    site.Add(new XAttribute("systemId", item.SiteID));

                    site.Add(new XAttribute("delete", "false"));

                    //market
                    XElement sitemarket = new XElement(mstns + "market");

                    site.Add(sitemarket);



                    //documentTypeAssociations to site.
                    AddDocumentTypeToSite(sitemarket,
                                            mstns,
                                            item.SiteID);


                    //taxonomyAssociations
                    XElement taxonomyAssociations = new XElement(mstns + "taxonomyAssociations");
                    taxonomyAssociations.Add(new XAttribute("mirror", "false"));
                    sitemarket.Add(taxonomyAssociations);

                    //taxonomyAssociation

                    IEnumerable<TaxonomyAssociationObjectModel> sitetaxonomyAssociationObjectModels =
                       from t in taxonomyAssociationObjectModels where t.SiteId == item.SiteID select t;

                    if (sitetaxonomyAssociationObjectModels.Count() > 0)
                    {
                        foreach (var sitetaxonomyAssociationObjectModel in sitetaxonomyAssociationObjectModels)
                        {
                            XElement taxonomyAssociation = new XElement(mstns + "taxonomyAssociation");

                            taxonomyAssociation.Add(new XAttribute("level", sitetaxonomyAssociationObjectModel.Level));

                            taxonomyAssociation.Add(new XAttribute("importId", ""));

                            taxonomyAssociation.Add(new XAttribute("systemId", sitetaxonomyAssociationObjectModel.TaxonomyAssociationId));

                            taxonomyAssociation.Add(new XAttribute("nameOverride", string.IsNullOrEmpty(sitetaxonomyAssociationObjectModel.NameOverride) ? "" : sitetaxonomyAssociationObjectModel.NameOverride));

                            taxonomyAssociation.Add(new XAttribute("marketId", string.IsNullOrEmpty(sitetaxonomyAssociationObjectModel.MarketId) ? "" : sitetaxonomyAssociationObjectModel.MarketId));

                            taxonomyAssociation.Add(new XAttribute("descriptionOverride", string.IsNullOrEmpty(sitetaxonomyAssociationObjectModel.DescriptionOverride) ? "" : sitetaxonomyAssociationObjectModel.DescriptionOverride));

                            taxonomyAssociation.Add(new XAttribute("cssClass", string.IsNullOrEmpty(sitetaxonomyAssociationObjectModel.CssClass) ? "" : sitetaxonomyAssociationObjectModel.CssClass));

                            taxonomyAssociation.Add(new XAttribute("delete", "false"));

                            taxonomyAssociations.Add(taxonomyAssociation);





                            ////documentTypeAssociation to TaxonomyAssociation

                            AddDocumentTypeAssociationToTaxonomyAssociation(
                                       sitetaxonomyAssociationObjectModel.TaxonomyAssociationId,
                                       mstns,
                                       taxonomyAssociation
                                       );

                            ///TaxonomyAssociationHierarchies
                            AddTaxonomyAssociationHierarchiesToTaxonomyAssociation(sitetaxonomyAssociationObjectModel.TaxonomyAssociationId,
                                sitetaxonomyAssociationObjectModel.MarketId,
                                mstns,
                                taxonomyAssociation,
                                market
                                );

                            ///Add Footnotes to TaxonomyAssociation
                            AddFootNotesToTaxonomyAssociationOrTaxonomyAssociationGroup(sitetaxonomyAssociationObjectModel.TaxonomyAssociationId, null,
                                mstns, taxonomyAssociation);



                            ///
                        }
                    }
                    else // add a blank taxonomyassociation
                    {
                        XElement taxonomyAssociation = new XElement(mstns + "taxonomyAssociation");

                        taxonomyAssociation.Add(new XAttribute("level", 1));

                        taxonomyAssociation.Add(new XAttribute("importId", ""));   

                        taxonomyAssociation.Add(new XAttribute("systemId","0"));

                        taxonomyAssociation.Add(new XAttribute("nameOverride", ""));

                        taxonomyAssociation.Add(new XAttribute("marketId", "XXXXXX"));

                        taxonomyAssociation.Add(new XAttribute("descriptionOverride", ""));

                        taxonomyAssociation.Add(new XAttribute("cssClass", ""));

                        taxonomyAssociation.Add(new XAttribute("delete", "false"));

                        taxonomyAssociations.Add(taxonomyAssociation);

                        //add blank document type
                        AddDocumentTypeAssociationToTaxonomyAssociation(-1,
                            mstns,
                            taxonomyAssociation
                            );

                        //add blank taxonomyassociationhierarchy
                        AddTaxonomyAssociationHierarchiesToTaxonomyAssociation(-1,
                                "XXXXXX",
                                mstns,
                                taxonomyAssociation,
                                market
                                );

                        ///Add blank footnote
                        AddFootNotesToTaxonomyAssociationOrTaxonomyAssociationGroup(null, null,
                            mstns, taxonomyAssociation);

                    }

                    sites.Add(site);
                }

            }

            return doc;

        }

        /// <summary>
        /// Add DocumentTypeToSite Function
        /// </summary>
        /// <param name="sitemarket">The sitemarket.</param>
        /// <param name="mstns">The MSTNS.</param>
        /// <param name="siteId">The site identifier.</param>
        private void AddDocumentTypeToSite(
                                            XElement sitemarket,
                                            XNamespace mstns,
                                            int siteId)
        {

            XElement documentTypeAssociations = new XElement(mstns + "documentTypeAssociations");

            documentTypeAssociations.Add(new XAttribute("mirror", "false"));

            sitemarket.Add(documentTypeAssociations);

            //documentTypeAssociation

            IEnumerable<DocumentTypeAssociationObjectModel> sitedocumentTypeAssociationObjectModels =
                from d in documentTypeAssociationObjectModels where d.SiteId == siteId select d;

            if (sitedocumentTypeAssociationObjectModels.Count() > 0)
            {
                foreach (var documentTypeAssociationItem in sitedocumentTypeAssociationObjectModels)
                {


                    XElement documentTypeAssociation = new XElement(mstns + "documentTypeAssociation");

                    documentTypeAssociation.Add(new XAttribute("headerText", string.IsNullOrEmpty(documentTypeAssociationItem.HeaderText) ? "" : documentTypeAssociationItem.HeaderText));

                    documentTypeAssociation.Add(new XAttribute("linkText", string.IsNullOrEmpty(documentTypeAssociationItem.LinkText) ? "" : documentTypeAssociationItem.LinkText));

                    documentTypeAssociation.Add(new XAttribute("order", documentTypeAssociationItem.Order == null ? "" : documentTypeAssociationItem.Order.ToString()));

                    documentTypeAssociation.Add(new XAttribute("marketId", string.IsNullOrEmpty(documentTypeAssociationItem.MarketId) ? "" : documentTypeAssociationItem.MarketId));

                    documentTypeAssociation.Add(new XAttribute("descriptionOverride", string.IsNullOrEmpty(documentTypeAssociationItem.DescriptionOverride) ? "" : documentTypeAssociationItem.DescriptionOverride));

                    documentTypeAssociation.Add(new XAttribute("cssClass", string.IsNullOrEmpty(documentTypeAssociationItem.CssClass) ? "" : documentTypeAssociationItem.CssClass));

                    documentTypeAssociation.Add(new XAttribute("delete", "false"));

                    documentTypeAssociations.Add(documentTypeAssociation);
                }

            }
            else // add a emtpty node for users to use when importing
            {
                XElement documentTypeAssociation = new XElement(mstns + "documentTypeAssociation");

                documentTypeAssociation.Add(new XAttribute("headerText", ""));

                documentTypeAssociation.Add(new XAttribute("linkText", ""));

                documentTypeAssociation.Add(new XAttribute("order", 0));

                documentTypeAssociation.Add(new XAttribute("marketId", "XXXXXXX"));

                documentTypeAssociation.Add(new XAttribute("descriptionOverride", ""));

                documentTypeAssociation.Add(new XAttribute("cssClass", ""));

                documentTypeAssociation.Add(new XAttribute("delete", "false"));

                documentTypeAssociations.Add(documentTypeAssociation);

            }

        }

        /// <summary>
        /// AddDocumentTypeAssociationToTaxonomyAssociation
        /// </summary>
        /// <param name="taxonomyAssociationId">The taxonomy association identifier.</param>
        /// <param name="mstns">The MSTNS.</param>
        /// <param name="taxonomyAssociation">The taxonomy association.</param>
        private void AddDocumentTypeAssociationToTaxonomyAssociation(
                                                                        int taxonomyAssociationId,
                                                                         XNamespace mstns,
                                                                        XElement taxonomyAssociation
                                                                        )
        {
            //documentTypeAssociations
            XElement tadocumentTypeAssociations = new XElement(mstns + "documentTypeAssociations");

            tadocumentTypeAssociations.Add(new XAttribute("mirror", "false"));

            taxonomyAssociation.Add(tadocumentTypeAssociations);

            IEnumerable<DocumentTypeAssociationObjectModel> tadocumentTypeAssociationObjectModels =
                                from d in documentTypeAssociationObjectModels where d.TaxonomyAssociationId == taxonomyAssociationId select d;

            if (tadocumentTypeAssociationObjectModels.Count() > 0)
            {
                foreach (var documentTypeAssociationItem in tadocumentTypeAssociationObjectModels)
                {


                    XElement documentTypeAssociation = new XElement(mstns + "documentTypeAssociation");

                    documentTypeAssociation.Add(new XAttribute("headerText", string.IsNullOrEmpty(documentTypeAssociationItem.HeaderText) ? "" : documentTypeAssociationItem.HeaderText));

                    documentTypeAssociation.Add(new XAttribute("linkText", string.IsNullOrEmpty(documentTypeAssociationItem.LinkText) ? "" : documentTypeAssociationItem.LinkText));

                    documentTypeAssociation.Add(new XAttribute("order", documentTypeAssociationItem.Order == null ? "" : documentTypeAssociationItem.Order.ToString()));

                    documentTypeAssociation.Add(new XAttribute("marketId", string.IsNullOrEmpty(documentTypeAssociationItem.MarketId) ? "" : documentTypeAssociationItem.MarketId));

                    documentTypeAssociation.Add(new XAttribute("descriptionOverride", string.IsNullOrEmpty(documentTypeAssociationItem.DescriptionOverride) ? "" : documentTypeAssociationItem.DescriptionOverride));

                    documentTypeAssociation.Add(new XAttribute("cssClass", string.IsNullOrEmpty(documentTypeAssociationItem.CssClass) ? "" : documentTypeAssociationItem.CssClass));

                    documentTypeAssociation.Add(new XAttribute("delete", "false"));

                    tadocumentTypeAssociations.Add(documentTypeAssociation);
                }

            }

        }

        /// <summary>
        /// AddTaxonomyAssociationHierarchiesToTaxonomyAssociation
        /// </summary>
        /// <param name="taxonomyAssociationId">The taxonomy association identifier.</param>
        /// <param name="parentMarketId">The parent market identifier.</param>
        /// <param name="mstns">The MSTNS.</param>
        /// <param name="taxonomyAssociation">The taxonomy association.</param>
        /// <param name="market">The market.</param>
        private void AddTaxonomyAssociationHierarchiesToTaxonomyAssociation(int taxonomyAssociationId,
                                                                            string parentMarketId,
                                                                         XNamespace mstns,
                                                                        XElement taxonomyAssociation,
                                                                        XElement market
                                                                    )
        {

            //taxonomyAssociationHierarchies
            XElement taxonomyAssociationHierarchies = new XElement(mstns + "taxonomyAssociationHierarchies");

            taxonomyAssociationHierarchies.Add(new XAttribute("mirror", "false"));


            taxonomyAssociation.Add(taxonomyAssociationHierarchies);



            IEnumerable<TaxonomyAssociationHierarchyObjectModel> currentTAIDTaxonomyAssociationHierarchyObjectModels =
                    from TAH in taxonomyAssociationHierarchyObjectModels where TAH.ParentTaxonomyAssociationId == taxonomyAssociationId select TAH;

            XElement taxonomyAssociationHierarchy = null;

            XElement taxonomyAssociationLinks = null;

            if (currentTAIDTaxonomyAssociationHierarchyObjectModels.Count() > 0)
            {
                int currentRelationShipType = -1;


                foreach (var currentTAIDTaxonomyAssociationHierarchyObjectModel in currentTAIDTaxonomyAssociationHierarchyObjectModels)
                {
                    if (currentRelationShipType != currentTAIDTaxonomyAssociationHierarchyObjectModel.RelationshipType)
                    {
                        taxonomyAssociationHierarchy = new XElement(mstns + "taxonomyAssociationHierarchy");

                        taxonomyAssociationHierarchy.Add(new XAttribute("relationshipType", currentTAIDTaxonomyAssociationHierarchyObjectModel.RelationshipType));

                        taxonomyAssociationHierarchy.Add(new XAttribute("delete", "false"));

                        currentRelationShipType = currentTAIDTaxonomyAssociationHierarchyObjectModel.RelationshipType;

                        taxonomyAssociationHierarchies.Add(taxonomyAssociationHierarchy);

                        //taxonomyAssociationLinks
                        taxonomyAssociationLinks = new XElement(mstns + "taxonomyAssociationLinks");

                        taxonomyAssociationLinks.Add(new XAttribute("mirror", "false"));

                        taxonomyAssociationHierarchy.Add(taxonomyAssociationLinks);

                    }

                    TaxonomyAssociationObjectModel childTaxonomyAssociationObjectModel =
                         taxonomyAssociationObjectModels.Single(s => s.TaxonomyAssociationId ==
                                 currentTAIDTaxonomyAssociationHierarchyObjectModel.ChildTaxonomyAssociationId);

                    if (childTaxonomyAssociationObjectModel != null)
                    {
                        //Add Link Under taxonomyAssociationHierarchies\taxonomyAssociationHierarchy\taxonomyAssociationLinks
                        XElement taxonomyAssociationLink = new XElement(mstns + "taxonomyAssociationLink");

                        //taxonomyAssociationLink.Add(new XAttribute("marketId", string.IsNullOrEmpty(childTaxonomyAssociationObjectModel.MarketId) ? "" : childTaxonomyAssociationObjectModel.MarketId));

                        taxonomyAssociationLink.Add(new XAttribute("importId", string.IsNullOrEmpty(childTaxonomyAssociationObjectModel.MarketId) ? "" : childTaxonomyAssociationObjectModel.MarketId + "C"));

                        taxonomyAssociationLink.Add(new XAttribute("systemId", childTaxonomyAssociationObjectModel.TaxonomyAssociationId));

                        taxonomyAssociationLink.Add(new XAttribute("order", currentTAIDTaxonomyAssociationHierarchyObjectModel.Order == null ? 0 : currentTAIDTaxonomyAssociationHierarchyObjectModel.Order));

                        taxonomyAssociationLink.Add(new XAttribute("delete", "false"));

                        taxonomyAssociationLinks.Add(taxonomyAssociationLink);

                       var markettaxanomyselectifexists = markettaxonomyAssociations.Descendants(mstns + "taxonomyAssociation").FirstOrDefault(x => x.Attribute("marketId").Value == childTaxonomyAssociationObjectModel.MarketId);

                       if (markettaxanomyselectifexists == null)
                       {

                           //Add taxonomyAssociation Under import\market\taxonomyAssociations
                           XElement markettaxonomyAssociation = new XElement(mstns + "taxonomyAssociation");

                           markettaxonomyAssociation.Add(new XAttribute("level", childTaxonomyAssociationObjectModel.Level));

                           markettaxonomyAssociation.Add(new XAttribute("importId", string.IsNullOrEmpty(childTaxonomyAssociationObjectModel.MarketId) ? "" : childTaxonomyAssociationObjectModel.MarketId + "C"));

                           markettaxonomyAssociation.Add(new XAttribute("systemId", childTaxonomyAssociationObjectModel.TaxonomyAssociationId));

                           markettaxonomyAssociation.Add(new XAttribute("nameOverride", string.IsNullOrEmpty(childTaxonomyAssociationObjectModel.NameOverride) ? "" : childTaxonomyAssociationObjectModel.NameOverride));

                           markettaxonomyAssociation.Add(new XAttribute("marketId", string.IsNullOrEmpty(childTaxonomyAssociationObjectModel.MarketId) ? "" : childTaxonomyAssociationObjectModel.MarketId));

                           markettaxonomyAssociation.Add(new XAttribute("descriptionOverride", string.IsNullOrEmpty(childTaxonomyAssociationObjectModel.DescriptionOverride) ? "" : childTaxonomyAssociationObjectModel.DescriptionOverride));

                           markettaxonomyAssociation.Add(new XAttribute("cssClass", string.IsNullOrEmpty(childTaxonomyAssociationObjectModel.CssClass) ? "" : childTaxonomyAssociationObjectModel.CssClass));

                           markettaxonomyAssociation.Add(new XAttribute("delete", "false"));

                           markettaxonomyAssociations.Add(markettaxonomyAssociation);

                           ///Add DocumentType to TaxonomyAssociation
                           AddDocumentTypeAssociationToTaxonomyAssociation(childTaxonomyAssociationObjectModel.TaxonomyAssociationId,
                               mstns,
                               markettaxonomyAssociation
                               );

                           ///Add Footnotes to TaxonomyAssociation
                           AddFootNotesToTaxonomyAssociationOrTaxonomyAssociationGroup(childTaxonomyAssociationObjectModel.TaxonomyAssociationId, null,
                               mstns, markettaxonomyAssociation);
                       }
                    }
                }

            }
            else // add blank taxonomyassociationhierarchy
            {
                taxonomyAssociationHierarchy = new XElement(mstns + "taxonomyAssociationHierarchy");

                taxonomyAssociationHierarchy.Add(new XAttribute("relationshipType", 1));

                taxonomyAssociationHierarchy.Add(new XAttribute("delete", "false"));
                
                taxonomyAssociationHierarchies.Add(taxonomyAssociationHierarchy);

                //taxonomyAssociationLinks
                taxonomyAssociationLinks = new XElement(mstns + "taxonomyAssociationLinks");

                taxonomyAssociationLinks.Add(new XAttribute("mirror", "false"));

                taxonomyAssociationHierarchy.Add(taxonomyAssociationLinks);

                ///

                XElement taxonomyAssociationLink = new XElement(mstns + "taxonomyAssociationLink");

                //taxonomyAssociationLink.Add(new XAttribute("marketId", "XXXXX"));

                taxonomyAssociationLink.Add(new XAttribute("importId", "XXXXXC"));

                taxonomyAssociationLink.Add(new XAttribute("order", 0));

                taxonomyAssociationLink.Add(new XAttribute("delete", "false"));

                taxonomyAssociationLinks.Add(taxonomyAssociationLink);

                if (markettaxonomyAssociations.Elements(mstns + "taxonomyAssociation").Count() == 0)
                {
                    //Add taxonomyAssociation Under import\market\taxonomyAssociations
                    XElement markettaxonomyAssociation = new XElement(mstns + "taxonomyAssociation");

                    markettaxonomyAssociation.Add(new XAttribute("level", 1));

                    markettaxonomyAssociation.Add(new XAttribute("importId", "XXXXXC"));

                    markettaxonomyAssociation.Add(new XAttribute("systemId", "0"));
                    
                    markettaxonomyAssociation.Add(new XAttribute("nameOverride", ""));

                    markettaxonomyAssociation.Add(new XAttribute("marketId", "XXXX"));

                    markettaxonomyAssociation.Add(new XAttribute("descriptionOverride", ""));

                    markettaxonomyAssociation.Add(new XAttribute("cssClass", ""));

                    markettaxonomyAssociation.Add(new XAttribute("delete", "false"));

                    markettaxonomyAssociations.Add(markettaxonomyAssociation);
                }


            }

        }


        /// <summary>
        /// AddFootNotesToTaxonomyAssociationOrTaxonomyAssociationGroup
        /// </summary>
        /// <param name="taxonomyAssociationId">The taxonomy association identifier.</param>
        /// <param name="taxonomyAssociationGroupId">The taxonomy association group identifier.</param>
        /// <param name="mstns">The MSTNS.</param>
        /// <param name="taxonomyAssociationortaxonomyAssociationGroup">The taxonomy associationortaxonomy association group.</param>
        private void AddFootNotesToTaxonomyAssociationOrTaxonomyAssociationGroup(
                                                                int? taxonomyAssociationId,
                                                                int? taxonomyAssociationGroupId,
                                                                 XNamespace mstns,
                                                                XElement taxonomyAssociationortaxonomyAssociationGroup
                                                                )
        {
            //footnotes
            XElement footnotes = new XElement(mstns + "footnotes");

            footnotes.Add(new XAttribute("mirror", "false"));

            taxonomyAssociationortaxonomyAssociationGroup.Add(footnotes);

            IEnumerable<FootnoteObjectModel> tafootnoteObjectModels = null;

            if (taxonomyAssociationId != null)
            {
                tafootnoteObjectModels =
                                from f in footnoteObjectModels where f.TaxonomyAssociationId == taxonomyAssociationId select f;
            }
            else
                if (taxonomyAssociationGroupId != null)
                {
                    tafootnoteObjectModels =
                                    from f in footnoteObjectModels where f.TaxonomyAssociationGroupId == taxonomyAssociationGroupId select f;

                }


            if (tafootnoteObjectModels != null && tafootnoteObjectModels.Count() > 0)
            {
                foreach (var tafootnoteObjectModelItem in tafootnoteObjectModels)
                {

                    XElement footnote = new XElement(mstns + "footnote");

                    footnote.Add(new XAttribute("languageCulture", string.IsNullOrEmpty(tafootnoteObjectModelItem.LanguageCulture) ? "" : tafootnoteObjectModelItem.LanguageCulture));

                    footnote.Add(new XAttribute("delete", "false"));

                    footnote.Value = string.IsNullOrEmpty(tafootnoteObjectModelItem.Text) ? "" : tafootnoteObjectModelItem.Text;

                    footnotes.Add(footnote);
                }

            }
            else // add a blank footnote for reference in import xml
            {
                
                XElement footnote = new XElement(mstns + "footnote");
                footnote.Add(new XAttribute("languageCulture", ""));

                footnote.Add(new XAttribute("delete", "false"));

                footnote.Value = "";

                footnotes.Add(footnote);
            }

        }

        #endregion

        /// <summary>
        /// Create DataTables For Import
        /// </summary>
       private void CreateDataTablesForImport()
        {
            documentTypeAssociationsDataTable = new DataTable();

            documentTypeAssociationsDataTable.Columns.Add("headerText", typeof(string));
            documentTypeAssociationsDataTable.Columns.Add("linkText", typeof(string));
            documentTypeAssociationsDataTable.Columns.Add("order", typeof(Int32));
            documentTypeAssociationsDataTable.Columns.Add("marketId", typeof(string));
            documentTypeAssociationsDataTable.Columns.Add("descriptionOverride", typeof(string));
            documentTypeAssociationsDataTable.Columns.Add("cssClass", typeof(string));
            documentTypeAssociationsDataTable.Columns.Add("delete", typeof(bool));
            documentTypeAssociationsDataTable.Columns.Add("siteid", typeof(Int32));
            documentTypeAssociationsDataTable.Columns.Add("taxonomymarketId", typeof(string));
            documentTypeAssociationsDataTable.Columns.Add("taxonomylevel", typeof(Int32));
            documentTypeAssociationsDataTable.Columns.Add("documenttypeid", typeof(Int32));


            verticalmarketdocumentTypeAssociationsDataTable = new DataTable();
            verticalmarketdocumentTypeAssociationsDataTable.Columns.Add("marketId", typeof(string));

            

            taxonomyAssociationsDataTable = new DataTable();

            taxonomyAssociationsDataTable.Columns.Add("level", typeof(Int32));
            taxonomyAssociationsDataTable.Columns.Add("importId", typeof(string));
            taxonomyAssociationsDataTable.Columns.Add("systemId", typeof(Int32));
            taxonomyAssociationsDataTable.Columns.Add("nameOverride", typeof(string));
            taxonomyAssociationsDataTable.Columns.Add("marketId", typeof(string));            
            taxonomyAssociationsDataTable.Columns.Add("descriptionOverride", typeof(string));
            taxonomyAssociationsDataTable.Columns.Add("cssClass", typeof(string));
            taxonomyAssociationsDataTable.Columns.Add("delete", typeof(bool));
            taxonomyAssociationsDataTable.Columns.Add("siteid", typeof(Int32));
            taxonomyAssociationsDataTable.Columns.Add("taxonomyId", typeof(string));

            verticalmarketTaxonomyDataTable = new DataTable();
            verticalmarketTaxonomyDataTable.Columns.Add("marketId", typeof(string));
            verticalmarketTaxonomyDataTable.Columns.Add("level", typeof(Int32));

            footnotesDataTable = new DataTable();
            footnotesDataTable.Columns.Add("taxonomyassociationsystemid", typeof(Int32));
            footnotesDataTable.Columns.Add("level", typeof(Int32));
            footnotesDataTable.Columns.Add("taxonomymarketId", typeof(string));
            footnotesDataTable.Columns.Add("languageCulture", typeof(string));
            footnotesDataTable.Columns.Add("text", typeof(string));
            footnotesDataTable.Columns.Add("delete", typeof(bool));


            taxonomyAssociationHierarchyDataTable = new DataTable();
            taxonomyAssociationHierarchyDataTable.Columns.Add("parenttaxonomyassociationsystemid", typeof(Int32));
            taxonomyAssociationHierarchyDataTable.Columns.Add("parentlevel", typeof(Int32));
            taxonomyAssociationHierarchyDataTable.Columns.Add("parenttaxonomymarketId", typeof(string));
            taxonomyAssociationHierarchyDataTable.Columns.Add("relationshipType", typeof(Int32));
            taxonomyAssociationHierarchyDataTable.Columns.Add("childimportid", typeof(string));
            taxonomyAssociationHierarchyDataTable.Columns.Add("deleteparent", typeof(bool));
            taxonomyAssociationHierarchyDataTable.Columns.Add("deletechild", typeof(bool));


        }


       /// <summary>
       /// Logs the error to database.
       /// </summary>
       /// <param name="isImport">if set to <c>true</c> [is import].</param>
       /// <param name="importId">The import identifier.</param>
       /// <param name="message">The message.</param>
       /// <param name="formattedMessage">The formatted message.</param>
        public void LogErrorToDB(bool isImport,int importId,string message,StringBuilder formattedMessage)
       {
           ErrorLogFactory logfactory = new ErrorLogFactory(this.DataAccess);

           logfactory.ClientName = this.ClientName;

           ErrorLogObjectModel errorlogobjectmodel = new ErrorLogObjectModel();

           if (isImport)
           {

               errorlogobjectmodel.Title = "Import XML Error";
               errorlogobjectmodel.ErrorCode = 700;
           }
            else
           {
               errorlogobjectmodel.Title = "Export XML Error";
               errorlogobjectmodel.ErrorCode = 800;
           }

           

           errorlogobjectmodel.EventId = importId;

           errorlogobjectmodel.Message = message;

           errorlogobjectmodel.FormattedMessage = formattedMessage!=null?formattedMessage.ToString():string.Empty;         
       

           errorlogobjectmodel.ProcessID = Process.GetCurrentProcess().Id.ToString();

           errorlogobjectmodel.ProcessName = "RRD.FSG.RP.Services.HostedXmlImportExport";

           errorlogobjectmodel.ThreadName = Thread.CurrentThread.Name;

           errorlogobjectmodel.Win32ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();

           errorlogobjectmodel.MachineName = Environment.MachineName;

           errorlogobjectmodel.AppDomainName = AppDomain.CurrentDomain.FriendlyName;

           errorlogobjectmodel.Priority = 2;

           errorlogobjectmodel.Severity = "Medium";

           logfactory.SaveEntity(errorlogobjectmodel);


       }

        /// <summary>
        /// Load Vertical Data From ImportXML
        /// </summary>
        /// <param name="importXML">The import XML.</param>
        /// <param name="importedBy">The imported by.</param>
        /// <param name="isBackup">if set to <c>true</c> [is backup].</param>
        /// <param name="verticalXmlImportId">The vertical XML import identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public int LoadDataFromImportXML(XDocument importXML, int importedBy, bool isBackup, int verticalXmlImportId)
        {
            int returnStatus = 1;

            StringBuilder marketIdsMissingMessage = new StringBuilder();

            XNamespace mstns = XNamespace.Get("http://rightprospectus.com/hostedSchema");


            IEnumerable<XElement> sites = importXML.Elements().Descendants(mstns + "site");

            XElement importMarket = importXML.Elements().Descendants(mstns + "market").FirstOrDefault();


            foreach (var site in sites)
            {
                int siteid = string.IsNullOrEmpty(site.Attribute("systemId").Value) ? 0 : Convert.ToInt32(site.Attribute("systemId").Value);


                string sitename = site.Attribute("name").Value;

                if (string.IsNullOrEmpty(sitename))
                    continue;

                bool isSiteAddedToDB = true;

                if (siteid == 0)
                {
                    isSiteAddedToDB = false;
                }
                else
                {
                    SiteFactory factory = new SiteFactory(this.DataAccess);
                    factory.ClientName = this.ClientName;

                    IEnumerable<SiteObjectModel> siteobjects = factory.GetAllEntities();


                    //make a call to the DB and find if the SiteName exists.if not exists then set isSiteAddedToDB = false;
                    if (siteobjects.SingleOrDefault(s => s.SiteID == siteid && s.Name == sitename) == null)
                    {
                        isSiteAddedToDB = false;
                    }
                }

                if (!isSiteAddedToDB)
                {
                    LogErrorToDB(true,
                        verticalXmlImportId,
                        "Site ID " + siteid.ToString() + " Site Name " + sitename 
                        + " is missing in the database.Please Add Site First,Export the XML and then try to import",
                        null);

                    return 0;
                }

                try
                {
                    XElement sitemarket = site.Descendants(mstns + "market").FirstOrDefault();

                    if (sitemarket != null)
                    {
                        CreateDataTablesForImport();

                        try
                        {



                            //Parse SiteLevel DocumentType Associations
                            ParseDocumentTypeAssociationsForImport(sitemarket,
                                                                    mstns,
                                                                    siteid,
                                                                    string.Empty,
                                                                    null);


                            //Parse SiteLevel taxonomy Associations
                            ParseTaxonomyAssociationsForImport(sitemarket,
                                mstns,
                                siteid
                                );

                            //Parse taxonomy Associations without a SiteID
                            ParseTaxonomyAssociationsForImport(importMarket,
                                mstns,
                                null
                                );

                        }
                        catch (Exception exception)
                        {
                            LogErrorToDB(true,
                                verticalXmlImportId,
                                    "Import XML Parsing Error " + exception.Message,
                                    null);

                            return 0;
                        }

                        try
                        {

                            UpdateTaxonomyIDsForMarketIds();

                            UpdateDocumentTypeIdsForMarketIds();
                        }
                        catch (Exception exception)
                        {
                            LogErrorToDB(true,
                                verticalXmlImportId,
                                    "Vertical Market DB Taxonomy ID and Document Type IDs parsing error. " 
                                    + exception.Message,
                                    null);

                            return 0;
                        }

                        try
                        {
                            DataAccess.ExecuteNonQuery(this.ConnectionString,
                                                   SPSaveVerticalDataFromImportXmlData,
                                                   this.DataAccess.CreateParameter(DBCTT_DocumentTypeAssociation, SqlDbType.Structured,
                                                            documentTypeAssociationsDataTable),
                                                   this.DataAccess.CreateParameter(DBCTT_TaxonomyAssociation, SqlDbType.Structured,
                                                            taxonomyAssociationsDataTable),
                                                   this.DataAccess.CreateParameter(DBCTT_TaxonomyAssociationFootnotes, SqlDbType.Structured,
                                                           footnotesDataTable),
                                                   this.DataAccess.CreateParameter(DBCTT_TaxonomyAssociationHierarchy, SqlDbType.Structured,
                                                            taxonomyAssociationHierarchyDataTable),
                                                   this.DataAccess.CreateParameter(DBCImportedBy, SqlDbType.Int,
                                                            importedBy),
                                                   this.DataAccess.CreateParameter(DBCIsBackup, SqlDbType.Bit,
                                                                isBackup));

                            DataRow[] taxonomynullRows = taxonomyAssociationsDataTable.Select("taxonomyId is null");

                            if(taxonomynullRows.Count() > 0)
                            {
                                returnStatus = 2;
                                marketIdsMissingMessage.Append("MarketIds missing in Vertical DB for site " + sitename + " <marketids>");

                                foreach (DataRow row in taxonomynullRows)
                                {
                                    marketIdsMissingMessage.Append("<marketid>" + row["marketId"] + "</marketid>");
                                }

                                marketIdsMissingMessage.Append("</marketids>");
                            }

                        } 
                        catch (Exception exception)
                        {
                            LogErrorToDB(true,
                                verticalXmlImportId,
                                    "Error saving Import XML Data in client DB. " + exception.Message,
                                    null);

                            return 0;
                        }
                    }

                }
                catch (Exception exception)
                {
                    LogErrorToDB(true,
                        verticalXmlImportId,
                            "Error in parsing the root Site Node.SiteID is " + siteid.ToString() + " " 
                            + exception.Message,
                            null);

                    return 0;
                }

                

            }
            if(returnStatus == 2)
            {
                LogErrorToDB(true,
                       verticalXmlImportId,
                           "Import Partially Completed.MarketIds missing in vertical market",                           
                           marketIdsMissingMessage);

            }
            return returnStatus;

        }


        /// <summary>
        /// Update DocumentTypeIds For MarketIds
        /// </summary>
        private void UpdateDocumentTypeIdsForMarketIds()
        {

            DataTable DocumentTypeIdsForMarketIDsDataTable = DataAccess.ExecuteDataTable(this.VerticalMarketConnectionString, SPGetDocumentTypeIdsForMarketIDs,
                                                   this.DataAccess.CreateParameter(DBCDocumentTypeMarketIds, SqlDbType.Structured,
                                                            verticalmarketdocumentTypeAssociationsDataTable));

            foreach (DataRow row in DocumentTypeIdsForMarketIDsDataTable.Rows)
            {
                DataRow[] rows = documentTypeAssociationsDataTable.Select("marketId = '" + row.Field<string>("marketId") + "'");

                if (rows.Count() > 0)
                {
                    foreach (DataRow mainrow in rows)
                    {
                        mainrow["DocumentTypeID"] = row["DocumentTypeID"];
                    }
                }
            }
        }


        /// <summary>
        /// Update TaxonomyIDs ForMarketIds
        /// </summary>
        private void UpdateTaxonomyIDsForMarketIds()
        {
            DataTable TaxonomyIdsForMarketIDsDataTable = DataAccess.ExecuteDataTable(this.VerticalMarketConnectionString, SPGetTaxonomyIdsForMarketIDs,
                                                   this.DataAccess.CreateParameter(DBCTaxonomyMarketIDs, SqlDbType.Structured,
                                                           verticalmarketTaxonomyDataTable));

            foreach (DataRow row in TaxonomyIdsForMarketIDsDataTable.Rows)
            {
                DataRow[] rows = taxonomyAssociationsDataTable.Select("marketId = '" + row.Field<string>("marketId")
                                                      + "' AND Level=" + row.Field<int>("Level"));

                if (rows.Count() > 0)
                {
                    foreach (DataRow maintablerow in rows)
                    {
                        maintablerow["TaxonomyID"] = row["TaxonomyID"];
                    }
                }
            }
        }

        /// <summary>
        /// Parse Document Type Associations For Import
        /// </summary>
        /// <param name="siteortaxonomyassociaitonelement">The siteortaxonomyassociaitonelement.</param>
        /// <param name="mstns">The MSTNS.</param>
        /// <param name="siteid">The siteid.</param>
        /// <param name="TaxonomyMarketId">The taxonomy market identifier.</param>
        /// <param name="TaxonomyLevel">The taxonomy level.</param>
        private void ParseDocumentTypeAssociationsForImport(XElement siteortaxonomyassociaitonelement,
                                                            XNamespace mstns,                                                           
                                                            int? siteid,
                                                            string TaxonomyMarketId,
                                                            int? TaxonomyLevel)
        {


            {


                IEnumerable<XElement> documentTypeAssociations = siteortaxonomyassociaitonelement.Elements(mstns + "documentTypeAssociations").Elements(mstns + "documentTypeAssociation");

                foreach (var documentTypeAssociation in documentTypeAssociations)
                {
                    string marketid = documentTypeAssociation.Attribute("marketId").Value;
                    if (!string.IsNullOrWhiteSpace(marketid))
                    {
                        documentTypeAssociationsDataTable.Rows.Add(
                                documentTypeAssociation.Attribute("headerText").Value,
                                string.IsNullOrWhiteSpace(documentTypeAssociation.Attribute("linkText").Value)?documentTypeAssociation.Attribute("headerText").Value:documentTypeAssociation.Attribute("linkText").Value,
                                documentTypeAssociation.Attribute("order").Value,
                                marketid,
                                documentTypeAssociation.Attribute("descriptionOverride").Value,
                                documentTypeAssociation.Attribute("cssClass").Value,
                                documentTypeAssociation.Attribute("delete").Value,
                                siteid,
                                TaxonomyMarketId,
                                TaxonomyLevel
                                );

                        this.verticalmarketdocumentTypeAssociationsDataTable.Rows.Add(marketid);                      
                    }
                }

            }


        }


        /// <summary>
        /// Parse TaxonomyAssociation ForImport
        /// </summary>
        /// <param name="sitemarket">The sitemarket.</param>
        /// <param name="mstns">The MSTNS.</param>
        /// <param name="siteid">The siteid.</param>
        private void ParseTaxonomyAssociationsForImport(XElement sitemarket,
                                                            XNamespace mstns,                                                            
                                                            int? siteid)
        {



            IEnumerable<XElement> taxonomyAssociations = sitemarket.Elements(mstns + "taxonomyAssociations").Elements(mstns + "taxonomyAssociation");

                foreach (var taxonomyAssociation in taxonomyAssociations)
                {
                    string marketid = taxonomyAssociation.Attribute("marketId").Value;

                    if (!string.IsNullOrWhiteSpace(marketid) && marketid != "XXXX")
                    {
                        int? taxonomyLevel= Convert.ToInt32(taxonomyAssociation.Attribute("level").Value);

                        string taxonomyassociationsystemid = "0";

                        if (taxonomyAssociation.Attribute("systemId") != null)
                        {
                            taxonomyassociationsystemid = string.IsNullOrWhiteSpace(taxonomyAssociation.Attribute("systemId").Value) ? "0" : taxonomyAssociation.Attribute("systemId").Value;
                        }

                        this.taxonomyAssociationsDataTable.Rows.Add(
                                taxonomyLevel,
                                taxonomyAssociation.Attribute("importId").Value,
                                taxonomyassociationsystemid,
                                taxonomyAssociation.Attribute("nameOverride").Value,
                                marketid,
                                taxonomyAssociation.Attribute("descriptionOverride").Value,
                                taxonomyAssociation.Attribute("cssClass").Value,
                                taxonomyAssociation.Attribute("delete").Value,
                                siteid
                                );

                        verticalmarketTaxonomyDataTable.Rows.Add(marketid, taxonomyLevel);

                        //Parse DocumentType for TaxonomyAssociations

                        ParseDocumentTypeAssociationsForImport(
                                taxonomyAssociation,
                                mstns,
                                null,
                                marketid,
                                taxonomyLevel
                            );

                        //Parse Footnotes within the TaxonomyAssociation
                        ParseFootNotesForImport(
                                                taxonomyAssociation,
                                                mstns,
                                                taxonomyLevel,
                                                taxonomyassociationsystemid,
                                                marketid
                                                );

                        //Parse Footnotes within the TaxonomyHierarchy
                        ParseTaxonomyHierarchyForImport(taxonomyAssociation,
                                mstns,
                                taxonomyLevel,
                                taxonomyassociationsystemid,
                                marketid);


                    }
                }           

        }


        /// <summary>
        /// Parse taxonomyHierarchy For Import
        /// </summary>
        /// <param name="taxonomyAssociation">The taxonomy association.</param>
        /// <param name="mstns">The MSTNS.</param>
        /// <param name="taxonomyLevel">The taxonomy level.</param>
        /// <param name="taxonomyassociationsystemid">The taxonomyassociationsystemid.</param>
        /// <param name="taxonomymarketId">The taxonomymarket identifier.</param>
        private void ParseTaxonomyHierarchyForImport(XElement taxonomyAssociation,
                                                    XNamespace mstns,
                                                    int? taxonomyLevel,
                                                    string taxonomyassociationsystemid,
                                                    string taxonomymarketId                                                    
                                                )
        {

            IEnumerable<XElement> taxonomyAssociationHierarchys = taxonomyAssociation.Elements(mstns + "taxonomyAssociationHierarchies").Elements(mstns + "taxonomyAssociationHierarchy");

            foreach (var taxonomyAssociationHierarchy in taxonomyAssociationHierarchys)
            {
                string relationshipType = taxonomyAssociationHierarchy.Attribute("relationshipType").Value;
                

                if (!string.IsNullOrWhiteSpace(relationshipType))
                {
                    string deleteParent =  taxonomyAssociationHierarchy.Attribute("delete").Value;

                    IEnumerable<XElement> taxonomyAssociationLinks = taxonomyAssociationHierarchy.Elements(mstns + "taxonomyAssociationLinks").Elements(mstns + "taxonomyAssociationLink");

                    foreach (var taxonomyAssociationLink in taxonomyAssociationLinks)
                    {
                        taxonomyAssociationHierarchyDataTable.Rows.Add(
                            taxonomyassociationsystemid,
                            taxonomyLevel,
                            taxonomymarketId,
                            relationshipType,
                            taxonomyAssociationLink.Attribute("importId").Value,
                            deleteParent,
                            taxonomyAssociationLink.Attribute("delete").Value
                            );
                    }
                }
            }
        }

        /// <summary>
        /// Parse FootNote For Import
        /// </summary>
        /// <param name="taxonomyAssociationortaxonomyAssociationGroup">The taxonomy associationortaxonomy association group.</param>
        /// <param name="mstns">The MSTNS.</param>
        /// <param name="taxonomyLevel">The taxonomy level.</param>
        /// <param name="taxonomyassociationsystemid">The taxonomyassociationsystemid.</param>
        /// <param name="taxonomymarketId">The taxonomymarket identifier.</param>
        private void ParseFootNotesForImport(XElement taxonomyAssociationortaxonomyAssociationGroup,
                                                    XNamespace mstns,
                                                    int? taxonomyLevel,
                                                    string taxonomyassociationsystemid,
                                                    string taxonomymarketId                                                    
                                                )
        {


            IEnumerable<XElement> footnotes = taxonomyAssociationortaxonomyAssociationGroup.Elements(mstns + "footnotes").Elements(mstns + "footnote");

            foreach (var footnote in footnotes)
            {
                string footnotevalue = footnote.Value;

                if (!string.IsNullOrWhiteSpace(footnotevalue))
                {
                    this.footnotesDataTable.Rows.Add(
                            taxonomyassociationsystemid,
                            taxonomyLevel,
                            taxonomymarketId,
                            footnote.Attribute("languageCulture").Value,
                            footnotevalue,
                            footnote.Attribute("delete").Value
                            );
                }
            }

        }


    }
}
