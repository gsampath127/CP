// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-03-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RRD.FSG.RP.Model.SearchEntities.Client
{
    /// <summary>
    /// Class ClientDocumentSearchDetail.
    /// </summary>
    public class ClientDocumentSearchDetail : AuditedSearchDetail<ClientDocumentObjectModel>, ISearchDetailCopyAs<ClientDocumentSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// ClientDocumentId
        /// </summary>
        /// <value>The client document identifier.</value>
        public int ClientDocumentId { get; set; }

        /// <summary>
        /// Determines the type of comparison for the TaxonomyId property.
        /// </summary>
        /// <value>The client document identifier compare.</value>
        public ValueCompare ClientDocumentIdCompare { get; set; }

        /// <summary>
        /// ClientDocumentTypeId
        /// </summary>
        /// <value>The client document type identifier.</value>
        public int? ClientDocumentTypeId { get; set; }

        /// <summary>
        /// Determines the type of comparison for the TaxonomyId property.
        /// </summary>
        /// <value>The client document type identifier compare.</value>
        public ValueCompare ClientDocumentTypeIdCompare { get; set; }

        /// <summary>
        /// FileName
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Determines the type of comparison for the FileName property..
        /// </summary>
        /// <value>The file name compare.</value>
        public TextCompare FileNameCompare { get; set; }

        /// <summary>
        /// MimeType
        /// </summary>
        /// <value>The type of the MIME.</value>
        public string MimeType { get; set; }

        /// <summary>
        /// Determines the type of comparison for the MimeType property..
        /// </summary>
        /// <value>The MIME type compare.</value>
        public TextCompare MimeTypeCompare { get; set; }

        /// <summary>
        /// IsPrivate
        /// </summary>
        /// <value><c>null</c> if [is private] contains no value, <c>true</c> if [is private]; otherwise, <c>false</c>.</value>
        public bool? IsPrivate { get; set; }

        /// <summary>
        /// Determines the type of comparison for the ResourceKey property..
        /// </summary>
        /// <value>The is private compare.</value>
        public ValueCompare IsPrivateCompare { get; set; }




        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<ClientDocumentObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.ClientDocumentTypeId, entity.ClientDocumentTypeId, this.ClientDocumentTypeIdCompare)
                    && this.Match(this.FileName, entity.FileName, this.FileNameCompare)
                    && this.Match(this.Name, entity.Name, this.NameCompare)
                    && this.Match(this.MimeType, entity.MimeType, this.MimeTypeCompare)
                    && this.Match(this.IsPrivate, entity.IsPrivate, this.IsPrivateCompare);
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
            where TCopy : ClientDocumentSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.ClientDocumentId = this.ClientDocumentId;
            copy.ClientDocumentIdCompare = this.ClientDocumentIdCompare;
            copy.ClientDocumentTypeId = this.ClientDocumentTypeId;
            copy.ClientDocumentTypeIdCompare = this.ClientDocumentTypeIdCompare;
            copy.FileName = this.FileName;
            copy.FileNameCompare = this.FileNameCompare;
            copy.MimeType = this.MimeType;
            copy.MimeTypeCompare = this.MimeTypeCompare;
            copy.IsPrivate = this.IsPrivate;
            copy.IsPrivateCompare = this.IsPrivateCompare;
            
            return copy;
        }

        #endregion
        /// <summary>
        /// Instantiates a new entity and copies the properties from this entity to the new one.
        /// </summary>
        /// <typeparam name="TCopy">Type of entity to create.</typeparam>
        /// <returns>A new entity with the search details copied.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        TCopy ISearchDetailCopyAs<ClientDocumentSearchDetail>.CopyAs<TCopy>()
        {
            throw new NotImplementedException();
        }
    }
}
