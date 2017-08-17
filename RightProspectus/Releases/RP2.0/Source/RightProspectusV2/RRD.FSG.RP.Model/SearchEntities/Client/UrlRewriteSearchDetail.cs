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
    public class UrlRewriteSearchDetail
        : AuditedSearchDetail<UrlRewriteObjectModel>, ISearchDetailCopyAs<UrlRewriteSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the UrlRewrite identifier.
        /// </summary>
        /// <value>The UrlRewriteId identifier.</value>
        public int? UrlRewriteId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the UrlRewriteId property.
        /// </summary>
        /// <value>The URL rewrite identifier compare.</value>
        public ValueCompare UrlRewriteIdCompare { get; set; }
        /// <summary>
        /// Gets or sets the MatchPattern.
        /// </summary>
        /// <value>The MatchPattern.</value>
        public string MatchPattern { get; set; }
        /// <summary>
        /// Determines the type of comparison for the MatchPattern property..
        /// </summary>
        /// <value>The match pattern compare.</value>
        public TextCompare MatchPatternCompare { get; set; }
        /// <summary>
        /// Gets or sets the RewriteFormat.
        /// </summary>
        /// <value>The RewriteFormat.</value>
        public string RewriteFormat { get; set; }
        /// <summary>
        /// Determines the type of comparison for the RewriteFormat property..
        /// </summary>
        /// <value>The rewrite format compare.</value>
        public TextCompare RewriteFormatCompare { get; set; }
        /// <summary>
        /// Gets or sets the PatternName.
        /// </summary>
        /// <value>The PatternName.</value>
        public string PatternName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the PatternName property..
        /// </summary>
        /// <value>The pattern name compare.</value>
        public TextCompare PatternNameCompare { get; set; }


        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<UrlRewriteObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.UrlRewriteId,entity.UrlRewriteId,this.UrlRewriteIdCompare)
                    && this.Match(this.MatchPattern,entity.MatchPattern,this.MatchPatternCompare)
                    && this.Match(this.RewriteFormat,entity.RewriteFormat,this.RewriteFormatCompare)
                    && this.Match(this.PatternName,entity.PatternName,this.PatternNameCompare);
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
            where TCopy : UrlRewriteSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.UrlRewriteId = this.UrlRewriteId;
            copy.UrlRewriteIdCompare = this.UrlRewriteIdCompare;
            copy.MatchPattern = this.MatchPattern;
            copy.MatchPatternCompare = this.MatchPatternCompare;
            copy.RewriteFormat = this.RewriteFormat;
            copy.RewriteFormatCompare = this.RewriteFormatCompare;
            copy.PatternName = this.PatternName;
            copy.PatternNameCompare = this.PatternNameCompare;
            return copy;
        }

        #endregion
    }
}
