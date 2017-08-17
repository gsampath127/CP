// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
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
    /// Defines a search detail entity.
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory"/>.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class TemplatePageSearchDetail
        : SearchDetail<TemplatePageObjectModel>, ISearchDetailCopyAs<TemplatePageSearchDetail>
    {
        #region Public Properties

        /// <summary>
        /// PageID
        /// </summary>
        public int? PageID { get; set; }
        /// <summary>
        /// Determines the type of comparison for the PageID property.
        /// </summary>
        public ValueCompare PageIDCompare { get; set; }
        /// <summary>
        /// TemplateID
        /// </summary>
        public int? TemplateID { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TemplateID property.
        /// </summary>
        public ValueCompare TemplateIDCompare { get; set; }
        /// <summary>
        /// TemplateName
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the TemplateName property..
        /// </summary>
        public TextCompare TemplateNameCompare { get; set; }
        /// <summary>
        /// PageName
        /// </summary>
        public string PageName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the PageName property..
        /// </summary>
        public TextCompare PageNameCompare { get; set; }



        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        public override Func<TemplatePageObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.TemplateID,entity.TemplateID,this.TemplateIDCompare)
                    && this.Match(this.PageID,entity.PageID,this.PageIDCompare)
                    && this.Match(this.TemplateName,entity.TemplateName,this.TemplateNameCompare)
                    && this.Match(this.PageName,entity.PageName,this.PageNameCompare);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns an enumeration of DbParameter entities based on values set in th underlying search detail entity.
        /// </summary>
        /// <param name="dataAccess">Data access instance used to create the parameters.</param>
        /// <returns>A collection of DbParameter entities.</returns>
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
            where TCopy : TemplatePageSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.TemplateID = this.TemplateID;
            copy.TemplateIDCompare = this.TemplateIDCompare;
            copy.PageID = this.PageID;
            copy.PageIDCompare = this.PageIDCompare;
            copy.TemplateName = this.TemplateName;
            copy.TemplateNameCompare = this.TemplateNameCompare;
            copy.PageName = this.PageName;
            copy.PageNameCompare = this.PageNameCompare;
            return copy;
        }

        #endregion
    }
}
