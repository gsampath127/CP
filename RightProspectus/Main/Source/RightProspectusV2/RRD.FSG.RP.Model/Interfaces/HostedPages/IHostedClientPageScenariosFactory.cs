// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-09-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;

namespace RRD.FSG.RP.Model.Interfaces.HostedPages
{
    /// <summary>
    /// Interface IHostedClientPageScenariosFactory
    /// </summary>
    public interface IHostedClientPageScenariosFactory
    {
        ///// <summary>
        ///// Gets the Billing Details
        ///// </summary>
        ///// <param name="clientName"></param>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns>DataSet</returns>
        BillingReportModel GetTaxonomyAssociationforBillingreport(string clientName, DateTime startDate, DateTime endDate);

        ///// <summary>
        ///// Get TaxonomyAssociation for DocumentUpdateReport
        ///// </summary>
        ///// <param name="clientName"></param>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <returns>DataSet</returns>
        List<DocumentUpdateReportModel> GetTaxonomyAssociationforDocumentUpdateReport(string clientName, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets the taxonomy association for URL Generation.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="marketID">The marketID.</param>
        /// <param name="siteName">The siteName.</param>
        /// <returns>List&lt;UrlGenerationObjectModel&gt;.</returns>
        List<UrlGenerationObjectModel> GetTaxonomyAssociationforUrlGeneration(string clientName, string marketID, string siteName);


        /// <summary>
        /// Gets the taxonomy association links.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="site">The site.</param>
        /// <param name="isProofing">The isProofing.</param>
        /// <param name="taxonomyOrderFeatureMode">taxonomyOrderFeatureMode.</param>
        /// <returns>List&lt;TaxonomyAssociationLinkModel&gt;.</returns>
        List<TaxonomyAssociationLinkModel> GetTaxonomyAssociationLinks(string clientName, string site, bool isProofing, FeatureEnums.FundOrder taxonomyOrderFeatureMode, FeatureEnums.FundNameFormat fundNameFormatFeature);

        /// <summary>
        /// Gets the taxonomy association hierarchy documents.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="site">The site.</param>
        /// <param name="isProofing">The isProofing.</param>
        /// <param name="externalID">The external identifier.</param>
        /// <param name="internalPTAID">The internal ptaid.</param>
        /// <param name="taxonomyOrderFeatureMode">taxonomyOrderFeatureMode</param>
        /// <returns>TaxonomyAssociationHierarchyModel.</returns>
        TaxonomyAssociationHierarchyModel GetTaxonomyAssociationHierarchyDocuments(string clientName, string site, bool isProofing, string externalID, int? internalPTAID, FeatureEnums.FundOrder taxonomyOrderFeatureMode, FeatureEnums.FundNameFormat fundNameFormatFeature);

        /// <summary>
        /// Gets the taxonomy specific documents.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="site">The site.</param>
        /// <param name="site">The site.</param>
        /// <param name="isProofing">isProofing.</param>
        /// <param name="internalPTAID">The internal ptaid.</param>
        /// <returns>TaxonomyAssociationData.</returns>
        TaxonomyAssociationData GetTaxonomySpecificDocuments(string clientName, string site, bool isProofing, string externalID, int? internalPTAID, FeatureEnums.FundNameFormat fundNameFormatFeature);

        /// <summary>
        /// Gets the taxonomy association documents.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="site">The site.</param>
        /// <param name="isProofing">The isProofing.</param>
        /// <param name="taxonomyOrderFeatureMode">The taxonomyOrderFeatureMode.</param>
        /// <returns>TaxonomyAssociationDocumentsModel.</returns>
        TaxonomyAssociationDocumentsModel GetTaxonomyAssociationDocuments(string clientName, string site, bool isProofing, FeatureEnums.FundOrder taxonomyOrderFeatureMode);

        /// <summary>
        /// Gets the site text from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="site">The site.</param>
        /// <returns>List&lt;HostedSiteText&gt;.</returns>
        List<HostedSiteText> GetSiteTextFromCache(string clientName, string site);

        /// <summary>
        /// Gets the page text from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>List&lt;HostedPageText&gt;.</returns>
        List<HostedPageText> GetPageTextFromCache(string clientName, string siteName, int pageId);

        /// <summary>
        /// Gets the current site information.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns>HostedSite.</returns>
        HostedSite GetCurrentSiteInfo(string clientName, string siteName);

        /// <summary>
        /// Gets the name of the current page.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns>System.String.</returns>
        string GetCurrentPageName(string clientName, string siteName);

        /// <summary>
        /// Gets the name of the page identifier for page.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns>System.Int32.</returns>
        int GetPageIDForPageName(string pageName);

        /// <summary>
        /// Gets the static resources from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <returns>List&lt;HostedStaticResource&gt;.</returns>
        List<HostedStaticResource> GetStaticResourcesFromCache(string clientName);

        /// <summary>
        /// Gets the site navigation from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="siteNavigationKey">The site navigation key.</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <param name="pageID">The page identifier.</param>
        /// <returns>Tuple&lt;System.String, System.String&gt;.</returns>
        Tuple<string, string> GetSiteNavigationFromCache(string clientName, string siteName, string siteNavigationKey, bool isProofing, int? pageID);

        /// <summary>
        /// Gets the page navigation from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="pageNavigationKey">The page navigation key.</param>
        /// <param name="isProofing">if set to <c>true</c> [is proofing].</param>
        /// <param name="pageID">The page identifier.</param>
        /// <returns>Tuple&lt;System.String, System.String&gt;.</returns>
        Tuple<string, string> GetPageNavigationFromCache(string clientName, string siteName, string pageNavigationKey, bool isProofing, int pageID);

        /// <summary>
        /// Gets the XBRL by taxonomy association identifier or external identifier.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="isProofing">isProofing</param>
        /// <param name="taxonomyAssociationId">The taxonomy association identifier.</param>
        /// <param name="externalId">The external identifier.</param>
        /// <param name="pid">if set to <c>true</c> [pid].</param>
        /// <returns>List&lt;XBRLObjectModel&gt;.</returns>
        List<XBRLObjectModel> GetXBRLByTaxonomyAssociationIDOrExternalID(string clientName, bool isProofing, int? taxonomyAssociationId, string externalId, bool pid);

        /// <summary>
        /// Gets the site feature mode from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="siteFeatureKey">The site feature key.</param>
        /// <returns>System.Int32.</returns>
        int GetSiteFeatureModeFromCache(string clientName, string siteName, string siteFeatureKey);

        /// <summary>
        /// Gets the page feature mode from cache.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <param name="pageID">The page identifier.</param>
        /// <param name="pageFeatureKey">The page feature key.</param>
        /// <returns>System.Int32.</returns>
        int GetPageFeatureModeFromCache(string clientName, string siteName, int pageID, string pageFeatureKey);

        /// <summary>
        /// Gets the name of the template.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns>System.String.</returns>
        string GetTemplateName(string clientName, string siteName);

        /// <summary>
        /// Gets the ClientDocument
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <returns>List&lt;HostedStaticResource&gt;.</returns>
        ClientDocumentObjectModel GetClientDocumentDetails(string clientName, int ClientDocumentId);

        /// <summary>
        /// GetTaxonomyAssociationClientDocuments
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="isProofing">isProofing</param>
        /// <param name="externalID1">MARKET ID</param>
        /// <param name="externalID2">Client Document type name.</param>
        /// <param name="internalTAID">The internal taid.</param>
        /// <param name="verticalIds">The vertical ids.</param>
        /// <returns>TaxonomyAssociationData.</returns>
        TaxonomyAssociationData GetTaxonomyAssociationClientDocuments(string clientName, bool isProofing, string externalID1, string externalID2,
                                                                    int? internalTAID);
        /// <summary>
        /// Gets the TaxonomyAssociationGroup Documents
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="site">Site details.</param>
        /// <param name="groupId">TaxonomyAssociationGroupId.</param>
        /// <returns>Gets the list of TaxonomyAssociationGroups.</returns>
        TaxonomyAssociationGroupModel GetTaxonomyAssociationGroupDocuments(string clientName, string site, bool isProofing,
                                                                                int? groupId, FeatureEnums.FundOrder taxonomyOrderFeatureMode);

        BrowserVersionObjectModel GetBrowserVersion(string clientName, string siteName, string browserName);

    }
}
