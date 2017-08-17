// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.FSG.RP.Model.Entities.System;
using System.Collections.Generic;

/// <summary>
/// The System namespace.
/// </summary>
namespace RRD.FSG.RP.Model.SortDetail.System
{
    /// <summary>
    /// Defines sort properties, a sort method, and a comparer class for a specific entity type.
    /// </summary>
    public class UserSortDetail
        : AuditedSortDetail<UserObjectModel>
    {
        /// <summary>
        /// Column to be sorted.
        /// </summary>
        /// <value>The column.</value>
        public virtual new UserSortColumn Column
        {
            get { return (UserSortColumn)base.Column; }
            set { base.Column = (AuditedSortColumn)value; }
        }

        /// <summary>
        /// Sorts a collection of entities based on the sort criteria set.
        /// </summary>
        /// <param name="source">Collection of entities to sort.</param>
        /// <returns>A sorted collection of entities.</returns>
        public override IEnumerable<UserObjectModel> Sort(IEnumerable<UserObjectModel> source)
        {
            switch (this.Column)
            {
                case UserSortColumn.Key:
                    return this.Sort(source, entity => entity.Key);
                case UserSortColumn.UserName:
                    return this.Sort(source, entity => entity.UserName);
                case UserSortColumn.Email:
                    return this.Sort(source, entity => entity.Email);
                case UserSortColumn.FirstName:
                    return this.Sort(source, entity => entity.FirstName);
                case UserSortColumn.LastName:
                    return this.Sort(source, entity => entity.LastName);
                default:
                    return base.Sort(source);
            }
        }

        /// <summary>
        /// Compares two entities using teh sort properties of this instance.
        /// </summary>
        /// <param name="x">First entity to compare.</param>
        /// <param name="y">Second entity to compare.</param>
        /// <returns>A negative number if x is less than y, a positive number if x is greater than y, and 0 if they are the same.</returns>
        public override int Compare(UserObjectModel x, UserObjectModel y)
        {
            switch (this.Column)
            {
                case UserSortColumn.Key:
                    return this.Compare(x.Key, y.Key);
                case UserSortColumn.UserName:
                    return this.Compare(x.UserName, y.UserName);
                case UserSortColumn.Email:
                    return this.Compare(x.Email, y.Email);
                case UserSortColumn.FirstName:
                    return this.Compare(x.FirstName, y.FirstName);
                case UserSortColumn.LastName:
                    return this.Compare(x.LastName, y.LastName);
                default:
                    return base.Compare(x, y);
            }
        }
    }
}
