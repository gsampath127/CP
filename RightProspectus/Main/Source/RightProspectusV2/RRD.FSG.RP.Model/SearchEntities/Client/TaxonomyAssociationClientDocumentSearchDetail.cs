using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRD.FSG.RP.Model.SearchEntities.Client
{
    public class TaxonomyAssociationClientDocumentSearchDetail
         : AuditedSearchDetail<TaxonomyAssociationClientDocumentObjectModel>, ISearchDetailCopyAs<TaxonomyAssociationClientDocumentSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// TaxonomyId
        /// </summary>
        /// <value>The taxonomyAssociation identifier.</value>
        public int? TaxonomyId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TaxonomyId property.
        /// </summary>
        /// <value>The taxonomy identifier compare.</value>
        public ValueCompare TaxonomyIdCompare { get; set; }
        /// <summary>
        /// TaxonomyName
        /// </summary>
        /// <value>The name of the taxonomy.</value>
        public string TaxonomyAssociationName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TaxonomyName property..
        /// </summary>
        /// <value>The taxonomy name compare.</value>
        public TextCompare TaxonomyAssociationNameCompare { get; set; }
        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        /// <value>The taxonomyAssociation identifier.</value>
        public int? ClientDocumentTypeId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TaxonomyId property.
        /// </summary>
        /// <value>The taxonomy identifier compare.</value>
        public ValueCompare ClientDocumentTypeIdCompare { get; set; }
        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        /// <value>The taxonomyAssociation identifier.</value>
        public string ClientDocumentTypeName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TaxonomyId property.
        /// </summary>
        /// <value>The taxonomy identifier compare.</value>
        public TextCompare ClientDocumentTypeNameCompare { get; set; }
        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        /// <value>The taxonomyAssociation identifier.</value>
        public int? ClientDocumentId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TaxonomyId property.
        /// </summary>
        /// <value>The taxonomy identifier compare.</value>
        public ValueCompare ClientDocumentIdCompare { get; set; }
        /// <summary>
        /// TaxonomyAssociationId
        /// </summary>
        /// <value>The taxonomyAssociation identifier.</value>
        public string ClientDocumentName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TaxonomyId property.
        /// </summary>
        /// <value>The taxonomy identifier compare.</value>
        public TextCompare ClientDocumentNameCompare { get; set; }
        /// <summary>
        /// ClientDocumentFileName
        /// </summary>
        /// <value>The FileName.</value>
        public string ClientDocumentFileName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TaxonomyId property.
        /// </summary>
        /// <value>The taxonomy identifier compare.</value>
        public TextCompare ClientDocumentFileNameCompare { get; set; }

        public override Func<TaxonomyAssociationClientDocumentObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.TaxonomyId, entity.TaxonomyId, this.TaxonomyIdCompare)
                    && this.Match(this.TaxonomyAssociationName, entity.TaxonomyAssociationName, this.TaxonomyAssociationNameCompare)
                    && this.Match(this.ClientDocumentTypeId, entity.ClientDocumentTypeId, this.ClientDocumentTypeIdCompare)
                    && this.Match(this.ClientDocumentTypeName, entity.ClientDocumentTypeName, this.ClientDocumentTypeNameCompare)
                    && this.Match(this.ClientDocumentId, entity.ClientDocumentId, this.ClientDocumentIdCompare)
                    && this.Match(this.ClientDocumentName,entity.ClientDocumentName, this.ClientDocumentTypeNameCompare)
                    && this.Match(this.ClientDocumentFileName, entity.ClientDocumentFileName, this.ClientDocumentFileNameCompare);

            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns an enumeration of DbParameter entities based on values set in th underlying search detail entity.
        /// </summary>
        /// <param name="dataAccess">Data access instance used to create the parameters.</param>
        /// <returns>A collection of DbParameter entities.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override IEnumerable<DbParameter> GetSearchParameters(IDataAccess dataAccess)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new search detail and copies the parameters from this one to it.
        /// </summary>
        /// <typeparam name="TCopy">Type of search detail to create.</typeparam>
        /// <returns>A new search detail with the same search parameters as this one.</returns>
        public virtual new TCopy CopyAs<TCopy>()
            where TCopy : TaxonomyAssociationClientDocumentSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.TaxonomyId = this.TaxonomyId;
            copy.TaxonomyIdCompare = this.TaxonomyIdCompare;
            copy.TaxonomyAssociationName = this.TaxonomyAssociationName;
            copy.TaxonomyAssociationNameCompare = this.TaxonomyAssociationNameCompare;
            copy.ClientDocumentTypeId = this.ClientDocumentTypeId;
            copy.ClientDocumentTypeIdCompare = this.ClientDocumentTypeIdCompare;
            copy.ClientDocumentTypeName = this.ClientDocumentTypeName;
            copy.ClientDocumentTypeNameCompare = this.ClientDocumentTypeNameCompare;
            copy.ClientDocumentId = this.ClientDocumentId;
            copy.ClientDocumentIdCompare = this.ClientDocumentIdCompare;
            copy.ClientDocumentName = this.ClientDocumentName;
            copy.ClientDocumentNameCompare = this.ClientDocumentNameCompare;
            copy.ClientDocumentFileName = this.ClientDocumentFileName;
            copy.ClientDocumentFileNameCompare = this.ClientDocumentFileNameCompare;
            return copy;
        }

        #endregion
    }
}
