// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-20-2015
//
// Last Modified By : 
// Last Modified On : 11-17-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;

/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.SearchEntities.System
{
    /// <summary>
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class TemplateFeatureSearchDetail
        : SearchDetail<TemplateFeatureObjectModel>, ISearchDetailCopyAs<TemplateFeatureSearchDetail>
    {

        #region Public Properties
        /// <summary>
        /// TemplateID
        /// </summary>
        /// <value>The template identifier.</value>
        public int? TemplateId { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TemplateID property.
        /// </summary>
        /// <value>The template identifier compare.</value>
        public ValueCompare TemplateIdCompare { get; set; }

        /// <summary>
        /// TemplateFeatureKey
        /// </summary>
        /// <value>The feature key.</value>
        public string  FeatureKey { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TemplateFeatureKey property.
        /// </summary>
        /// <value>The feature key compare.</value>
        public TextCompare FeatureKeyCompare { get; set; }

        /// <summary>
        /// TemplateFeatureDescription
        /// </summary>
        /// <value>The feature description.</value>
        public string FeatureDescription { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TemplateFeatureDescription property.
        /// </summary>
        /// <value>The feature description compare.</value>
        public TextCompare FeatureDescriptionCompare { get; set; }


        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<TemplateFeatureObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.TemplateId, entity.TemplateId, this.TemplateIdCompare)
                    && this.Match(this.FeatureKey, entity.FeatureKey, this.FeatureKeyCompare)
                    && this.Match(this.FeatureDescription, entity.FeatureDescription, this.FeatureDescriptionCompare);
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
            where TCopy : TemplateFeatureSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.TemplateId = this.TemplateId;
            copy.TemplateIdCompare = this.TemplateIdCompare;
            copy.FeatureKey = this.FeatureKey;
            copy.FeatureKeyCompare = this.FeatureKeyCompare;
            copy.FeatureDescription = this.FeatureDescription;
            copy.FeatureDescriptionCompare = this.FeatureDescriptionCompare;
         
            return copy;
        }
        #endregion
     
    }
}
