using RRD.FSG.RP.Model.Entities.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.Interfaces
{
    /// <summary>
    /// Interface IVerticalDataImportFactory
    /// </summary>
    public interface IVerticalDataImportFactory
    {
        string ClientName { get; set; }
        int UserId { get; set; }
        string SaveDocumentTypeAssociation(List<DocumentTypeAssociationObjectModel> added,
                      List<DocumentTypeAssociationObjectModel> updated, List<DocumentTypeAssociationObjectModel> deleted);
        string SaveTaxonomyAssociation(List<TaxonomyAssociationObjectModel> added,
                     List<TaxonomyAssociationObjectModel> updated, List<TaxonomyAssociationObjectModel> deleted);
        string ApproveProofing(int siteId);

        List<TaxonomyAssociationObjectModel> GetTaxonomyAssociationUsingSiteId(string clientName, int siteId, bool isProofing);
        List<TaxonomyAssociationProductObjectModel> GetTaxonomyAssociationProduct(int siteId);
        string SaveTaxonomyHierarchy(List<TaxonomyAssociationProductObjectModel> updated, List<TaxonomyAssociationProductObjectModel> deleted, List<TaxonomyAssociationProductObjectModel> added);
        List<TaxonomyAssociationObjectModel> SaveTaxonomyAssocaitionExcelImport(int? siteId, List<TaxonomyAssociationObjectModel> importData, out string status);
        string SaveDocumentTypeAssociationExcelImport(int? siteId, List<DocumentTypeAssociationObjectModel> importData);
        string SaveTaxonomyHierarchyExcelImport(List<TaxonomyAssociationProductObjectModel> importData);
        void CustomizeFundOrder(int siteid);
        string SaveFootNotes(List<FootnoteObjectModel> added,
                    List<FootnoteObjectModel> updated, List<FootnoteObjectModel> deleted);
        string SaveFootnoteExcelImport(List<FootnoteObjectModel> importData);
        List<TaxonomyGroupObjectModel> GetTaxonomyAssociationGroups(int? siteId);
        string SaveTaxonomyAssociationGroup(List<TaxonomyGroupObjectModel> added,
                      List<TaxonomyGroupObjectModel> updated, List<TaxonomyGroupObjectModel> deleted);
        List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> GetTaxonomyAssociationGroupFunds(int? siteId);
        string SaveTaxonomyGroupFundMapping(List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> added, List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> updated, List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> deleted);
        string SaveTaxonomyGroupFundsExcelImport(List<TaxonomyAssociationGroupTaxonomyAssociationObjectModel> importData);
        string SaveTaxonomyGroupExcelImport(List<TaxonomyGroupObjectModel> importData);
        List<TaxonomyAssociationObjectModel> VerifyTaxonomywithVerticalData(List<TaxonomyAssociationObjectModel> taxonomyData);
    }
}
