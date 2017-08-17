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
    /// Can be used for building a list of search parameters for the GetEntitiesBySearch method of <see cref="I:IFactory" />.
    /// Can also be used to search a collection of entities directly.
    /// </summary>
    public class UserSearchDetail
        : AuditedSearchDetail<UserObjectModel>, ISearchDetailCopyAs<UserSearchDetail>
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the User identifier.
        /// </summary>
        /// <value>The User identifier.</value>
        public int? UserID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the UserID property.
        /// </summary>
        /// <value>The user identifier compare.</value>
        public ValueCompare UserIDCompare { get; set; }
        /// <summary>
        /// Gets the name of the database of the Users being searched.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the FirstName property..
        /// </summary>
        /// <value>The first name compare.</value>
        public TextCompare FirstNameCompare { get; set; }
        /// <summary>
        /// Gets the LastName being searched.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }
        /// <summary>
        /// Determines the type of comparison for the LastName property..
        /// </summary>
        /// <value>The last name compare.</value>
        public TextCompare LastNameCompare { get; set; }
        /// <summary>
        /// Gets the Email being searched.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }
        /// <summary>
        /// Determines the type of comparison for the Email property..
        /// </summary>
        /// <value>The email compare.</value>
        public TextCompare EmailCompare { get; set; }




        /// <summary>
        /// Internal property that returns a search predicate function delegate used by the Search method.
        /// </summary>
        /// <value>The search predicate.</value>
        public override Func<UserObjectModel, bool> SearchPredicate
        {
            get
            {
                return entity =>
                    base.SearchPredicate(entity)
                    && this.Match(this.UserName,entity.UserName,this.FirstNameCompare)
                    && this.Match(this.FirstName, entity.FirstName, this.FirstNameCompare)
                    && this.Match(this.LastName,entity.LastName,this.LastNameCompare)
                    && this.Match(this.Email,entity.Email,this.EmailCompare)
                    && this.Match(this.UserID,entity.UserId,this.UserIDCompare);
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
            where TCopy : UserSearchDetail, new()
        {
            TCopy copy = base.CopyAs<TCopy>();
            copy.UserID = this.UserID;
            copy.UserIDCompare = this.UserIDCompare;
            copy.FirstName = this.FirstName;
            copy.FirstNameCompare = this.FirstNameCompare;
            copy.LastName = this.LastName;
            copy.LastNameCompare = this.LastNameCompare;
            copy.Email = this.Email;
            copy.EmailCompare = this.EmailCompare;
            return copy;
        }

        #endregion
    }
}
