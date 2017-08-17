// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-27-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;

namespace RRD.FSG.RP.Model.Interfaces.VerticalMarket
{
    /// <summary>
    /// Interface IHostedVerticalPageScenariosFactory
    /// </summary>
    public interface IHostedVerticalPageScenariosFactory
    {
        /// <summary>
        /// Gets the taxonomy association links vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyassociationlinks">The taxonomyassociationlinks.</param>
        /// <returns>List&lt;TaxonomyAssociationLinkModel&gt;.</returns>
        List<TaxonomyAssociationLinkModel> GetTaxonomyAssociationLinksVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                                                                        List<TaxonomyAssociationLinkModel> taxonomyassociationlinks, FeatureEnums.FundOrder taxonomyOrderFeatureMode, FeatureEnums.FundNameFormat fundNameFormatFeature);


        /// <summary>
        /// Gets the taxonomy association for UrlGeneration  vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyassociationurlgenerator">The UrlGenerationObjectModel.</param>
        /// <returns>List&lt;UrlGenerationObjectModel&gt;.</returns>
        List<UrlGenerationObjectModel> GetUrlGenerationVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                                                                        List<UrlGenerationObjectModel> urlgenerationdata);
        /// <summary>
        /// Gets the taxonomy association for UrlGeneration  vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyassociationurlgenerator">The UrlGenerationObjectModel.</param>
        /// <returns>List&lt;UrlGenerationObjectModel&gt;.</returns>
        List<CUSIPMergerLiqudationReportObjectModel> GetCUSIPMergerLiqudationReportData(DataTable marketIDs);

        /// Gets the taxonomy association links vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyassociationlinks">The taxonomyassociationlinks.</param>
        /// <returns>List&lt;TaxonomyAssociationLinkModel&gt;.</returns>
        List<BillingReportActiveFundDetails> GetBillingReportVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                                                                       List<BillingReportActiveFundDetails> taxonomyassociationlinks, DateTime startDate, DateTime endDate);

        /// Gets the GetDocumentUpdateReportReportVerticalData
        /// </summary>
        /// /// <param name="EndDate">EndDate</param>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>        
        /// <returns>DataTable</returns>
        DataTable GetDocumentUpdateReportVerticalData(DateTime StartDate, DateTime EndDate, DataTable clientTaxonomyIDs, string clientName);


        /// <summary>
        /// Gets the taxonomy association hierarchy documents vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyAssociationHierarchyModel">The taxonomy association hierarchy model.</param>
        /// <param name="taxonomyOrderFeatureMode">taxonomyOrderFeatureMode.</param>
        /// <returns>TaxonomyAssociationHierarchyModel.</returns>
        TaxonomyAssociationHierarchyModel GetTaxonomyAssociationHierarchyDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                                                                                TaxonomyAssociationHierarchyModel taxonomyAssociationHierarchyModel, FeatureEnums.FundOrder taxonomyOrderFeatureMode, FeatureEnums.FundNameFormat fundNameFormatFeature);

        /// <summary>
        /// Gets the taxonomy specific documents vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyAssociationData">The taxonomy association data.</param>
        /// <returns>TaxonomyAssociationData.</returns>
        TaxonomyAssociationData GetTaxonomySpecificDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                                                                                TaxonomyAssociationData taxonomyAssociationData, FeatureEnums.FundNameFormat fundNameFormatFeature);

        /// <summary>
        /// Gets the taxonomy specific documents vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyAssociationData">The taxonomy association data.</param>
        /// <returns>TaxonomyAssociationData.</returns>
        TaxonomyAssociationData GetTaxonomyAssociationClientDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                                                                                TaxonomyAssociationData taxonomyAssociationData);

        /// <summary>
        /// Gets the taxonomy association documents vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyAssociationDocumentsModel">The taxonomy association documents model.</param>
        /// <param name="taxonomyOrderFeatureMode">taxonomyOrderFeatureMode.</param>
        /// <returns>TaxonomyAssociationDocumentsModel.</returns>
        TaxonomyAssociationDocumentsModel GetTaxonomyAssociationDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                                                                                TaxonomyAssociationDocumentsModel taxonomyAssociationDocumentsModel, FeatureEnums.FundOrder taxonomyOrderFeatureMode);

        /// <summary>
        /// Gets the XBRL details for taxonomy identifier.
        /// </summary>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyId">The taxonomy identifier.</param>
        /// <param name="taxonomyName">Name of the taxonomy.</param>
        /// <returns>List&lt;XBRLObjectModel&gt;.</returns>
        List<XBRLObjectModel> GetXBRLDetailsForTaxonomyID(string clientName, int taxonomyId, string taxonomyName);


        /// <summary>
        /// Gets the request material print requests.
        /// </summary>
        /// <param name="clientTaxonomyIDsWithDocTypeIDs">The client taxonomy i ds with document type i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="requestMaterialPrintRequestData">The request material print request data.</param>
        /// <returns>List&lt;RequestMaterialPrintHistory&gt;.</returns>
        List<RequestMaterialPrintHistory> GetRequestMaterialPrintRequests(DataTable clientTaxonomyIDsWithDocTypeIDs, string clientName, List<RequestMaterialPrintHistory> requestMaterialPrintRequestData);

        /// <summary>
        /// Gets the taxonomy association group documents vertical data.
        /// </summary>
        /// <param name="clientTaxonomyIDs">The client taxonomy i ds.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="taxonomyAssociationHierarchyModel">The taxonomy association group model.</param>
        /// <param name="taxonomyOrderFeatureMode">TtaxonomyOrderFeatureMode.</param>
        /// <returns>TaxonomyAssociationGroupModel.</returns>
        TaxonomyAssociationGroupModel GetTaxonomyAssociationGroupDocumentsVerticalData(DataTable clientTaxonomyIDs, string clientName,
                                            TaxonomyAssociationGroupModel taxonomyAssociationGroupModel, FeatureEnums.FundOrder taxonomyOrderFeatureMode);
    }

}
