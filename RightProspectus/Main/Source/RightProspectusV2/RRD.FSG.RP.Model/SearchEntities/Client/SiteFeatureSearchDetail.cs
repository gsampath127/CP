// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
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
    public class SiteFeatureSearchDetail
        : AuditedSearchDetail<SiteFeatureObjectModel>, ISearchDetailCopyAs<SiteFeatureSearchDetail>
    {

        #region Public Properties
        /// <summary>
        /// SiteId.
        /// </summary>
        /// <value>The site identifier.</value>

        public int SiteId { get; set; }

        /// <summary>
        /// Determines the type of comparison for the SiteId property.
        /// </summary>
        /// <value>The site identifier compare.</value>
        public ValueCompare SiteIdCompare { get; set; }

        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        /// <value>The Key.</value>

        public string SiteKey { get; set; }

        /// <summary>
        /// Determines the type of comparison for the Key property.
        /// </summary>
        /// <value>The site key compare.</value>
        public TextCompare SiteKeyCompare { get; set; }

        /// <summary>
        /// Gets or sets the Feature Mode.
        /// </summary>
        /// <value>The Key.</value>
        public int FeatureMode { get; set; }

        /// <summary>
        /// Determines the type of comparison for the FeatureMode property.
        /// </summary>
        /// <value>The feature mode compare.</value>
        public ValueCompare FeatureModeCompare { get; set; }


        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<SiteFeatureObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.SiteId, entity.SiteId, this.SiteIdCompare)
                    && this.Match(this.SiteKey, entity.SiteKey, this.SiteKeyCompare);
                   
                 
            }
        }
        #endregion


        #region

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
            where TCopy : SiteFeatureSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.SiteId = this.SiteId;
            copy.SiteIdCompare = this.SiteIdCompare;
            copy.SiteKey = this.SiteKey;
            copy.SiteKeyCompare = this.SiteKeyCompare;
            copy.FeatureMode = this.FeatureMode;
            copy.FeatureModeCompare = this.FeatureModeCompare;
            return copy;
        }

        

        #endregion
    }
}
