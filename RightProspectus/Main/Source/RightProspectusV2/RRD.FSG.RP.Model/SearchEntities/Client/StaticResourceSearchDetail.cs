// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
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
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class StaticResourceSearchDetail
        : AuditedSearchDetail<StaticResourceObjectModel>, ISearchDetailCopyAs<StaticResourceSearchDetail>
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the StaticResource identifier.
        /// </summary>
        /// <value>The StaticResourceId identifier.</value>
        public int? StaticResourceId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the StaticResourceId property.
        /// </summary>
        /// <value>The static resource identifier compare.</value>
        public ValueCompare StaticResourceIdCompare { get; set; }
        /// <summary>
        /// Gets or sets the FileName identifier.
        /// </summary>
        /// <value>The FileName.</value>
        public string FileName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the FileName property..
        /// </summary>
        /// <value>The file name compare.</value>
        public TextCompare FileNameCompare { get; set; }
        /// <summary>
        /// Gets or sets the Size identifier.
        /// </summary>
        /// <value>The Size.</value>
        public int? Size { get; set; }
        /// <summary>
        /// Determines the type of comparison for the Size property.
        /// </summary>
        /// <value>The size compare.</value>
        public ValueCompare SizeCompare { get; set; }
        /// <summary>
        /// Gets or sets the MimeType identifier.
        /// </summary>
        /// <value>The MimeType.</value>
        public string MimeType { get; set; }
        /// <summary>
        /// Determines the type of comparison for the MimeType property..
        /// </summary>
        /// <value>The MIME type compare.</value>
        public TextCompare MimeTypeCompare { get; set; }



        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<StaticResourceObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.StaticResourceId,entity.StaticResourceId,this.StaticResourceIdCompare)
                    && this.Match(this.FileName,entity.FileName,this.FileNameCompare)
                    && this.Match(this.Size,entity.Size,this.SizeCompare)
                    && this.Match(this.MimeType,entity.MimeType,this.MimeTypeCompare);

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
            where TCopy : StaticResourceSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.StaticResourceId = this.StaticResourceId;
            copy.StaticResourceIdCompare = this.StaticResourceIdCompare;
            copy.FileName = this.FileName;
            copy.FileNameCompare = this.FileNameCompare;
            copy.Size = this.Size;
            copy.SizeCompare = this.SizeCompare;
            copy.MimeType = this.MimeType;
            copy.MimeTypeCompare = this.MimeTypeCompare;
            return copy;
        }

        #endregion
    }
}
