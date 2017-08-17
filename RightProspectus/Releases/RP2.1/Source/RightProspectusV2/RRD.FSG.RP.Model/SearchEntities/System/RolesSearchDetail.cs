﻿// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-20-2015
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
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class RolesSearchDetail
        : AuditedSearchDetail<RolesObjectModel>, ISearchDetailCopyAs<RolesSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the Roles identifier.
        /// </summary>
        /// <value>The Roles identifier.</value>
        public int? RolesID { get; set; }
        /// <summary>
        /// Determines the type of comparison for the RolesID property.
        /// </summary>
        /// <value>The roles identifier compare.</value>
        public ValueCompare RolesIDCompare { get; set; }

        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<RolesObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.Name,entity.Name,this.NameCompare)
                    && this.Match(this.RolesID,entity.RoleId,this.RolesIDCompare);
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
            where TCopy : RolesSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.RolesID = this.RolesID;
            copy.RolesIDCompare = this.RolesIDCompare;
            return copy;
        }

        #endregion
    }
}