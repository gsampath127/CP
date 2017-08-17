// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-27-2015
//
// Last Modified By : 
// Last Modified On : 10-29-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace RRD.FSG.RP.Model.SearchEntities.System
{
    /// <summary>
    /// Class TemplatePageNavigationSearchDetail.
    /// </summary>
  public  class TemplatePageNavigationSearchDetail: SearchDetail<TemplatePageNavigationObjectModel>, ISearchDetailCopyAs<TemplatePageNavigationSearchDetail>
    {
        
       #region Public Properties
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
       /// PageID
       /// </summary>
       /// <value>The page identifier.</value>
       public int? PageId { get; set; }
       /// <summary>
       /// Determines the type of comparison for the PageID
       /// </summary>
       /// <value>The page identifier compare.</value>
       public ValueCompare PageIdCompare { get; set; }

       /// <summary>
       /// NavigationKey
       /// </summary>
       /// <value>The navigation key.</value>
       public string NavigationKey { get; set; }
       /// <summary>
       /// Determines the type of comparison for the NavigationKey property..
       /// </summary>
       /// <value>The navigation key compare.</value>
       public TextCompare NavigationKeyCompare { get; set; }
       /// <summary>
       /// XslTransform
       /// </summary>
       /// <value>The XSL transform.</value>
       public string XslTransform { get; set; }
       /// <summary>
       /// Determines the type of comparison for the XslTransform property.
       /// </summary>
       /// <value>The XSL transform compare.</value>
       public TextCompare XslTransformCompare { get; set; }
       /// <summary>
       /// DefaultNavigationXml
       /// </summary>
       /// <value>The default navigation XML.</value>
       public string DefaultNavigationXml { get; set; }
       /// <summary>
       /// Determines the type of comparison for the DefaultNavigationXml property..
       /// </summary>
       /// <value>The default navigation XML compare.</value>
       public TextCompare DefaultNavigationXmlCompare { get; set; }


       /// <summary>
       /// Internal property that returns a search predicate function delegate used by the Search method.
       /// </summary>
       /// <value>The search predicate.</value>
       public override Func<TemplatePageNavigationObjectModel, bool> SearchPredicate
       {
           get
           {
               return entity =>
                   base.SearchPredicate(entity)
                   && this.Match(this.TemplateID, entity.TemplateID, this.TemplateIDCompare)
                   && this.Match(this.PageId, entity.PageID, this.PageIdCompare)
                   && this.Match(this.NavigationKey, entity.NavigationKey, this.NavigationKeyCompare)
                   && this.Match(this.XslTransform, entity.XslTransform, this.XslTransformCompare)
                   && this.Match(this.DefaultNavigationXml, entity.DefaultNavigationXml, this.DefaultNavigationXmlCompare);
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
           where TCopy : TemplatePageNavigationSearchDetail, new()
       {
           TCopy copy = base.CopyAs<TCopy>();
           copy.TemplateID = this.TemplateID;
           copy.TemplateIDCompare = this.TemplateIDCompare;
           copy.PageId = this.PageId;
           copy.PageIdCompare = this.PageIdCompare;
           copy.NavigationKey = this.NavigationKey;
           copy.NavigationKeyCompare = this.NavigationKeyCompare;
           copy.XslTransform = this.XslTransform;
           copy.XslTransformCompare = this.XslTransformCompare;
           copy.DefaultNavigationXml = this.DefaultNavigationXml;
           copy.DefaultNavigationXmlCompare = this.DefaultNavigationXmlCompare;
           return copy;
       }

       #endregion

    }
}
