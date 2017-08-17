// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RRD.FSG.RP.Model.SearchEntities.System
{
    /// <summary>
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class TemplatePageTextSearchDetail
        : SearchDetail<TemplatePageTextObjectModel>, ISearchDetailCopyAs<TemplatePageTextSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// PageID
        /// </summary>
        /// <value>The page identifier.</value>
        public int? PageID { get; set; }
        /// <summary>
        /// Determines the type of comparison for the PageID property.
        /// </summary>
        /// <value>The page identifier compare.</value>
        public ValueCompare PageIDCompare { get; set; }
        /// <summary>
        /// TemplateID
        /// </summary>
        /// <value>The template identifier.</value>
        public int? TemplateID { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TemplateID property.
        /// </summary>
        /// <value>The template identifier compare.</value>
        public ValueCompare TemplateIDCompare { get; set; }
        /// <summary>
        /// ResourceKey
        /// </summary>
        /// <value>The resource key.</value>
        public string ResourceKey { get; set; }
        /// <summary>
        /// Determines the type of comparison for the ResourceKey property..
        /// </summary>
        /// <value>The resource key compare.</value>
        public TextCompare ResourceKeyCompare { get; set; }
        /// <summary>
        /// IsHTML
        /// </summary>
        /// <value><c>null</c> if [is HTML] contains no value, <c>true</c> if [is HTML]; otherwise, <c>false</c>.</value>
        public bool? IsHTML { get; set; }
        /// <summary>
        /// Determines the type of comparison for the IsHTML property.
        /// </summary>
        /// <value>The is HTML compare.</value>
        public ValueCompare IsHTMLCompare { get; set; }
        /// <summary>
        /// DefaultText
        /// </summary>
        /// <value>The default text.</value>
        public string DefaultText { get; set; }
        /// <summary>
        /// Determines the type of comparison for the DefaultText property..
        /// </summary>
        /// <value>The default text compare.</value>
        public TextCompare DefaultTextCompare { get; set; }


        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<TemplatePageTextObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.PageID,entity.PageID,this.PageIDCompare)
                    && this.Match(this.TemplateID,entity.TemplateID,this.TemplateIDCompare)
                    && this.Match(this.ResourceKey,entity.ResourceKey,this.ResourceKeyCompare)
                    && this.Match(this.IsHTML,entity.IsHTML,this.IsHTMLCompare)
                    && this.Match(this.DefaultText,entity.DefaultText,this.DefaultTextCompare);
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
            where TCopy : TemplatePageTextSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.PageID = this.PageID;
            copy.PageIDCompare = this.PageIDCompare;
            copy.TemplateID = this.TemplateID;
            copy.TemplateIDCompare = this.TemplateIDCompare;
            copy.ResourceKey = this.ResourceKey;
            copy.ResourceKeyCompare = this.ResourceKeyCompare;
            copy.IsHTML = this.IsHTML;
            copy.IsHTMLCompare = this.IsHTMLCompare;
            copy.Name = this.Name;
            copy.NameCompare = this.NameCompare;
            return copy;
        }

        #endregion
    }
}
