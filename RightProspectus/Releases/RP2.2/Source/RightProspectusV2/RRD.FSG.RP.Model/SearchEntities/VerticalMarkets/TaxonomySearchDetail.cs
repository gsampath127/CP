// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.VerticalMarket;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RRD.FSG.RP.Model.SearchEntities.VerticalMarkets
{
    /// <summary>
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class TaxonomySearchDetail
        : SearchDetail<TaxonomyObjectModel>, ISearchDetailCopyAs<TaxonomySearchDetail>
    {
        #region Public Properties

        /// <summary>
        /// TaxonomyId
        /// </summary>
        /// <value>The taxonomy identifier.</value>
        public int? TaxonomyId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the PageID property.
        /// </summary>
        /// <value>The page identifier compare.</value>
        public ValueCompare PageIDCompare { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TemplateID property.
        /// </summary>
        /// <value>The taxonomy identifier compare.</value>
        public ValueCompare TaxonomyIdCompare { get; set; }
        /// <summary>
        /// TaxonomyName
        /// </summary>
        /// <value>The name of the taxonomy.</value>
        public string TaxonomyName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TaxonomyName property..
        /// </summary>
        /// <value>The taxonomy name compare.</value>
        public TextCompare TaxonomyNameCompare { get; set; }
        /// <summary>
        /// Level
        /// </summary>
        /// <value>The level.</value>
        public int? Level { get; set; }
        /// <summary>
        /// Determines the type of comparison for the Level property..
        /// </summary>
        /// <value>The level compare.</value>
        public ValueCompare LevelCompare { get; set; }



        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<TaxonomyObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.TaxonomyId, entity.TaxonomyId, this.TaxonomyIdCompare)
                    && this.Match(this.TaxonomyName, entity.TaxonomyName, this.TaxonomyNameCompare)
                    && this.Match(this.Level, entity.Level, this.LevelCompare);
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
            where TCopy : TaxonomySearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.TaxonomyId = this.TaxonomyId;
            copy.TaxonomyIdCompare = this.TaxonomyIdCompare;
            copy.TaxonomyName = this.TaxonomyName;
            copy.TaxonomyNameCompare = this.TaxonomyNameCompare;
            copy.Level = this.Level;
            copy.LevelCompare = this.LevelCompare;
            return copy;
        }

        #endregion
    }
}
